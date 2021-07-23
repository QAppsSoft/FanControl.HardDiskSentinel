using System.Collections.Generic;
using FanControl.Plugins;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace FanControl.HardDiskSentinel
{
    public class PluginSensor : IPluginSensor
    {
        private readonly Drive _drive;
        private readonly ManagementScope _scope = new ManagementScope(@"root\wmi");
        private readonly ObjectQuery _query = new ObjectQuery("SELECT * FROM HDSentinel");
        private static readonly float _defaultTemperature = 36f;

        public PluginSensor(Drive drive)
        {
            _drive = drive;
        }

        public string Name => $"{_drive.Model} | {_drive.Serial}";

        public float? Value { get; private set; }

        public void Update()
        {
            using var searcher = new ManagementObjectSearcher(_scope, _query);

            try
            {
                var temperatureValue = searcher.Get()
                    .Cast<ManagementObject>()
                    .Select(DriveInfoReader.ReadFromWmi)
                    // Filter using StartWith because apparently PowerShell trims the last two character from model number in Windows Server 2019
                    .Where(drive => drive.Model.StartsWith(_drive.Model) || drive.Model == _drive.Model)
                    .Where(drive =>  drive.Serial == _drive.Serial)
                    .Select(drive => drive.Temperature)
                    .First();

                Value = float.TryParse(temperatureValue, out var temperature) ? temperature : _defaultTemperature;
            }
            catch (ManagementException exception)
            {
#if DEBUG
                Debug.Print(exception.ToString());
#endif
                Value = _defaultTemperature;
            }
        }

        public string Id => Name;

        public void Dispose()
        {
        }
    }
}