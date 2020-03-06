﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LightX_01.Classes;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace LightX_01.ViewModel
{
    public class GuideWindowViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<string> _testList;
        private GuideData _currentTest;
        private ParametersList _currentList;
        private RunList _currentTestsState;
        private BitmapImage _currentImage;
        private int TestIndex;
        private RelayCommand _nextTestCommand;
        private RelayCommand _previousTestCommand;


        #endregion Fields

        #region Properties

        //public ObservableCollection<string> TestList
        //{
        //    get { return _testList; }
        //    set
        //    {
        //        if (_testList != value)
        //        {
        //            _testList = value;
        //            RaisePropertyChanged(() => TestList);
        //        }
        //    }
        //}

        public GuideData CurrentTest
        {
            get { return _currentTest; }
            set
            {
                _currentTest = value;
                RaisePropertyChanged(() => CurrentTest);
            }
        }

        public ParametersList CurrentList
        {
            get { return _currentList; }
            set
            {
                _currentList = value;
                RaisePropertyChanged(() => CurrentList);
            }
        }

        public RunList CurrentTestsState
        {
            get { return _currentTestsState; }
            set
            {
                if (_currentTestsState != value)
                {
                    _currentTestsState = value;
                    RaisePropertyChanged(() => CurrentTestsState);
                }
            }
        }

        public BitmapImage CurrentImage
        {
            get { return _currentImage; }
            set
            {
                if (value != _currentImage)
                {
                    _currentImage = value;
                    RaisePropertyChanged(() => CurrentImage);
                }
            }
        }
        
        #endregion Properties

        private void FetchCurrentImage()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            image.UriSource = new Uri(CurrentTest.ImagesPath[0], UriKind.Relative);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            CurrentImage = image;
        }

        private void FetchCurrentTestList()
        {

            CurrentTestsState = new RunList(_testList);
            CurrentTestsState[TestIndex].Foreground = Brushes.Black;
            CurrentTestsState[TestIndex].FontWeight = FontWeights.Bold;
        }

        private void FetchAllData()
        {
            CurrentList = new ParametersList(CurrentTest);
            FetchCurrentTestList();
            FetchCurrentImage();
        }

        public GuideWindowViewModel(GuideData test, ObservableCollection<string> testList, int i)
        {
            _testList = testList;
            CurrentTest = test;
            TestIndex = i;
            FetchAllData();
        }
    }
}
