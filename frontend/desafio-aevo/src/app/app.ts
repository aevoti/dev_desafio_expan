import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MaterialModule } from './material/material.module';
import { SidenavListComponent } from './includes/sidenav/sidenav-list.component';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MaterialModule, SidenavListComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  public title = 'AEVO - Produtos eletronicos';

  public constructor(private titleService: Title) {
    this.setTitle(this.title);
  }

  public setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }
}
