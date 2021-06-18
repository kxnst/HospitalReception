using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HospitalReception.Models.Instances
{
    public abstract class DbObject : INotifyPropertyChanged
    {
        public int id { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public static bool isValid(DbObject check) => false;
        public DbObject() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
