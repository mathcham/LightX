﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightX.Classes
{
    public class BoolStringClass : BaseClass
    {
        #region Fields

        private string _text;
        private Tests _value;
        private bool _isSelected;

        #endregion Fields

        #region Properties

        public string Text
        {
            get { return _text; }
        }

        public Tests Value
        {
            get { return _value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion Properties



        // Constructor

        public BoolStringClass(string text, Tests value, bool isSelected = true)
        {
            _text = text;
            _value = value;
            _isSelected = isSelected;
        }
    }
}
