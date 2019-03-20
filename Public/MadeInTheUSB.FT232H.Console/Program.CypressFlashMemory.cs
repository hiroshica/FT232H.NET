using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FTD2XX_NET.FTDI;
using MadeInTheUSB.FT232H.Components.APA102;
using MadeInTheUSB.FT232H.Components;

namespace MadeInTheUSB.FT232H.Console
{
    partial class Program
    {
        public enum Manufacturers : int
        {
            Unknown = 0,
            Cypress = 1,
        };

        public enum CYPRESS_S25FLXXX_DEVICE_ID
        {
            Undefined = 0,
            S25FL116K_2Mb = 0x14,
            S25FL132K_4Mb = 0x15,
            S25FL164K_8Mb = 0x16,
            S25FL127S_16MB = 8216
        };
        public enum CYPRESS_SECTOR_ARCHITECTURE
        {
            _Uniform256KBSectors = 0,
            _4kBParameterSectorsWithUniform64kBSectors = 1,
            Undefined = 128
        };
        public enum CYPRESS_FAMILIY_ID
        {
            FL_S_Family = 0x80,
            Undefined = 0,
        };

        static void CypressFlashMemorySample(ISPI spi)
        {
            var flash = new CypressFlashMemory(spi);
            flash.ReadIdentification();
            System.Console.WriteLine(flash.GetDeviceInfo());
            var _64kAbdcString = PerformanceHelper.Get64kStringAbcd();
            var _64kAbdcBuffer = PerformanceHelper.GetAsciiBuffer(_64kAbdcString).ToList();

            flash.FormatFlashFAT(1);
            var r = flash.WritePages(CypressFlashMemory.BLOCK_SIZE, _64kAbdcBuffer);

            for (var _64kBlock = 1; _64kBlock < flash.MaxBlock; _64kBlock++)
            {
                var buffer = new List<byte>();
                if(flash.ReadPages(_64kBlock* CypressFlashMemory.BLOCK_SIZE, CypressFlashMemory.BLOCK_SIZE, buffer))
                {
                    var _64kAbdcString2 = PerformanceHelper.AsciiBufferToString(buffer.ToArray());
                    if(_64kAbdcString2 != _64kAbdcString)
                    {
                        System.Console.WriteLine("Error reading page");
                    }
                }
            }

            return;

            //const int EEPROM_READ_IDENTIFICATION = 0x9F;
            //byte[] buffer = new byte[18];

            //if (spi.Ok(spi.Query(new byte[] { EEPROM_READ_IDENTIFICATION }, buffer)))
            //{

            //    var manufacturer = (Manufacturers)buffer[0];
            //    var deviceID = (CYPRESS_S25FLXXX_DEVICE_ID)((buffer[1] << 8) + buffer[2]);
            //    var sectorArchitecture = (CYPRESS_SECTOR_ARCHITECTURE)buffer[4];
            //    var familyID = (CYPRESS_FAMILIY_ID)buffer[5];
            //    var packageModel = string.Empty;
            //    packageModel += ((char)buffer[6]).ToString();
            //    packageModel += ((char)buffer[7]).ToString();

            //    System.Console.WriteLine($"FLASH Memory manufacturer:{manufacturer}, deviceID:{deviceID}, sectorArchitecture:{sectorArchitecture}, familyID:{familyID}, packageModel:{packageModel}");
            //}
            /*
            if (spi.Ok(spi.Write(new byte[] { 1, 2, 3, 4 })))
            {
                var response = new byte[] { 0, 0, 0, 0 };
                if (spi.Ok(spi.Read(response)))
                {

                }
            }*/
        }
    }
}
