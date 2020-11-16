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
        timeToShoot = weaponData.shootRate;
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateMe()
    {
        if (timeToShoot <= 0)
        {
            Projectile projectileInstance = Instantiate(weaponData.projectile, shootSource.transform.position, shootSource.rotation);
            projectileInstance.Init(weaponData.projectileData);
            
            timeToShoot = weaponData.shootRate;
            audioSource.PlayOneShot(weaponData.shootSound);
            return;
        }

        timeToShoot -= Time.deltaTime;
    }
}
