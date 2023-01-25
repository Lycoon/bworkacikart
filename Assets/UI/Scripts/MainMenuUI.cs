using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private GameObject optionsMenu;

    private bool isFadingOut = false;

    // Utils functions
    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(2);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
    }

    // Button events
    public void OnClickPlay()
    {
        HideMainMenuUI();
        StartCoroutine(LoadSceneAsync("Demo 3"));
    }

    public void OnClickOptions()
    {
        optionsMenu.SetActive(optionsMenu.activeSelf ? false : true);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    // Fade animations
    private void Update() 
    {
        if (isFadingOut)
        {
            mainMenuCanvasGroup.alpha -= Time.deltaTime;
            if (mainMenuCanvasGroup.alpha <= 0) 
            {
                mainMenuCanvasGroup.alpha = 0;
                isFadingOut = false;
            }
        }
    }

    public void HideMainMenuUI()
    {
        isFadingOut = true;
    }
}
