import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../services/student.service';

@Component({
  selector: 'app-moji-predmeti',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './moji-predmeti.component.html',
  styleUrls: ['./moji-predmeti.component.scss']
})


export class MojiPredmetiComponent implements OnInit {
  selectedSemester: string = "IV semestar"; // Default semester
  predmeti: any[] = [];
  
  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.loadStudentSubjects();
  }

  loadStudentSubjects(): void {
    this.studentService
      .getStudentSubjects(5, 1, 1)
      .subscribe({
        next: (data) => {
          this.predmeti = data; // Assign the fetched data to predmeti
        },
        error: (err) => {
          console.error('Error fetching student subjects:', err);
          // Optionally handle the error (e.g., show a message to the user)
        }
      });
  }

  // 🔹 Make sure this method is inside the class and public
  getStatusClass(status: string): string {
    return status === "Polozeno" ? "status-passed" : "status-failed";
  }
  

}
