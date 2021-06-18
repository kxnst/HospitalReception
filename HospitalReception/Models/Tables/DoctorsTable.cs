using HospitalReception.Models.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.Models.Tables
{
    public class DoctorsTable : DbTable<Doctor>
    {
        public static new string dbName = "Doctors";

        public DoctorsTable()
        {
            loadDb(dbName);
        }
    }
}
