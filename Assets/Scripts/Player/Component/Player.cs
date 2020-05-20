
using System.Collections.Generic;
using UnityEngine;

public class Player : GamePlayObject{

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

    public uint id;

    //组件
    private MoveComponent moveComponent;
    //private NetComponent netComponent;
    private MoveComponent netMoveComponent;
    public GraphicComponent graphicComponent;
    private PhysicsComponent physicsComponent;
    //组件list
    public List<BaseComponent> componentList = new List<BaseComponent>();
    private int componentAmount = -1;

    public void Start() {

        GameManager.Instance.AddPlayer(id, this);

        if (isLocal) {
            moveComponent = new PlayerMoveComponet();
            graphicComponent = new PlayerGraphicComponent();
            physicsComponent = new PlayerPhysicsComponent();
            componentList.Add(moveComponent);
            componentList.Add(graphicComponent);
            componentList.Add(physicsComponent);
            
        } else {
            moveComponent = new PlayerNetMoveComponent();
            graphicComponent = new PlayerNetGraphicComponent();
            componentList.Add(moveComponent);
            componentList.Add(graphicComponent);
        }

        if(componentList == null) {
            return;
        }
        componentAmount = componentList.Count;
        foreach(BaseComponent component in componentList) {
            component.OnInit(this);
        }
    }

    public void Update(float deltaTime) {
        if (componentList == null) {
            return;
        }
        componentAmount = componentList.Count;
        foreach(BaseComponent component in componentList) {
            component.OnUpdate(deltaTime);
        }
    }
}





