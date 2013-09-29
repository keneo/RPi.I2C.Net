using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPi.I2C.Net;
using RPi.I2C.Net.Devices.Mpu6050Driver;

namespace SampleApp.Mpu6050
{
    class Program
    {
        static void Main(string[] args)
        {
            int sleep = args.Length == 0 ? 0 : int.Parse(args[0]);

            using (var bus = I2CBus.Open("/dev/i2c-1"))
            {
                var mpu6050 = new Mpu6050Api(bus);

                Console.Write("Initialization...");
                mpu6050.Init();
                Console.WriteLine("OK");

                Console.Write("Calibration...");
                var baseReadings = mpu6050.ReadSensors(1000);
                Console.WriteLine("OK");
                Console.WriteLine("Base readings: " + baseReadings);
                Console.WriteLine();
                Console.WriteLine("Streaming... (Press CTRL+C to stop)");


                Readings accumulator = new Readings();

                for (;;)
                {
                    var r = mpu6050.ReadSensors(100).DiffrenceTo(baseReadings);

                    accumulator.Accumulate(r);

                    DateTime now = DateTime.Now;

                    Console.WriteLine(now + "." + now.Millisecond.ToString("000") + ": " + accumulator);
                }
            }
        }
    }
}
