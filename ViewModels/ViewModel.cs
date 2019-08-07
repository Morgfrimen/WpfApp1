using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Переменные
        Window window;
        View.AddUser user;
        View.AddComp comp;
        #endregion
        #region Конструктор класса
        public ViewModel(Window Window) => window = Window;
        #endregion
        #region Реализация интерфейса+метод обновления
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Команды
        private Models.CommandMainWindow _addCommandUser;
        public Models.CommandMainWindow AddCommandUser
        {
            get
            {
                return _addCommandUser ??
                  (_addCommandUser = new Models.CommandMainWindow(obj =>
                  {
                      View.AddUser users = new View.AddUser(window);
                      user = users;
                      users.Show();

                  }));
            }
        }
        private Models.CommandAddUser _exitCommandUser;
        public Models.CommandAddUser ExitCommandUser
        {
            get
            {
                return _exitCommandUser ??
                  (_exitCommandUser = new Models.CommandAddUser(obj =>
                  {
                      user.Close();
                  }));
            }
        }
        private Models.CommandMainWindow _addCommandCompany;
        public Models.CommandMainWindow AddCommandCompany
        {
            get
            {
                return _addCommandCompany ??
                  (_addCommandCompany = new Models.CommandMainWindow(obj =>
                  {
                      View.AddComp user = new View.AddComp(window);
                      comp = user;
                      user.Show();

                  }));
            }
        }
        private Models.CommandAppComp _exitCommandComp;
        public Models.CommandAppComp ExitCommandComp
        {
            get
            {
                return _exitCommandComp ??
                  (_exitCommandComp = new Models.CommandAppComp(obj =>
                  {
                      comp.Close();
                  }));
            }
        }
        #endregion


    }
}
