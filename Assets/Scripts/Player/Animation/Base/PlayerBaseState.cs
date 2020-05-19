using UnityEngine;
using UnityEditor;

public abstract class PlayerBaseState{

    protected AnimatePlayer player;

    public abstract void HandleInput();

}