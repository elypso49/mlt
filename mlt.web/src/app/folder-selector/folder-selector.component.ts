import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import {SynoFolder} from "../core/models/SynoFolder";

@Component({
  selector: 'app-folder-selector',
  templateUrl: './folder-selector.component.html',
  styleUrls: ['./folder-selector.component.scss']
})
export class FolderSelectorComponent implements OnInit {
  @Input() baseFolders: SynoFolder[] = [];  // Accept list of folders from parent
  @Output() pathSelected: EventEmitter<string> = new EventEmitter<string>();  // Emit selected path to parent

  currentFolderControl = new FormControl('');
  filteredFolders: SynoFolder[] = [];
  currentFolderOptions: SynoFolder[] = [];
  selectedFolders: SynoFolder[] = [];  // Stores the selected folder path
  finalInput: string = '';  // Stores the final input value if needed

  ngOnInit() {
    // Start with the base folder options
    this.currentFolderOptions = this.baseFolders;
    this.filteredFolders = this.baseFolders;
    this.selectDefaultFolder(); // Automatically select default folder (Movies)
  }

  // Automatically select 'Movies' as the default folder
  selectDefaultFolder() {
    const defaultFolder = this.baseFolders.find(folder => folder.name === 'Movies');
    if (defaultFolder) {
      this.onFolderSelected(defaultFolder);
    }
  }

  // Function to filter the folders based on the user's input
  filterFolders() {
    const filterValue = this.currentFolderControl.value?.toLowerCase();
    this.filteredFolders = this.currentFolderOptions.filter(folder => folder.name.toLowerCase().includes(filterValue!));
  }

  // When a folder is selected from the autocomplete
  onFolderSelected(folder: SynoFolder) {
    this.selectedFolders.push(folder);  // Add the selected folder to the list
    this.currentFolderOptions = folder.folders;  // Update to subfolders of selected folder
    this.currentFolderControl.setValue('');  // Reset the input for next folder
    this.filterFolders();  // Re-filter the new set of folders

    // If there are no subfolders, allow final input
    if (this.currentFolderOptions.length === 0) {
      this.finalInput = '';
    }

    // Emit the updated path whenever a folder is selected
    this.emitSelectedPath();
  }

  // Emit the selected path to the parent component
  emitSelectedPath() {
    const fullPath = this.selectedFolders[this.selectedFolders.length - 1]?.path || '';
    const fullFinalPath = this.finalInput ? `${fullPath}/${this.finalInput}` : fullPath;
    this.pathSelected.emit(fullFinalPath);  // Emit the full path to parent
  }

  // Remove the last selected folder and go back one step
  removeFolder(index: number) {
    this.selectedFolders.splice(index, 1);  // Remove the selected folder
    this.currentFolderOptions = index === 0 ? this.baseFolders : this.selectedFolders[index - 1].folders;
    this.filterFolders();  // Re-filter after removal

    // Emit the updated path after removal
    this.emitSelectedPath();
  }

  // Function to submit the final path
  submit() {
    const fullPath = this.selectedFolders[this.selectedFolders.length - 1]?.path || '';
    const fullFinalPath = this.finalInput ? `${fullPath}/${this.finalInput}` : fullPath;
    console.log('Selected Path:', fullFinalPath);
    this.pathSelected.emit(fullFinalPath);  // Emit the final path on submit
  }
}
