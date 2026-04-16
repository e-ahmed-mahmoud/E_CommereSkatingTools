import { AfterViewInit, Component, inject, OnInit, signal } from '@angular/core';
import { HttpShopService } from '../../../core/services/http-shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinner } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-filter-dialog',
  imports: [MatDivider, MatSelectionList, MatListOption, MatButton, FormsModule],
  templateUrl: './filter-dialog.html',
  styleUrl: './filter-dialog.css',
})
export class FilterDialog implements OnInit {
  protected httpShopService = inject(HttpShopService);
  private refDialog = inject(MatDialogRef<FilterDialog>);
  private data = inject(MAT_DIALOG_DATA);

  selectedBrands = signal<string[]>([]);
  selectedTypes = signal<string[]>([]);

  ngOnInit(): void {
    this.selectedBrands.set(this.data.brands || []);
    this.selectedTypes.set(this.data.types || []);
  }

  onApplyCliked() {
    this.refDialog.close({
      brands: this.selectedBrands(),
      types: this.selectedTypes()
    })
  }
}
