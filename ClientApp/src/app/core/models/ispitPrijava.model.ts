export interface IspitPrijava {
    todaysDate: string;
    ime: string;
    prezime: string;
    brojIndexa: string;
    studijskiProgramNaziv: string;
    datumIspita?: string | null;
    selectedPredmetId?: number | null;
  }
  
  export interface DostupniIspiti {
    ispitId: number;
    predmetNaziv: string;
    datumIspita: string;
  }