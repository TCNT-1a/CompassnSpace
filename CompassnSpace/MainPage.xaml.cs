﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Graphics;
using System;

namespace CompassnSpace
{
    public partial class MainPage : ContentPage
    {
        private CompassDrawable _compassDrawable;

        public MainPage()
        {
            InitializeComponent();
            _compassDrawable = new CompassDrawable();
            CompassGraphicsView.Drawable = _compassDrawable;
            StartCompass();
        }

        private void StartCompass()
        {
            Compass.Default.ReadingChanged += Compass_ReadingChanged;
            Compass.Default.Start(SensorSpeed.UI);
        }

        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var heading = e.Reading.HeadingMagneticNorth;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _compassDrawable.Heading = (float)heading;
                CompassGraphicsView.Invalidate();
                DirectionLabel.Text = $"Direction: {heading}°";
            });
        }
    }
}