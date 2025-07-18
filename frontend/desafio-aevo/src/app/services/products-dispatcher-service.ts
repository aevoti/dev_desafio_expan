import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IProduct } from "../interfaces/i.product";

@Injectable({
  providedIn: 'root'
})

export class ProductDispatcherService {
  private readonly apiUrl = 'http://localhost:5284/api/products';

  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  create(product: IProduct): Observable<any> {
    return this.http.post(this.apiUrl, product);
  }
}