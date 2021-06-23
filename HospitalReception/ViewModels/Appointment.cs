using HospitalReception.Models;
using HospitalReception.Models.Instances;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HospitalReception.ViewModels
{
    class Appointment : AbstractViewModel<Models.Instances.Schedule>
    {
        public Appointment()
        {
            Container = DbModel.Instance.Schedule.Select();
            DoctorsContainer = DbModel.Instance.Doctors.Select();
            PatientsContainer = DbModel.Instance.Patients.Select();
            timeRenderer = new AllowedTimeRenderer();
            Selected = new Models.Instances.Schedule();
            selectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
        private ObservableCollection<Doctor> doctorsContainer;
        private ObservableCollection<Patient> patientsContainer;
        private Patient selectedPatient;
        private Doctor selectedDoctor;
        private DateTime selectedDate;
        private AllowedTimeRenderer timeRenderer;
        private DateTime[] allowedTime;
        private DateTime selectedTime;
        public DateTime SelectedTime
        {
            get => selectedTime;
            set
            {
                selectedTime = value;
                Selected.Date = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Hour
                    , SelectedTime.Minute, 0);
                OnPropertyChanged("SelectedTime");
            }
        }
        public DateTime[] AllowedTime
        {
            get => allowedTime;
            set
            {
                allowedTime = value;
                OnPropertyChanged("AllowedTime");
            }
        }
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                Selected.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                OnPropertyChanged("SelectedDate");
                ChangeTime();

            }
        }

        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
                Selected.PatientId = value.id;
            }
        }
        public Doctor SelectedDoctor
        {
            get => selectedDoctor;
            set
            {
                selectedDoctor = value;
                Selected.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                OnPropertyChanged("SelectedDoctor");
                Selected.DoctorId = value.id;
                ChangeTime();
            }
        }

        public ObservableCollection<Doctor> DoctorsContainer
        {
            get => doctorsContainer;
            set
            {
                doctorsContainer = value;
                OnPropertyChanged("DoctorsContainer");
            }
        }
        public ObservableCollection<Patient> PatientsContainer
        {
            get => patientsContainer;
            set
            {
                patientsContainer = value;
                OnPropertyChanged("PatientsContainer");
            }
        }

        public void ChangeTime()
        {
            if(selectedDoctor == null)
            {
                return;
            }

            DateTime[] reserved = (from t in container
                                   where t.Date.Year == SelectedDate.Year
                                   && t.Date.Month == SelectedDate.Month
                                   && t.Date.Day == SelectedDate.Day
                                   && t.DoctorId == SelectedDoctor.id
                                   select t.Date).ToArray();
            AllowedTime = timeRenderer.RenderAllowedTime(reserved, SelectedDate).ToArray();

        }
        public override void AfterSave()
        {
            ChangeTime();
        }
    }
}
