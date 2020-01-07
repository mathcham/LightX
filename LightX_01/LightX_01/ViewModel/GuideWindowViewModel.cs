﻿using GalaSoft.MvvmLight.Command;
using LightX_01.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LightX_01.ViewModel
{
    public class GuideWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private PatientData patientData;
        private int TestIndex = 0;

        public RelayCommand NextTestCommand { get; private set; }

        public GuideWindowViewModel(PatientData passed_patientData)
        {
            patientData = passed_patientData;
            string jsonPatienData = JsonConvert.SerializeObject(patientData);
            MessageBox.Show(jsonPatienData);
            this.NextTestCommand = new RelayCommand(this.NextTest);
        }

        public string CurrentTest
        {
            get
            {
                return patientData.TestList[TestIndex];
            }
            //set????? pour le propretyChanged setup
        }

        public void NextTest()
        {
            TestIndex++;
            OnPropertyChange("CurrentTest");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
