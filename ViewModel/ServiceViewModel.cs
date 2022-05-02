using Host_v1.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Host_v1.ViewModel
{
    class ServiceViewModel: INotifyPropertyChanged
    {
        DbOperations db;
        private Service selectedService;
        public ObservableCollection<Service> Services { get; set; }
        IDialogService ds;
        public ServiceViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            Services = new ObservableCollection<Service>(db.GetAllService());
            SelectedService=db.GetAllService().FirstOrDefault();
        }
        public Service SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedService");
            }
        }
        private RelayCommand addService;
        public RelayCommand AddService
        {
            get
            {
                return addService ??
                    (addService = new RelayCommand(obj =>
                    {
                        Service service = new Service() { Name = "Новая услуга"};
                        Services.Insert(0, service);
                        SelectedService = service;
                        ds.ShowMessage("Новая услуга добавлена!");
                    }));
            }
        }
        private RelayCommand removeService;
        public RelayCommand RemoveService
        {
            get
            {
                return removeService ??
                    (removeService = new RelayCommand(obj =>
                    {

                        if (db.FindService(SelectedService.ID_service) != null)
                        {
                            db.RemoveService(SelectedService);
                            Services.Remove(SelectedService);
                            db.Save();
                            ds.ShowMessage("Объект удален!");
                        }
                        else Services.Remove(SelectedService);
                    },
                    (obj) => SelectedService != null));
            }
        }

        private RelayCommand saveService;
        public RelayCommand SaveService
        {
            get
            {
                return saveService ??
                    (saveService = new RelayCommand(obj =>
                    {
                        if (SelectedService != null)
                        {
                            var service = db.FindService(SelectedService.ID_service);
                            if (service == null)
                            {
                                db.AddService(SelectedService);
                            }
                            db.Save();
                            ds.ShowMessage("Изменения сохранены!");
                        }

                    },
                    (obj) => (SelectedService != null) && SelectedService.Name.TrimEnd() != "Новая услуга" && SelectedService.Description != null));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
