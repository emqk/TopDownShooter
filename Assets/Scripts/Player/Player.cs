using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Weapon")]
    [SerializeField] Transform weaponRoot;
    Vector3 defaultWeaponRootLocalPosition;
    Vector3 shootWeaponRootLocalPosition;
    UpgradeKitData firstWeaponUpgradeData;
    UpgradeKitData secondWeaponUpgradeData;

    [SerializeField] Weapon equipedWeapon;

    [SerializeField] UnityEvent onWeaponEquip;

    Weapon firstWeapon;
    Weapon secondWeapon;

    GameObject characterSkin;

    [Header("Other")]
    Camera playerCamera;
    FillUI healthUI;
    Statistic health = new Statistic(0, 100, 100);

    PlayerController playerController;

    public void AddHealth(int amount)
    {
        health.ChangeByAmount(amount);
        RefreshHPFillUI();
    }

    public void TakeDamage(int damageAmount)
    {
        health.ChangeByAmount(-damageAmount);
        RefreshHPFillUI();

        if (!health.IsGreaterThanMinimum())
        {
            Die();
        }
    }

    public void Die()
    {
        UIManager.instance.ShowEndPanel();
    }

    void RefreshHPFillUI()
    {
        healthUI.Refresh(health.GetAmount(), health.GetAmountNormalized());
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

    public void OnWeaponShoot()
    {
        weaponRoot.localPosition = shootWeaponRootLocalPosition;
    }

    void SetupCharacterData()
    {
        CharacterData characterData = Database.instance.GetCharacterData();
        characterSkin = Instantiate(characterData.Prefab, transform.GetChild(0));

        health = new Statistic(0, characterData.MaxHealth, characterData.MaxHealth);
        playerController.SetMovementSpeed(characterData.MovementSpeed);
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerCamera = BattleManager.instance.MainCamera;
        defaultWeaponRootLocalPosition = weaponRoot.localPosition;
        shootWeaponRootLocalPosition = defaultWeaponRootLocalPosition - weaponRoot.forward * 0.35f;

        healthUI = UIManager.instance.HealthUI;
        healthUI.Init(transform, playerCamera);

        SetupCharacterData();
        SetupWeapons();
        RefreshHPFillUI();
    }

    void Update()
    {
        firstWeapon?.UpdateMe();
        secondWeapon?.UpdateMe();

        equipedWeapon?.UpdateWeaponHeatImage();

        UpdateWeaponRootPosition();
    }

    void UpdateWeaponRootPosition()
    {
        weaponRoot.localPosition = Vector3.Lerp(weaponRoot.localPosition, defaultWeaponRootLocalPosition, Time.deltaTime * 5);
    }

    private void LateUpdate()
    {
        healthUI.RefreshLocation();
    }

    public bool WeaponShoot()
    {
        if (equipedWeapon)
        {
            return equipedWeapon.Shoot();
        }

        return false;
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
