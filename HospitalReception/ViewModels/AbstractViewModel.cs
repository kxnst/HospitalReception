using HospitalReception.Models;
using HospitalReception.Models.Instances;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReception.ViewModels
{
    public class AbstractViewModel<T> : INotifyPropertyChanged where T : DbObject, new()
    {
        private T selected;
        protected string errorText;
        public string ErrorText
        {
            get => errorText;
            protected set {
                errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }
        public T Selected
        {
            get => selected ?? (selected = new T());
            set
            {
                selected = (T)value.Clone();
                OnPropertyChanged("Selected");
            }
        }
        private ObservableCollection<T> tmpContainer;
        public ObservableCollection<T> TmpContainer
        {
            get => tmpContainer;
            set
            {
                tmpContainer = value;
                OnPropertyChanged("TmpContainer");
            }
        }
        protected ObservableCollection<T> container;
        public ObservableCollection<T> Container
        {
            get
            {
                return container ?? (container = new ObservableCollection<T>());
            }
            protected set {
                container = value;
                OnPropertyChanged("Container");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private RelayCommand newObject;
        private RelayCommand save;
        public RelayCommand NewObject
        {
            get
            {
                return newObject ??
                (newObject = new RelayCommand(obj =>
                {
                    Selected = new T();
                }
                    )
                );
            }
        }
        public RelayCommand Save
        {
            get
            {
                return save ??
                    (save = new RelayCommand(obj =>
                    {
                        BeforeSave();
                        if ((bool)typeof(T).GetMethod("isValid").Invoke(null, new object[] { Selected }))
                        {
                            Type t = typeof(DbModel);
                            DbTable<T> dbTable = (DbTable<T>)t.GetProperty(GetType().Name).GetValue(DbModel.Instance);
                            Type t2 = dbTable.GetType();
                            t2.GetMethod("UpdateInsert", new [] { typeof(T) }).Invoke(dbTable, new object[] { Selected });
                            ErrorText = "Запис успішно оновлено/додано";
                            Container = dbTable.Select();
                            Selected = Selected;
                        }
                        else
                        {
                            ErrorText = "Перевірте вхідні дані на коректність";
                        }
                        AfterSave();
                    }
                    )
                );
            }
        }
        public virtual void BeforeSave()
        {

        }
        public virtual void AfterSave()
        {

        }
    }
}