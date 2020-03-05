using System;
using SadConsole;
using Console = SadConsole.Console;
using last_mechromancer.Entity;
using last_mechromancer.Entity.Factories;
using last_mechromancer.UI;

namespace last_mechromancer {
    public static class Program {
        static void Main() {
            Game.Create(80, 25);
            Game.OnInitialize = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        static void Init() {
            var console = new Console(80, 25);
            //console.FillWithRandomGarbage();
            Global.CurrentScreen = console;
            var msgs = new MessageConsole(20, 15);
            for (int i=0; i<20; i++)
                MessageBus.Instance.Send(new GameLogMessage($"Message {Utils.Decorate(i.ToString(), "121,107,91")}"));
            console.Children.Add(msgs);
        }
    }
}
