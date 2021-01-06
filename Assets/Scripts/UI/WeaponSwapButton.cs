using UnityEngine;
using UnityEngine.UI;

public class WeaponSwapButton : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image buttonImage;

    public void SwapPlayerWeapon()
    {
        player.SwapWeapon();
        RefreshImage();
    }

    public void RefreshImage()
    {
        buttonImage.sprite = player.GetEquipedWeaponData().Thumbnail;
    }
}
