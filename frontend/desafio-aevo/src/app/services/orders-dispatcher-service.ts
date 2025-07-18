import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IOrder } from "../interfaces/i.order";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class OrderDispatcherService {
  private readonly apiUrl = 'http://localhost:5284/api/orders';

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  create(orderItems: { productId: string; quantity: number }[]): Observable<any> {
    let body = {
      items: orderItems.map(item => ({
        productID: item.productId,
        quantity: item.quantity
      }))
    };

    return this.http.post(this.apiUrl, body);
  }
}