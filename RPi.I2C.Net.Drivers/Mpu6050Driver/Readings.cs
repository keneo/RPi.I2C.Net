using System;

namespace RPi.I2C.Net.Drivers.Mpu6050Driver
{
    public class Readings
    {
        public double[] Acc;

        public double AccLength2
        {
            get
            {
                double x = Acc[0];
                double y = Acc[1];
                double z = Acc[2];
                double sum = x*x + y*y + z*z;

                // should be Math.Sqrt now but its buggy
                return sum;
            }
        }

        public double Temp;
        public double[] Gyro;

        public override string ToString()
        {
            return
                base.ToString()
                +
                " Acc: "
                + Acc[0].ToString("+00.00G;-00.00G") + ", "
                + Acc[1].ToString("+00.00G;-00.00G") + ", "
                + Acc[2].ToString("+00.00G;-00.00G") + ", "
                + AccLength2.ToString("sqrt(000.00)G") + ", "
                +
                " Temp: "
                + Temp.ToString("000.000c") + ",\t"
                +
                " Gyro: "
                + Gyro[0].ToString("00.000") + ", "
                + Gyro[1].ToString("00.000") + ", "
                + Gyro[2].ToString("00.000") + ", ";
        }
    }
}