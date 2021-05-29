using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Weapon")]
    [SerializeField] Transform weaponRoot;
    Vector3 defaultWeaponRootLocalPosition;
    Vector3 shootWeaponRootLocalPosition;
    UpgradeKitData firstWeaponUpgradeData;

    [SerializeField] Weapon equipedWeapon;

    Weapon weapon;
    const float weaponAfterShootOffset = 0.35f;
    float weaponMoveToDefaultPositionSpeed = 5;

    GameObject characterSkin;

    [Header("Other")]
    Camera playerCamera;
    FillUI healthUI;
    Statistic health = new Statistic(0, 100, 100);

    PlayerController playerController;

    const float PLAYER_HALF_SIZE = 0.5f;


    /////////////// Implement interfaces - BEGIN ///////////////

    public void TakeDamage(int damageAmount)
    {
        health.ChangeByAmount(-damageAmount);
        RefreshHPFillUI();
        CameraShaker.instance.ShakeCamera(0.5f);

        if (!health.IsGreaterThanMinimum())
        {
            Die();
        }
    }

    public void Die()
    {
        BattleManager.instance.EndBattle();
    }

    public bool IsInDamageRadius(float distance, float radius)
    {
        return distance <= radius + PLAYER_HALF_SIZE;
    }

    /////////////// Implement interfaces - END ///////////////

    public void AddHealth(int amount)
    {
        health.ChangeByAmount(amount);
        RefreshHPFillUI();
    }

    void RefreshHPFillUI()
    {
        healthUI.Refresh(health.GetAmount(), health.GetAmountNormalized());
    }

    void SetupWeapon()
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
        if (weapon)
            weapon.Init(firstWeaponUpgradeData);
        else
            Debug.Log("Can't setup weapon - weapon is null!");
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
        shootWeaponRootLocalPosition = defaultWeaponRootLocalPosition - weaponRoot.forward * weaponAfterShootOffset;

        healthUI = UIManager.instance.HealthUI;
        healthUI.Init(transform, playerCamera);

        SetupCharacterData();
        SetupWeapon();
        RefreshHPFillUI();
    }

    void Update()
    {
        if(weapon)
            weapon.UpdateMe();

        UpdateWeaponRootPosition();
    }

    void UpdateWeaponRootPosition()
    {
        weaponRoot.localPosition = Vector3.Lerp(weaponRoot.localPosition, defaultWeaponRootLocalPosition, Time.deltaTime * weaponMoveToDefaultPositionSpeed);
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
    }
}
