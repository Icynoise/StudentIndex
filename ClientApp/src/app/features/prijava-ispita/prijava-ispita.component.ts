// src/app/prijava-ispita/prijava-ispita.component.ts
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Import FormsModule for ngModel
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule for HTTP requests
import { DostupniIspiti, IspitPrijava } from '../../core/models/ispitPrijava.model';
import { IspitiService } from '../../core/services/ispitiPrijava.service';
import { CommonModule, DatePipe } from '@angular/common';
import { __values } from 'tslib';

@Component({
  selector: 'app-prijava-ispita',
  standalone: true,
  imports: [FormsModule, HttpClientModule, CommonModule],
  providers: [DatePipe], // Add this line
  templateUrl: './prijava-ispita.component.html',
  styleUrl: './prijava-ispita.component.scss'
})
export class PrijavaIspitaComponent implements OnInit {
  formData: IspitPrijava = {
    todaysDate: '',
    ime: '',
    prezime: '',
    brojIndexa: '',
    studijskiProgramNaziv: '',
    datumIspita: null,
    selectedPredmetId: null
  };
  availableExams: DostupniIspiti[] = [];
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(private ispitiService: IspitiService) {}

  ngOnInit(): void {
    this.loadStudentData();
    this.loadAvailableExams();
  }

  loadStudentData(): void {
    this.ispitiService.getStudentInfo().subscribe({
      next: (data) => {
        this.formData = {
          ...data,
          todaysDate: new Date(data.todaysDate).toLocaleDateString(),
          selectedPredmetId: null, // Ensure this is null even after loading data
          datumIspita: null
        };
      },
      error: (err) => {
        this.errorMessage = 'Failed to load student data: ' + err.error;
      }
    });
  }

  loadAvailableExams(): void {
    this.ispitiService.getAvailableExams().subscribe({
      next: (exams) => {
        this.availableExams = exams.map(exam => ({
          ...exam,
          datumIspita: exam.datumIspita
        }));
      },
      error: (err) => {
        this.errorMessage = 'Failed to load available exams: ' + err.error;
      }
    });
  }

  onPredmetChange(): void {
    const selectedExam = this.availableExams.find(exam => exam.ispitId === Number(this.formData?.selectedPredmetId));
    if (selectedExam && this.formData) {
        this.formData.datumIspita = selectedExam.datumIspita;
    }
  }

  onSubmit(): void {
    if (!this.formData?.selectedPredmetId) {
      this.errorMessage = 'Molimo izaberite predmet.';
      return;
    }

    this.errorMessage = null;
    this.successMessage = null;

    this.ispitiService.registerForExam(this.formData.selectedPredmetId).subscribe({
      next: (response) => {
        this.successMessage = response.message || 'Ispit uspešno prijavljen!';
        this.formData!.selectedPredmetId = undefined;
        this.formData!.datumIspita = undefined;
      },
      error: (err) => {
        this.errorMessage = 'Greška prilikom prijave ispita: ' + err.error;
      }
    });
  }
}