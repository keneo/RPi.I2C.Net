﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
            using (var bus = RPi.I2C.Net.I2CBus.Open("/dev/i2c-1"))
			{
                var dev = bus[42];

			    dev.WriteByte(96);
			    byte[] treeBytes = dev.ReadBytes(3);
			}
		}
	}
}
