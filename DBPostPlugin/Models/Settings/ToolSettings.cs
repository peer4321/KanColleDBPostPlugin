using MetroTrilithon.Serialization;
using System.Runtime.CompilerServices;

namespace DBPostPlugin.Models.Settings
{
    public static class ToolSettings
    {
        public static SerializableProperty<bool> SendDb { get; }
            = new SerializableProperty<bool>(GetKey(), Providers.Local, false) { AutoSave = true };

        public static SerializableProperty<string> DbAccessKey { get; }
            = new SerializableProperty<string>(GetKey(), Providers.Local, null) { AutoSave = true };

        public static SerializableProperty<bool> NotifyLog { get; }
            = new SerializableProperty<bool>(GetKey(), Providers.Local, false) { AutoSave = true };

        private static string GetKey([CallerMemberName] string propertyName = "")
        {
            return nameof(ToolSettings) + "." + propertyName;
        }
    }
}
