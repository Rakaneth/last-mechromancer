using GoRogue.Messaging;

namespace last_mechromancer {
    public class MessageBus : GoRogue.Messaging.MessageBus {
        private static MessageBus _instance;
        public static MessageBus Instance {
            get {
                if (_instance == null)
                    _instance = new MessageBus();
                return _instance;
            }
        }

        private MessageBus() : base() {}
    }
}