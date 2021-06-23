using Microsoft.VisualStudio.TestTools.UnitTesting;
using HospitalReception.Models.Instances;
using HospitalReception.Models.Tables;
using System;
using HospitalReception.Models;
using System.Linq;

namespace TestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestIsValid()
        {
            Doctor d1t = new Doctor();
            d1t.Fio = "Тест Тест Тест";
            d1t.Specialization = "Тест Тест Тест";
            d1t.Experience = 18;
            Doctor d2t = new Doctor();
            d2t.Fio = "";
            d2t.Specialization = "Тест Тест Тест";
            d2t.Experience = 18;
            Patient p1t = new Patient();
            p1t.Fio = "Тест Тест Тест";
            p1t.Disease = "Тест Тест Тест";
            p1t.Age = 18;
            Patient p2t = new Patient();
            p2t.Fio = "";
            p2t.Disease = "Тест Тест Тест";
            p2t.Age = 18;
            Schedule s1t = new Schedule();
            s1t.PatientId = 1;
            s1t.DoctorId = 1;
            s1t.Reason = "тесттест";
            s1t.Date = System.DateTime.Now.AddDays(12);
            Schedule s2t = new Schedule();
            s2t.PatientId = -1;
            s2t.DoctorId = -1;
            s2t.Reason = "";
            s2t.Date = System.DateTime.MinValue;
            Assert.AreEqual(true, Doctor.isValid(d1t));
            Assert.AreEqual(false, Doctor.isValid(d2t));
            Assert.AreEqual(true, Patient.isValid(p1t));
            Assert.AreEqual(false, Patient.isValid(p2t));
            Assert.AreEqual(true, Schedule.isValid(s1t));
            Assert.AreEqual(false, Schedule.isValid(s2t));
        }
        [TestMethod]
        public void TestClone()
        {
            DateTime t = DateTime.Now.AddDays(12); ;
            Schedule s1t = new Schedule();
            s1t.PatientId = 1;
            s1t.DoctorId = 1;
            s1t.Reason = "тесттест";
            s1t.Date = t;
            Patient p1t = new Patient();
            p1t.Fio = "Тест Тест Тест";
            p1t.Disease = "Тест Тест Тест";
            p1t.Age = 18;
            Doctor d1t = new Doctor();
            d1t.Fio = "Тест Тест Тест";
            d1t.Specialization = "Тест Тест Тест";
            d1t.Experience = 18;
            Schedule s2t = (Schedule)s1t.Clone();
            Patient p2t = (Patient)p1t.Clone();
            Doctor d2t = (Doctor)d1t.Clone();
            Assert.AreEqual(true, (p1t.Age==p2t.Age) && (p1t.Disease == p2t.Disease) && (p1t.Fio == p2t.Fio));
            Assert.AreEqual(true, (d1t.Experience == d2t.Experience) && (d2t.Fio == d1t.Fio) && (d2t.Specialization == d1t.Specialization));
            Assert.AreEqual(true, (s1t.Reason == s2t.Reason) && (s1t.PatientId == s2t.PatientId) && (s1t.DoctorId == s2t.DoctorId) && (s1t.Date == s2t.Date));
        }
        [TestMethod]
        public void TestInsert()
        {
            Patient p = new Patient();
            p.Disease = "ТЕст тест";
            p.Age = 18;
            p.Fio = "тест тест тест";
            p.id = 1;
            DbModel.Instance.Patients.Select().Clear();
            DbModel.Instance.Patients.updateDb();
            DbModel.Instance.Patients.Insert(p);
            Assert.AreEqual(DbModel.Instance.Patients.Select()[0].id, p.id);
        }
        [TestMethod]
        public void TestUpdate()
        {
            Patient p = new Patient();
            p.Disease = "ТЕст тест";
            p.Age = 18;
            p.Fio = "тест тест тест";
            DbModel.Instance.Patients.Select().Clear();
            DbModel.Instance.Patients.Insert(p);
            Patient p2 = (Patient)p.Clone();
            p2.Disease = "тест";
            p2.Fio = "тест";
            p2.Age = 19;
            DbModel.Instance.Patients.Update(p2);
            Assert.AreEqual(DbModel.Instance.Patients.Select()[0].Disease, p2.Disease);
            Assert.AreEqual(DbModel.Instance.Patients.Select()[0].Fio, p2.Fio);
            Assert.AreEqual(DbModel.Instance.Patients.Select()[0].Age, p2.Age);
        }
        [TestMethod]
        public void TestSelect()
        {
            Patient p = new Patient();
            p.Disease = "ТЕст тест";
            p.Age = 18;
            p.Fio = "тест тест тест";
            DbModel.Instance.Patients.Select().Clear();
            DbModel.Instance.Patients.Insert(p);
            Assert.AreEqual(DbModel.Instance.Patients.Select()[0].Disease, p.Disease);
            Assert.AreEqual(DbModel.Instance.Patients.Select()[0].Fio, p.Fio);
            Assert.AreEqual(DbModel.Instance.Patients.Select()[0].Age, p.Age);
        }
    }
}
