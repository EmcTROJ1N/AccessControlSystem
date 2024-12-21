import {provideRouter, Routes} from '@angular/router';
import {CarsComponent} from "Pages/cars";
import {EmployeesComponent} from "Pages/employees";
import {AccessCheckComponent} from "Pages/access-check";
import {ApplicationConfig} from "@angular/core";
import {CarAddComponent} from "Pages/car-add";
import {CarDetailsComponent} from "Widgets/car-details";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {baseUrlInterceptor} from "Shared/Interceptors/base-url.interceptor";
import {EmployeeAddComponent} from "Pages/employee-add";
import {EmployeeDetailsComponent} from "Widgets/employee-details";

const routes: Routes = [
  { path: '', redirectTo: 'cars', pathMatch: 'full' },
  { path: 'cars', component: CarsComponent },
  { path: 'car/add', component: CarAddComponent },
  { path: 'car/:id', component: CarDetailsComponent },

  { path: 'employees', component: EmployeesComponent },
  { path: 'employee/add', component: EmployeeAddComponent },
  { path: 'employee/:id', component: EmployeeDetailsComponent },

  { path: 'access', component: AccessCheckComponent },
];


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([baseUrlInterceptor])
    ),
  ]
};
