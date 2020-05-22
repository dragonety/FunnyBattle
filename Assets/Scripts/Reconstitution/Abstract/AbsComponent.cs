using System.Collections.Generic;
using UnityEngine;
using UnityEditor.VersionControl;

namespace Reconstitution {

    public abstract class AbsComponet : IComponent {

        private List<UpdateDelegate> updateList;
        private List<UpdateDelegate> fixedUpdateList;
        private Dictionary<int, List<MessageDelegate>> messageDict;

        public AbsComponet() {
            updateList = new List<UpdateDelegate>();
            fixedUpdateList = new List<UpdateDelegate>();
            messageDict = new Dictionary<int, List<MessageDelegate>>();
        }

        public IEntity Entity { get; set; }

        public abstract void OnInit();

        public virtual void OnRemove() {

            foreach(UpdateDelegate updateDelegate in updateList) {
                Entity.updateRegister.UnregisterUpdate(updateDelegate);
            }

            foreach(UpdateDelegate fixedUpdateDelegate in fixedUpdateList) {
                Entity.updateRegister.UnregisterFixedUpdate(fixedUpdateDelegate);
            }

            foreach( KeyValuePair<int, List<MessageDelegate>> kv in messageDict) {
                foreach (MessageDelegate messageDelegate in kv.Value) {
                    Entity.messageRegister.Unregister(kv.Key, messageDelegate);
                }                
            }

            updateList.Clear();
            fixedUpdateList.Clear();
            messageDict.Clear();

        }

        public void RegisterUpdate(UpdateDelegate updateDelegate) {
            if (!updateList.Contains(updateDelegate)) {
                updateList.Add(updateDelegate);
                Entity.updateRegister.RegisterUpdate(updateDelegate);
            }
        }

        public void UnregisterUpdate(UpdateDelegate updateDelegate) {
            if (updateList.Contains(updateDelegate)) {
                updateList.Remove(updateDelegate);
                Entity.updateRegister.UnregisterUpdate(updateDelegate);
            }
        }

        public void RegisterFixedUpdate(UpdateDelegate fixedUpdateDelegate) {
            if (!fixedUpdateList.Contains(fixedUpdateDelegate)) {
                fixedUpdateList.Add(fixedUpdateDelegate);
                Entity.updateRegister.RegisterFixedUpdate(fixedUpdateDelegate);
            }
        }

        public void UnregisterFixedUpdate(UpdateDelegate fixedUpdateDelegate) {
            if (fixedUpdateList.Contains(fixedUpdateDelegate)) {
                fixedUpdateList.Remove(fixedUpdateDelegate);
                Entity.updateRegister.UnregisterFixedUpdate(fixedUpdateDelegate);
            }
        }

        public void RegisterMessage(int id, MessageDelegate messageDelegate) {
            List<MessageDelegate> messageList = null;
            if (!messageDict.TryGetValue(id, out messageList)) {
                messageList = new List<MessageDelegate>();
                messageDict.Add(id, messageList);
            }

            if (!messageList.Contains(messageDelegate)) {
                messageList.Add(messageDelegate);
                Entity.messageRegister.Register(id, messageDelegate);
            }
        }

        public void UnregisterMessage(int id, MessageDelegate messageDelegate) {
            List<MessageDelegate> messageList = null;
            if (messageDict.TryGetValue(id, out messageList)) {
                if (messageList.Contains(messageDelegate)) {
                    messageList.Remove(messageDelegate);
                    Entity.messageRegister.Unregister(id, messageDelegate);
                }
            }
        }

    }

}