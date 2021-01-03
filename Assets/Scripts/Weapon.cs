﻿using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData firstWeaponData;
    [SerializeField] WeaponData secondWeaponData;
    [SerializeField] Transform shootSource;

    [SerializeField] UnityEvent onWeaponEquip;

    WeaponData equipedWeaponData = null;

    GameObject firstWeaponModel;
    GameObject secondWeaponModel;

    float currentHeat = 0;
    const float overheatValue = 2;
    bool isOverheated = false;
    float timeToShoot;
    AudioSource audioSource;

    void Start()
    {
        timeToShoot = firstWeaponData.ShootRate;
        audioSource = GetComponent<AudioSource>();
        if (firstWeaponData)
        {
            firstWeaponModel = Instantiate(firstWeaponData.Model, transform);
        }
        else
        {
            Debug.Log("Fist weapon is null!");
        }

        if (secondWeaponData)
        {
            secondWeaponModel = Instantiate(secondWeaponData.Model, transform);
        }
        else
        {
            Debug.Log("Second weapon is null!");
        }


        if (firstWeaponData)
        {
            EquipWeapon(true);
        }
        else if(secondWeaponData)
        {
            EquipWeapon(false);
        }
    }

    public WeaponData GetEquipedWeaponData()
    {
        return equipedWeaponData;
    }

    public void SwapWeapon()
    {
        if (equipedWeaponData == firstWeaponData)
        {
            EquipWeapon(false);
        }
        else
        {
            EquipWeapon(true);
        }
    }

    void EquipWeapon(bool first)
    {
        if (first)
        {
            if (firstWeaponData)
            {
                firstWeaponModel?.SetActive(true);
                secondWeaponModel?.SetActive(false);
                equipedWeaponData = firstWeaponData;
            }
            else
            {
                Debug.LogError("Can't equip first weapon - weapon data in null!");
            }
        }
        else
        {
            if (secondWeaponData)
            {
                firstWeaponModel?.SetActive(false);
                secondWeaponModel?.SetActive(true);
                equipedWeaponData = secondWeaponData;
            }
            else
            {
                Debug.LogError("Can't equip second weapon - weapon data in null!");
            }
        }

        onWeaponEquip.Invoke();
    }

    public void UpdateMe()
    {
        timeToShoot -= Time.deltaTime;
        currentHeat -= Time.deltaTime;
        if (currentHeat < 0)
        {
            currentHeat = 0;
            isOverheated = false;
        }
        else if (currentHeat >= overheatValue)
            isOverheated = true;
    }

    public void Shoot()
    {
        if (timeToShoot <= 0 && !isOverheated)
        {
            Projectile projectileInstance = Instantiate(equipedWeaponData.Projectile, shootSource.transform.position, shootSource.rotation);
            projectileInstance.Init(equipedWeaponData.ProjectileData);

            timeToShoot = equipedWeaponData.ShootRate;
            currentHeat += equipedWeaponData.HeatPerShot;
            audioSource.PlayOneShot(equipedWeaponData.ShootSound);
            return;
        }
    }
}
