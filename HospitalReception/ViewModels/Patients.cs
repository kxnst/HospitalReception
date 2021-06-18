using HospitalReception.Models;
using HospitalReception.Models.Instances;
using System.Collections.ObjectModel;
using System.Linq;

namespace HospitalReception.ViewModels
{
    public class Patients : AbstractViewModel<Patient>
    {
        public Patients()
        {
            int[] ids = { };
            container = new ObservableCollection<Patient>(DbModel.Instance.Patients.Select(ids));
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
                    TmpContainer = new ObservableCollection<Patient>((from Patient t in Container
                                                                     where t.Fio.Contains(InputText)
                                                                     select t).ToList());
                }
                    )
                );

        }

    }
}
