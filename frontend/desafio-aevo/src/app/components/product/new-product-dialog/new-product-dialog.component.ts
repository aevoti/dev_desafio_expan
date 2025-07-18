import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../material/material.module';
import { ProductDispatcherService } from '../../../services/products-dispatcher-service';
import { provideNgxMask } from 'ngx-mask';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'dialog-new-product',
  templateUrl: './new-product-dialog.component.html',
  encapsulation: ViewEncapsulation.None,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule],
  providers: [provideNgxMask()],
})
export class NewProductDialog implements OnInit {
  public productForm!: FormGroup;
  public isLoading = false;

  constructor(
    private fb: FormBuilder,
    private productDispatcherService: ProductDispatcherService,
    public dialogRef: MatDialogRef<NewProductDialog>,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      Name: [''],
      Price: [null, [Validators.required, Validators.min(0)]],
    });
  }

  public createProduct(): void {
    if (this.productForm.invalid) return;

    this.isLoading = true;


    this.productDispatcherService.create(this.productForm.value).subscribe({
      next: () => {
        this.isLoading = false;
        this.dialogRef.close('created');
      },
      error: (err) => {
        this.isLoading = false;
        let message = this.getErrorMessage(err);
        this.snackBar.open(message, 'Fechar', {
          duration: 5000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          panelClass: ['error-snackbar']
        });
      },
    });
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    if (error.status === 0) return 'Falha de conexão com o servidor';
    if (error.error?.errors && Array.isArray(error.error.errors)) {
      return error.error.errors.join(' ');
    }
    return error.error || error.statusText || 'Erro desconhecido';
  }
}