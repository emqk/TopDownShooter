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

    Weapon weapon;

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
        BattleManager.instance.EndBattle();
    }

    void RefreshHPFillUI()
    {
        healthUI.Refresh(health.GetAmount(), health.GetAmountNormalized());
    }

    void SetupWeapons()
    {
        WeaponData weaponData = Database.instance.GetWeaponData();
        firstWeaponUpgradeData = Database.instance.GetWeaponUpgradeKitData();

        //Spawn weapons
        if (weaponData)
        {
            weapon = Instantiate(weaponData.Prefab, weaponRoot).GetComponent<Weapon>();
        }
        else
        {
            Debug.Log("Weapon is null!");
        }

        //Equip first available weapon
        if (weapon)
        {
            EquipWeapon();
        }

        //Set weapon data to all available weapons
        weapon?.Init(firstWeaponUpgradeData);
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
        weapon?.UpdateMe();
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

    void EquipWeapon()
    {
        if (weapon)
        {
            weapon?.gameObject.SetActive(true);
            equipedWeapon = weapon;
        }
        else
        {
            Debug.Log("Can't equip weapon - weapon data in null!");
        }

        onWeaponEquip.Invoke();
    }
}
