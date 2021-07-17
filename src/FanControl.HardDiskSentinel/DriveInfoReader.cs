using System.Management;

namespace FanControl.HardDiskSentinel
{
    public static class DriveInfoReader
    {
        public static Drive Read(ManagementObject managementObject)
        {
            var drive = new Drive();

            // Functions separated to make it easier to read
            drive.InitValues(
                managementObject.GetPropertyValue("ModelID").ToString().Trim(),
                managementObject.GetPropertyValue("SerialNumber").ToString().Trim(),
                managementObject.GetPropertyValue("FirmwareRevision").ToString().Trim(),
                managementObject.GetPropertyValue("Interface").ToString().Trim(),
                managementObject.GetPropertyValue("PowerOnTime").ToString().Trim(),
                managementObject.GetPropertyValue("TemperatureC").ToString().Trim()
            );

            drive.PopulateData(
                managementObject.GetPropertyValue("PowerOnHours").ToString().Trim(),
                managementObject.GetPropertyValue("StartStopCount").ToString().Trim(),
                managementObject.GetPropertyValue("BadSectorCount").ToString().Trim(),
                managementObject.GetPropertyValue("WeakSectorCount").ToString().Trim(),
                managementObject.GetPropertyValue("SpinRetryCount").ToString().Trim(),
                managementObject.GetPropertyValue("CommunicationIssueCount").ToString().Trim(),
                managementObject.GetPropertyValue("StatusCode").ToString().Trim(),
                managementObject.GetPropertyValue("TRIMStatus").ToString().Trim()
            );

            drive.PopulateReport(
                managementObject.GetPropertyValue("Report").ToString().Trim(),
                managementObject.GetPropertyValue("Health").ToString().Trim(),
                managementObject.GetPropertyValue("Performance").ToString().Trim(),
                managementObject.GetPropertyValue("LifetimeWriteMB").ToString().Trim(),
                managementObject.GetPropertyValue("SMART").ToString().Trim()
            );

            return drive;
        }

        public static DriveBasic ReadBasic(ManagementObject managementObject)
        {
            var drive = new DriveBasic();

            drive.InitValues(
                managementObject.GetPropertyValue("ModelID").ToString().Trim(),
                managementObject.GetPropertyValue("SerialNumber").ToString().Trim(),
                managementObject.GetPropertyValue("FirmwareRevision").ToString().Trim(),
                managementObject.GetPropertyValue("Interface").ToString().Trim(),
                managementObject.GetPropertyValue("PowerOnTime").ToString().Trim(),
                managementObject.GetPropertyValue("TemperatureC").ToString().Trim()
            );

            return drive;
        }
    }
}