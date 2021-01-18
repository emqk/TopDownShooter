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

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
