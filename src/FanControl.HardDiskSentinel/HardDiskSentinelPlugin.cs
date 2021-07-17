using FanControl.Plugins;
using System.Linq;
using System.Management;

namespace FanControl.HardDiskSentinel
{
    public class HardDiskSentinelPlugin : IPlugin
    {
        public string Name => "Hard Disk Sentinel Plugin";

        public void Close()
        {
        }

        public void Initialize()
        {
        }

        public void Load(IPluginSensorsContainer container)
        {
            // Get Drives from WMI
            var scope = new ManagementScope(@"root\wmi");
            var query = new ObjectQuery("SELECT * FROM HDSentinel");

            using var searcher = new ManagementObjectSearcher(scope, query);

            var drives = searcher.Get()
                .Cast<ManagementObject>()
                .Select(DriveInfoReader.Read);

            var sensors = drives.Select(drive => new PluginSensor(drive));

            container.TempSensors.AddRange(sensors);
        }
    }
}