using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Models
{
    public class CommandAddUser:ICommand
    {
        #region Реализация интерфейса
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CommandAddUser(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
        #endregion

        public static void Add(Data.Company company,View.AddUser user)
        {
            Models.LogicalAddUser.AddUserCompany(company, user);
        }

        public static void WindowsAdd()
        {
            Models.LogicalAddUser.UserAdd();
        }

        //форма 2
        public static void ClickButtonAdd(View.AddUserCompany addUserCompany)
        {
            Models.LogicalAddUser.ClickButtonAdd(addUserCompany);
        }
    }

    
}
