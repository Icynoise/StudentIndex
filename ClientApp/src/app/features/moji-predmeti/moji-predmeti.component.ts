import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Predmet } from '../../core/models/predmet.model';
import { PredmetService } from '../../core/services/predmet.service';

@Component({
  selector: 'app-moji-predmeti',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './moji-predmeti.component.html',
  styleUrls: ['./moji-predmeti.component.scss']
})


export class MojiPredmetiComponent implements OnInit {

  predmeti: Predmet[] = [];
  selectedSemesterId: number = 1;
  semesters = [
    { id: 1, name: 'I semestar' },
    { id: 2, name: 'II semestar' },
    { id: 3, name: 'III semestar' },
    { id: 4, name: 'IV semestar' },
    { id: 5, name: 'V semestar' },
    { id: 6, name: 'VI semestar' },
    { id: 7, name: 'VII semestar' },
    { id: 8, name: 'VIII semestar' },
  ];
  loading: boolean = false; // Add loading state
  
  constructor(private predmetService: PredmetService) {}

  ngOnInit(): void {
    this.loadStudentSubjects();
  }

  loadStudentSubjects(): void {
    this.loading = true; // Set loading to true
    this.predmetService
      .getStudentSubjects(this.selectedSemesterId)
      .subscribe({
        next: (data) => {
          this.predmeti = data; // Assign the fetched data to predmeti
          this.loading = false; // Set loading to false when done
        },
        error: (err) => {
          console.error('Error fetching student subjects:', err);
          this.loading = false; // Set loading to false on error
        }
      });
  }

  onSemesterChange(): void {
    this.loadStudentSubjects(); // Reload subjects when semester changes
  }

  // 🔹 Make sure this method is inside the class and public
  getStatusClass(status: string): string {
    return status === "Polozeno" ? "status-passed" : "status-failed";
  }

}
