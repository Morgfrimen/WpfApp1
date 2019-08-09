using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Переменные
        MainWindow window;
        View.AddUser user;
        View.AddUserCompany userCompany;
        public View.AddUserCompany AddUserCompany { get => userCompany; set => userCompany = value; }
        public View.AddUser AddUser { get => user; set => user = value; }
        View.AddComp comp;
        public View.AddComp AddComp { get => comp; set => comp = value; }
        #endregion

        #region Конструктор класса
        public ViewModel(MainWindow Window)
        {
            window = Window;
            window.Loaded += Window_Loaded;
            window.DeleteUser.MouseDoubleClick += DeleteUser_MouseDoubleClick;
            window.DeleteComp.MouseDoubleClick += DeleteComp_MouseDoubleClick;
        }




        #region События
        private void DeleteComp_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Очистить таблицу?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                Models.Data.ModelCode.CompanyContext bd = new Models.Data.ModelCode.CompanyContext();
                bd.Users.RemoveRange(bd.Users);
                bd.Companies.RemoveRange(bd.Companies);
                bd.SaveChanges();
                Models.LogicalMainWindow.PrintDataGrid(window.CompanBD, bd.Companies.ToList());
                Models.LogicalMainWindow.PrintDataGrid(window.UserBD, bd.Users.ToList());
            }
        }
        private void DeleteUser_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(MessageBoxResult.Yes== MessageBox.Show("Очистить таблицу?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                Models.Data.ModelCode.CompanyContext bd = new Models.Data.ModelCode.CompanyContext();
                bd.Users.RemoveRange(bd.Users);
                bd.SaveChanges();
                Models.LogicalMainWindow.PrintDataGrid(window.UserBD, bd.Users.ToList());
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            Models.Data.ModelCode.CompanyContext context = new Models.Data.ModelCode.CompanyContext();
            Models.LogicalMainWindow.PrintDataGrid(window.UserBD, context.Users.ToList());
            Models.LogicalMainWindow.PrintDataGrid(window.CompanBD, context.Companies.ToList());
        }
        #endregion

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
        #region Пользователи
        private Models.CommandMainWindow _addCommandUser;
        public Models.CommandMainWindow AddCommandUser
        {
            get
            {
                return _addCommandUser ??
                  (_addCommandUser = new Models.CommandMainWindow(obj =>
                  {

                      Models.CommandAddUser.WindowsAdd();
                      
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
                      Application.Current.Windows[Application.Current.Windows.Count-1].Close();
                      //user.Close();
                  }));
            }
        }

        private Models.CommandAddUser _addStringUserBD;
        public Models.CommandAddUser AddStringUserBD
        {
            get
            {
                return _addStringUserBD ??
                  (_addStringUserBD = new Models.CommandAddUser(obj =>
                  {
                      Models.CommandAddUser.Add(Models.LogicalAddCompany.Company, user);
                  }));
            }
        }

        //форма 2
        private Models.CommandAddUser _clickButtonAdd;
        public Models.CommandAddUser ClickButtonAdd
        {
            get
            {
                return _clickButtonAdd ??
                  (_clickButtonAdd = new Models.CommandAddUser(obj =>
                  {
                      Models.CommandAddUser.ClickButtonAdd(AddUserCompany);
                  }));
            }
        }

        #endregion
        #region Компании
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
                      try
                      {
                          comp.Close();
                      }
                      catch
                      {

                      }
                      
                  }));
            }
        }

        private Models.CommandAppComp _addStringBD;
        public Models.CommandAppComp AddStringBD
        {
            get
            {
                return _addStringBD ??
                  (_addStringBD = new Models.CommandAppComp(obj =>
                  {
                      Models.Data.Company company = new Models.Data.Company() { Name = comp.Name.Text, Status = comp.Combo.SelectedValue.ToString()};
                      Models.CommandAppComp.Add(company,comp);
                  }));
            }
        }
        #endregion

        #region Основное окно
        private Models.CommandMainWindow _chanded;
        public Models.CommandMainWindow Changed
        {
            get
            {
                return _chanded ??
                  (_chanded = new Models.CommandMainWindow(obj =>
                  {
                      Models.LogicalMainWindow.DeletedBD((List<Models.Data.Company>)window.CompanBD.ItemsSource);
                      Models.LogicalMainWindow.DeletedBD((List<Models.Data.User>)window.UserBD.ItemsSource);
                     
                      
                  }));
            }
        }

        private Models.CommandMainWindow _deleted;
        public Models.CommandMainWindow Deleted
        {
            get
            {
                return _deleted ??
                  (_deleted = new Models.CommandMainWindow(obj =>
                  {
                      Models.LogicalMainWindow.ClickDelenedRow();
                  }));
            }
        }
        #endregion
        #endregion


    }
}
