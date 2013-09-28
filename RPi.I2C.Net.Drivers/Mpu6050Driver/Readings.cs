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

                return sum;// *9.81 / 280000000;
            }
        }

        public double Temp;
        public short[] Gyro;

        public override string ToString()
        {
            return
                base.ToString()
                +
                " Acc: "
                + Acc[0].ToString("+00.00;-00.00") + ", "
                + Acc[1].ToString("+00.00;-00.00") + ", "
                + Acc[2].ToString("+00.00;-00.00") + ", "
                + AccLength2.ToString("+000.0;-000.0") + ", "
                +
                " Temp: "
                + Temp.ToString("000.000") + ",\t"
                +
                " Gyro: "
                + Gyro[0].ToString("00000") + ", "
                + Gyro[1].ToString("00000") + ", "
                + Gyro[2].ToString("00000") + ", ";
        }
    }
}