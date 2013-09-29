using System.Linq;

namespace RPi.I2C.Net.Devices.Mpu6050Driver
{
    public class Mpu6050Api
    {
        I2CDeviceSocket _i2CDeviceSocket;

        public Mpu6050Api(II2CBus bus, byte chipAddress = 0x68)
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

        public Readings ReadSensors(int samples)
        {
            Readings[] accumulator = new Readings[samples];

            for (int i = 0; i < samples; i++)
            {
                accumulator[i] = ReadRawReadings().Decode();

                System.Threading.Thread.Sleep(1);
            }

            Readings r = new Readings()
            {
                Acc = new double[3] { accumulator.Average(a => a.Acc[0]), accumulator.Average(a => a.Acc[1]), accumulator.Average(a => a.Acc[2]), },
                Temp = accumulator.Average(a => a.Temp),
                Gyro = new double[3] { accumulator.Average(a => a.Gyro[0]), accumulator.Average(a => a.Gyro[1]), accumulator.Average(a => a.Gyro[2]), }
            };

            return r;
        }
    }
}
