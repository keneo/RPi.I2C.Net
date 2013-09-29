namespace RPi.I2C.Net.Devices.Mpu6050Driver
{
    public class Mpu6050
    {
        I2CDeviceSocket _i2CDeviceSocket;

        public Mpu6050(II2CBus bus, byte chipAddress = 0x68)
        {
            _i2CDeviceSocket = new I2CDeviceSocket(bus, chipAddress);
        }

        public double ReadTemperature()
        {
            short word = _i2CDeviceSocket.ReadRegisterWord(0x41);
            double temp = word / 340.0 + 36.53;
            return temp;
        }

        public RawReadings ReadRawReadings()
        {
            byte[] buf=_i2CDeviceSocket.ReadRegisters(0x3b,2*7);

            return new RawReadings(buf);
        }


        public void Init()
        {
            _i2CDeviceSocket.WriteCommand(0x6b,0);
        }
    }
}
