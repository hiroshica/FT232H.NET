using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FTD2XX_NET.FTDI;
using MadeInTheUSB.FT232H;
using MadeInTheUSB.FT232H.Components.APA102;
using MadeInTheUSB.FT232H.Components;

namespace TestApp
{
    public class GpioSpiDevice : GpioSpiDeviceBaseClass
    {
        public GpioSpiDevice(MpsseSpiConfig spiConfig) : base(spiConfig)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ft232Device = FT232HDetector.Detect();
            if (ft232Device.Ok)
            {
                System.Console.WriteLine(ft232Device.ToString());
            }
            else
            {
                Environment.Exit(1);
            }
            // MCP3088 and MAX7219 is limited to 10Mhz
            var clockSpeed = MpsseSpiConfig._30Mhz;
            // clockSpeed = MpsseSpiConfig._10Mhz;
            var ft232hGpioSpiDevice = new GpioSpiDevice(MpsseSpiConfig.Make(clockSpeed));

            var spi = ft232hGpioSpiDevice.SPI;
            var gpios = ft232hGpioSpiDevice.GPIO;

            for (var iI = 0; iI < 10; ++iI)
            {
                for (var pin = 0; pin < gpios.MaxGpio; ++pin)
                {
                    gpios.DigitalWrite(pin, PinState.High);
                    Thread.Sleep(50);
                    gpios.DigitalWrite(pin, PinState.Low);
                    Thread.Sleep(50);
                }
            }

            Thread.Sleep(100);
            //GpioSample(gpios, true);
        }
        static void GpioSample(IDigitalWriteRead gpios, bool oneLoopOnly = false)
        {
            var goOn = true;
            const int waitTime = 65;

            while (goOn)
            {

                for (var i = 0; i < gpios.MaxGpio; i++)
                {
                    gpios.DigitalWrite(i, PinState.High);
                    Thread.Sleep(waitTime);
                }
                Thread.Sleep(waitTime);
                for (var i = 0; i < gpios.MaxGpio; i++)
                {
                    gpios.DigitalWrite(i, PinState.Low);
                    Thread.Sleep(waitTime);
                }
                Thread.Sleep(waitTime);
                if (System.Console.KeyAvailable)
                {
                    var k = System.Console.ReadKey();
                    if (k.Key == ConsoleKey.Q)
                        goOn = false;
                }
                if (oneLoopOnly)
                    goOn = false;
            }
        }

    }
}
