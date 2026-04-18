import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {

  isLoading = signal(false);

  busyRequestCount = 0;

  busy() {
    this.busyRequestCount++;
    this.isLoading.set(true);
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.isLoading.set(false);
      this.busyRequestCount = 0;
    }
  }

}
