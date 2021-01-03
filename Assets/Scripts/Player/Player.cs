using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Weapon")]
    [SerializeField] Transform weaponRoot;
    [SerializeField] WeaponData firstWeaponData;
    [SerializeField] WeaponData secondWeaponData;

    [SerializeField] Weapon equipedWeapon;

    [SerializeField] UnityEvent onWeaponEquip;

    Weapon firstWeapon;
    Weapon secondWeapon;

    [Header("Other")]
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

        //Weapon
        if (firstWeaponData)
        {
            firstWeapon = Instantiate(firstWeaponData.Prefab, weaponRoot);
        }
        else
        {
            Debug.Log("Fist weapon is null!");
        }

        if (secondWeaponData)
        {
            secondWeapon = Instantiate(secondWeaponData.Prefab, weaponRoot);
        }
        else
        {
            Debug.Log("Second weapon is null!");
        }


        if (firstWeaponData)
        {
            EquipWeapon(true);
        }
        else if (secondWeaponData)
        {
            EquipWeapon(false);
        }
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

    public WeaponData GetEquipedWeaponData()
    {
        return equipedWeapon.GetWeaponData();
    }

    public void SwapWeapon()
    {
        if (equipedWeapon == firstWeapon)
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
                firstWeapon?.gameObject.SetActive(true);
                secondWeapon?.gameObject.SetActive(false);
                equipedWeapon = firstWeapon;
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
                firstWeapon?.gameObject.SetActive(false);
                secondWeapon?.gameObject.SetActive(true);
                equipedWeapon = secondWeapon;
            }
            else
            {
                Debug.LogError("Can't equip second weapon - weapon data in null!");
            }
        }

        onWeaponEquip.Invoke();
    }
}
