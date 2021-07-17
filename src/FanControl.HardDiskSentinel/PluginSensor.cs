using System.Linq;
using System.Management;
using FanControl.Plugins;

namespace FanControl.HardDiskSentinel
{
    public class PluginSensor : IPluginSensor
    {
        private readonly Drive _drive;

        public PluginSensor(Drive drive)
        {
            _drive = drive;
        }

        public string Name => $"{_drive.Model} | {_drive.Serial}";

        public float? Value { get; private set; }

        public void Update()
        {
            var scope = new ManagementScope(@"root\wmi");
            var query = new ObjectQuery("SELECT * FROM HDSentinel");

            using var searcher = new ManagementObjectSearcher(scope, query);

            var items = searcher.Get();

            var temperature = items.Cast<ManagementObject>()
                .Select(DriveInfoReader.Read)
                .Where(drive => drive.Model == _drive.Model &&
                                drive.Serial == _drive.Serial)
                .Select(drive => drive.Temperature)
                .Select(float.Parse).FirstOrDefault();

            Value = temperature;
        }

        public string Id => Name;

        public void Dispose()
        {
        }
    }
}