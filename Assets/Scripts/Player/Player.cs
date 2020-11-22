using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] Weapon equipedWeapon;
    [SerializeField] Camera playerCamera;
    [SerializeField] float hpImageYPercentOffset;
    [SerializeField] Image healthFillImage;
    Statistic health = new Statistic();

    public void AddHealth(int amount)
    {
        health.ChangeByAmount(amount);
    }

    public void TakeDamage(int damageAmount)
    {
        health.ChangeByAmount(-damageAmount);
        healthFillImage.fillAmount = health.GetAmountNormalized();
    }

    private void LateUpdate()
    {
        Vector3 pos = playerCamera.WorldToScreenPoint(transform.position);
        healthFillImage.transform.parent.position = new Vector3(pos.x, pos.y + hpImageYPercentOffset * Screen.height, pos.z);

        if (equipedWeapon)
        {
            equipedWeapon.UpdateMe();
        }
    }

    public void WeaponShoot()
    {
        if (equipedWeapon)
        {
            equipedWeapon.Shoot();
        }
    }
}
