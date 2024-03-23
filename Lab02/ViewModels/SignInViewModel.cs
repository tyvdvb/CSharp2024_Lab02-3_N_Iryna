using Lab02.Exceptions;
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
using System.Windows.Input;

namespace Lab02.ViewModels
{
    internal class SignInViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool _isEnabled = true;
        private bool _isProcessing = false;
        private RelayCommand<object> _proceedCommand;
        private Action _goToPersonInfoView;

        public SignInViewModel(Action goToPersonInfoView)
        {
            _goToPersonInfoView = goToPersonInfoView;
        }

        #endregion
        #region Properties

        private string _firstName;
        private string _lastName;
        private string _email;





        private DateTime? _dateOfBirth;

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                NotifyPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }


        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set
            {
                if (_isProcessing != value)
                {
                    _isProcessing = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(IsEnabled)); 
                }
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
                   !String.IsNullOrWhiteSpace(Email) &&
                   DateOfBirth != null;
        }






        internal async void Proceed()
        {



            IsEnabled = false;
            IsProcessing = true;

            try
            {
                await Task.Run(() => { Thread.Sleep(2000); });



                if (DateOfBirth == null)
                {
                    throw new EmptyFieldException("Please specify the birth date.");
                }

                if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Email))
                {
                    throw new EmptyFieldException("Please fill in all required fields.");
                }

                var person = new Person(FirstName, LastName, Email, (DateTime)DateOfBirth);


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
                IsProcessing = false;
                CommandManager.InvalidateRequerySuggested();

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
