using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Models
{
    public class LogicalAddUser : INotifyPropertyChanged
    {
        #region Реализация интерфейса + метод обновления
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        static List<Models.Data.User> userList = new List<Data.User>();
        public static void ShowWimdowUser(Models.Data.Company company)
        {
            View.AddUser user = new View.AddUser(Application.Current.MainWindow);
            ViewModels.ViewModel viewModel = (ViewModels.ViewModel)user.DataContext;
            viewModel.AddUser = user;
            user.Show();
            
        }

        public static void AddUserCompany(Models.Data.Company company,View.AddUser user)//Когра команда клина на пользователе
        {
            
            if (user.Name.Text=="" | user.Login.Text=="")
            {
                MessageBox.Show(messageBoxText: "Имя пользователя и его логин не могу быть пустыми.", caption: "Исключение"
                    , MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            Models.Data.User users = new Data.User();
            users.Login = user.Login.Text;
            users.Name = user.Name.Text;
            users.Password = user.Password.Text;
            users.Company = company;
            users.Comn = company.Name;
            company.Users.Add(users);
            
            userList.Add(users);
           
           // MessageBoxResult result = new MessageBoxResult();
            if (MessageBoxResult.Yes == 
                MessageBox.Show(
                    messageBoxText: "Нужно добавить ещё пользователя?", 
                    caption: "Предложение.", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Question))
            {
                user.Name.Text = "";
                user.Login.Text = "";
                user.Password.Text = "";
                return;
            }
            else
            {
                AddBD(company, userList);
                user.Close();
            }

        }
        private static void AddBD(Models.Data.Company company,List<Models.Data.User> users)
        {
            using (Models.Data.ModelCode.CompanyContext companyContext = new Data.ModelCode.CompanyContext())
            {
                //Формирование контекста
                companyContext.Companies.Add(company);
                foreach (var item in userList)
                {
                    companyContext.Users.Add(item);
                }
                //Сохранение данных
                companyContext.SaveChangesAsync();
                //Отчет
                MessageBox.Show(messageBoxText: "ОбЪекты успешны сохранены",caption:"Успех операции",MessageBoxButton.OK,MessageBoxImage.Information);

                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                Models.LogicalMainWindow.PrintDataGrid(mainWindow.UserBD, companyContext.Users.ToList());
                Models.LogicalMainWindow.PrintDataGrid(mainWindow.CompanBD, companyContext.Companies.ToList());
            }

        }
 
    }
}
