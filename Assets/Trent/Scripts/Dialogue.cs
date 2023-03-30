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


    private int index;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
       
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
            
        }
        
    }
    void stopTalk(char talker)
    {
        Debug.Log(talker);
        if (talker == '0')
        {
            animator.SetBool("isTalking", false);
            
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
        }
    }
}
