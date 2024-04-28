using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject subUI;
    [SerializeField] private GameObject gameoverUI;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
    public void DrawPanel(GameObject panel)
    {

        StartCoroutine(DrawPanelCoroutine(panel));

    }

    IEnumerator DrawPanelCoroutine(GameObject currActivePanel)
    {
        foreach (GameObject panel in panels)
        {
            if (panel != currActivePanel && panel.activeSelf)
            {
                SwitchPanel(panel);
            }
        }
        yield return new WaitForSecondsRealtime(0.25f);
        subUI.SetActive(true);
        currActivePanel.SetActive(true);
        Animator animator = currActivePanel.GetComponent<Animator>();
        bool toggled = animator.GetBool("Toggle");
        animator.SetBool("Toggle", !toggled);
    }

    public void ClosePanel(GameObject panel)
    {
        StartCoroutine(ClosePanelCoroutine(panel));
    }

    IEnumerator ClosePanelCoroutine(GameObject currActivePanel)
    {
        currActivePanel.SetActive(true);
        Animator animator = currActivePanel.GetComponent<Animator>();
        bool toggled = animator.GetBool("Toggle");
        animator.SetBool("Toggle", !toggled);
        yield return new WaitForSecondsRealtime(0.5f);
        currActivePanel.SetActive(false);
        subUI.SetActive(false);
    }

    public void SwitchPanel(GameObject panel)
    {
        StartCoroutine(SwitchPanelCoroutine(panel));
    }

    IEnumerator SwitchPanelCoroutine(GameObject currActivePanel)
    {
        currActivePanel.SetActive(true);
        Animator animator = currActivePanel.GetComponent<Animator>();
        bool toggled = animator.GetBool("Toggle");
        animator.SetBool("Toggle", !toggled);
        yield return new WaitForSecondsRealtime(0.5f);
        currActivePanel.SetActive(false);
    }

    public void Save()
    {
        PlayerPrefs.SetString("saveData", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        subUI.SetActive(false);
        gameoverUI.SetActive(false);
        string saveData = PlayerPrefs.GetString("saveData", "MainMenu");
        SceneLoader.instance.LoadScene(saveData);
    }

    public void Delete()
    {
        PlayerPrefs.DeleteKey("saveData");
    }

    public void ReturnToMainMenu()
    {
        subUI.SetActive(false);
        gameoverUI.SetActive(false);
        SceneLoader.instance.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetWindowMode()
    {
        if(Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        
    }
}