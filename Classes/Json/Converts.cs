using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Windows;

namespace AccaountManager.Classes.Json
{
    public class Converts
    {
        static public async Task<string> CheckSteamJsonPath()
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

        static public int WriteSteamPathInfo(string json)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                var steamJsonPath = JsonConvert.DeserializeObject<Structs.SteamPathInfoStruct>(json);
                if (openFileDialog.ShowDialog() == true)
                {
                    steamJsonPath.Path = openFileDialog.FileName;
                    var jsonOut = JsonConvert.SerializeObject(steamJsonPath);
                    File.WriteAllText("../../LauncherPaths/SteamPathInfo.json", jsonOut);
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return 99;
            }
        }

        static public int WriteSteamAccauntInfo(string name,string login,string password)
        {
            try 
            {
                var steamAccount = new Classes.Json.Structs.SteamAccount();
                steamAccount.Name = name;
                steamAccount.Password= password;
                steamAccount.Login  = login;
                var jsonOut= JsonConvert.SerializeObject(steamAccount);
                File.WriteAllText($"../../Accounts/Steam/{name}.json", jsonOut);
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

        static public Structs.SteamPathInfoStruct ReadSteamPath()
        {
            var json = string.Empty;
            using (var fileStream = File.OpenRead("../../LauncherPaths/SteamPathInfo.json"))
            {
                using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                {
                    json = streamReader.ReadToEnd();
                }
            }
            var steamPath=JsonConvert.DeserializeObject<Structs.SteamPathInfoStruct>(json);
            return steamPath;
        }
    }
}
