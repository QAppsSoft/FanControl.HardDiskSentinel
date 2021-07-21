using FanControl.Plugins;
using System.Linq;
using System.Management.Automation;

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
            var ps = PowerShell.Create();

            ps.AddScript("Get-PhysicalDisk | where {($_.CannotPoolReason -match 'In a Pool')}");

            var disks = ps.Invoke()
                .Select(DriveInfoReader.Read)
                .ToArray();

            var sensors = disks.Select(d => new PluginSensor(d));

            container.TempSensors.AddRange(sensors);
        }
    }
}