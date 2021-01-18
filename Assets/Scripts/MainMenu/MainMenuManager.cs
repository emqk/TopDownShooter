using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Serializer.Load();
        RefreshCharacterSkin();
        RefreshWeaponsUI();
    }

    void RefreshCharacterSkin()
    {
        PurchaseData characterSkin = Database.instance.GetCharacterData();
        MainMenuUIManager.instance.RefreshVisualizationFromData(characterSkin);
    }

    void RefreshWeaponsUI()
    {
        WeaponData firstWeapon = Database.instance.GetWeaponData(true);
        WeaponData secondWeapon = Database.instance.GetWeaponData(false);
        
        if(firstWeapon)
            MainMenuUIManager.instance.RefreshWeaponIcon(firstWeapon, true);
        if(secondWeapon)
            MainMenuUIManager.instance.RefreshWeaponIcon(secondWeapon, false);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
