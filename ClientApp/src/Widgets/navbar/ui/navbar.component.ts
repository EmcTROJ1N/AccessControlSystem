import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf
  ],
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  Items: any[] = [
    { Link: '/cars', Name: 'Cars' },
    { Link: '/employees', Name: 'Employees' },
    { Link: '/access', Name: 'Access Check' },
  ]
}
