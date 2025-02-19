import { ApplicationConfig } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'; 

export const routes: Routes = [
  { path: '', redirectTo: 'pocetna', pathMatch: 'full' }, 
  { path: 'pocetna', loadComponent: () => import('./features/pocetna/pocetna.component').then(m => m.PocetnaComponent) },
  { path: 'prijava-ispita', loadComponent: () => import('./features/prijava-ispita/prijava-ispita.component').then(m => m.PrijavaIspitaComponent) },
  { path: 'moji-predmeti', loadComponent: () => import('./features/moji-predmeti/moji-predmeti.component').then(m => m.MojiPredmetiComponent) },
  { path: 'ispitni-rokovi', loadComponent: () => import('./features/ispitni-rokovi/ispitni-rokovi.component').then(m => m.IspitniRokoviComponent) },
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), 
    provideHttpClient() 
  ]
};
