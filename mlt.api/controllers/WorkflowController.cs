using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.common.dtos.rss.enums;
using mlt.realdebrid.services;
using mlt.synology.services;
using mlt.workflow.services;

namespace mlt.api.controllers;

[Route("api/[controller]"), ApiController]
public class WorkflowController(IWorkflowService service, IRealDebridService realDebridService, IDownloadStationService downloadStationService) : BaseController
{
    [HttpPost]
    public Task<IActionResult> DownloadAll()
        => HandleRequest(async () => Ok(await service.DownloadAll()));

    [HttpPost("upload")]
    public async Task<IActionResult> UploadTorrentFile(IFormFile file, string? destination = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Sanitize the file name
            var fileName = $"importFile_{Guid.NewGuid()}"; 

            // Define the upload directory
            var uploadDirectory = "/app/Uploads";
            if (!Directory.Exists(uploadDirectory))
                Directory.CreateDirectory(uploadDirectory);

            // Save the file with the sanitized name
            var filePath = Path.Combine(uploadDirectory, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            // Generate a URL for the uploaded file using the sanitized file name
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

            // Call your service to handle this file URL
            var result = await realDebridService.AddTorrentsInBatchesWithRetry(new List<string> { fileUrl });
        
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            var torrentInfo = await realDebridService.GetTorrentInfo(result.Data!.First().TorrentId) is { IsSuccess: true } torrentInfoResult ? torrentInfoResult.Data : null;
        
            // if (torrentInfo is null)
            //     return;

            var debridedLinks = await realDebridService.UnrestrictLinks(torrentInfo.Links) is { IsSuccess: true } unrestrictedLinksResult
                ? unrestrictedLinksResult.Data?.ToList()
                : null;
        
            // if (debridedLinks is null || !debridedLinks.Any())
            //     return;

            await downloadStationService.CreateTask(debridedLinks!.Select(x=> x.Download), destination ?? "Movies");

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }
    
    [HttpPost("uploadBinary")]
    public async Task<IActionResult> UploadBinaryFile(string? destination = null)
    {
        try
        {
            // Read binary data from the request body
            using (var memoryStream = new MemoryStream())
            {
                await Request.Body.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                // Define the upload directory
                var uploadDirectory = "/app/Uploads";
                if (!Directory.Exists(uploadDirectory))
                    Directory.CreateDirectory(uploadDirectory);

                // Save the file back to disk
                var fileName = $"uploadedFile_{Guid.NewGuid()}.torrent"; // or use a different extension if necessary
                var filePath = Path.Combine(uploadDirectory, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

                // Optionally return the file URL
                var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                
                // Call your service to handle this file URL
                var result = await realDebridService.AddTorrentsInBatchesWithRetry(new List<string> { fileUrl });
        
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                var torrentInfo = await realDebridService.GetTorrentInfo(result.Data!.First().TorrentId) is { IsSuccess: true } torrentInfoResult ? torrentInfoResult.Data : null;
        
                // if (torrentInfo is null)
                //     return;

                var debridedLinks = await realDebridService.UnrestrictLinks(torrentInfo.Links) is { IsSuccess: true } unrestrictedLinksResult
                    ? unrestrictedLinksResult.Data?.ToList()
                    : null;
        
                // if (debridedLinks is null || !debridedLinks.Any())
                //     return;

                await downloadStationService.CreateTask(debridedLinks!.Select(x=> x.Download), destination ?? "Movies");

                return Ok(result);
            }
        }
        catch (Exception e)
        {
            // Log the error and return a 500 Internal Server Error response
            Console.WriteLine($"Error occurred: {e.Message}");
            return StatusCode(500, "Internal Server Error during file upload.");
        }
    }
    
    public class FileUploadDto
    {
        public string FileName { get; set; }
        public string FileData { get; set; }          // Base64 encoded string
        public string DestinationFolder { get; set; } // Base64 encoded string
    }

    [HttpPost("uploadBase64")]
    public async Task<IActionResult> UploadBase64File([FromBody] FileUploadDto fileUpload)
    {
        try
        {
            // Log incoming data for verification
            Console.WriteLine($"Received fileName: {fileUpload.FileName}");
            Console.WriteLine($"Received fileData (first 100 chars): {fileUpload.FileData?.Substring(0, 100)}");

            // Validate if the Base64 string is valid
            byte[] fileBytes;
            try
            {
                Console.WriteLine($"Try to convert from base 64");
                fileBytes = Convert.FromBase64String(fileUpload.FileData);
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Error decoding Base64: {fe.Message}");
                return BadRequest("Invalid Base64 string.");
            }

            Console.WriteLine("Define the upload directory");
            // Define the upload directory
            var uploadDirectory = "/app/Uploads";
            if (!Directory.Exists(uploadDirectory))
                Directory.CreateDirectory(uploadDirectory);
            
            var fileName = $"uploadedFile_{Guid.NewGuid()}.torrent"; // or use a different extension if necessary

            Console.WriteLine("Save the file back to disk");
            // Save the file back to disk
            var filePath = Path.Combine(uploadDirectory, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);
            
            // Optionally return the file URL
            //Temporary solution until i found a way to retrieve the api host
            var forwardedHost = (Request.Headers["X-Forwarded-Host"].FirstOrDefault() ?? Request.Host.Value).Replace("mlt", "mlt-api");
            var fileUrl = $"{Request.Scheme}://{forwardedHost}/uploads/{fileName}";
            Console.WriteLine($"Optionally return the file URL : {fileUrl}");

            Console.WriteLine("Call the external service");
            // Call the external service
            var result = await realDebridService.AddTorrentsInBatchesWithRetry(new List<string> { fileUrl });
            if (!result.IsSuccess)
            {
                Console.WriteLine($"External service error: {string.Join(", ", result.Errors)}");
                return BadRequest("Failed to upload the file to the external service.");
            }

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            var torrentInfo = await realDebridService.GetTorrentInfo(result.Data!.First().TorrentId) is { IsSuccess: true } torrentInfoResult ? torrentInfoResult.Data : null;

            var debridedLinks = await realDebridService.UnrestrictLinks(torrentInfo.Links) is { IsSuccess: true } unrestrictedLinksResult
                ? unrestrictedLinksResult.Data?.ToList()
                : null;

            await downloadStationService.CreateTask(debridedLinks!.Select(x=> x.Download), string.IsNullOrWhiteSpace(fileUpload.DestinationFolder) ? "Movies" : fileUpload.DestinationFolder.Substring(1) );

            return Ok(result);
        }
        catch (Exception e)
        {
            // Log the error and return a 500 Internal Server Error response
            Console.WriteLine($"Error occurred: {e.Message}");
            return StatusCode(500, "Internal Server Error during file upload.");
        }
    }

}