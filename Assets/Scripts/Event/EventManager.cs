using System.Collections.Generic;


namespace BattleEvent {

	public class EventManager : Singleton<EventManager> {

		public delegate void EventHandler(BaseEventMsg msg);

		private Dictionary<int, EventHandler> eventHandlerMap = new Dictionary<int, EventHandler>();

		public void RegisterEvent(EventType type, EventHandler eventHandler) {

			if (eventHandlerMap == null) {
				eventHandlerMap = new Dictionary<int, EventHandler>();
			}

			int eventTypeId = (int)type;

			if (eventHandlerMap.ContainsKey(eventTypeId)) {
				eventHandlerMap[eventTypeId] += eventHandler;
			} else {
				eventHandlerMap.Add(eventTypeId, eventHandler);
			}
		}

		public void UnRegisterEvent(EventType type) {

			int eventTypeId = (int)type;

			if(eventHandlerMap == null) {
				return;
			}

			if (eventHandlerMap.ContainsKey(eventTypeId)) {
				eventHandlerMap.Remove(eventTypeId);
			}

		}

		public void UnRegisterEvent(EventType type, EventHandler eventHandler) {
			int eventTypeId = (int)type;

			if (eventHandlerMap == null) {
				return;
			}

			if (eventHandlerMap.ContainsKey(eventTypeId)) {
				eventHandlerMap[eventTypeId] -= eventHandler;
			}
		}

		public void SendEvent(EventType type, BaseEventMsg msg) {
			int eventTypeId = (int)type;

			if(eventHandlerMap == null) {
				return;
			}

			if (eventHandlerMap.ContainsKey(eventTypeId)) {
				if(eventHandlerMap[eventTypeId] != null) {
					eventHandlerMap[eventTypeId](msg);
				}
			}
		}

		public void SendEvent(EventType type, params object[] inParams) {

			BaseEventMsg msg = new BaseEventMsg(type, inParams);

			if (msg != null) {
				SendEvent(type, msg);
			}
		}

	}



	public enum EventType {
		none,

		spawnMagic,

		hitMagic,

		attack,

		hitAttack,

		block
	}

}


