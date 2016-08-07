using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator_WPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        private int _symbolsOnDisplay;
        private bool _isDot;

        private string _operation;
        public string Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
                OnPropertyChanged("Operation");
            }
        }

        private string _number;
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        #endregion

        #region Constructor

        public ViewModel()
        {
            Number = 0.ToString();
            _symbolsOnDisplay = 0;
            _isDot = false;
        }

        #endregion

        #region Commands

        private ICommand _clickNumberButtonCommand;
        public ICommand ClickNumberButton
        {
            get
            {
                if (_clickNumberButtonCommand == null)
                {
                    _clickNumberButtonCommand = new RelayCommand<object>(
                        o =>
                        {
                            if (_symbolsOnDisplay < 9)
                            {
                                var argument = Convert.ToInt32(o);
                                int tmpNumber = int.Parse(Number) * 10;

                                Number = (tmpNumber + argument).ToString();

                                _symbolsOnDisplay++;
                            }

                        }, null
                    );
                }

                return _clickNumberButtonCommand;
            }
        }

        private ICommand _eraseCommand;
        public ICommand Erase
        {
            get
            {
                if (_eraseCommand == null)
                {
                    _eraseCommand = new RelayCommand<object>(
                        o =>
                        {
                            if (!string.IsNullOrEmpty(Number) && !Number.Equals("0"))
                            {
                                Number = Number.Remove(Number.Count() - 1);

                                if (Number.Count() == 0)
                                {
                                    Number = 0.ToString();
                                }

                                _symbolsOnDisplay--;
                            }
                        }   
                    );
                }

                return _eraseCommand;
            }
        }

        #endregion


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
