using EOL_GND.Model;
using System.Drawing.Text;
using System.Threading;
using System.Xml.Linq;

namespace EOL_GND.Device
{
    public class ItSeriesDevice : PowerDevice
    {
        private ITechDevice itDevice = new ITechDevice();

        // Dispose 패턴에 사용하는 변수.
        private bool disposedValue = false;
        public ItSeriesDevice(string name) : base(DeviceType.IT_Series, name)
        {
            
        }

        public override string ReadIDN(CancellationToken token)
        {
            //
            return SendCommand("*CLS * ESE * ESE ? *OPC", true, token);

        }

        private string SendCommand(string command, bool checkResponse, CancellationToken token)
        {
            itDevice.WriteLine(command, token);
            if (checkResponse)
            {
                return itDevice.ReadLine(token);
            }

            return null;
        }





















        public override void Connect(CancellationToken token)
        {
            itDevice.Connect(Setting as VisaDeviceSetting, token);
        }

        public override void Disconnect()
        {
            throw new System.NotImplementedException();
        }

        public override object GetMaxValue(object step, string paramName, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override object GetMinValue(object step, string paramName, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override bool GetPowerState(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override double MeasureCurrent(int channel, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override double MeasureVoltage(int channel, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override void PowerOff(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override void PowerOn(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

       

        public override void ReadPower(int channel, out double voltage, out double current, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override string ReadSN(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override void Reset(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override string RunCommand(string command, bool read, int readTimeout, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public override void SetPower(int channel, double voltage, double? current, int queryDelay, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}