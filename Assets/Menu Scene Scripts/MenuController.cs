using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void CardSelect(int cardNum)
    {
        switch (cardNum)
        {
            case 0: 
                break;
            case 1: Play();
                break;
            case 2: Quit();
                break;
            default:
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

}
