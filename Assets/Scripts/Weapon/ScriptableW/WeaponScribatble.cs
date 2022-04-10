using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "player/ScriptableWeapon", order = 1)]
public class WeaponScribatble : ScriptableObject
{
    [Header("Stats")]
    public int maxAmo;
    public float cadence;
    public float damage;
    public bool canBeGrab;
    public float reloadTime;
    public float buyingPrice;
    public float sellingPrice;
}
