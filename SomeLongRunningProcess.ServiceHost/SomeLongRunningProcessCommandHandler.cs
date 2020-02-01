using System;
using System.Threading;
using NServiceBus;
using SomeLongRunningProcess.TypeContracts;
using UINotifications.TypeContracts;

namespace SomeLongRunningProcess.ServiceHost
{
    public class SomeLongRunningProcessCommandHandler :
        IHandleMessages<ElementUpdate>,
        IHandleMessages<SomeLongRunningProcessCommand> 
    {
        public IBus Bus { get; set; }

        public void Handle(ElementUpdate message)
        {
            Bus.Publish(new BusyEvent()
            {
                UIElementId = message.UIElementId
            });
        }

        public void Handle(SomeLongRunningProcessCommand message)
        {
            Console.WriteLine("SomeLongRunningProcess -> Started.");

            var time = new Random().Next(3000, 10000);
            Console.WriteLine($"SomeLongRunningProcess -> Sleep {time} .");
            Thread.Sleep(time);


            Console.WriteLine("SomeLongRunningProcess -> Finished.");

            Bus.Publish(new ReadyEvent()
            {
                UIElementId = message.UIElementId
            });
        }

    }


}