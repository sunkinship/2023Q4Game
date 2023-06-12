using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public string[] lines;
    public Sprite[] portraits;
    public AudioClip playerVoices, npcVoices;
    public float textSpeed;

    [Header("Game Obejct References")]
    public TextMeshProUGUI textComp;
    public Image dialoguePortrait;

    private PlayerInputHandler playerInputHandler;
    private Animator playerAnimator;
    private Animator npcAnimator;

    private int index;
    private bool isWriting;


    private void Start()
    {
        
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        npcAnimator = GameObject.FindGameObjectWithTag("NPC").GetComponent<Animator>();

        index = 0;
        StartDialogue();
    }

    private void Update()
    {
       
        WaitForInput();
    }

    private void WaitForInput()
    {
        if (playerInputHandler.InteractInput)
        {
            Debug.Log("hi");
            playerInputHandler.UseInteractInput();
            //done writing go to next line
            if (textComp.text == lines[index].Substring(1))
            {
                NextLine();
            }
            //still writing skip dialogue  
            else
            {
                StopAllCoroutines();
                textComp.text = lines[index].Substring(1);
                StopTalkVoiceAndAni(lines[index][0]);
            }
        }
    }

    private void StartDialogue()
    {
        textComp.text = string.Empty;
        StartTalkVoiceAndAni(lines[index][0]);
        StartCoroutine(WriteLine());
        dialoguePortrait.sprite = portraits[index];
    }

    private IEnumerator WriteLine()
    {
        foreach (char c in lines[index].Substring(1).ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        StopTalkVoiceAndAni(lines[index][0]);
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartDialogue();
        }
        else
        {
            gameObject.SetActive(false);
            StopTalkVoiceAndAni(lines[index][0]);
        }
    }

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

    private IEnumerator PlayVoice(AudioClip voices)
    {
        while (isWriting)
        {
            Debug.Log(voices);
            float clipLength = voices.length;
            float startTime = Time.time;
            AudioManager.Instance.PlaySFX(voices);
            while (!(Time.time >= startTime + clipLength))
                yield return null;
        }
        yield break;
    }

  
}