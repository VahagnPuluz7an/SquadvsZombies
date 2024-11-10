using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;
        
        while (asyncLoad.progress < 0.9f)
        {
            slider.value = asyncLoad.progress;
            yield return null;
        }
        
        slider.value = asyncLoad.progress;
        //yield return new WaitWhile(() => PlayerPrefs.GetInt(UserConsentManager.PlayerPrefsConsentKey) == 0);
        asyncLoad.allowSceneActivation = true;
    }
}