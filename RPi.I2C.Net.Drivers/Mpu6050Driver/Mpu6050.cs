namespace RPi.I2C.Net.Drivers.Mpu6050Driver
{
    public class Mpu6050
    {
        I2CDevice _i2CDevice;

        public Mpu6050(II2CBus bus, byte chipAddress = 0x68)
        {
            _i2CDevice = new I2CDevice(bus, chipAddress);
        }

        public double ReadTemperature()
        {
            short word = _i2CDevice.ReadRegisterWord(0x41);
            double temp = word / 340.0 + 36.53;
            return temp;
        }

        public RawReadings ReadRawReadings()
        {
            byte[] buf=_i2CDevice.ReadRegisters(0x3b,2*7);

            return new RawReadings(buf);
        }

        
    }
}
