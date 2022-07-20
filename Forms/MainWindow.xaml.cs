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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace AccaountManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetSteamPath();
            FillSteamAcsCombobox();
        }


       

       private async void GetSteamPath()
       {
            var json =await Classes.Json.Converts.CheckSteamJsonPath();
            var steamJsonPath = JsonConvert.DeserializeObject<Classes.Json.Structs.SteamPathInfoStruct>(json);
            if(steamJsonPath.Path=="")
            {
                if(MessageBox.Show("Не найден путь к файлу steam.exe. Вы хотите указать его?", "Внимание!", 
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    switch (Classes.Json.Converts.WriteSteamPathInfo(json))
                    {
                        case 0:
                            MessageBox.Show("Данные сохранены","Успех",MessageBoxButton.OK,MessageBoxImage.Information);
                            break;
                        case 1:
                            SteamPanel.Visibility = Visibility.Collapsed;
                            break;
                    }
                    
                }
            }

        }

        private void FillSteamAcsCombobox()
        {
            Classes.SteamAcs.GetAcs();
            SteamAccountsCombobox.ItemsSource = Classes.SteamAcs.dtSteamAcs.DefaultView;
            SteamAccountsCombobox.DisplayMemberPath = Classes.SteamAcs.dtSteamAcs.Columns[0].ToString();
            SteamAccountsCombobox.SelectedValuePath = Classes.SteamAcs.dtSteamAcs.Columns[0].ToString();
            SetComboboxItem();
        }

        private void SetComboboxItem()
        {
            if (SteamAccountsCombobox.Items.Count > 0)
            {
                SteamAccountsCombobox.SelectedItem = SteamAccountsCombobox.Items[0];
            }
        }

        private void ButtonAddSteamAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow addAccountWindow = new AddAccountWindow();
            this.Close();
            addAccountWindow.Show();
        }

        private async void ButtonLoginSteam_Click(object sender, RoutedEventArgs e)
        {
            if (SteamAccountsCombobox.Text != "")
            {
                Classes.SteamAcs.LoginSteam(SteamAccountsCombobox.Text);
            }
            else
            {
                switch (SteamAccountsCombobox.Items.Count)
                {
                    case 0:
                        await ShowSteamErrorMessage("Нет аккаунтов для входа");
                        break;
                    default:
                        await ShowSteamErrorMessage("Не выбран аккаунт для входа");
                        break;
                }
            }
        }

        private async Task ShowSteamErrorMessage(string message)
        {
            SteamError.Text = message;
            SteamError.Visibility = Visibility.Visible;
            await Task.Delay(5000);
            SteamError.Visibility = Visibility.Collapsed;

        }
        private async void ButtonDeleteSteamAccount_Click(object sender, RoutedEventArgs e)
        {
            if (SteamAccountsCombobox.Text != "")
            {
                if (MessageBox.Show($"Вы подтверждаете удаление данных аккаунта [{SteamAccountsCombobox.Text}]?",
                    "Подтверждение пользователя", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Classes.SteamAcs.DeleteAccauntInfo(SteamAccountsCombobox.Text);
                    Classes.SteamAcs.GetAcs();
                    SetComboboxItem();
                }
            }
            else
            {
                switch (SteamAccountsCombobox.Items.Count)
                {
                    case 0:
                        await ShowSteamErrorMessage("Нет аккаунтов для удаления");
                        break;
                    default:
                        await ShowSteamErrorMessage("Не выбран аккаунт для удаления");
                        break;
                }
            }
        }

        private async void ButtonEditSteamAccount_Click(object sender, RoutedEventArgs e)
        {
            if (SteamAccountsCombobox.Text != "")
            {
                var steamAccount = Classes.Json.Converts.ReadSteamAccountInfo(SteamAccountsCombobox.Text);
                EditAccountWindow editAccountWindow = new EditAccountWindow();
                editAccountWindow.SteamLoginTextBox.Text = steamAccount.Login;
                editAccountWindow.SteamNameTextBox.Text = steamAccount.Name;
                editAccountWindow.savedFileName = steamAccount.Name;
                editAccountWindow.SteamPasswordTextBox.Text = steamAccount.Password;
                this.Close();
                editAccountWindow.Show();
            }
            else
            {
                switch (SteamAccountsCombobox.Items.Count)
                {
                    case 0:
                        await ShowSteamErrorMessage("Нет аккаунтов для редактирования ");
                        break;
                    default:
                        await ShowSteamErrorMessage("Не выбран аккаунт для редактирования");
                        break;
                }
            }
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
