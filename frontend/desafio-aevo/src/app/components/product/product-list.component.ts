import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDispatcherService } from '../../services/products-dispatcher-service';
import { MaterialModule } from '../../material/material.module';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { IProduct } from '../../interfaces/i.product';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RouterModule } from '@angular/router';
import { NewProductDialog } from './new-product-dialog/new-product-dialog.component';
import { OrderDispatcherService } from '../../services/orders-dispatcher-service';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, MaterialModule, RouterModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ProductListComponent implements OnInit {
  public displayedColumns: string[] = ['name', 'price', 'quantity'];
  public quantities: { [productId: string]: number } = {};
  public selection = new Set<string>();
  public dataSource = new MatTableDataSource<IProduct>();
  public isLoading = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private productDispatcherService: ProductDispatcherService,
    private orderDispatcherService: OrderDispatcherService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  private loadProducts() {
    this.productDispatcherService.getAll().subscribe({
      next: products => {
        this.dataSource = new MatTableDataSource(products);
        this.isLoading = false;

        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: err => {
        let message = this.getErrorMessage(err);
        this.snackBar.open(message, 'Fechar', {
          duration: 5000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
          panelClass: ['error-snackbar']
        });
      }
    });
  }

  public openCreateProductDialog(): void {
    let dialogRef = this.dialog.open(NewProductDialog, {
      width: '500px',
      disableClose: true,
      autoFocus: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'created') {
        this.loadProducts();
      }
    });
  }

  public applyFilter(event: Event) {
    let filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filter = filterValue;
  }

  public buySelected() {
    let orderItems = Object.entries(this.quantities)
      .filter(([_, qty]) => qty > 0)
      .map(([productId, qty]) => ({
        productId,
        quantity: qty
      }));

    if (orderItems.length === 0) return;

    this.isLoading = true;

    this.orderDispatcherService.create(orderItems).subscribe({
      next: () => {
        this.isLoading = false;
        this.snackBar.open('Pedido realizado com sucesso!', 'Fechar', { duration: 3000 });
        this.quantities = {};
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
      }
    });
  }

  get totalQuantity(): number {
    return Object.values(this.quantities).reduce((sum, qty) => sum + qty, 0);
  }

  get hasAnyQuantity(): boolean {
    return Object.values(this.quantities).some(qty => qty > 0);
  }

  public increaseQuantity(id: string): void {
    this.setQuantity(id, (this.quantities[id] || 0) + 1);
  }

  public decreaseQuantity(id: string): void {
    let current = this.quantities[id] || 0;
    this.setQuantity(id, current > 0 ? current - 1 : 0);
  }

  private setQuantity(id: string, value: number): void {
    let v = Number(value);
    this.quantities[id] = v >= 0 ? v : 0;
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    if (error.status === 0) return 'Falha de conexão com o servidor';
    if (error.error?.errors && Array.isArray(error.error.errors)) {
      return error.error.errors.join(' ');
    }
    return error.error || error.statusText || 'Erro desconhecido';
  }
}

@Component({
  standalone: true,
  imports: [MaterialModule],
  selector: 'dialog-confirm-product-remove-dialog',
  templateUrl: './dialog-confirm-product-remove-dialog.htm',
})
export class DialogConfirmProductRemoveDialog { }
