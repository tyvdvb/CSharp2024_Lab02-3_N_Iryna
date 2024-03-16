using Lab02.Models;
using Lab02.Tools;
using Lab1.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab02.ViewModels
{
    internal class PersonInfoViewModel
    {
        #region Fields

        private Action _goBack;
        private RelayCommand<object> _returnCommand;
        private RelayCommand<object> _closeCommand;
        private Person _person = PersonManager.Current;


        #endregion

        #region Properties
        public PersonInfoViewModel(Action a)
        {
            _goBack = a;
            CheckBirthday();

        }


        public Person Person
        {
            get { return _person; }
        }

        public string FirstName
        {
            get { return _person.FirstName; }
        }

        public string LastName
        {
            get { return _person.LastName; }
        }

        public string Email
        {
            get { return _person.Email; }
        }

        public string DateOfBirth
        {
            get { return _person.BirthDate.ToString("d"); }
        }

        public bool IsAdult
        {
            get { return _person.IsAdult; }
        }

        public bool IsBirthday
        {
            get { return _person.IsBirthday; }
        }

        public string SunSign
        {
            get { return _person.SunSign; }
        }

        public string ChineseSign
        {
            get { return _person.ChineseSign; }
        }


        public RelayCommand<object> CloseCommand => _closeCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
        public RelayCommand<object> ReturnCommand => _returnCommand ??= new RelayCommand<object>(_ => _goBack?.Invoke());

        #endregion

        #region Methods

        private void CheckBirthday()
        {
            if (IsBirthday)
            {
                MessageBox.Show("Happy birthday, bud!");
            }
        }

        #endregion
    }
}
