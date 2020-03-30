using System;
using SadConsole;
using Console = SadConsole.Console;
using last_mechromancer.Entity;
using last_mechromancer.Entity.Factories;
using last_mechromancer.UI;
using last_mechromancer.Mapping;
using Microsoft.Xna.Framework;
using Game = SadConsole.Game;

namespace last_mechromancer {
    public static class Program {
        static void Main() {
            Game.Create(100, 40);
            Game.OnInitialize = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        static void Init() {
            var map = MapBuilder.Build(MapType.DUNGEON, 100, 100, "test", Color.SlateGray, Color.White);
            var console = new MapConsole(map);
            console.IsVisible = true;
            console.IsFocused = true;
            var player = new PlayerFactory()
                .SetName("Player")
                .Build();
            map.AddEntity(player);
            console.Focus = player;
            player.Position = (25, 25);
            //console.FillWithRandomGarbage();
            Global.CurrentScreen = console;
            

        }
    }
}
