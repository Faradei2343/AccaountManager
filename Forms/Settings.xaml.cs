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
using Microsoft.Win32;
using AccaountManager.Classes.Json;
using Newtonsoft.Json;

namespace AccaountManager.Forms
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }


        private async Task<Classes.Json.Structs.Settings> GetSettings()
        {
            var json = await Converts.ReadAppSettings();
            var steamJsonPath = JsonConvert.DeserializeObject<Classes.Json.Structs.Settings>(json);
            return steamJsonPath;
        }

        private async void ButtonSteamPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
            {
                var json = await Converts.ReadAppSettings();
                var steamJsonPath = JsonConvert.DeserializeObject<Classes.Json.Structs.Settings>(json);
                steamJsonPath.Path = openFileDialog.FileName;
                switch (Converts.WriteAppSettings(steamJsonPath))
                {
                    case 0:
                        await GetSettings();
                        MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
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
        private void CheckBoxStartWithWin_Checked(object sender, RoutedEventArgs e)
        {
            ChangeStartUpStatus(CheckBoxStartWithWin);
        }

        private void CheckBoxStartWithWin_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeStartUpStatus(CheckBoxStartWithWin);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var settings=await GetSettings();
            SteamPathBox.Text = settings.Path;
            CheckBoxStartWithWin.IsChecked = settings.StartWithWindows;
        }

        private async void ChangeStartUpStatus(CheckBox checkBox)
        {
            var json = await Converts.ReadAppSettings();
            var steamJsonPath = JsonConvert.DeserializeObject<Classes.Json.Structs.Settings>(json);
            steamJsonPath.StartWithWindows = checkBox.IsChecked.Value;
            switch (Converts.WriteAppSettings(steamJsonPath))
            {
                case 0:
                    await GetSettings();
                    MessageBox.Show("Данные сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }
    }
}
