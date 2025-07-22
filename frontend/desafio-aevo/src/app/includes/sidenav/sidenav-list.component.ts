import { Component, Input } from '@angular/core';
import { MaterialModule } from '../../material/material.module';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  standalone: true,
  selector: 'app-sidenav',
  imports: [MaterialModule, RouterModule, CommonModule],
  templateUrl: './sidenav-list.component.html',
  styleUrls: ['./sidenav-list.component.scss']
})
export class SidenavListComponent {
  @Input() sidenav!: MatSidenav;

  public routeLinks = [
    { link: "home", name: "Home" },
    { link: "products", name: "Produtos" },
    { link: "orders", name: "Meus pedidos" },
  ];

  constructor(private router: Router) { }

  public navigate(link: string): void {
    this.router.navigate([link]).then(() => {
      this.sidenav?.close();
    });
  }
}
