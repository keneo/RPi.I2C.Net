using System;
using System.IO;

namespace RPi.I2C.Net.Devices.Mpu6050Driver
{
    public class RawReadings
    {
        private readonly byte[] _buf;

        public RawReadings(byte[] buf)
        {
            _buf = buf;
        }

        public Readings Decode()
        {
            MemoryStream ms = new MemoryStream(_buf);
            Func<byte> b = () => (byte)ms.ReadByte();
            Func<short> s = () => (short)(((short)b() << 8) | b());
            Func<double> sg = () => ((double)s())/(short.MaxValue/2) ;

            var ret = new Readings
                          {
                              Acc = new double[3] { sg(), sg(), sg() },
                              Temp = s() / 340.0 + 36.53,
                              Gyro = new double[3] { sg(), sg(), sg() }
                          };

            return ret;
        }
    }
}