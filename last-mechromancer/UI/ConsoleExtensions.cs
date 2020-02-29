using SadConsole;

namespace last_mechromancer.UI {
    public static class ConsoleExtensions {
        public static void Border(this Console cons, string caption=null) {
            var wholeBox = new Microsoft.Xna.Framework.Rectangle(0, 0, cons.Width, cons.Height);
            var cell = new Cell(cons.DefaultForeground, cons.DefaultBackground);
            cons.DrawBox(wholeBox, cell, connectedLineStyle: CellSurface.ConnectedLineThick);
            if (caption != null) {
                var cx = (cons.Width - caption.Length) / 2;
                cons.Print(cx, 0, caption);
            }
        }
    }
}