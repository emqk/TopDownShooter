﻿using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform shootSource;

    float timeToShoot;

    void Start()
    {
        timeToShoot = weaponData.shootRate;
    }

    public void UpdateMe()
    {
        if (timeToShoot <= 0)
        {
            Instantiate(weaponData.projectile, shootSource.transform.position, shootSource.rotation);
            timeToShoot = weaponData.shootRate;
            return;
        }

        timeToShoot -= Time.deltaTime;
    }
}
