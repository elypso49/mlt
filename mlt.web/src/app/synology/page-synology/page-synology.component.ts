import {Component, OnInit} from '@angular/core';
import {SynoTask} from "../../core/models/SynoTask";
import {SynologyService} from "../../services/synology.service";
import {DownloadStatus} from "../../core/enums/DownloadStatus";
import {faker} from '@faker-js/faker';

@Component({
  selector: 'app-page-synology',
  templateUrl: './page-synology.component.html',
  styleUrls: ['./page-synology.component.scss']
})
export class PageSynologyComponent implements OnInit {
  synoTasks: SynoTask[] = [];

  constructor(private synologyService: SynologyService) {}

  ngOnInit(): void {
    this.synologyService.getSynoTasks().subscribe({
      next: (tasks: SynoTask[]) => {
        this.synoTasks = tasks.sort((a, b) => {
          return new Date(b.createdDateTime!).getTime() - new Date(a.createdDateTime!).getTime();
        });
      },
      error: (err) => {
        console.error('Failed to fetch tasks', err);
      }
    });


    // this.synoTasks.push(
    //   {
    //     title: 'Waiting',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Waiting,
    //     id: 'test-waiting',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 0
    //   },
    //   {
    //     title: 'Downloading',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Downloading,
    //     id: 'test-downloading',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 500000
    //   },
    //   {
    //     title: 'Paused',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Paused,
    //     id: 'test-paused',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 500000
    //   },
    //   {
    //     title: 'Finishing',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Finishing,
    //     id: 'test-finishing',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 1000000
    //   },
    //   {
    //     title: 'Finished',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Finished,
    //     id: 'test-finished',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 1000000
    //   },
    //   {
    //     title: 'Hash Checking',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.HashChecking,
    //     id: 'test-hash-checking',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 1000000
    //   },
    //   {
    //     title: 'Seeding',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Seeding,
    //     id: 'test-seeding',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 1000000
    //   },
    //   {
    //     title: 'Filehosting Waiting',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.FilehostingWaiting,
    //     id: 'test-filehosting-waiting',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 0
    //   },
    //   {
    //     title: 'Extracting',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Extracting,
    //     id: 'test-extracting',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 0
    //   },
    //   {
    //     title: 'Error',
    //     destination: faker.address.streetAddress(),
    //     status: DownloadStatus.Error,
    //     id: 'test-error',
    //     uri: 'https://example.com',
    //     size: 1000000,
    //     sizeDownloaded: 0
    //   }
    // );
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

  getStatusColor(status: string): string {
    switch (status) {
      case 'waiting':
        return '#ffcc00'; // Yellow
      case 'downloading':
        return '#007bff'; // Blue
      case 'paused':
        return '#ffc107'; // Orange
      case 'finishing':
        return '#28a745'; // Green
      case 'finished':
        return '#28a745'; // Green
      case 'hash_checking':
        return '#17a2b8'; // Teal
      case 'seeding':
        return '#6f42c1'; // Purple
      case 'filehosting_waiting':
        return '#ffcc00'; // Yellow
      case 'extracting':
        return '#f0ad4e'; // Light Orange
      case 'error':
        return '#dc3545'; // Red
      default:
        return '#6c757d'; // Gray for unknown status
    }
  }

}
