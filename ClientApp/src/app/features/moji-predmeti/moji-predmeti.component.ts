import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-moji-predmeti',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './moji-predmeti.component.html',
  styleUrls: ['./moji-predmeti.component.scss']
})
export class MojiPredmetiComponent {
  selectedSemester: string = "IV semestar"; // Default semester

  // List of subjects
  predmeti = [
    { naziv: "Visi programski jezici i RAD alati", ects: 6, status: "Polozeno" },
    { naziv: "Internet marketing i elektronsko poslovanje", ects: 5, status: "Polozeno" },
    { naziv: "Ekspertni sistemi", ects: 6, status: "Nepolozeno" },
    { naziv: "Cyber pravo", ects: 4, status: "Polozeno" },
    { naziv: "Softverski inzinjering", ects: 8, status: "Nepolozeno" },
  ];

  // 🔹 Make sure this method is inside the class and public
  getStatusClass(status: string): string {
    return status === "Polozeno" ? "status-passed" : "status-failed";
  }
}
