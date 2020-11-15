using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Weapon equipedWeapon;

    public void WeaponShoot()
    {
        if (equipedWeapon)
        {
            equipedWeapon.UpdateMe();
        }
    }
}
