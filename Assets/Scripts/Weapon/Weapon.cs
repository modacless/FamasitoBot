using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    protected WeaponScribatble stats;
    [SerializeField]
    protected GameObject bullet;
    protected int actualAmo;
    protected bool isReloading = false;
    protected bool CanShoot = true;


    public virtual void TryFire()
    {
        if (actualAmo != 0 && !isReloading && CanShoot) Fire();
    }
    public virtual void Fire()
    {
        Instantiate(bullet);
        actualAmo--;
        StartCoroutine(couldown());

    }
    public virtual void TryReload()
    {
        if (actualAmo != stats.maxAmo && !isReloading) StartCoroutine(Reload());
    }
    public virtual IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(stats.reloadTime);
        actualAmo = stats.maxAmo;
        isReloading = false;
    }
    public virtual IEnumerator couldown()
    {
        CanShoot = false;
        yield return new WaitForSeconds(stats.cadence);
        CanShoot = true;
    }
}
