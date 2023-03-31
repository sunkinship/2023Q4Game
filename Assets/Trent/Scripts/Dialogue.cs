using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComp;
    public string[] lines;
    // public ArrayList[] portraits;
    //public Array[] portraits;
    //public float[] portrait;
    public Sprite[] portraits;
    public float textSpeed;
    public Image dialoguePortrait;
    public Animator animator;
    public Animator animator2;


    private int index;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        animator2 = GameObject.FindGameObjectWithTag("NPC").GetComponent<Animator>();
        textComp.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (textComp.text == lines[index].Substring(1))
            {
                NextLine();
               
            }
            else
            {
                StopAllCoroutines();
                textComp.text = lines[index].Substring(1);
                stopTalk(lines[index][0]);
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        startTalk(lines[index][0]);
        StartCoroutine(TypeLine());
        dialoguePortrait.sprite = portraits[index];
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].Substring(1).ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        stopTalk(lines[index][0]);
    }

     void startTalk(char talker)
    {
        Debug.Log(talker);
        if (talker == '0')
        {
            animator.SetBool("isTalking", true);
            
            
        } else if (talker == '1')
        {
            animator2.SetBool("isTalking2", true);
        }
        
    }
    void stopTalk(char talker)
    {
        Debug.Log(talker);
        if (talker == '0')
        {
            animator.SetBool("isTalking", false);
            
        } else if (talker == '1')
        {
            animator2.SetBool("isTalking2", false);
        }
        
    }

    void NextLine()
    {
        
        if (index < lines.Length - 1)
        {
            index++;
            startTalk(lines[index][0]);

            textComp.text = string.Empty;
            StartCoroutine(TypeLine());
            dialoguePortrait.sprite = portraits[index];
        }
        else
        {
            gameObject.SetActive(false);
            stopTalk(lines[index][0]);
        }
    }
}
