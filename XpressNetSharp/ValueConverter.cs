using System;
using System.Linq;

namespace XpressNetSharp
{
    public static class ValueConverter
    {
        public static void LocoAddress(int number, out byte highAddressByte, out byte lowAddressByte)
        {
            var byteArray = BitConverter.GetBytes(number);

            lowAddressByte = byteArray[0];
            highAddressByte = byteArray[1];

            if (number >= 100 && number <= XpressNetConstants.MAX_LOCO_ADDRESS)
            {
                highAddressByte = (byte)(highAddressByte | 0xC0);
            }

        }

        public static ushort ToUShort(byte[] byteArray)
        {
            //we have to reverse the byte array otherwise it reads wrong...
            return BitConverter.ToUInt16(byteArray.Reverse().ToArray(), 0);
        }

        public static void FromUShort(ushort number, out byte highByte, out byte lowByte)
        {
            var byteArray = BitConverter.GetBytes(number);
            lowByte = byteArray[0];
            highByte = byteArray[1];
        }
    }
}
