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
    private int stageNum = 0;
    private Queue<Dialogue> DialogueSection;
    private Queue<string> sentences;
    private int selectedCard = -1;
    public bool choosing = false;
    private string name;
    public GameObject dialogueBox;
    public GameObject menuButton;
    public Dialogue monologue;
    private bool startMonologue=true;
    private bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        DialogueSection = new Queue<Dialogue>();
        StartMonologue(monologue);
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
        if (end==false)
        {
            DialogueSection.Clear();
            StopAllCoroutines();
            sentences.Clear();
            nameText.text = "Choose who to ask the following question to:";
            nextButton.SetActive(false);
            dialogueText.text = stages[stage].startDialogue;
            selectedCard = -1;
            choosing = true;
        }
       
    }
    public void RunStageDialogue(int stage,int characterNum)
    {
        
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
    public void StartMonologue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
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

    
    public void DisplaySentence()
    {
        if (sentences.Count==0)
        {
            if (startMonologue == true)
            {
                startMonologue = false;
                RunStagePrompt(0);
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
