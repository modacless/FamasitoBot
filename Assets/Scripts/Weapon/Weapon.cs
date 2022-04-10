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
    [SerializeField]
    protected GameObject bulletGenerator;
    public int actualAmo;
    public bool isReloading = false;
    public bool CanShoot = true;

    private void Start()
    {
        actualAmo = stats.maxAmo;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryFire();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            TryReload();
        }
    }
    public virtual void TryFire()
    {
        if (actualAmo != 0 && !isReloading && CanShoot) Fire();
    }
    public virtual void Fire()
    {
        GameObject myBullet = Instantiate(bullet, bulletGenerator.transform.position, transform.parent.transform.rotation);
        myBullet.GetComponent<Bullet>().direction = -transform.up;
        Debug.Log(transform.up);
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
