using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Magic : GamePlayObject{

    public GameObject gameObject;
    public Transform transform;

    public Vector3 velocity;

    private MoveComponent moveComponent;

    public List<BaseComponent> componentList = new List<BaseComponent>();
    private int componentAmount = -1;

    public void Start() {

        moveComponent = new MagicMoveComponet();
        componentList.Add(moveComponent);

        if (componentList != null) {
            componentAmount = componentList.Count;
            foreach(BaseComponent component in componentList) {
                //component.OnInit(this);
            }
        }
    }

    public void Update(float deltaTime) {

        if (componentList != null) {
            componentAmount = componentList.Count;
            foreach(BaseComponent component in componentList) {
                component.OnUpdate(deltaTime);
            }
        }

    }

}