using Calculator_WPF.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Calculator_WPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Display _display;

        private string _operation;
        private double _firstNumber;

        private string _firstArgument;
        public string FirstArgument
        {
            get
            {
                return _firstArgument;
            }
            set
            {
                _firstArgument = value;
                OnPropertyChanged("FirstArgument");
            }
        }
        
        public string Number
        {
            get
            {
                return _display.Content;
            }
            set
            {
                _display.AddCharacter(value);
                OnPropertyChanged("Number");
            }
        }

        #endregion

        #region Constructor

        public ViewModel()
        {
            _display = new Display();
            Number = _display.Content;
        }

        #endregion

        #region Methods
        private void calculate()
        {
            double result = Calc.Calculate(_firstNumber, double.Parse(Number), _operation);
            _display.Reset();
            OnPropertyChanged("Number");

            if (Convert.ToString(result).Count() < 10)
            {
                Number = result.ToString();
            }
            else
            {
                Number = result.ToString("0.0000e+0");
            }

            FirstArgument = "";
            _operation = "";
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
                            Number = Convert.ToString(o);

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
                            _display.Erase();
                            OnPropertyChanged("Number");

                            if (!string.IsNullOrEmpty(FirstArgument) && Number == "0")
                            {
                                FirstArgument = "";
                            }
                        }
                    );
                }

                return _eraseCommand;
            }
        }

        private ICommand _changeSignCommand;
        public ICommand ChangeSign
        {
            get
            {
                if (_changeSignCommand == null)
                {
                    _changeSignCommand = new RelayCommand<object>(
                        o => 
                        {
                            _display.ChangeSign();
                            OnPropertyChanged("Number");

                        }
                    );
                }

                return _changeSignCommand;
            }
        }

        public ICommand _percentBtnClickCommand;
        public ICommand PercentBtnClick
        {
            get
            {
                if (_percentBtnClickCommand == null)
                {
                    _percentBtnClickCommand = new RelayCommand<object>(
                        o =>
                        {

                            var tmp = double.Parse(Number);
                            _display.Reset();
                            Number = (tmp * 0.01).ToString();

                            if (!string.IsNullOrEmpty(FirstArgument))
                            {
                                calculate();
                            }
                        }
                    );
                }

                return _percentBtnClickCommand;
            }
        }

        private ICommand _operationCommand;
        public ICommand Operation
        {
            get
            {
                if (_operationCommand == null)
                {
                    _operationCommand = new RelayCommand<object>(
                        o =>
                        {
                            if (!string.IsNullOrEmpty(FirstArgument))
                            {
                                calculate();
                            }

                            _firstNumber = double.Parse(Number);
                            _operation = Convert.ToString(o);
                            FirstArgument = _firstNumber.ToString() + _operation;
                            _display.Reset();
                        }    
                    );
                }

                return _operationCommand;
            }
        }

        private ICommand _equalOperationCommand;
        public ICommand EqualOperation
        {
            get
            {
                if (_equalOperationCommand == null)
                {
                    _equalOperationCommand = new RelayCommand<object>(
                        o =>
                        {
                            if (!string.IsNullOrEmpty(FirstArgument))
                            {
                                calculate();
                                _display.Reset();
                            }
                        }
                    );
                }

                return _equalOperationCommand;
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
