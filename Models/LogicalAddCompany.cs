using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Models
{
    public class LogicalAddCompany : INotifyPropertyChanged
    {
        #region Реализация интерфейса + метод обновления
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        static Data.Company _company;
        public static  Data.Company Company { get => _company; set => _company = value; }


        /// <summary>
        /// Добавляет уже введенную компанию и запускает новую форму с пользователем
        /// </summary>
        /// <param name="company">Модель компании</param>
        /// <param name="comp">Объект окна</param>
        public static void AddCompanyBD(Models.Data.Company company,View.AddComp comp)
        {
            if (comp.Name.Text == "" | comp.Name.Text == " ")
            {
                MessageBox.Show(messageBoxText: "Название компании не может быть пустым", caption: "Исключение"
                    , MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            //создание компании
            company.Name = comp.Name.Text;
            company.Status = comp.Combo.Text;
            Company = company;
            
            Models.LogicalAddUser.ShowWimdowUser(company);
            comp.Close();
        }
    }
}
