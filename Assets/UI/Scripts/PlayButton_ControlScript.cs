using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton_ControlScript : MonoBehaviour
{
    public Animator PlayButton;
    public AudioSource ButtonSound;
    public int SceneNumber;
    public float SceneChangeTime;

   
    // Start is called before the first frame update
    void Start()
    {
        ButtonSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonSelected()
    {
        PlayButton.SetBool("Selected", true);
    }

    public void PlayButtonDeselected()
    {
        PlayButton.SetBool("Selected", false);
    }

    public void PlayButtonPressed()
    {
        PlayButton.SetBool("Pressed", true);
        ButtonSound.Play();
        StartCoroutine("ChangeScene");
    }

  

   IEnumerator ChangeScene()
    {
        PlayButton.SetBool("Pressed", false);

        ButtonSound.Stop();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SceneNumber);

        yield return new WaitForSeconds(SceneChangeTime);
    }
}
