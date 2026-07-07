import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Predmet } from '../../core/models/predmet.model';
import { PredmetService } from '../../core/services/predmet.service';
import { QueryOptions } from '../../core/models/query-options.model';

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
  loading: boolean = false;

  sortColumn: string | null = null;
  sortDescending: boolean = false;

  constructor(private predmetService: PredmetService) {}

  ngOnInit(): void {
    this.loadStudentSubjects();
  }

  loadStudentSubjects(): void {
    this.loading = true;

    const options: QueryOptions = {};
    if (this.sortColumn) {
      options.sort = this.sortDescending ? `${this.sortColumn}|dsc` : this.sortColumn;
    }

    this.predmetService
      .getStudentSubjects(this.selectedSemesterId, options)
      .subscribe({
        next: (data) => {
          this.predmeti = data;
          this.loading = false;
        },
        error: (err) => {
          console.error('Error fetching student subjects:', err);
          this.loading = false;
        }
      });
  }

  onSemesterChange(): void {
    this.loadStudentSubjects();
  }

  sortBy(column: string): void {
    if (this.sortColumn === column) {
      this.sortDescending = !this.sortDescending;
    } else {
      this.sortColumn = column;
      this.sortDescending = false;
    }
    this.loadStudentSubjects();
  }

  sortIndicator(column: string): string {
    if (this.sortColumn !== column) return '';
    return this.sortDescending ? '▼' : '▲';
  }

  getStatusClass(status: string): string {
    return status === "Polozeno" ? "status-passed" : "status-failed";
  }

}
