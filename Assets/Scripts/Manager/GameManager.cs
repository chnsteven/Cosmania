using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    //public static event Action<string> UnlockDoor;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private List<GameObject> toActivateOnStart;
    [SerializeField] private List<GameObject> roomsInCurrentScene;
    [SerializeField] private string previousRoom;
    [SerializeField] public string nextRoom;
    private GameObject previousRoomGO;

    public static GameManager instance;
    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        instance = this;
        TurnOnAllUI(true);
        //addRoomsInCurrentScene();
        spawnPlayer();
    }

    

    public void SelectOption(string option)
    {
        switch (option)
        {
            case "Start":
                PlayerPrefs.DeleteAll();
                SceneLoader.instance.LoadScene("Rural");
                break;
            case "Continue":
                string saveData = PlayerPrefs.GetString("saveData", "");
                if(saveData != "") SceneLoader.instance.LoadScene(saveData);
                break;
            case "Quit":
                Application.Quit();
                break;
            default:
                break;
        }
    }

    private void spawnPlayer()
    {
        previousRoomGO = GameObject.Find(previousRoom);
        if (previousRoomGO == null) return;
        Transform spawnLocation = previousRoomGO.transform.Find("SpawnLocation");
        string playerTag = "Player";
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (spawnLocation == null) return;
        player.transform.position = spawnLocation.position;
        player.transform.rotation = spawnLocation.rotation;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    public void TurnOnAllUI(bool turnOn)
    {
        foreach (GameObject ui in toActivateOnStart)
        {
            ui.SetActive(turnOn);
            CanvasGroup canvasGroup = ui.GetComponent<CanvasGroup>();
            if (canvasGroup == null) continue;
            StartCoroutine(FadeIn(canvasGroup));
        }
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float speed = 2f;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            canvasGroup.alpha = t;
            yield return 0f;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (Gamepad.instance.GetEscDown())
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }
    }
    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        gameOverUI.SetActive(true);
        gameOverUI.GetComponent<Animator>().SetTrigger("Enable");

    }
}
