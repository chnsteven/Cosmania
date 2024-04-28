using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Ink.Runtime;
using UnityEngine.EventSystems;
//using Ink.UnityIntegration;
using System;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public static event Action EnterDialog;
    public static DialogManager instance;
    [Header("Dialog UI")]
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private TMP_Text displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private GameObject dialogPanel;
    [Header("Choice UI")]
    [SerializeField] private List<GameObject> choices;
    private TMP_Text[] choicesText;
    private GameObject choicePanel;
    [Header("Others")]
    [SerializeField] private CanvasGroup mainUICanvasGroup;
    [SerializeField] private Button continueButton;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    [SerializeField] private TextAsset inkJSON;
    public bool dialogIsPlaying { get; private set; }
    [Header("Story")]
    private Story currentStory;
    private DialogVariables dialogVariables;
    private Coroutine typingSentence;
    private bool canContinueToNextLine = true;
    [SerializeField] private float typeSpeed = 0f;
    public static event Action EndOfStory;
    [Header("Playable")]
    [SerializeField] private PlayableDirector playable;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";

    private void ToggleDialogUI(bool toggle) {
        dialogIsPlaying = toggle;
        dialogUI.SetActive(toggle);
        dialogPanel.SetActive(toggle);
        choicePanel.SetActive(false);
        mainUICanvasGroup.interactable = !toggle;
    }

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than 1 instance of DialogManager");
        }
        instance = this;
        dialogVariables = new DialogVariables(loadGlobalsJSON);
        dialogPanel = dialogUI.transform.Find("DialogPanel").gameObject;
        choicePanel = dialogUI.transform.Find("ChoicePanel").gameObject;
        ToggleDialogUI(false);
        
        choicesText = new TMP_Text[choices.Count];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TMP_Text>();
            index++;
        }
    }

    private void SetPlayableSpeed(int speed)
    {
        if (playable == null) return;
            PlayableGraph playableGraph = playable.playableGraph;
        if (playableGraph.IsValid())
        {
            playableGraph.GetRootPlayable(0).SetSpeed(speed);
        }
    }
    public void EnterDialogMode()
    {
        Gamepad.instance.isDisabled = true;
        currentStory = new Story(inkJSON.text);
        dialogVariables.StartListening(currentStory);
        ToggleDialogUI(true);
        
        SetPlayableSpeed(0);
        //Debug.Log("Invoked EnterDialog");
        EnterDialog?.Invoke();
        
        ContinueStory();
    }

    private void ExitDialogMode()
    {
        dialogVariables.StopListening(currentStory);
        ToggleDialogUI(false);
        dialogText.text = "";
        SetPlayableSpeed(1);
        EndOfStory?.Invoke();
        Gamepad.instance.isDisabled = false;
    }

    public void ContinueStory()
    {
        
        if (!canContinueToNextLine)
        {
            return;
        }
        if (currentStory.canContinue)
        {
            if (typingSentence != null) StopCoroutine(typingSentence);
            typingSentence = StartCoroutine(TypeSentence());
            HandleTags(currentStory.currentTags);
            UpdateContinueButtonText();
        }
        else
        {
            ExitDialogMode();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(":");
            if(splitTag.Length != 2)
            {
                Debug.LogError("Improper tag");
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;

            }
        }
    }

    private IEnumerator TypeSentence()
    {
        string nextSentence = currentStory.Continue();
        HideChoices();
        canContinueToNextLine = false;
        continueButton.gameObject.SetActive(false);
        dialogText.text = "";
        foreach (char c in nextSentence.ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        continueButton.gameObject.SetActive(true);
        DisplayChoices();
        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count == 0)
        {
            choicePanel.SetActive(false);
        }
        else
        {
            choicePanel.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }
        if (currentChoices.Count > choices.Count)
        {
            Debug.LogError("Too much choices");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Count; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        choicePanel.SetActive(false);
        ContinueStory();
    }

    private void UpdateContinueButtonText()
    {
        TMP_Text continueButtonText = continueButton.GetComponentInChildren<TMP_Text>();
        if (currentStory.canContinue)
        {
            continueButtonText.text = "Next";
        }
        else
        {
            continueButtonText.text = "Finish";
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            //Debug.LogWarning("Ink variable was found to be null: " + variableName);

        }
        return variableValue;
    }

    public DialogVariables GetDialogVariables()
    {
        return dialogVariables;
    }

    private void OnEnable()
    {
        PlayerInvestigationState.PlayerEnterInvestigationState += DelayDialogMode;
    }

    private void OnDisable()
    {
        PlayerInvestigationState.PlayerEnterInvestigationState -= DelayDialogMode;
    }

    private void DelayDialogMode()
    {
        StartCoroutine(DelayDialogCoroutine());
    }
    private IEnumerator DelayDialogCoroutine()
    {
        
        yield return new WaitForSeconds(0.2f);
        EnterDialogMode();
    }
}
