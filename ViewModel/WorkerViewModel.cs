
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sanator.ViewModel
{
    class WorkerViewModel : INotifyPropertyChanged
    {
        DbOperations db;
        private Worker selectedWorker;
        public ObservableCollection<Worker> workers { get; set; }
        IDialogService ds;
        public WorkerViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            workers = new ObservableCollection<Worker>(db.GetAllWorker());
            SelectedWorker=db.GetAllWorker().FirstOrDefault();
        }
        public Worker SelectedWorker
        {
            get { return selectedWorker; }
            set
            {
                selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }
        private RelayCommand addWorker;
        public RelayCommand AddWorker
        {
            get
            {
                return addWorker ??
                    (addWorker = new RelayCommand(obj =>
                    {
                        Worker worker = new Worker() { Fio = "Новый сотрудник", Birth=DateTime.Now };
                        workers.Insert(0, worker);
                        SelectedWorker = worker;
                        ds.ShowMessage("Новый сотрудник добавлен!");
                    }));
            }
        }
        private RelayCommand removeWorker;
        public RelayCommand RemoveWorker
        {
            get
            {
                return removeWorker ??
                    (removeWorker = new RelayCommand(obj =>
                    {

                        if (db.FindWorker(SelectedWorker.ID_worker) != null)
                        {
                            db.RemoveWorker(SelectedWorker);
                            workers.Remove(SelectedWorker);
                            db.Save();
                            ds.ShowMessage("Объект удален!");
                        }
                        else workers.Remove(SelectedWorker);
                    },
                    (obj) => SelectedWorker != null));
            }
        }

        private RelayCommand saveWorker;
        public RelayCommand SaveWorker
        {
            get
            {
                return saveWorker ??
                    (saveWorker = new RelayCommand(obj =>
                    {
                    
                            var Worker = db.FindWorker(SelectedWorker.ID_worker);
                            if (Worker == null)
                            {
                                db.AddWorker(SelectedWorker);
                            }
                            db.Save();
                            ds.ShowMessage("Изменения сохранены!");
                        
                       
                    },
                    (obj) => (SelectedWorker != null) && SelectedWorker.Fio.TrimEnd() != "Новый сотрудник" && SelectedWorker.Number != null && SelectedWorker.Passport != null));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
