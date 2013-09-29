using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPi.I2C.Net;
using RPi.I2C.Net.Devices.Mpu6050Driver;


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

			    sensor.Init();

                for (; ; )
                {
                    int probes = 100;

                    Readings[] accumulator = new Readings[probes];

                    for (int i = 0; i < probes; i++)
                    {
                        accumulator[i] = sensor.ReadRawReadings().Decode();

                        if (sleep > 0)
                            System.Threading.Thread.Sleep(sleep);
                    }

                    Readings r = new Readings()
                                     {
                                         Acc = new double[3]{accumulator.Average(a=>a.Acc[0]),accumulator.Average(a=>a.Acc[1]),accumulator.Average(a=>a.Acc[2]),},
                                         Temp = accumulator.Average(a=>a.Temp),
                                         Gyro = new double[3]{accumulator.Average(a=>a.Gyro[0]),accumulator.Average(a=>a.Gyro[1]),accumulator.Average(a=>a.Gyro[2]),}
                                     };

                    //double temp = sensor.ReadTemperature();



                    DateTime now = DateTime.Now;

                    Console.WriteLine(now + "." + now.Millisecond.ToString("000") + ": " + r.ToString());
                    //Console.WriteLine(t);

                }
			}
		}
	}
}
