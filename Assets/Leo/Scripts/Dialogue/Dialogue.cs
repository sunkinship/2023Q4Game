using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance;

    [Header("On Load Settings")]
    public bool playDialogueOnLoad;
    public float secondsToWaitBeforePlay;
    public bool jumpscare;

    [Header("Load After Settings")]
    public bool loadLevel;
    public bool loadFinalScene;
    public bool loadMenu;
    public bool blackFadeAfter;

    [Header("Dialogue Settings")]
    public string[] lines, lines2;
    public Sprite[] portraits, portraits2;
    public AudioClip playerVoices, npcVoices;
    public float textSpeed, voiceLength;

    [Header("Animator References")]
    public Animator playerAnimator;
    public Animator npcAnimator;

    [Header("Game Obejct References")]
    public GameObject dialogueBox;
    public TextMeshProUGUI textBox;
    public Image dialoguePortrait;
    public Animator scareAni;

    private PlayerInputHandler inputHandler;

    private string[] currentLines;
    private Sprite[] currentPortraits;
    private int lineIndex;
    private bool inDialogue;
    private bool isWriting;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inputHandler = GameObject.FindGameObjectWithTag("Input").GetComponent<PlayerInputHandler>();

        StartCoroutine(WaitToSetAction());
    }

    private void Update()
    { 
        if (inDialogue)
        {
            WaitForInput();
        }
    }

    private IEnumerator WaitToSetAction()
    {
        while (Transition.Instance.GetFadeEnd() == false)
            yield return null;
        if (playDialogueOnLoad && GameManager.gameState == GameManager.GameState.story)
            InitializeDialgue();
        else
            GameManager.Instance.SetActionPlay();
        Transition.Instance.ResetFadeEnd();
    }

    public void InitializeDialgue()
    {
        currentLines = lines;
        currentPortraits = portraits;
        StartCoroutine(WaitToStart());
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(secondsToWaitBeforePlay);
        StartDialogueSequence();
    }

    private void StartDialogueSequence()
    {
        dialogueBox.SetActive(true);
        GameManager.Instance.SetActionDialogue();
        inDialogue = true;
        lineIndex = 0;
        StartDialogueLine();
    }

    public void NextDialogueSequence()
    {
        if (isWriting == false)
        {
            currentLines = lines2;
            currentPortraits = portraits2;
            StartDialogueSequence();
        }
    }

    private void StartDialogueLine()
    {
        textBox.text = string.Empty;
        StartTalkVoiceAndAni(currentLines[lineIndex][0]);
        dialoguePortrait.sprite = currentPortraits[lineIndex];
        StartCoroutine(WriteLine());
    }

    private void WaitForInput()
    {
        if (inputHandler.InteractInput)
        {
            inputHandler.UseInteractInput();
            //done writing go to next line
            if (textBox.text == currentLines[lineIndex].Substring(1))
            {
                NextLine();
            }
            //still writing skip dialogue  
            else
            {
                StopAllCoroutines();
                textBox.text = currentLines[lineIndex].Substring(1);
                StopTalkVoiceAndAni(currentLines[lineIndex][0]);
            }
        }
    }

    private IEnumerator WriteLine()
    {
        foreach(char c in currentLines[lineIndex].Substring(1).ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        StopTalkVoiceAndAni(currentLines[lineIndex][0]);
    }

    private void NextLine()
    {
        //more dialogue
        if (lineIndex < currentLines.Length - 1)
        {
            lineIndex++;
            StartDialogueLine();
        }
        //end dialogue
        else
        {
            if (jumpscare)
                Jumpscare();
            else
                ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        inDialogue = false;
        dialogueBox.SetActive(false); 
        StopTalkVoiceAndAni(currentLines[lineIndex][0]);
        playerAnimator.SetBool("isTalking", false);
        if (loadLevel)
        {
            if (blackFadeAfter)
            {
                Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", GameManager.Instance.LoadNextNextNextScene);
            }
            else
            {
                Transition.Instance.TriggerFadeBoth("StartQuickWhite", "EndQuickWhite", GameManager.Instance.LoadNextScene);
            }
        }
        else if (loadFinalScene)
        {
            if (GameManager.abilityStateStory > 2)
            {
                Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadGoodFinalScene);
            }
            else
            {
                Transition.Instance.TriggerFadeBoth("StartLongBlack", "EndLongBlack", GameManager.Instance.LoadBadFinalScene);
            }
        }
        else if (loadMenu)
        {
            PlayerPrefs.SetInt("beatGame", 1);
            PlayerPrefs.SetInt("goodEnd", 1);
            Transition.Instance.TriggerFadeBoth("StartLongWhite", "EndLongWhite", GameManager.Instance.LoadMainMenu);
        }
        else
        {
            GameManager.Instance.SetActionPlay();
        }
    }

    #region Jumpscare
    private void Jumpscare()
    {
        PlayerPrefs.SetInt("beatGame", 1);
        scareAni.enabled = true;
        scareAni.SetTrigger("Scare");
    }
    #endregion

    #region Audio and Animation
    private void StartTalkVoiceAndAni(char talkerID)
    {
        isWriting = true;
        if (talkerID == '0')
        {
            playerAnimator.SetBool("isTalking", true);
            StartCoroutine(PlayVoice(playerVoices));
        } 
        else if (talkerID == '1')
        {
            npcAnimator.SetBool("isTalking2", true);
            StartCoroutine(PlayVoice(npcVoices));
        } 
    }

    private void StopTalkVoiceAndAni(char talkerID)
    {
        isWriting = false;
        if (talkerID == '0')
        {
            playerAnimator.SetBool("isTalking", false);    
        } 
        else if (talkerID == '1')
        {
            npcAnimator.SetBool("isTalking2", false);
        }  
    }

    private IEnumerator PlayVoice(AudioClip voiceClip)
    {
        while (isWriting)
        {
            float startTime = Time.time;
            AudioManager.Instance.PlaySFX(voiceClip);
            while (Time.time <= startTime + voiceLength)
                yield return null;
        }
        yield break;
    }
    #endregion
}