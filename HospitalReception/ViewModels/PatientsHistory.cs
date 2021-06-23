using HospitalReception.Models;
using HospitalReception.Models.Instances;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.ViewModels
{
    class PatientsHistory : AbstractViewModel<Schedule>
    {
        private ObservableCollection<Patient> patientsContainer;
        private Patient selectedPatient;
        
        public ObservableCollection<Patient> PatientsContainer
        {
            get => patientsContainer;
            set
            {
                patientsContainer = value;
                OnPropertyChanged("PatientsContainer");
            }
        }
        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
                updateSchedule();
            }
        }
        public PatientsHistory()
        {
            Container = DbModel.Instance.Schedule.Select();
            PatientsContainer = DbModel.Instance.Patients.Select();
        }
        private void updateSchedule()
        {
            TmpContainer = new ObservableCollection<Schedule>(
                from t in Container
                where t.PatientId == SelectedPatient.id
                orderby t.Date descending
                select t);
        }

    }
}
