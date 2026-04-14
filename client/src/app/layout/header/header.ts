import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon'

@Component({
  selector: 'app-header',
  imports: [MatIcon, MatButton],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header { }
