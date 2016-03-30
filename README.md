# XpressNetSharp
This is a C# library for communicating with XpressNet compatible model railway command stations.

# Licence.
The License for XpressNetSharp can be found in the root folder called LICENSE.MD.


# Getting started.
The simplist way to get started is to download the library using Nuget and reference it in your project.

A proper example is comming, but for the moment you could try something like this:


            //Open your serialport first (called IoPort in this example)
            var msg = new CmdStnSoftwareVersionReqMessage();
            IResponse resp = null;
            var initialisationDateTime = DateTime.Now;
            var timeout = false;
            do
            {
                resp = await msg.WriteAsync(IoPort, true);
                if (DateTime.Now > initialisationDateTime.AddMilliseconds(1000))
                    timeout = true;

            } while (resp is CmdStnSoftwareVersionResp == false && timeout == false);

            var cmdStnVer = resp as CmdStnSoftwareVersionResp;
            if (timeout != true)
            {
                if (cmdStnVer.Major < 6 && cmdStnVer.Minor < 10)
                {
                    // if it is less than required we should perform an upgrade!
                    await PerformUpdate();
                    Console.Out.WriteLine("Got Command station firmware out of date");
                }
                else
                {
                    Console.Out.WriteLine("Command station firmware is up to date");
                }
            }
            else
            {
                State = ConnectionState.Error;
                throw new Exception("Connection didn't initiate properly (failed to get software version)");
            }

            var initialisationHandler = new InitialisationHandler(IoPort);
            await initialisationHandler.InitialiseElink();
            State = ConnectionState.Connected;





you could also have an interface for a throttle such as:

public class Throttle : IThrottle
    {
        private readonly Stream _stream;

        public Throttle(Stream stream)
        {
            _stream = stream;
        }

        public async Task SetLocoSpeedAndDirection(int locoAddress, int speed, ControllerAdapter.Direction direction, int speedStep)
        {
            var msg = new SetLocoSpeedAndDirection_SpeedSteps128Message((ushort)locoAddress, (byte)speed, XpressNetSharp.Direction.Reverse);
            if (direction == ControllerAdapter.Direction.Forward)
            {
                msg = new SetLocoSpeedAndDirection_SpeedSteps128Message((ushort)locoAddress, (byte)speed, XpressNetSharp.Direction.Forward);
            }
            await msg.WriteAsync(_stream);
        }

        public async Task EmergencyStopLoco(int locoAddress)
        {
            var msg = new EmergencyStopSingleLocoReqMessage((ushort) locoAddress);
            await msg.WriteAsync(_stream);
        }

        public async Task SetFunction_Group1(int locoAddress, ControllerAdapter.FunctionState function0, ControllerAdapter.FunctionState function1, ControllerAdapter.FunctionState function2, ControllerAdapter.FunctionState function3, ControllerAdapter.FunctionState function4)
        {
            var msg = new SetFunctionOperationInstruction_Group1Message(locoAddress, ConvertToXpressNetFunctionState(function0), ConvertToXpressNetFunctionState(function1), ConvertToXpressNetFunctionState(function2), ConvertToXpressNetFunctionState(function3), ConvertToXpressNetFunctionState(function4));
            await msg.WriteAsync(_stream);
        }

        public async Task SetFunction_Group2(int locoAddress, ControllerAdapter.FunctionState function5, ControllerAdapter.FunctionState function6, ControllerAdapter.FunctionState function7, ControllerAdapter.FunctionState function8)
        {
            var msg = new SetFunctionOperationInstruction_Group2Message(locoAddress, ConvertToXpressNetFunctionState(function5), ConvertToXpressNetFunctionState(function6), ConvertToXpressNetFunctionState(function7), ConvertToXpressNetFunctionState(function8));
            await msg.WriteAsync(_stream);
        }

        public async Task SetFunction_Group3(int locoAddress, ControllerAdapter.FunctionState function9, ControllerAdapter.FunctionState function10, ControllerAdapter.FunctionState function11, ControllerAdapter.FunctionState function12)
        {
            var msg = new SetFunctionOperationInstruction_Group3Message(locoAddress, ConvertToXpressNetFunctionState(function9), ConvertToXpressNetFunctionState(function10), ConvertToXpressNetFunctionState(function11), ConvertToXpressNetFunctionState(function12));
            await msg.WriteAsync(_stream);
        }

        public async Task SetFunction_Group4(int locoAddress, ControllerAdapter.FunctionState function13, ControllerAdapter.FunctionState function14, ControllerAdapter.FunctionState function15, ControllerAdapter.FunctionState function16, ControllerAdapter.FunctionState function17, ControllerAdapter.FunctionState function18, ControllerAdapter.FunctionState function19, ControllerAdapter.FunctionState function20)
        {
            var msg = new SetFunctionOperationInstruction_Group4Message(locoAddress, ConvertToXpressNetFunctionState(function13), ConvertToXpressNetFunctionState(function14), ConvertToXpressNetFunctionState(function15), ConvertToXpressNetFunctionState(function16), ConvertToXpressNetFunctionState(function17), ConvertToXpressNetFunctionState(function18), ConvertToXpressNetFunctionState(function19), ConvertToXpressNetFunctionState(function20));
            await msg.WriteAsync(_stream);
        }

        public async Task SetFunction_Group5(int locoAddress, ControllerAdapter.FunctionState function21, ControllerAdapter.FunctionState function22, ControllerAdapter.FunctionState function23, ControllerAdapter.FunctionState function24, ControllerAdapter.FunctionState function25, ControllerAdapter.FunctionState function26, ControllerAdapter.FunctionState function27, ControllerAdapter.FunctionState function28)
        {
            var msg = new SetFunctionOperationInstruction_Group5Message(locoAddress, ConvertToXpressNetFunctionState(function21), ConvertToXpressNetFunctionState(function22), ConvertToXpressNetFunctionState(function23), ConvertToXpressNetFunctionState(function24), ConvertToXpressNetFunctionState(function25), ConvertToXpressNetFunctionState(function26), ConvertToXpressNetFunctionState(function27), ConvertToXpressNetFunctionState(function28));
            await msg.WriteAsync(_stream);
        }

        private XpressNetSharp.FunctionState ConvertToXpressNetFunctionState(ControllerAdapter.FunctionState function)
        {
            var convertedfunction = (function == ControllerAdapter.FunctionState.On) ? XpressNetSharp.FunctionState.On : XpressNetSharp.FunctionState.Off;
            return convertedfunction;
        }
    }