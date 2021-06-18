using HospitalReception.Models.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.Models.Tables
{
    public class PatientsTable : DbTable<Patient>
    {
        public static new string dbName = "Patients";

        public PatientsTable()
        {
            loadDb(dbName);
        }
    }
}
