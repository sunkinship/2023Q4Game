using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public string[] lines, lines2;
    public Sprite[] portraits, portraits2;
    public AudioClip playerVoices, npcVoices;
    public float textSpeed;
    public bool startDialogueOnLoad;

    [Header("Game Obejct References")]
    public TextMeshProUGUI textBox;
    public Image dialoguePortrait;

    private PlayerInputHandler playerInputHandler;
    private Animator playerAnimator;
    private Animator npcAnimator;

    private int lineIndex, dialogueIndex;
    private bool inDialogue, isWriting;


    private void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        npcAnimator = GameObject.FindGameObjectWithTag("NPC").GetComponent<Animator>();

        dialogueIndex = 0;
        if (startDialogueOnLoad)
        {
            StartDialogueSequence();
        }
    }

    private void Update()
    { 
        if (inDialogue)
        {
            WaitForInput();
        }
    }

    private void StartDialogueSequence()
    {
        GameManager.Instance.playerState = GameManager.State.dialogue;
        inDialogue = true;
        lineIndex = 0;
        StartDialogueLine(dialogueIndex);
    }

    public void NextDialogueSequence()
    {
        dialogueIndex++;
        StartDialogueSequence();
    }

    private void StartDialogueLine(int sequenceIndex)
    {
        textBox.text = string.Empty;
        if (sequenceIndex == 0)
        {
            StartTalkVoiceAndAni(lines[lineIndex][0]);
            dialoguePortrait.sprite = portraits[lineIndex];
        }
        else
        {
            StartTalkVoiceAndAni(lines2[lineIndex][0]);
            dialoguePortrait.sprite = portraits2[lineIndex];
        }
        StartCoroutine(WriteLine());
    }

    private void WaitForInput()
    {
        if (playerInputHandler.InteractInput)
        {
            playerInputHandler.UseInteractInput();
            //done writing go to next line
            if (textBox.text == lines[lineIndex].Substring(1))
            {
                NextLine();
            }
            //still writing skip dialogue  
            else
            {
                StopAllCoroutines();
                textBox.text = lines[lineIndex].Substring(1);
                StopTalkVoiceAndAni(lines[lineIndex][0]);
            }
        }
    }

    private IEnumerator WriteLine()
    {
        foreach (char c in lines[lineIndex].Substring(1).ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        StopTalkVoiceAndAni(lines[lineIndex][0]);
    }

    private void NextLine()
    {
        //more dialogue
        if (lineIndex < lines.Length - 1)
        {
            lineIndex++;
            StartDialogueLine(dialogueIndex);
        }
        //end dialogue
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        inDialogue = false;
        GameManager.Instance.playerState = GameManager.State.play;
        gameObject.SetActive(false);
        StopTalkVoiceAndAni(lines[lineIndex][0]);
    }

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
            float clipLength = voiceClip.length;
            float startTime = Time.time;
            AudioManager.Instance.PlaySFX(voiceClip);
            while (Time.time <= startTime + clipLength)
                yield return null;
        }
        yield break;
    }
    #endregion
}