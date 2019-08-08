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
    public static class LogicalAddUser
    {

        static List<Models.Data.User> userList = new List<Data.User>();
        public static void ShowWimdowUser(Models.Data.Company company)
        {
            View.AddUser user = new View.AddUser(Application.Current.MainWindow);
            ViewModels.ViewModel viewModel = (ViewModels.ViewModel)user.DataContext;
            viewModel.AddUser = user;
            user.Show();
            
        }

        public static void AddUserCompany(Models.Data.Company company,View.AddUser user)
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
                
                companyContext.Companies.Add(company);
                companyContext.SaveChangesAsync();

                MessageBox.Show(messageBoxText: "Объекты успешны сохранены",caption:"Успех операции",MessageBoxButton.OK,MessageBoxImage.Information);

                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                Models.LogicalMainWindow.PrintDataGrid(mainWindow.UserBD, companyContext.Users.ToList());
                Models.LogicalMainWindow.PrintDataGrid(mainWindow.CompanBD, companyContext.Companies.ToList());
            }

        }
        //форма с комбобоксом пошла
        public static void UserAdd()
        {
            Data.ModelCode.CompanyContext bd = new Data.ModelCode.CompanyContext();
            if (bd.Companies.ToList().Count == 0)
            {
                View.AddComp user = new View.AddComp(Application.Current.MainWindow);
                ViewModels.ViewModel Add = user.DataContext as ViewModels.ViewModel;
                Add.AddComp = user;
                user.Show();
            }
            else
            {
                View.AddUserCompany addUserCompany = new View.AddUserCompany(Application.Current.MainWindow as MainWindow);
                ViewModels.ViewModel Add = addUserCompany.DataContext as ViewModels.ViewModel;
                Add.AddUserCompany = addUserCompany;
                List<string> companies = new List<string>();
                for (int i = 0; i < bd.Companies.ToList().Count; i++)
                {
                    companies.Add(bd.Companies.ToList()[i].Name);
                }
                addUserCompany.UserCombo.ItemsSource = companies;
                addUserCompany.UserCombo.SelectedItem = 0;
                addUserCompany.Show();
            }
        }

        public static void ClickButtonAdd(View.AddUserCompany addUserCompany)
        {
            Data.ModelCode.CompanyContext bd = new Data.ModelCode.CompanyContext();
            if (addUserCompany.Name.Text == "")
            {
                MessageBox.Show(messageBoxText: "Имя пользователя и его логин не могу быть пустыми.", caption: "Исключение"
               , MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            Data.User user = new Data.User();
            user.Login = addUserCompany.Login.Text;
            user.Name = addUserCompany.Name.Text;
            user.Password = addUserCompany.Password.Text;
            foreach (Data.Company item in bd.Companies.ToList())
            {
                if (item.Name == addUserCompany.UserCombo.Text)
                {
                    Data.Company company = item;
                    user.Company = company;
                    user.Comn = company.Name;
                }
            }
            if (MessageBoxResult.Yes ==
               MessageBox.Show(
               messageBoxText: "Нужно добавить ещё пользователя?",
               caption: "Предложение.",
               MessageBoxButton.YesNo,
               MessageBoxImage.Question))
            {
                bd.Users.Add(user);
                bd.SaveChangesAsync();
                addUserCompany.Name.Text = "";
                addUserCompany.Login.Text = "";
                addUserCompany.Password.Text = "";
                return;
            }
            else
            {
                bd.Users.Add(user);
                bd.SaveChangesAsync();
                addUserCompany.Close();
            }
            Models.LogicalMainWindow.PrintDataGrid(((MainWindow)Application.Current.MainWindow).UserBD, bd.Users.ToList());
            Models.LogicalMainWindow.PrintDataGrid(((MainWindow)Application.Current.MainWindow).CompanBD, bd.Companies.ToList());
        }


    }
}
