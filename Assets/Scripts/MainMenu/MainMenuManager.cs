using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] LoadingScreenPanelUI loadingScreenPrefab;

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
            Instantiate(loadingScreenPrefab);
            string sceneName = mapData.SceneName;
            StartCoroutine(DelayedLoadScene(sceneName));
        }
        else
        {
            Debug.LogError("Can't load scene - mapData is null!");
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
