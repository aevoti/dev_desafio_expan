import { Component, OnInit } from '@angular/core';
import { Product, ProductServiceMock } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { OrderServiceMock } from '../../services/order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  itemCount: number = 0;

  constructor(private productService: ProductServiceMock, private orderService: OrderServiceMock, private router: Router) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
      console.log(data);
    });

    this.updateItemCount();
  }

  addToOrder(product: Product) {
    this.orderService.addProduct(product);
    this.updateItemCount();
  }
  
  updateItemCount() {
    this.itemCount = this.orderService.getOrderItems().length;
  }
  
  goToOrderForm() {
    this.router.navigate(['/order']);
  }
}
