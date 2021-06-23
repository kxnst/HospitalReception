using HospitalReception.Models.Instances;
using HospitalReception.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalReception.Models
{
    public class DbModel
    {
        public DbTable<Patient> Patients { get; private set; }
        public DbTable<Schedule> Schedule { get; private set; }
        public DbTable<Doctor> Doctors { get; private set; }
        public DbTable<Schedule> Appointment { get; private set; }

        private static DbModel instance;
        public static DbModel Instance
        {
            get
            {
                return instance ?? (instance = new DbModel());
            }
            private set { }
        }
        private void init()
        {
            this.Patients = new PatientsTable();
            this.Schedule = new ScheduleTable();
            this.Doctors = new DoctorsTable();
            this.Appointment = Schedule;
        }
        private DbModel()
        {
            init();
        }

    }
}
