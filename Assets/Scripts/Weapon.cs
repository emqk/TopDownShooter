using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootSource;
    [SerializeField] WeaponData weaponData;

    float currentHeat = 0;
    const float overheatValue = 2;
    bool isOverheated = false;
    float timeToShoot;
    AudioSource audioSource;

    void Start()
    {
        timeToShoot = weaponData.ShootRate;
        audioSource = GetComponent<AudioSource>();
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

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public void Shoot()
    {
        if (timeToShoot <= 0 && !isOverheated)
        {
            Projectile projectileInstance = Instantiate(weaponData.Projectile, shootSource.transform.position, shootSource.rotation);
            projectileInstance.Init(weaponData.ProjectileData);

            timeToShoot = weaponData.ShootRate;
            currentHeat += weaponData.HeatPerShot;
            audioSource.PlayOneShot(weaponData.ShootSound);
            return;
        }
    }
}
