using MetroTrilithon.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPostPlugin.Models.Settings
{
    public static class Providers
    {
        public static string RoamingFilePath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DBPostPlugin", "Settings.xaml");
        public static string LocalFilePath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "DBPostPlugin", "Settings.xaml");
        public static ISerializationProvider Roming { get; } = new FileSettingsProvider(RoamingFilePath);
        public static ISerializationProvider Local { get; } = new FileSettingsProvider(LocalFilePath);
    }
}
