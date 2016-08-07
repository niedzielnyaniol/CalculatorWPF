using System.Linq;

namespace Calculator_WPF.Model
{
    public class Display
    {
        private int _symbolsOnDisplay;
        private bool _isDot;

        public string Content;

        public Display()
        {
            Reset();
        }

        public void AddCharacter(string character)
        {
            if (_symbolsOnDisplay < 9)
            {
                if (Content == "0")
                {
                    Content = Content.Remove(_symbolsOnDisplay - 1);
                    _symbolsOnDisplay = 0;
                }
                

                if (character == ",")
                {
                    if (!_isDot)
                    {
                        _isDot = true;
                        Content += character;
                        _symbolsOnDisplay++;
                    }
                }
                else
                {
                    Content += character;
                    _symbolsOnDisplay++;
                }
            }
        }

        public void Erase()
        {
            if (!string.IsNullOrEmpty(Content) && !Content.Equals("0"))
            {
                if (Content.ElementAt(_symbolsOnDisplay - 1) == ',')
                {
                    _isDot = false;
                }

                Content = Content.Remove(_symbolsOnDisplay - 1);
                _symbolsOnDisplay--;

                if (Content.Count() == 0)
                {
                    Content = 0.ToString();
                    _symbolsOnDisplay = 1;
                }

            }
        }

        public void ChangeSign()
        {
            if (Content != "0")
            {
                string tmp = Content;

                if (tmp.ElementAt(0) == '-')
                {
                    Content = tmp.Remove(0, 1);
                    _symbolsOnDisplay--;
                }
                else
                {
                    Content = "-" + tmp;
                    _symbolsOnDisplay++;
                }
            }
        }

        public void Reset()
        {
            Content = "0";
            _symbolsOnDisplay = 1;
            _isDot = false;
        }
    }
}
