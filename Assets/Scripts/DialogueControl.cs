using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueControl : MonoBehaviour
{
    public static DialogueControl instance;
  
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

    public stage[] stages;
    
    public Text nameText;
    public Text dialogueText;
    public GameObject nextButton;
    public Image dialogImage;
    public Sprite[] images = new Sprite[5];
    public Sprite[] CharacterSprites = new Sprite[5];
    public GameObject imageObject;
    public GameObject character;
    public Animator characterAnim;
    private int stageNum = 0;
    private Queue<Dialogue> DialogueSection;
    private Queue<string> sentences;
    private int selectedCard = -1;
    public bool choosing = false;
    private string name;
    public GameObject dialogueBox;
    public GameObject menuButton;
    public Dialogue monologue;
    public Dialogue monologue2;
    private bool startMonologue=true;
    private bool end = false;
    private bool runMon1 = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        DialogueSection = new Queue<Dialogue>();
        StartMonologue();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void CardSelected(int cardNum)
    {
        if (choosing = true && selectedCard == -1)
        {
            selectedCard = cardNum;
            RunStageDialogue(stageNum, selectedCard);
            choosing = false;
        }
    }
    public void RunStagePrompt(int stage)
    {
        HideCharacter();
        if (end==false)
        {
            imageObject.SetActive(true);
            DialogueSection.Clear();
            StopAllCoroutines();
            sentences.Clear();
            nameText.text = "Choose who to ask the following question to:";
            dialogImage.sprite = images[3];
            nextButton.SetActive(false);
            dialogueText.text = stages[stage].startDialogue;
            selectedCard = -1;
            choosing = true;
        }
       
    }
    public void RunStageDialogue(int stage,int characterNum)
    {
        ShowCharacter(characterNum);
        nextButton.SetActive(true);
        DialogueSection.Clear();
        Conversation currentConv = stages[stage].conversations[characterNum];
        foreach (Dialogue dialogue in currentConv.dialogues)
        {
            DialogueSection.Enqueue(dialogue); 
        }
        RunDialogue();
    }

    public void RunDialogue()
    {
        
        if (DialogueSection.Count == 0)
        {
            stageNum++;
            if (stageNum>=6)
            {
                EndGame();
            }
            RunStagePrompt(stageNum);

            return;
        }
        Dialogue dialogue = DialogueSection.Dequeue();
        nameText.text = dialogue.name;
        SwitchDialogueImage(dialogue.name);
        sentences.Clear();
        
        foreach (string sentence in dialogue.sentences)
        {
            if (sentence != "" || sentence != null)
            {
                sentences.Enqueue(sentence);
            }
        }
        DisplaySentence();
    }
    public void StartMonologue()
    {
       
        
        Dialogue dialogue;
        ShowCharacter(4);
        if (runMon1==false)
        {
            dialogue = monologue;
            
        }
        else
        {
            ShowCharacter(3);
            dialogue = monologue2;
        }
        nameText.text = dialogue.name;
        SwitchDialogueImage(dialogue.name);
        sentences.Clear();
        
        
        foreach (string sentence in dialogue.sentences)
        {
            if (sentence != "" || sentence != null)
            {
                sentences.Enqueue(sentence);
            }
        }
        DisplaySentence();
    }

    public void EndGame()
    {
        
        nameText.text = "";
        StopAllCoroutines();
        StartCoroutine(OneByOneChar("The End"));
        end = true;
        nextButton.SetActive(false);
        menuButton.SetActive(true);
    }
    private void SwitchDialogueImage(string name)
    {
        imageObject.SetActive(true);
        switch (name)
        {
            case "Becky":
                dialogImage.sprite = images[0];
                break;
            case "Olivia":
                dialogImage.sprite = images[1];
                break;
            case "Gary":
                dialogImage.sprite = images[2];
                break;
            case "You":
                dialogImage.sprite = images[3];
                break;
            default:
                imageObject.SetActive(false);
                break;
        }
    }

    private void ShowCharacter(int characterVal)
    {
        character.GetComponent<SpriteRenderer>().sprite = CharacterSprites[characterVal];
        characterAnim.SetBool("CharacterShown", true);
    }
    private void HideCharacter()
    {
        characterAnim.SetBool("CharacterShown", false);
    }

    public void DisplaySentence()
    {
        if (sentences.Count==0)
        {
            if (startMonologue == true )
            {
                if (runMon1 == false)
                {
                    runMon1 = true;
                    StartMonologue();
                }
                else
                {
                    startMonologue = false;
                    RunStagePrompt(0);
                }
               
            }
            else
            {
                RunDialogue();
            }
            
            return;
        }
        string sentence = sentences.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(OneByOneChar(sentence));
    }

    IEnumerator OneByOneChar(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


}
