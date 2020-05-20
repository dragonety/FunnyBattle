using UnityEngine;
using UnityEditor;

namespace BattleEvent {

	public class BaseEventMsg {
        internal static object EventType;
        public EventType msgType;

		public object[] paramObjects = null;

		public BaseEventMsg() {

		}

		public BaseEventMsg(EventType type, params object[] inParams) {
			msgType = type;
			paramObjects = inParams;
		}

		public void SetEventMsg(EventType type, params object[] inParams) {
			msgType = type;
			paramObjects = inParams;
		}

	}

}