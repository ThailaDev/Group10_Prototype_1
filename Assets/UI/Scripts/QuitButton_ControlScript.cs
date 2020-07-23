using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton_ControlScript : MonoBehaviour
{
    public Animator QuitButton;
    public AudioSource ButtonSound;
    
    public float ClosingGameTime;


    // Start is called before the first frame update
    void Start()
    {
        ButtonSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonSelected()
    {
        QuitButton.SetBool("Selected", true);
    }

    public void ButtonDeselected()
    {
        QuitButton.SetBool("Selected", false);
    }

    public void ButtonPressed()
    {
        QuitButton.SetBool("Pressed", true);
        ButtonSound.Play();
        StartCoroutine("ChangeScene");
    }



    IEnumerator ChangeScene()
    {
        QuitButton.SetBool("Pressed", false);

        ButtonSound.Stop();

        Application.Quit();

        yield return new WaitForSeconds(ClosingGameTime);

        
    }

}

