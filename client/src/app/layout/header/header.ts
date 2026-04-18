import { Component, inject, signal } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon'
import { RouterLink, RouterLinkActive } from "@angular/router";
import { LoadingService } from '../../core/services/LoadingService';
import { MatProgressBar } from '@angular/material/progress-bar';

@Component({
  selector: 'app-header',
  imports: [MatIcon, MatButton, RouterLink, RouterLinkActive, MatProgressBar],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {

  loadingService = inject(LoadingService);
}
