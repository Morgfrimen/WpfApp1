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

        static MainWindow window=(MainWindow)Application.Current.MainWindow;
        public static MainWindow MainWindow { get => window; }

        //тупо удалям все записи в БД и приравниваем то,что есть при нажатии кнопки Изменить
        List<Data.User> users;
        public List<Data.User> Users { get => users; set => users = value; }

        public static void DeletedBD(List<Data.User> gridUsers)
        {
            Models.Data.ModelCode.CompanyContext bd = new Data.ModelCode.CompanyContext();

            List<Data.User> User = bd.Users.ToList();
            List<Data.Company> companies = bd.Companies.ToList();
            bd.Companies.RemoveRange(bd.Companies);
            for (int i = 0; i < bd.Companies.ToList().Count; i++)
            {
                bd.Companies.Add(companies[i]);
            }
            bd.Users.RemoveRange(bd.Users);
            for (int i = 0; i < gridUsers.Count; i++)
            {
                bd.Users.Add(gridUsers[i]);
            }

            bd.SaveChanges();
            PrintDataGrid(window.CompanBD, bd.Companies.ToList());
            PrintDataGrid(window.UserBD, bd.Users.ToList());



        }

        public static void PrintDataGrid(DataGrid grid,List<Data.User> items)
        {
            grid.ItemsSource = items;
            DataGridColumn column = grid.Columns[grid.Columns.Count - 1];
            column.Visibility = Visibility.Collapsed;

        }

        public static void PrintDataGrid(DataGrid grid, List<Data.Company> items)
        {
            grid.ItemsSource = items;
            DataGridColumn column = grid.Columns[grid.Columns.Count - 1];
            column.Visibility = Visibility.Collapsed;

        }



    }
}
