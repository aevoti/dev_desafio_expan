import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class ProductServiceMock {
  private products: Product[] = [
    { id: 1, name: 'Notebook Gamer', price: 4500 },
    { id: 2, name: 'Mouse RGB', price: 250 },
    { id: 3, name: 'Teclado Mecânico', price: 600 }
  ];

  getProducts(): Observable<Product[]> {
    return of(this.products);
  }
}

