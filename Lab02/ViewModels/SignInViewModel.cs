using Lab02.Models;
using Lab02.Tools;
using Lab1.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab02.ViewModels
{
    internal class SignInViewModel
    {
        #region Fields

        private bool _isEnabled = true;
        private RelayCommand<object> _proceedCommand;
        private Action _goToPersonInfoView;

        public SignInViewModel(Action goToPersonInfoView)
        {
            _goToPersonInfoView = goToPersonInfoView;
        }

        #endregion
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Today;

        public bool IsEnabled 
        { 
            get 
            { 
                return _isEnabled; 
            }
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ??= new RelayCommand<object>(_ => Proceed(), CanExecute);

        #endregion

        #region Methods
        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(FirstName) &&
                   !String.IsNullOrWhiteSpace(LastName) &&
                   !String.IsNullOrWhiteSpace(Email);
        }

        internal async void Proceed()
        {
            IsEnabled = false;

            try
            {
                await Task.Run(() => { Thread.Sleep(2000); });

                if (DateOfBirth > DateTime.Today || DateOfBirth < DateTime.Today.AddYears(-135))
                {
                    MessageBox.Show("Invalid birthday.");
                    IsEnabled = true;
                    return;
                }

                var person = new Person(FirstName, LastName, Email, DateOfBirth);

                if (person.isNotFilled())
                {
                    IsEnabled = true;
                    return;
                }

                PersonManager.Current = person;

                await Task.Run(() => { Thread.Sleep(2000); });


                _goToPersonInfoView?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                IsEnabled = true;
            }

        }

        #endregion

        #region PropepryChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
