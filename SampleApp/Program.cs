using System;
using System.Collections.Generic;
using System.Text;
using RPi.I2C.Net;
using RPi.I2C.Net.Drivers;
using RPi.I2C.Net.Drivers.Mpu6050Driver;

namespace SampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
		    int sleep = args.Length == 0 ? 0 : int.Parse(args[0]);

			using (var bus = I2CBus.Open("/dev/i2c-1"))
			{
                var sensor = new Mpu6050(bus);
                for (int i = 0; ;i++ )
                {
                    var r = sensor.ReadRawReadings().Decode();

                    //double temp = sensor.ReadTemperature();

                    DateTime now = DateTime.Now;

                    if (i % 10 == 0)
                    {
                        Console.WriteLine(now + "." + now.Millisecond.ToString("000") + ": " + r.ToString());
                        //Console.WriteLine(t);
                    }

                    if (sleep > 0)
                        System.Threading.Thread.Sleep(sleep);
                }
			}
		}
	}
}
