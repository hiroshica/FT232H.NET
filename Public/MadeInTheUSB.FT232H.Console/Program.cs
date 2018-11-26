using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FTD2XX_NET.FTDI;
using MadeInTheUSB.FT232H.Components.APA102;
      
namespace MadeInTheUSB.FT232H.Console
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var ft232Device = FT232HDetector.Detect();
            if(ft232Device.Ok)
                System.Console.WriteLine(ft232Device.ToString());

            var ft232hGpioSpiDevice = new GpioSpiDevice(MpsseSpiConfig.GetDefault());
            var gpios               = ft232hGpioSpiDevice.GPIO;
            GpioSample(gpios, true);

            var spi                 = ft232hGpioSpiDevice.SPI;

            //CypressFlashMemorySample(spi);

            Api102RgbLedSample(spi);
            GpioSample(gpios, true);
        }
    }
}
