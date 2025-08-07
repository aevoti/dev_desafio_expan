
import { Component } from '@angular/core';
import { OrderServiceMock } from '../../services/order.service';
import { ProductServiceMock } from '../../services/product.service';
import { Product } from '../../models/product';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface OrderItem {
  product: Product;
  quantity: number;
}

@Component({
  selector: 'app-order-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']  
})
export class OrderFormComponent {
  products: Product[] = [];
  selectedProducts: Product[] = [];
  quantities: number[] = [];
  orderItems: OrderItem[] = [];
  alertMessage: string | null = null;

  constructor(private orderService: OrderServiceMock, private productService: ProductServiceMock) {
    this.productService.getProducts().subscribe((p) => {
      this.products = p;
      return 
    });
    this.selectedProducts = this.orderService.getSelectedProducts();
    this.orderItems = this.orderService.getOrderItems();
  }

  showAlert(message: string) {
    this.alertMessage = message;
    setTimeout(() => {
      this.alertMessage = null;
    }, 3000);
  }

  submitOrder() {
    const orderItems = this.selectedProducts.map(prod => ({
      id: prod.id,
      quantity: 1
    }));
  
    this.orderService.createOrder(orderItems);
    this.selectedProducts = [];
  }

  confirmOrder() {
    this.showAlert('Pedido confirmado com sucesso!');
    console.log('Pedido confirmado:', this.selectedProducts);
    this.orderService.clearOrder();
    this.selectedProducts = [];
  }
  
  getTotal(): number {
    return this.orderItems.reduce((sum, item) => sum + item.product.price * item.quantity, 0);
  }

  removeItem(index: number) {
    this.orderService.removeItem(index);
    this.orderItems = this.orderService.getOrderItems();
    this.showAlert('Item removido do pedido.');
  }   
  
}

