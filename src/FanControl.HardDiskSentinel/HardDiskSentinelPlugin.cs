using System;
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
                .Select(DriveInfoReader.ReadFromPowerShell)
                .ToArray();

            var sensors = disks.Select(BuildPluginSensor);

            container.TempSensors.AddRange(sensors);
        }

        private static PluginSensor BuildPluginSensor(Drive drive)
        {
            return new PluginSensor(drive);
        }
    }
}