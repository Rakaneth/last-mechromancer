using SadConsole;
using System;
using System.Collections.Generic;
using Console = SadConsole.Console;
using GoRogue.Messaging;


namespace last_mechromancer.UI {

    public class GameLogMessage {
        public string Message {get;}

        public GameLogMessage(string msg) {
            Message = msg;
        }
    }
    public class MessageConsole : Console, ISubscriber<GameLogMessage>, IDisposable {
        private List<string> _messages = new List<string>();
        private const string caption = "Messages";

        public MessageConsole(int width, int height) : base(width, height) {
            MessageBus.Instance.RegisterSubscriber(this);
            UsePrintProcessor = true;
        }
        void ISubscriber<GameLogMessage>.Handle(GameLogMessage message) {
            _messages.Add(message.Message);
            IsDirty = true;
        }

        public override void Draw(TimeSpan timeElapsed) {
            this.Border("Messages");
            var start = _messages.Count - 1;
            var end = Math.Max(0, _messages.Count - Height + 2);
            for (int i=start; i>=end; i--)
                this.PrettyPrint(1, _messages.Count - i, _messages[i]);
            base.Draw(timeElapsed);
            
        }

        public void Dispose() {
            MessageBus.Instance.UnregisterSubscriber(this);
        }
    }
}