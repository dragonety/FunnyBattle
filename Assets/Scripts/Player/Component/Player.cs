
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public bool isLocal;

    public GameObject gameObject;
    public Transform transform;

    public GameObject magicBall;

    public RectTransform healthBar;
    public Transform shootPoint;
    public GameObject sword;
    public GameObject shield;

    public float moveSpeed;
    public float rotateSpeed;
    public float shootSpeed;
    public float lerpRate;

    public bool isDebug;
    public bool isLoopBlock;

    public int health;

    public Vector3 position;
    public Quaternion rotation;

    //组件
    private MoveComponent moveComponent;
    //private NetComponent netComponent;
    private MoveComponent netMoveComponent;
    private GraphicComponent graphicComponent;
    private PhysicsComponent physicsComponent;
    //组件list
    public List<BaseComponent> componentList = new List<BaseComponent>();
    private int componentAmount = -1;

    public void Start() {

        if (isLocal) {
            moveComponent = new PlayerMoveComponet();
            graphicComponent = new PlayerGraphicComponent();
            //netComponent = new 
            componentList.Add(moveComponent);
            componentList.Add(graphicComponent);
        } else {
            moveComponent = new PlayerNetMoveComponent();
            componentList.Add(moveComponent);
        }

        if(componentList == null) {
            return;
        }
        componentAmount = componentList.Count;
        foreach(BaseComponent component in componentList) {
            component.OnInit(this);
        }
    }

    public void Update() {
        if (componentList == null) {
            return;
        }
        componentAmount = componentList.Count;
        foreach(BaseComponent component in componentList) {
            component.OnUpdate(Time.deltaTime);
        }
    }
}





