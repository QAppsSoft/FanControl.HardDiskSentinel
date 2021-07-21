namespace FanControl.HardDiskSentinel
{
    public class Drive
    {
        private Drive(string model, string serial, string firmware, string temperature)
        {
            Model = model;
            Serial = serial;
            Firmware = firmware;
            Temperature = temperature;
        }

        public string Model { get; }
        public string Serial { get; }
        public string Firmware { get; }
        public string Temperature { get; }

        public static class Factory
        {
            public static Drive Build(string model, string serial, string firmware, string temperature)
            {
                return new Drive(model, serial, firmware, temperature);
            }
        }
    }
}