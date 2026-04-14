import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from "./layout/header/header";
import { HttpClient } from '@angular/common/http';

const baseURL = 'https://localhost:5001/api';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header],
  providers: [HttpClient],
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {

  products = signal([]);

  httpService = inject(HttpClient);

  ngOnInit(): void {

    this.httpService.get<any>(`${baseURL}/products/Get`).subscribe({
      next: response => {
        this.products.set(response.items)
      },
      error: err => console.log(err),
      complete: () => console.log("completed")
    });

  }


}


