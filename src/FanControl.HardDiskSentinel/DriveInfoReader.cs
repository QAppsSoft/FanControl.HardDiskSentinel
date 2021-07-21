using System.Management;
using System.Management.Automation;

namespace FanControl.HardDiskSentinel
{
    public static class DriveInfoReader
    {
        public static Drive ReadBasic(ManagementObject managementObject)
        {
            return Drive.Factory.Build(
                managementObject.GetPropertyValue("ModelID").ToString().Trim(),
                managementObject.GetPropertyValue("SerialNumber").ToString().Trim(),
                managementObject.GetPropertyValue("FirmwareRevision").ToString().Trim(),
                managementObject.GetPropertyValue("TemperatureC").ToString().Trim());
        }

        public static Drive Read(PSObject managementObject)
        {
            return Drive.Factory.Build(managementObject.Properties["Model"].Value.ToString(),
                managementObject.Properties["SerialNumber"].Value.ToString(),
                managementObject.Properties["FirmwareVersion"].Value.ToString(),
                36.ToString());
        }
    }
}