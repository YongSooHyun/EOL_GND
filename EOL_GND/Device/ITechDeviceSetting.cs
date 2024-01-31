using EOL_GND.Model;
using System.Xml.Serialization;

namespace EOL_GND.Device
{
    [XmlInclude(typeof(WaveformGeneratorDeviceSetting))]
    [XmlInclude(typeof(DmmDeviceSetting))]
    [XmlInclude(typeof(OscopeDeviceSetting))]
    [XmlInclude(typeof(PowerDeviceSetting))]
    [XmlInclude(typeof(AmplifierDeviceSetting))]
    public class ITechDeviceSetting : DeviceSetting
    {
        public string Address { get; internal set; }
        public int IOTimeout { get; internal set; }
    }
}