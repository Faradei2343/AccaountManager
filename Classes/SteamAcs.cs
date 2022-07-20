using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace AccaountManager.Classes
{
    public class SteamAcs
    {
        static public DataTable dtSteamAcs = new DataTable();
        
        static public void GetAcs()
        {
            dtSteamAcs.Clear();
            dtSteamAcs.Columns.Clear();
            dtSteamAcs.Columns.Add();
            foreach(var file in Directory.GetFiles("../../Accounts/Steam"))
            {
                var fileName= Path.GetFileName(file);
                dtSteamAcs.Rows.Add(fileName.Substring(0,fileName.Length-5));
            }
        }

        static public int LoginSteam(string fileName)
        {
            var steamPath = Json.Converts.ReadSteamPath();
            var steamAcInfo = Json.Converts.ReadSteamAccountInfo(fileName);
            Process taskKill = new Process();
            taskKill.StartInfo.FileName = "cmd.exe";
            taskKill.StartInfo.Arguments = "taskkill /f /IM steam.exe";
            taskKill.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            taskKill.Start();
            taskKill.WaitForExit(200);
            Process process = new Process();
            process.StartInfo.FileName=$"{steamPath.Path}";
            process.StartInfo.Arguments = $" -login {steamAcInfo.Login} {steamAcInfo.Password}";
            process.Start();
            return 0;
        }

        static public void DeleteAccauntInfo(string fileName)
        {
            File.Delete($"../../Accounts/Steam/{fileName}.json");
        }
    }
}
