import { Routes } from '@angular/router';
import { LoginComponent } from './features/login/login.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: '',
    canActivate: [authGuard],
    children: [
      {
        path: 'pocetna',
        loadComponent: () => import('./features/pocetna/pocetna.component').then(m => m.PocetnaComponent)
      },
      {
        path: 'prijava-ispita',
        loadComponent: () => import('./features/prijava-ispita/prijava-ispita.component').then(m => m.PrijavaIspitaComponent)
      },
      {
        path: 'moji-predmeti',
        loadComponent: () => import('./features/moji-predmeti/moji-predmeti.component').then(m => m.MojiPredmetiComponent)
      },
      {
        path: 'ispitni-rokovi',
        loadComponent: () => import('./features/ispitni-rokovi/ispitni-rokovi.component').then(m => m.IspitniRokoviComponent)
      }
    ]
  }
];
