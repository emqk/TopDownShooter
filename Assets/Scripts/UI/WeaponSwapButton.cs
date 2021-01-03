using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwapButton : MonoBehaviour
{
    [SerializeField] Weapon playerWeapon;
    [SerializeField] Image buttonImage;

    public void SwapPlayerWeapon()
    {
        playerWeapon.SwapWeapon();
        RefreshImage();
    }

    public void RefreshImage()
    {
        buttonImage.sprite = playerWeapon.GetEquipedWeaponData().Thumbnail;
    }
}
