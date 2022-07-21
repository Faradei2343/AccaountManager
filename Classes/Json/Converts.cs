using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Windows;
using AccaountManager.Classes.Json.Structs;
using System.Reflection;

namespace AccaountManager.Classes.Json
{
    public class Converts
    {
        static public async Task<string> ReadAppSettings()
        {
            var json = string.Empty;
            using (var fileStream = File.OpenRead("../../LauncherPaths/SteamPathInfo.json"))
            {
                using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                {
                    json = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    return json;
                }
            }
        }

        static public int WriteAppSettings(Settings settings)
        {
            try
            {
                var jsonOut = JsonConvert.SerializeObject(settings);
                File.WriteAllText("../../LauncherPaths/SteamPathInfo.json", jsonOut);
                if (settings.StartWithWindows == true)
                {
                    RegistryKey rkApp = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    rkApp.SetValue("AccauntManager", Assembly.GetExecutingAssembly().Location);
                }
                else
                {
                    RegistryKey rkApp = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                    rkApp.DeleteValue("AccauntManager");
                }
                   
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return 99;
            }
        }

        static public int WriteSteamAccauntInfo(SteamAccount steamAccount)
        {
            try 
            {
                var jsonOut= JsonConvert.SerializeObject(steamAccount);
                File.WriteAllText($"../../Accounts/Steam/{steamAccount.Name}.json", jsonOut);
                return 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return 99;
            }
        }

        static public Structs.SteamAccount ReadSteamAccountInfo(string fileName)
        {
                var json = string.Empty;
                using (var fileStream = File.OpenRead($"../../Accounts/Steam/{fileName}.json"))
                {
                    using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                    {
                        json = streamReader.ReadToEnd();
                    }
                }
                var steamAccount = JsonConvert.DeserializeObject<Structs.SteamAccount>(json);
                return steamAccount;
        }

        static public Structs.Settings ReadSteamPath()
        {
            var json = string.Empty;
            using (var fileStream = File.OpenRead("../../LauncherPaths/SteamPathInfo.json"))
            {
                using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                {
                    json = streamReader.ReadToEnd();
                }
            }
            var steamPath=JsonConvert.DeserializeObject<Structs.Settings>(json);
            return steamPath;
        }
    }
}
