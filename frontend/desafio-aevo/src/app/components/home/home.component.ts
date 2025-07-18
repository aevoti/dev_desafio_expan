import { CommonModule } from "@angular/common";
import { Component, ViewEncapsulation } from "@angular/core";
import { RouterModule } from "@angular/router";
import { MaterialModule } from "../../material/material.module";

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [CommonModule, RouterModule, MaterialModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent { }