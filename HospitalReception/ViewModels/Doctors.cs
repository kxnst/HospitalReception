using HospitalReception.Models;
using HospitalReception.Models.Instances;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.ViewModels
{
    public class Doctors : AbstractViewModel<Doctor>
    {
        public Doctors()
        {
            int[] ids = {};
            container = new ObservableCollection<Doctor>(DbModel.Instance.Doctors.Select(ids));
            TmpContainer = container;
        }
        private string inputText;
        public string InputText
        {
            get => inputText;
            set
            {
                inputText = value;
                OnPropertyChanged("InputText");
            }
        }
        private RelayCommand textChanged;
        public RelayCommand TextChanged
        {
            set => textChanged = value;
            get => textChanged ??
                (textChanged = new RelayCommand(obj => {
                    TmpContainer = new ObservableCollection<Doctor>((from Doctor t in Container
                                   where t.Fio.Contains(InputText)
                                   select t).ToList());
                        }
                    )
                );

        }
    }
}
