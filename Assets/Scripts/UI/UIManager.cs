using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] WeaponHeatImage weaponHeatImage;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public WeaponHeatImage GetWeaponHeatImage()
    {
        return weaponHeatImage;
    }
}
