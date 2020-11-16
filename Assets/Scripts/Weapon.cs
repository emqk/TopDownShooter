using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform shootSource;

    float timeToShoot;
    AudioSource audioSource;

    void Start()
    {
        timeToShoot = weaponData.ShootRate;
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateMe()
    {
        if (timeToShoot <= 0)
        {
            Projectile projectileInstance = Instantiate(weaponData.Projectile, shootSource.transform.position, shootSource.rotation);
            projectileInstance.Init(weaponData.ProjectileData);
            
            timeToShoot = weaponData.ShootRate;
            audioSource.PlayOneShot(weaponData.ShootSound);
            return;
        }

        timeToShoot -= Time.deltaTime;
    }
}
