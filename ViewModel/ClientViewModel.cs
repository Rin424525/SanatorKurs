using Sanator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sanator.ViewModel
{
    class ClientViewModel : INotifyPropertyChanged
    {
        public DbOperations db;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Client> clients { get; set; }
        private Client selectedClient;
        private IDialogService ds;
        public ClientViewModel(DbOperations db, IDialogService ds)
        {
            this.ds = ds;
            this.db = db;
            clients = new ObservableCollection<Client>(db.GetAllClient());
            clients.OrderBy(u => u.FIO);
            SelectedClient= db.GetAllClient().FirstOrDefault();
        }
        
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        private RelayCommand addClient;
        public RelayCommand AddClient
        {
            get
            {
                return addClient ??
                    (addClient = new RelayCommand(obj =>
                    {
                        Client client = new Client() { Fio = "Новый клиент" , Birth=DateTime.Now};
                        clients.Insert(0, client);
                        SelectedClient = client;
                        ds.ShowMessage("Новый объект добавлен!");
                      
                    }));
            }
        }
        private RelayCommand removeClient;
        public RelayCommand RemoveClient
        {
            get
            {
                return removeClient ??
                    (removeClient = new RelayCommand(obj =>
                    {

                        if (db.FindClient(SelectedClient.ID_client) != null)
                        {
                            db.RemoveClient(SelectedClient);
                            clients.Remove(SelectedClient);
                            db.Save();                          
                        }
                        else clients.Remove(SelectedClient);
                        ds.ShowMessage("Объект удален!");                                 
                    },
                    (obj) => SelectedClient != null));
            }
        }

        private RelayCommand saveClients;
        public RelayCommand SaveClients
        {
            get
            {
                return saveClients ??
                    (saveClients = new RelayCommand(obj =>
                    {
                        if (SelectedClient != null)
                        {
                            var client = db.FindClient(SelectedClient.ID_client);
                            if (client == null)
                            {
                                db.AddClient(SelectedClient);
                            }
                            db.Save();
                            ds.ShowMessage("Изменения сохранены!");
                        }
                        else ds.ShowMessage("Пожалуйста, выберете клиента из списка!");
                    },obj=>CanExecuteSave()));     
            }
        }
        private bool CanExecuteSave()
        {
            if (SelectedClient != null)
            {
                if (SelectedClient.Fio.TrimEnd() != "Новый клиент" && SelectedClient.Number != null && SelectedClient.Passport != null) return true;               
            }
            return false;

        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
