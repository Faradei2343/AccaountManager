using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Timers;
using System.Windows.Navigation;
using System.ComponentModel.DataAnnotations;
using AccaountManager.Classes.Json.Structs;

namespace AccaountManager
{
    /// <summary>
    /// Логика взаимодействия для EditAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        public AddAccountWindow()
        {
            InitializeComponent();
        }

        private async void ButtonSaveAccount_Click(object sender, RoutedEventArgs e)
        {
            SteamAccount steamAcc = new SteamAccount(SteamNameTextBox.Text, 
            SteamLoginTextBox.Text, SteamPasswordTextBox.Text);
            var context = new ValidationContext(steamAcc);
            var results=new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            if (Validator.TryValidateObject(steamAcc, context, results, true))
            {
                if (Classes.Json.Converts.WriteSteamAccauntInfo(steamAcc) == 0)
                {
                    MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else
            {
                string errorString=string.Empty;
                foreach(var error in results)
                {
                    errorString = error.ErrorMessage+"\r\n";
                }
                await ShowErrorText(errorString);
            }
        }

        private void MarkNullTextBocks()
        {
            if (SteamNameTextBox.Text == "")
            {
                SteamNameTextBox.Background = Brushes.Crimson;
                SteamNameTextBox.Opacity = 0.7;
            }
            if (SteamPasswordTextBox.Text == "")
            {
                SteamPasswordTextBox.Background = Brushes.Crimson;
                SteamPasswordTextBox.Opacity = 0.7;
            }
            if (SteamLoginTextBox.Text == "")
            {
                SteamLoginTextBox.Background = Brushes.Crimson;
                SteamLoginTextBox.Opacity = 0.7;
            }
        }
        private void UnMarkNullTextBocks()
        {
            if (SteamNameTextBox.Text == "")
            {
                SteamNameTextBox.Background = Brushes.White;
                SteamNameTextBox.Opacity = 1;
            }
            if (SteamPasswordTextBox.Text == "")
            {
                SteamPasswordTextBox.Background = Brushes.White;
                SteamPasswordTextBox.Opacity = 1;
            }
            if (SteamLoginTextBox.Text == "")
            {
                SteamLoginTextBox.Background = Brushes.White;
                SteamLoginTextBox.Opacity = 1;
            }
        }
        private async Task ShowErrorText(string message)
        {
            MarkNullTextBocks();
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
            await  Task.Delay(5000);
            ErrorTextBlock.Visibility = Visibility.Collapsed;
            UnMarkNullTextBocks();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TopDockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
