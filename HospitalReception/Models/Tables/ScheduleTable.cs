using HospitalReception.Models.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.Models.Tables
{
    public class ScheduleTable : DbTable<Schedule>
    {
        public static new string dbName = "Schedule";
        public ScheduleTable()
        {
            loadDb(dbName);
        }

    }
}
