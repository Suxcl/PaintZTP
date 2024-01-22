using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PaintZTP.ViewModel;

namespace PaintZTP.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((MainViewModel)DataContext).Image_MouseLeftButtonDown.Execute(e);
        }

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           // ((MainViewModel)DataContext).ScaleSelectedShape.Execute(e); // wywołanie RelayCommanda obsługującego kliknięcie na obrazie prawym przyciskiem myszy
        }
    }
}
