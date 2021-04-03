using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Serializer.Load();
        MainMenuUIManager.instance.RefreshCharacterSkin();
        MainMenuUIManager.instance.RefreshWeaponsUI();
    }

    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
        AdsManager.instance.AdvanceCounter();
    }

    public void Play()
    {
        MapData mapData = Database.instance.GetMapData();
        if (mapData)
        {
            string sceneName = mapData.SceneName;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Can't load scene - mapData is null!");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
