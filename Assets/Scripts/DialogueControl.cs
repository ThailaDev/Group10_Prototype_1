using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        DialogueSection = new Queue<Dialogue>();
        RunStagePrompt(0);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    RunStageDialogue(0,0);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DisplaySentence();
        //}
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    RunStagePrompt(0);
        //}
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
        DialogueSection.Clear();
        StopAllCoroutines();
        sentences.Clear();
        nameText.text = "Choose who to ask the following question to:";
        nextButton.SetActive(false);
        dialogueText.text = stages[stage].startDialogue;
        selectedCard = -1;
        choosing = true;
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

    public void EndGame()
    {
        Debug.Log("end");
        dialogueBox.SetActive(false);
    }
    public void DisplaySentence()
    {
        if (sentences.Count==0)
        {
            RunDialogue();
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
            yield return new WaitForSeconds(.05f);

        }
    }
}
