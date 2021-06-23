using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalReception.Models.Instances
{
    public class Patient : DbObject, IHuman
    {
        private string fio;
        private string disease;
        private int age;
        public string Fio
        {
            get => fio;
            set
            {
                fio = value;
                OnPropertyChanged("Fio");
            }
        }
        public string Disease
        {
            get => disease;
            set
            {
                disease = value;
                OnPropertyChanged("Disease");
            }
        }
        public int Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }
        public static bool isValid(Patient patient)
        {
            if (patient == null)
                return false;
            if (patient.Fio == null || patient.Disease == null || patient.Age <= 0)
                return false;
            bool validFio = Regex.IsMatch(patient.Fio, "[\\p{L}\\s]+");
            bool validAge = patient.Age > 0;
            bool validDisease = Regex.IsMatch(patient.Disease, "[\\p{L}\\s]{2,}");
            return validFio && validAge && validDisease;
        }
    }
}
