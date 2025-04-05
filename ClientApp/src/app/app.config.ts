// src/app/app.config.ts
import { ApplicationConfig } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './core/interceptors/auth.interceptor'; // Adjust path
import { LoginComponent } from './features/login/login.component';
import { authGuard } from './core/guards/auth.guard';




const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: '',
    canActivate: [authGuard], // Protect all child routes
    children: [
      { path: 'pocetna', loadComponent: () => import('./features/pocetna/pocetna.component').then(m => {return m.PocetnaComponent}) },
      { path: 'prijava-ispita', loadComponent: () => import('./features/prijava-ispita/prijava-ispita.component').then(m => m.PrijavaIspitaComponent) },
      { path: 'moji-predmeti', loadComponent: () => import('./features/moji-predmeti/moji-predmeti.component').then(m => m.MojiPredmetiComponent) },
      { path: 'ispitni-rokovi', loadComponent: () => import('./features/ispitni-rokovi/ispitni-rokovi.component').then(m => m.IspitniRokoviComponent) }
    ]
  }
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([authInterceptor])),
    provideRouter(routes)
  ]
};