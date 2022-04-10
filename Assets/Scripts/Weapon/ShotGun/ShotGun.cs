using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    public float angle //commenter;
    public int amoPerShoot;
    public override void TryFire()
    {
        if (!isReloading && CanShoot && actualAmo >= amoPerShoot) Fire();
    }

    public override void Fire()
    {
        //lancer x bullet dans l'angle de tir
        actualAmo -= amoPerShoot;
        StartCoroutine(couldown());
    }
}


