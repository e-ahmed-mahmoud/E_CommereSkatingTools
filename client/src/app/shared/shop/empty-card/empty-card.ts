import { Component } from '@angular/core';
import { MatIcon } from "@angular/material/icon";
import { MatAnchor } from "@angular/material/button";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-empty-card',
  imports: [MatIcon, MatAnchor, RouterLink],
  templateUrl: './empty-card.html',
  styleUrl: './empty-card.css',
})
export class EmptyCard { }
