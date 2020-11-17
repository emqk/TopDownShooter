using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] Weapon equipedWeapon;
    [SerializeField] Image healthFillImage;
    Statistic health = new Statistic();

    public void TakeDamage(int damageAmount)
    {
        health.ChangeByAmount(-damageAmount);
        healthFillImage.fillAmount = health.GetAmountNormalized();
    }

    public void WeaponShoot()
    {
        if (equipedWeapon)
        {
            equipedWeapon.UpdateMe();
        }
    }
}
