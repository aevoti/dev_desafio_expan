import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product/product-list.component';
import { OrderListComponent } from './components/order/order-list.component';
import { HomeComponent } from './components/home/home.component';
export let routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },

    { path: 'products', component: ProductListComponent },
    { path: 'orders', component: OrderListComponent },
];
