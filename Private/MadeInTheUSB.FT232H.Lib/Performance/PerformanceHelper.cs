using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInTheUSB.FT232H
{
    public class PerformanceHelper : IDisposable
    {
        public static string Get64kString(string _4CharString)
        {
            var s = "";
            for (var i = 0; i < 1024 * 16; i++)
                s += _4CharString;
            return s;
        }
        public static byte[] GetAsciiBuffer(string s)
        {
            return Encoding.ASCII.GetBytes(s);
        }
        public static string AsciiBufferToString(byte [] buffer)
        {
            return Encoding.ASCII.GetString(buffer);
        }
        public static string Get64kStringFred()
        {
            return Get64kString("FRED");
        }
        public static string Get64kStringAbcd()
        {
            return Get64kString("ABCD");
        }
        public static string Get64kString0123()
        {
            return Get64kString("0123");
        }
        public static string Get4kStringABCD()
        {
            var s = "";
            for (var i = 0; i < 1024; i++)
                s += "ABCD";
            return s;
        }
        Stopwatch _stopwatch;
        int _byteCounter = 0;
        public PerformanceHelper Start()
        {
            _stopwatch = Stopwatch.StartNew();
            return this;
        }
        public void AddByte(int byteCount)
        {
            _byteCounter += byteCount;
        }
        public void Stop()
        {
            _stopwatch.Stop();
        }
        public string GetResultInfo()
        {
            var bytePerSecond = _byteCounter / (_stopwatch.ElapsedMilliseconds / 1000.0);
            var mbBytePerSecond = _byteCounter / (_stopwatch.ElapsedMilliseconds / 1000.0) / 1024 / 1024;
            return $"{mbBytePerSecond:0.00} Mb/S b/S:{bytePerSecond:0.00}, time:{_stopwatch.ElapsedMilliseconds / 1000.0:0.00}";
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
