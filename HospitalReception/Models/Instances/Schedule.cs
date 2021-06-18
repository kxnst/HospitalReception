using System;

namespace HospitalReception.Models.Instances
{
    public class Schedule : DbObject
    {
        private int doctorId;
        private int patientID;
        private string reason;
        public DateTime date;
        public int DoctorId
        {
            get => doctorId; 
            set => doctorId = value;
        }
        public int PatientId
        {
            get => patientID;
            set => patientID = value;
        }
        public string Reason
        {
            get => reason; 
            set => reason = value;
        }
        public DateTime Date
        {
            get => date;
            set => date = value;
        }
        public static bool isValid(Schedule t)
        {
            try
            {
                return (t.DoctorId > 0) && (t.PatientId > 0) && (t.Reason.Length > 5) && (t.Date > DateTime.Now);
            } 
            catch(Exception)
            {
                return false;
            }
        }
    }
}
