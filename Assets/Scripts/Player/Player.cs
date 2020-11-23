using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] Weapon equipedWeapon;
    [SerializeField] Camera playerCamera;
    [SerializeField] float hpImageYPercentOffset;
    [SerializeField] Image healthFillImage;
    Statistic health = new Statistic(0, 100, 100);

    public void AddHealth(int amount)
    {
        health.ChangeByAmount(amount);
        RefreshHPFillUI();

        if (!health.IsGreaterThanMinimum())
        {
            Die();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health.ChangeByAmount(-damageAmount);
        RefreshHPFillUI();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    void RefreshHPFillUI()
    {
        healthFillImage.fillAmount = health.GetAmountNormalized();
    }

    void Start()
    {
        RefreshHPFillUI();
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
