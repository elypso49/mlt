import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageSynologyComponent } from './page-synology/page-synology.component';
import { HttpClientModule } from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Import FormsModule and ReactiveFormsModule
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon'; // For chip icons
import { MatButtonModule } from '@angular/material/button'; // For buttons
import { FolderSelectorComponent } from '../folder-selector/folder-selector.component';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {BrowserModule} from "@angular/platform-browser";
import {MatLegacyChipsModule} from "@angular/material/legacy-chips"; // Your folder selector component

@NgModule({
  declarations: [
    PageSynologyComponent,
    FolderSelectorComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule, // Needed for Angular Material animations
    FormsModule,
    ReactiveFormsModule,
    MatChipsModule,
    MatIconModule,
    MatButtonModule,
    MatLegacyChipsModule

  ]
})
export class SynologyModule { }
