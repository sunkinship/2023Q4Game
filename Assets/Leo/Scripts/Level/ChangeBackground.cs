using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    public GameObject bg1, bg2, bg3, bg4;

    public int bg;

    public void ChangeBG()
    {
        switch (bg)
        {
            case 1:
                SetBG1();
                break;
            case 2:
                SetBG2();
                break;
            case 3:
                SetBG3();
                break;
            case 4:
                SetBG4();
                break;
        }
    }

    private void SetBG1()
    {
        bg2.SetActive(false);
        bg3.SetActive(false);
        bg4.SetActive(false);
        bg1.SetActive(true);
    }

    private void SetBG2()
    {
        bg1.SetActive(false);
        bg3.SetActive(false);
        bg4.SetActive(false);
        bg2.SetActive(true);
    }

    private void SetBG3()
    {
        bg1.SetActive(false);
        bg2.SetActive(false);
        bg4.SetActive(false);
        bg3.SetActive(true);
    }

    private void SetBG4()
    {
        bg1.SetActive(false);
        bg2.SetActive(false);
        bg3.SetActive(false);
        bg4.SetActive(true);
    }
}
