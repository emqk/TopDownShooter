using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] LoadingScreenPanelUI loadingScreenPrefab;
    [SerializeField] List<PurchaseData> startingPurchaseDatas;

    public static MainMenuManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (Serializer.SaveFileExist())
        {
            Serializer.Load();
        }
        else
        {
            Debug.Log("Save file not found. Starting new game");
            foreach (PurchaseData data in startingPurchaseDatas)
            {
                Database.instance.AddPurchaseElementID(data);
                EquipPurchaseData(data);
            }
        }
        MainMenuUIManager.instance.RefreshCharacterSkin();
        MainMenuUIManager.instance.RefreshWeaponsUI();
        MainMenuUIManager.instance.RefreshMapUI();
    }

    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
    }

    public void EquipPurchaseData(PurchaseData purchaseData)
    {

        if (purchaseData as WeaponData)
        {
            UpgradeKitData upgradeKitData = Database.instance.GetUpgradeKitDataByID(purchaseData.UpgradeKit.ID);

            Database.instance.SetWeaponData((WeaponData)purchaseData);
            Database.instance.SetWeaponUpgradeKitData(upgradeKitData);
            MainMenuUIManager.instance.RefreshWeaponsUI();
        }
        else if (purchaseData as CharacterData)
        {
            Database.instance.SetCharacterData((CharacterData)purchaseData);
            MainMenuUIManager.instance.RefreshVisualizationFromData(purchaseData);
        }
        else if (purchaseData as MapData)
        {
            Database.instance.SetMapData((MapData)purchaseData);
            MainMenuUIManager.instance.RefreshMapUI();
        }
    }

    public void Play()
    {
        CharacterData characterData = Database.instance.GetCharacterData();
        WeaponData weaponData = Database.instance.GetWeaponData();
        MapData mapData = Database.instance.GetMapData();

        if (!characterData)
        {
            PopupData popupData = new PopupData()
            {
                title = "Can't start",
                description = "Select a character before starting",
                buttonsData = new List<PopupButttonData> { new PopupButttonData() { text = "Ok", onClick = PopupManager.instance.CloseLastPopup } },
                popupType = PopupType.Bad
            };
            PopupManager.instance.CreatePopup(popupData);

            Debug.Log("Can't load scene - characterData is null!");
            return;
        }
        if (!weaponData)
        {
            PopupData popupData = new PopupData()
            {
                title = "Can't start",
                description = "Select a weapon before starting",
                buttonsData = new List<PopupButttonData> { new PopupButttonData() { text = "Ok", onClick = PopupManager.instance.CloseLastPopup } },
                popupType = PopupType.Bad
            };
            PopupManager.instance.CreatePopup(popupData);

            Debug.Log("Can't load scene - weaponData is null!");
            return;
        }
        if (!mapData)
        {
            PopupData popupData = new PopupData() {
                title = "Can't start"
                , description = "Select a map before starting"
                , buttonsData = new List<PopupButttonData> { new PopupButttonData() { text = "Ok", onClick = PopupManager.instance.CloseLastPopup } }
                , popupType = PopupType.Bad };
            PopupManager.instance.CreatePopup(popupData);

            Debug.Log("Can't load scene - mapData is null!");
            return;
        }

        //Is data is valid, load battle scene
        if (mapData)
        {
            Instantiate(loadingScreenPrefab);
            string sceneName = mapData.SceneName;
            StartCoroutine(DelayedLoadScene(sceneName));
        }
    }

    IEnumerator DelayedLoadScene(string sceneName)
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
