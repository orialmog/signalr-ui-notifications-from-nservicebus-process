using System;
using System.Threading;
using NServiceBus;
using SomeLongRunningProcess.TypeContracts;
using UINotifications.TypeContracts;

namespace SomeLongRunningProcess.ServiceHost
{
    public class SomeLongRunningProcessCommandHandler :
        IHandleMessages<UIElementUpdate>,
        IHandleMessages<SomeLongRunningProcessCommand> 
    {
        public IBus Bus { get; set; }

        public void Handle(UIElementUpdate message)
        {
            Bus.Publish(new BusyEvent()
            {
                UIElementId = message.UIElementId
            });
        }

        public void Handle(SomeLongRunningProcessCommand message)
        {

            var time = new Random().Next(3000, 5000);

            Console.WriteLine("SomeLongRunningProcess -> Started.");
            Console.WriteLine($"SomeLongRunningProcess -> Sleep {time} .");
            Thread.Sleep(time);
            Console.WriteLine("SomeLongRunningProcess -> Finished.");

            Bus.Publish(new ReadyEvent()
            {
                UIElementId = message.UIElementId,
                Data = time
            });
        }

    }


}