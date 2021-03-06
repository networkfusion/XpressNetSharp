﻿using System;

namespace XpressNetSharp
{
    /// <summary>
    /// Sets the speed and direction of the locomotive (14 speed steps).
    /// </summary>
    public class SetLocoSpeedAndDirection_SpeedSteps14Message : Packet
    {
        /// <summary>
        /// Sets the speed and direction of the locomotive.
        /// </summary>
        /// <param name="address">Address of the locomotive (0 - 9999)</param>
        /// <param name="speed">Speed of the locomotive (0 - 13)</param>
        /// <param name="direction">Direction of the locomotive</param>
        public SetLocoSpeedAndDirection_SpeedSteps14Message(int address, byte speed, Direction direction) : base(PacketHeaderType.LocomotiveFunction) 
        {
            Payload.Add(Convert.ToByte(PacketIdentifier.LocomotiveFunctionRequest.SetLocoSpeedAndDirection_SpeedSteps14));

            if (address >= XpressNetConstants.MIN_LOCO_ADDRESS && address <= XpressNetConstants.MAX_LOCO_ADDRESS)
            {
                byte[] data = new byte[2];
                ValueConverter.LocoAddress(address, out data[0], out data[1]);
                Payload.AddRange(data);
            }
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");

            if (speed > 0 || speed < 13)
                Payload.Add(Convert.ToByte((byte)direction | speed));
            else
                throw new XpressNetProtocolViolationException("Number out of bounds");
        }
    }
}
