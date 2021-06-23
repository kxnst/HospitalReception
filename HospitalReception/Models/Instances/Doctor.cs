using HospitalReception.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalReception.Models.Instances
{
    public class Doctor : DbObject, IHuman
    {
        private string fio;
        private string specialization;
        private int experience;
        public string Fio {
            get => fio;
            set
            {
                fio = value;
                OnPropertyChanged("Fio");
            }
        }
        public string Specialization
        {
            get => specialization;
            set
            {
                specialization = value;
                OnPropertyChanged("Specialization");
            }
        }
        public int Experience
        {
            get => experience;
            set
            {
                experience = value;
                OnPropertyChanged("Experience");
            }
        }

        public static bool isValid(Doctor doctor)
        {
            if (doctor == null)
                return false;
            if (doctor.fio == null || doctor.specialization == null || doctor.experience <= 0)
                return false;
            bool validFio = Regex.IsMatch(doctor.Fio, "[\\p{L}\\s]+");
            bool validExperience = doctor.Experience > 0;
            bool validSpecialization = Regex.IsMatch(doctor.Specialization, "[\\p{L}\\s]{2,}");
            return validFio && validExperience && validSpecialization;
        }
    }
}
