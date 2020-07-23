using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsButton_ControlScript : MonoBehaviour
{
    public Animator CreditsButton;
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

    public void ButtonSelected()
    {
        CreditsButton.SetBool("Selected", true);
    }

    public void ButtonDeselected()
    {
        CreditsButton.SetBool("Selected", false);
    }

    public void ButtonPressed()
    {
        CreditsButton.SetBool("Pressed", true);
        ButtonSound.Play();
        StartCoroutine("ChangeScene");
    }



    IEnumerator ChangeScene()
    {
        CreditsButton.SetBool("Pressed", false);

        ButtonSound.Stop();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SceneNumber);

        yield return new WaitForSeconds(SceneChangeTime);
    }

}
