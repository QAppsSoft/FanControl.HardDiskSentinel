using System.Linq;
using System.Management;
using FanControl.Plugins;

namespace FanControl.HardDiskSentinel
{
    public class PluginSensor : IPluginSensor
    {
        private readonly Drive _drive;
        private readonly ManagementScope _scope = new ManagementScope(@"root\wmi");
        private readonly ObjectQuery _query = new ObjectQuery("SELECT * FROM HDSentinel");

        public PluginSensor(Drive drive)
        {
            _drive = drive;
        }

        public string Name => $"{_drive.Model} | {_drive.Serial}";

        public float? Value { get; private set; }

        public void Update()
        {
            
            using var searcher = new ManagementObjectSearcher(_scope, _query);

            var items = searcher.Get();

            var temperature = items.Cast<ManagementObject>()
                .Select(DriveInfoReader.ReadBasic)
                .Where(drive => drive.Model == _drive.Model && drive.Serial == _drive.Serial)
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