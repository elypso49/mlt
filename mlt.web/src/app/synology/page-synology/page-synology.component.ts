import {Component, OnInit} from '@angular/core';
import {SynoTask} from "../../core/models/SynoTask";
import {SynologyService} from "../../services/synology.service";
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {environment} from "../../../environments/environment";
import {ToastrService} from "ngx-toastr";
import {SynoFolder} from "../../core/models/SynoFolder";
import {fa} from "@faker-js/faker";

@Component({
  selector: 'app-page-synology',
  templateUrl: './page-synology.component.html',
  styleUrls: ['./page-synology.component.scss']
})
export class PageSynologyComponent implements OnInit {
  synoTasks: SynoTask[] = [];
  synoFolders: SynoFolder[] = [];
  isModalOpen = false;
  isCleaning = false;
  destinationFolder: string = '';

  selectedFiles: File[] = [];
  uploading: boolean = false;
  uploadStatus: string = '';

  constructor(private synologyService: SynologyService,
              private httpClient: HttpClient,
              private toastr: ToastrService) {
  }

  openModal() {
    this.isModalOpen = true;
  }

  // Close the modal
  closeModal() {
    this.isModalOpen = false;
  }

  // Handle file selected
  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const selectedFilesFromInput = Array.from(input.files);

      // Append selected files while filtering out duplicates
      this.selectedFiles = [...this.selectedFiles, ...selectedFilesFromInput.filter(file => !this.selectedFiles.some(f => f.name === file.name))];
    }
  }

  handlePathSelected(selectedPath: string) {
    console.log(selectedPath);
    this.destinationFolder = selectedPath;
  }


  // Handle drag over event
  onDragOver(event: DragEvent) {
    event.preventDefault();
    const dropArea = document.querySelector('.upload-wrapper');
    dropArea?.classList.add('drag-over');
  }

  // Handle drag leave event
  onDragLeave(event: DragEvent) {
    const dropArea = document.querySelector('.upload-wrapper');
    dropArea?.classList.remove('drag-over');
  }

  // Handle drop event
  onDrop(event: DragEvent) {
    event.preventDefault();
    const dropArea = document.querySelector('.upload-wrapper');
    dropArea?.classList.remove('drag-over');

    if (event.dataTransfer && event.dataTransfer.files.length > 0) {
      const droppedFiles = Array.from(event.dataTransfer.files);

      // Append dropped files while filtering out duplicates
      this.selectedFiles = [...this.selectedFiles, ...droppedFiles.filter(file => !this.selectedFiles.some(f => f.name === file.name))];
    }
  }

  uploadFileAsBase64() {
    if (this.selectedFiles.length === 0) {
      this.uploadStatus = 'No files selected!';
      return;
    }

    this.uploading = true;

    // Iterate over all selected files and upload them
    this.selectedFiles.forEach(file => {
      const reader = new FileReader();

      reader.onload = () => {
        const fileBase64 = reader.result as string;
        const base64Data = fileBase64.split(',')[1]; // Strip the Base64 prefix

        const requestPayload = {
          fileName: file.name,  // Use the actual file name
          fileData: base64Data,
          destinationFolder: this.destinationFolder
        };

        console.log(requestPayload);

        // Send the Base64 data as JSON
        this.httpClient.post(`${environment.services.MltApiEndpoint}/api/Workflow/uploadBase64`, requestPayload, {
          headers: {'Content-Type': 'application/json'}
        }).subscribe({
          next: (response) => {
            this.uploadStatus = `File ${file.name} uploaded successfully!`;
            this.toastr.info('Success', `File ${file.name} uploaded successfully`);
            console.log('Response:', response);

            // Close modal after uploading all files
            if (file === this.selectedFiles[this.selectedFiles.length - 1]) {
              this.uploading = false;
              this.closeModal();
              setTimeout(() => {
                window.location.reload();
              }, 1000);  // Add a slight delay before reload
            }
          },
          error: (error: HttpErrorResponse) => {
            this.toastr.error('Error', `An error occurred ${error}`);
            console.error('Error:', error);

            this.uploading = false;

            // Close modal if an error occurs
            if (file === this.selectedFiles[this.selectedFiles.length - 1]) {
              this.closeModal();
            }
          }
        });
      };

      reader.readAsDataURL(file);  // Read file as Base64
    });
  }

  // Fetch tasks
  fetchTasks() {
    this.synologyService.getSynoTasks().subscribe({
      next: (tasks: SynoTask[]) => {
        this.synoTasks = tasks.sort((a, b) => {
          return new Date(b.createdDateTime!).getTime() - new Date(a.createdDateTime!).getTime();
        });

        console.log(this.synoTasks)
      },
      error: (err) => {
        console.error('Failed to fetch tasks', err);
      }
    });
  }

  ngOnInit(): void {
    // Initial fetch
    this.fetchTasks();
    this.fetchFolders();

    // Set interval to fetch tasks every 20 seconds
    setInterval(() => {
      if (!this.isModalOpen) {  // Only fetch if the modal is not open
        this.fetchTasks();
      }
    }, 20000); // 20 seconds interval
  }

  getStatusIconClass(status: string): string {
    switch (status) {
      case 'waiting':
        return 'fa-solid fa-hourglass-half';
      case 'downloading':
        return 'fa-solid fa-download';
      case 'paused':
        return 'fa-solid fa-pause';
      case 'finishing':
        return 'fa-solid fa-check';
      case 'finished':
        return 'fa-solid fa-check';
      case 'hash_checking':
        return 'fa-solid fa-hashtag';
      case 'seeding':
        return 'fa-solid fa-seedling';
      case 'filehosting_waiting':
        return 'fa-solid fa-clock';
      case 'extracting':
        return 'fa-solid fa-box-open';
      case 'error':
        return 'fa-solid fa-exclamation';
      default:
        return 'fa-solid fa-question';
    }
  }

  private fetchFolders() {
    this.synologyService.getSynoFolders().subscribe({
      next: (folders: SynoFolder[]) => {
        console.log(folders)
        this.synoFolders = folders;
      },
      error: (err) => {
        console.error('Failed to fetch tasks', err);
      }
    });
  }

  protected readonly SynoFolder = SynoFolder;

  cleanTasks() {
    this.isCleaning = true;

    this.synologyService.cleanSynoTasks().subscribe(
      (isSuccess) => {
        if (isSuccess) {
          this.toastr.info('Success', 'The tasks have been cleaned');

          // Optionally reload after a slight delay if needed
          setTimeout(() => {
            window.location.reload();
          }, 1000);
        } else {
          this.toastr.error('Failed', 'The tasks could not be cleaned');
        }

        this.isCleaning = false;
      },
      (error) => {
        // Handle any errors that occur during the API call
        console.error('Error cleaning Synology tasks:', error);
        this.toastr.error('Failed', 'An error occurred while cleaning the tasks');
        this.isCleaning = false;
      }
    );
  }
}
