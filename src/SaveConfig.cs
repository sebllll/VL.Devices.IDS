using Microsoft.Extensions.Logging;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using VL.Devices.IDS.Advanced;

namespace VL.Devices.IDS
{
    [ProcessNode(Name = "ConfigSaver")]
    public class SaveConfig : IConfiguration
    {
        private readonly ILogger logger;

        string? userSet;
        
        IConfiguration? input;

        FreshConfig? output;

        public SaveConfig([Pin(Visibility = Model.PinVisibility.Hidden)] NodeContext nodeContext)
        {
            logger = nodeContext.GetLogger();
        }

        [return: Pin(Name = "Output")]
        public IConfiguration Update(IConfiguration input, string userSet)
        {
            if (input != this.input || userSet != this.userSet)
            {
                this.input = input;
                this.userSet = userSet;
                output = new FreshConfig(this);
            }
            return output!;
        }
    }
}

//namespace VL.Devices.IDS
//{
//    [ProcessNode(Name = "ConfigSaver")]
//    public class SaveConfig : IDisposable
//    {
//        private readonly ILogger logger;
//        private readonly SerialDisposable serialDisposable = new();

//        public SaveConfig([Pin(Visibility = Model.PinVisibility.Hidden)] NodeContext nodeContext)
//        {
//            logger = nodeContext.GetLogger();
//        }

//        public void Update(VideoIn? input, bool save, string userSet = "UserSet0")
//        {
//            if (input is null)
//                return;

//            if (save)
//            {
                
//                serialDisposable.Disposable = input.AcquisitionStarted.Take(1)
//                    .Subscribe(a =>
//                    {
//                        try
//                        {
//                            //a.NodeMap.
//                            //vision_apiPINVOKE.SWIGPendingException.Pending is false > exception
//                            a.NodeMap.FindNodeEnumeration("UserSetSelector").SetCurrentEntry(userSet);
//                            a.NodeMap.FindNodeCommand("UserSetSave").Execute();
//                            a.NodeMap.FindNodeCommand("UserSetSave").WaitUntilDone();
//                        }
//                        catch (Exception e)
//                        {
//                            logger.LogError(e, $"Failed to safe camera configuration into " + userSet);
//                        }
//                    });
//            }
//        }

//        public void Dispose()
//        {
//            serialDisposable.Dispose();
//        }
//    }
//}
