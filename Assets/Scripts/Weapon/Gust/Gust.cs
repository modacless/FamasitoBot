using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : Weapon
{
    public float timePerCadence;
    public int amoPerGust;
    public override void TryFire()
    {
        if (!isReloading && CanShoot && actualAmo >= amoPerGust) Fire();
    }

    //plutôt refaire un enum
    public override IEnumerator couldown()
    {
        CanShoot = false;
        for (int i = 1; i > amoPerGust; i++)
        {
            yield return new WaitForSeconds(stats.cadence);
            Instantiate(bullet);
            actualAmo--;
        }
        yield return new WaitForSeconds(timePerCadence);
        CanShoot = true;
    }
}
