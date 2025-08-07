import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Product } from '../models/product';

export type OrderStatus = 'Recebido' | 'Em Processamento' | 'Concluído' | 'Falhou';

export interface Order {
  id: number;
  products: { id: number; quantity: number }[];
  status: OrderStatus;
}

interface OrderItem {
  product: Product;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class OrderServiceMock {
  private orders: Order[] = [];
  private orders$ = new BehaviorSubject<Order[]>(this.orders);
  private orderId = 1;
  private selectedProducts: Product[] = [];
  private items: OrderItem[] = [];

  addProduct(product: Product) {
    const existingItem = this.items.find(item => item.product.id === product.id);
    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      this.items.push({ product, quantity: 1 });
    }
  }

  getOrderItems(): OrderItem[] {
    return this.items;
  }

  getSelectedProducts(): Product[] {
    return this.selectedProducts;
  }

  clearOrder() {
    this.selectedProducts = [];
  }

  createOrder(products: { id: number; quantity: number }[]) {
    const newOrder: Order = {
      id: this.orderId++,
      products,
      status: 'Recebido'
    };
    this.orders.push(newOrder);
    this.orders$.next(this.orders);
    this.simulateProcessing(newOrder);
  }

  getOrders() {
    return this.orders$.asObservable();
  }

  private simulateProcessing(order: Order) {
    setTimeout(() => {
      order.status = 'Em Processamento';
      this.orders$.next(this.orders);
      setTimeout(() => {
        const isSuccess = Math.random() < 0.5;
        order.status = isSuccess ? 'Concluído' : 'Falhou';
        this.orders$.next(this.orders);
      }, 90000);
    }, 30000);
  }

  removeItem(index: number) {
    this.items.splice(index, 1);
  }
}

