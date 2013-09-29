namespace RPi.I2C.Net.Devices.Mpu6050Driver
{
    public class Readings
    {
        public double[] Acc = new double[3];

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
        public double[] Gyro = new double[3];

        public override string ToString()
        {
            return
                "Readings:"
                +
                " Acc: "
                + Acc[0].ToString("+00.00;-00.00") + ", "
                + Acc[1].ToString("+00.00;-00.00") + ", "
                + Acc[2].ToString("+00.00;-00.00") + ", "
                + AccLength2.ToString("sqrt(000.000)") + ", "
                +
                " Temp: "
                + Temp.ToString("000.000c") + ", "
                +
                " Gyro: "
                + Gyro[0].ToString("+00.000;-00.000") + ", "
                + Gyro[1].ToString("+00.000;-00.000") + ", "
                + Gyro[2].ToString("+00.000;-00.000") + ", ";
        }

        public Readings DiffrenceTo(Readings baseReadings)
        {
            var ret = new Readings()
                          {
                              Acc = new double[3] { this.Acc[0] - baseReadings.Acc[0], this.Acc[1] - baseReadings.Acc[1], this.Acc[2] - baseReadings.Acc[2], }, // this only makes sense if device is not rotated
                              Temp = this.Temp - baseReadings.Temp,
                              Gyro = new double[3] { this.Gyro[0] - baseReadings.Gyro[0], this.Gyro[1] - baseReadings.Gyro[1], this.Gyro[2] - baseReadings.Gyro[2], }
                          };

            return ret;
        }

        public void Accumulate(Readings toAdd)
        {
            this.Acc[0] += toAdd.Acc[0];
            this.Acc[1] += toAdd.Acc[1];
            this.Acc[2] += toAdd.Acc[2];

            //this.Temp += toAdd.Temp; //no point

            this.Temp = toAdd.Temp;

            this.Gyro[0] += toAdd.Gyro[0];
            this.Gyro[1] += toAdd.Gyro[1];
            this.Gyro[2] += toAdd.Gyro[2];
        }
    }
}