import { ApplicationConfig } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';


const routes: Routes = [
  { path: '', redirectTo: 'pocetna', pathMatch: 'full' }, // Redirect to 'pocetna'
  { path: 'pocetna', loadComponent: () => import('./features/pocetna/pocetna.component').then(m => m.PocetnaComponent) },
  { path: 'prijava-ispita', loadComponent: () => import('./features/prijava-ispita/prijava-ispita.component').then(m => m.PrijavaIspitaComponent) },
];

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes)]
};
