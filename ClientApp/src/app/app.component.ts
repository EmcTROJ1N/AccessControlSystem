import { Component } from '@angular/core';
import {RouterOutlet} from "@angular/router";
import {NavbarComponent} from "../Widgets/navbar";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [
    RouterOutlet,
    NavbarComponent,
    HttpClientModule
  ],

  providers: [
    HttpClientModule,
  ],
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Access System';
}
