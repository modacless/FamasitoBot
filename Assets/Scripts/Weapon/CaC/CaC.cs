using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CaC : Weapon
{
    public override void TryFire()
    {
        if (CanShoot) Fire();
    }

    public override void Fire()
    {
        //FAIRE DE DEGATS SI QUELQUECHOSE SE TROUVE DANS L'ANGLE DE SHOOT ET A UNE RANGE SUFFISANTE
        StartCoroutine(couldown());
    }
}
