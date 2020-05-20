using System;
using System.Collections.Generic;

namespace Reconstitution {
    public class MessageRegister : IMessage, IDisposable {

        private List<MessageDelegate> temp;
        private Dictionary<int, List<MessageDelegate>> handlers;

        public MessageRegister() {
            temp = new List<MessageDelegate>();
            handlers = new Dictionary<int, List<MessageDelegate>>();

            OnInit();
        }
        
        public void OnInit() {
            Clear();
        }

        public void Dispose() {
            Clear();
        }

        public void Clear() {
            temp.Clear();
            handlers.Clear();
        }

        public void Dispatcher(int id, IBody body = null) {
            List<MessageDelegate> events = null;
            if (handlers.TryGetValue(id, out events)) {
                temp.Clear();
                temp.AddRange(events);
                foreach (MessageDelegate messageDelegate in temp) {
                    messageDelegate(body);
                }
            }
        }

        public void Register(int id, MessageDelegate handler) {
            List<MessageDelegate> events = null;
            if (!handlers.TryGetValue(id, out events)) {
                events = new List<MessageDelegate>();
                handlers.Add(id, events);
            }
            if (!events.Contains(handler)) {
                events.Add(handler);
            }
        }

        public void Unregister(int id, MessageDelegate handler) {
            List<MessageDelegate> events = null;
            if (handlers.TryGetValue(id, out events)) {
                if (events.Contains(handler)) {
                    events.Remove(handler);
                }
            }
        }
    }
}