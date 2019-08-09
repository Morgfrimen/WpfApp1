using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp1.Models
{
    public class LogicalMainWindow : INotifyPropertyChanged
    {
        #region Реализация интерфейса + метод обновления
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        static MainWindow window = (MainWindow)Application.Current.MainWindow;
        public static MainWindow MainWindow { get => window; }

        //тупо удалям все записи в БД и приравниваем то,что есть при нажатии кнопки Изменить
        List<Data.User> users;
        public List<Data.User> Users { get => users; set => users = value; }

        public static void DeletedBD(List<Data.User> gridUsers)
        {
            Models.Data.ModelCode.CompanyContext bd = new Data.ModelCode.CompanyContext();
            if (gridUsers.Count > bd.Users.ToList().Count)
            {
                bd.Users.Add(gridUsers[gridUsers.Count - 1]);
            }
            for (int i = 0; i < bd.Users.ToList().Count; i++)
            {
                Data.User user = bd.Users.ToList()[i];
                Data.User user1 = gridUsers[i];
                if (user1.Login != user.Login | user1.Name != user.Name | user1.Password != user.Password)
                {
                    bd.Users.ToList()[i].Login = user1.Login;
                    bd.Users.ToList()[i].Name = user1.Name;
                    bd.Users.ToList()[i].Password = user1.Password;
                    //bd.Users.RemoveRange(bd.Users);
                    //bd.Users.AddRange(BD);

                }
            }
            bd.SaveChanges();
            PrintDataGrid(window.CompanBD, bd.Companies.ToList());
            PrintDataGrid(window.UserBD, bd.Users.ToList());



        }

        public static void PrintDataGrid(DataGrid grid, List<Data.User> items)
        {
            grid.ItemsSource = items;
            DataGridColumn column = grid.Columns[grid.Columns.Count - 1];
            column.Visibility = Visibility.Collapsed;
            column = grid.Columns[0];
            column.Visibility = Visibility.Collapsed;
            column = grid.Columns[1];
            column.Header = "Имя пользователя";
            column = grid.Columns[2];
            column.Header = "Логин";
            column = grid.Columns[3];
            column.Header = "Пароль";
            column = grid.Columns[4];
            column.Header = "Компания";
        }

        public static void PrintDataGrid(DataGrid grid, List<Data.Company> items)
        {
            grid.ItemsSource = items;
            DataGridColumn column = grid.Columns[grid.Columns.Count - 1];
            column.Visibility = Visibility.Collapsed;
            column = grid.Columns[0];
            column.Visibility = Visibility.Collapsed;
            column = grid.Columns[1];
            column.Header = "Название компании";
            grid.Columns[2].Header = "Статус договора";
            grid.Columns[2].Width = 150;
            //string[] str= new string[3] { "Ещё не заключен", "Заключен", "Расторгнут" };
            //Models.Data.ModelCode.CompanyContext bd = new Data.ModelCode.CompanyContext();
            //List<Data.Company> companies = bd.Companies.ToList();

            //grid.Columns[2] = new DataGridComboBoxColumn()
            //{
            //    Header ="Статус заказа",
            //    ItemsSource=str,
            //    Width =150,
            //    TextBinding =new Binding() {Source=companies[0],Path=new PropertyPath("Status") }
            //};




        }

        public static void DeletedBD(List<Data.Company> gridUsers)//
        {
            Models.Data.ModelCode.CompanyContext bd = new Data.ModelCode.CompanyContext();
            if (gridUsers.Count > bd.Companies.ToList().Count)
            {
                bd.Companies.Add(gridUsers[gridUsers.Count - 1]);
            }
            
            for (int i = 0; i < bd.Companies.ToList().Count; i++)
            {
                Data.Company user = bd.Companies.ToList()[i];
                Data.Company user1 = gridUsers[i];
                if (user1.Name != user.Name | user1.Status != user.Status)
                {
                    bd.Companies.ToList()[i].Name = user1.Name;
                    bd.Companies.ToList()[i].Status = user1.Status;
                }
            }
            bd.SaveChanges();
            PrintDataGrid(window.CompanBD, bd.Companies.ToList());
            PrintDataGrid(window.UserBD, bd.Users.ToList());
        }

        public static void ClickDelenedRow()
        {
            Data.ModelCode.CompanyContext bd = new Data.ModelCode.CompanyContext();
            if (MainWindow.UserBD.SelectedItem != null)
            {
                Data.User user = bd.Users.Where(o => o.Id ==((Data.User)MainWindow.UserBD.SelectedItem).Id).FirstOrDefault();
                bd.Users.Remove(user);
                bd.SaveChanges();
                PrintDataGrid(window.CompanBD, bd.Companies.ToList());
                PrintDataGrid(window.UserBD, bd.Users.ToList());

            }
            else if (MainWindow.CompanBD.SelectedItem != null)
            {
                try
                {
                    Data.Company user = bd.Companies.Where(o => o.Id == ((Data.Company)MainWindow.CompanBD.SelectedItem).Id).FirstOrDefault();
                    bd.Companies.Remove(user);
                    bd.SaveChanges();
                    PrintDataGrid(window.CompanBD, bd.Companies.ToList());
                    PrintDataGrid(window.UserBD, bd.Users.ToList());
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Нужно удалить сначала пользователей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Неуспех!");
            }
        }
    }
}
