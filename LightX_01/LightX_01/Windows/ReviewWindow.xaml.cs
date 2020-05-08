﻿using LightX_01.Classes;
using LightX_01.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LightX_01
{
    public partial class ReviewWindow : Window
    {
        private readonly ReviewWindowViewModel _reviewWindowViewModel;

        public string Comment
        {
            get { return _reviewWindowViewModel.CurrentComment; }
        }

        public ObservableCollection<bool> SelectedImages
        {
            get
            {
                ObservableCollection<bool> selectedImages = new ObservableCollection<bool>();
                foreach (ReviewImage reviewImage in _reviewWindowViewModel.ReviewImages)
                    selectedImages.Add(reviewImage.IsSelected);
                return selectedImages;
            }
        }

        public ReviewWindow(List<string> images, string comment)
        {
            _reviewWindowViewModel = new ReviewWindowViewModel(images, comment);
            InitializeComponent();
            DataContext = _reviewWindowViewModel;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                _reviewWindowViewModel.SelectImageEvent(sender as Image);
            else
                _reviewWindowViewModel.ActiveImageEvent(sender as Image);
        }
    }
}
