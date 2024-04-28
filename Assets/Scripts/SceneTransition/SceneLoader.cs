using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Door currentRoom;
    public static SceneLoader instance;
    private float minLoadingWaitTime = 1f;
    private float elapsedTime = 0f;
    public bool isLoading = false;

    private void Start()
    {
        instance = this;
    }

    public void LoadScene(string sceneToLoad)
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    private IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        isLoading = true;
        GameManager.instance.TurnOnAllUI(false);
        yield return StartCoroutine(SceneFader.instance.Fadeout());
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone && elapsedTime < minLoadingWaitTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        PlayerPrefs.SetString("previousRoom", currentScene.name);
        isLoading = false;
    }
}
