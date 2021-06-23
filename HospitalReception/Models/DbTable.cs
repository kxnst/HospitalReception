using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HospitalReception.Models.Instances;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace HospitalReception.Models
{
    public abstract class DbTable<T> where T: DbObject
    {
        protected ObservableCollection<T> container = new ObservableCollection<T>();

        protected static int id = 1;

        protected static string dbName = "";

        protected static string path = "";
        public DbTable()
        {
        }

        protected void loadDb(string path_)
        {
            path = Directory.GetCurrentDirectory() + "\\" + path_ + ".txt";
            if (!File.Exists(path))
            {
                var stream = File.Create(path);
                stream.Close();
            }
            using (FileStream fstream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array);

                string textFromFile = Encoding.Default.GetString(array);
                ObservableCollection<T> deserializedProduct;
                try
                {
                    deserializedProduct = new ObservableCollection<T>(JsonConvert.DeserializeObject<List<T>>(textFromFile ?? "[]"));
                }
                catch (Exception)
                {
                    deserializedProduct = new ObservableCollection<T>();
                }
                container = deserializedProduct ?? new ObservableCollection<T>();
                id = (container.Count!=0) ? container.OrderByDescending(x => x.id).First().id : 0;
            } 

        }
        public void updateDb() 
        {

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                string encoded = JsonConvert.SerializeObject(container);
                byte[] bytes = Encoding.Default.GetBytes(encoded);
                stream.Write(bytes);
            }
            
        }

        public void Update(int[] ids, string property, object value)
        {
            ObservableCollection<T> toUpdate = Select(ids);
            Type type = typeof(T);
            //Use reflection to update objects
            PropertyInfo pInfo = type.GetProperty(property);
            if (pInfo == null)
                return;
            foreach (T obj in toUpdate)
            {
                pInfo.SetValue(obj, value);
            }

            updateDb();
        }
        public void Update(DbObject dbObject)
        {
            Type type = typeof(T);
            int[] ids =
            {
                dbObject.id
            };
            PropertyInfo[] fInfos = type.GetProperties();
            foreach(PropertyInfo info in fInfos)
            {
                Update(ids, info.Name, info.GetValue(dbObject));
            }
        }
        public ObservableCollection<T> Select(int id)
        {
            int[] ids = { id };
            return Select(ids);
        }
        public ObservableCollection<T> Select()
        {
            int[] ids = { };
            return Select(ids);
        }
        public ObservableCollection<T> Select(int[] ids)
        {
            if(ids.Length==0)
            {
                return container;
            }
            var result = (from obj in container
                          where ids.Contains(obj.id)
                          select obj).ToList();

            return new ObservableCollection<T>(result);
       
        }
        public void Insert(T obj)
        {
            obj.id = ++id;
            container.Add(obj);
            updateDb();
        }
        public void UpdateInsert(T dbObject)
        {
            if (dbObject.id < 1) {
                Insert(dbObject);
            } else {
                Update(dbObject);
            }
        }
    }
}
