using System;

namespace FanControl.HardDiskSentinel
{
    public class DriveBasic
    {
        public DriveBasic()
        {
            Initialized = false;
            Model = "";
            Serial = "";
            FirmwareRev = "";
            InterfaceType = "";
            PowerOnTime = "";
            Temperature = "";
        }

        public bool Initialized { get; set; }

        public string Model { get; set; }
        public string Serial { get; set; }
        public string FirmwareRev { get; set; }
        public string InterfaceType { get; set; }
        public string PowerOnTime { get; set; }
        public string Temperature { get; set; }

        public void InitValues(string model, string serial, string firmware, string interfaceType, string powerOnTime, string temperature)
        {
            Initialized = true;
            Model = model;
            Serial = serial;
            FirmwareRev = firmware;
            InterfaceType = interfaceType;
            PowerOnTime = powerOnTime;
            Temperature = temperature;
        }
    }

    public class Drive : DriveBasic
    {
        public Drive()
        {
            Initialized = false;
            Model = "";
            Serial = "";
            FirmwareRev = "";
            InterfaceType = "";
            PowerOnTime = "";
            PowerOnHours = "";
            StartStopCount = "";
            BadSectorCount = "";
            WeakSectorCount = "";
            SpinRetryCount = "";
            CommunicationIssueCount = "";
            StatusCode = "";
            TrimStatus = "";
            Report = "";
            Health = "";
            Performance = "";
            LifetimeWrites = "";
            SmartDATA = "";
        }
        
        public void PopulateData(string powerOnHrs, string startStop, string badSector, string weakSector, string spinRetry, string commIssue, string statusCode, string trim)
        {
            PowerOnHours = powerOnHrs;
            StartStopCount = startStop;
            BadSectorCount = badSector;
            WeakSectorCount = weakSector;
            SpinRetryCount = spinRetry;
            CommunicationIssueCount = commIssue;
            StatusCode = statusCode;
            TrimStatus = trim;
        }

        public void PopulateReport(string report, string health, string perf, string writes, string smart)
        {
            Report = report;
            Health = health;
            Performance = perf;
            LifetimeWrites = writes;
            SmartDATA = smart;
        }

        public void PrintReport()
        {
            Console.WriteLine("Model: " + Model);
            Console.WriteLine("Serial: " + Serial);
            Console.WriteLine("Health: " + Health);
            Console.WriteLine("Power On Time: " + PowerOnTime);
            Console.WriteLine("Power On Hours: " + PowerOnHours);
            Console.WriteLine("Total Writes: " + LifetimeWrites);
            Console.WriteLine("Performance: " + Performance);
        }

        public string PowerOnHours { get; set; }
        public string StartStopCount { get; set; }
        public string BadSectorCount { get; set; }
        public string WeakSectorCount { get; set; }
        public string SpinRetryCount { get; set; }
        public string CommunicationIssueCount { get; set; }
        public string StatusCode { get; set; }
        public string TrimStatus { get; set; }

        public string Report { get; set; }
        public string Health { get; set; }
        public string Performance { get; set; }
        public string LifetimeWrites { get; set; }

        public string SmartDATA { get; set; }
    }
}