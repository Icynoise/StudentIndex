import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { StudentService } from '../../../core/services/student.service';
import { Student } from '../../../core/models/student.model';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule, RouterModule,RouterLink,RouterLinkActive],
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  constructor(public authService: AuthService, public router: Router, public studentService: StudentService) {}

  ime: string = '';
  prezime: string = '';
  nazivStudijskiProgram: string = '';
  student: any;

  ngOnInit(){
    this.loadStudentInfo();
  }

  loadStudentInfo() {
    this.studentService.getStudentInfo().subscribe({
      next: (data: Student) => {
        this.ime = data.ime;
        this.prezime = data.prezime;
        this.nazivStudijskiProgram = data.nazivStudijskiProgram;
      },
      error: (err) => {
        console.error('Error fetching student subjects:', err);
      }
    });
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}