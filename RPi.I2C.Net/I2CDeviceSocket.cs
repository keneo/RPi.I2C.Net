namespace RPi.I2C.Net
{
    public class I2CDeviceSocket
    {
        private readonly II2CBus _bus;
        private readonly byte _chipAddress;

        public I2CDeviceSocket(II2CBus bus, byte chipAddress)
        {
            _bus = bus;
            _chipAddress = chipAddress;
        }

        public void WriteByte(byte b)
        {
            _bus.WriteByte(_chipAddress,b);
        }

        public void WriteBytes(byte[] bytes)
        {
            _bus.WriteBytes(_chipAddress, bytes);
        }

        public void WriteCommand(byte command, byte data)
        {
            _bus.WriteCommand(_chipAddress, command, data);
        }

        public void WriteCommand(byte command, byte data1, byte data2)
        {
            _bus.WriteCommand(_chipAddress, command, data1,data2);
        }

        public void WriteCommand(byte command, ushort data)
        {
            _bus.WriteCommand(_chipAddress, command, data);
        }

        public byte[] ReadBytes(int count)
        {
            return _bus.ReadBytes(_chipAddress, count);
        }

        public byte[] ReadRegisters(byte registersAddress, int count)
        {
            WriteByte(registersAddress);
            return ReadBytes(count);
        }

        public short ReadRegisterWord(byte registerAddress)
        {
            byte[] b= ReadRegisters(registerAddress, 2);
            return (short)((b[0] << 8) | b[1]);
        }
    }
}