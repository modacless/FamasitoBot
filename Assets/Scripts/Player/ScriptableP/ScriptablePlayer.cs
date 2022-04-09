using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptablePlayer", menuName = "player/ScriptablePlayer", order = 0)]
public class ScriptablePlayer : ScriptableObject
{
    [Header("Physics")]
    //Mapping des touches
    public KeyCode upKey = KeyCode.Z;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode rightKey = KeyCode.D;
    //Valeurs
    public float speed = 1;
    public AnimationCurve accelerationCurve;
    public AnimationCurve decelerationCurve;

}
