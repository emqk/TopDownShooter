using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Weapon")]
    [SerializeField] Transform weaponRoot;
    UpgradeKitData firstWeaponUpgradeData;
    UpgradeKitData secondWeaponUpgradeData;

    [SerializeField] Weapon equipedWeapon;

    [SerializeField] UnityEvent onWeaponEquip;

    Weapon firstWeapon;
    Weapon secondWeapon;

    GameObject characterSkin;

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

    void SetupWeapons()
    {
        WeaponData firstWeaponData = Database.instance.GetWeaponData(true);
        WeaponData secondWeaponData = Database.instance.GetWeaponData(false);

        firstWeaponUpgradeData = Database.instance.GetWeaponUpgradeKitData(true);
        secondWeaponUpgradeData = Database.instance.GetWeaponUpgradeKitData(false);

        //Spawn weapons
        if (firstWeaponData)
        {
            firstWeapon = Instantiate(firstWeaponData.Prefab, weaponRoot).GetComponent<Weapon>();
        }
        else
        {
            Debug.Log("Fist weapon is null!");
        }

        if (secondWeaponData)
        {
            secondWeapon = Instantiate(secondWeaponData.Prefab, weaponRoot).GetComponent<Weapon>();
        }
        else
        {
            Debug.Log("Second weapon is null!");
        }

        //Equip first available weapon
        if (firstWeapon)
        {
            EquipWeapon(true);
        }
        else if (secondWeapon)
        {
            EquipWeapon(false);
        }

        //Set weapon data to all available weapons
        firstWeapon?.Init(firstWeaponUpgradeData);
        secondWeapon?.Init(secondWeaponUpgradeData);
    }

    void SetupCharacterSkin()
    {
        PurchaseData skinData = Database.instance.GetCharacterData();
        characterSkin = Instantiate(skinData.Prefab, transform.GetChild(0));
    }

    void Start()
    {
        SetupCharacterSkin();
        SetupWeapons();
        RefreshHPFillUI();
    }

    void Update()
    {
        firstWeapon?.UpdateMe();
        secondWeapon?.UpdateMe();

        equipedWeapon?.UpdateWeaponHeatImage();
    }

    private void LateUpdate()
    {
        Vector3 pos = playerCamera.WorldToScreenPoint(transform.position);
        healthFillImage.transform.parent.position = new Vector3(pos.x, pos.y + hpImageYPercentOffset * Screen.height, pos.z);
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
            if (firstWeapon)
            {
                firstWeapon?.gameObject.SetActive(true);
                secondWeapon?.gameObject.SetActive(false);
                equipedWeapon = firstWeapon;
            }
            else
            {
                Debug.Log("Can't equip first weapon - weapon data in null!");
            }
        }
        else
        {
            if (secondWeapon)
            {
                firstWeapon?.gameObject.SetActive(false);
                secondWeapon?.gameObject.SetActive(true);
                equipedWeapon = secondWeapon;
            }
            else
            {
                Debug.Log("Can't equip second weapon - weapon data in null!");
            }
        }

        onWeaponEquip.Invoke();
    }
}
