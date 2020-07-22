using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Steps;

[System.Serializable]
public class stage // class will contain all possible conversations in a specific stage
{
    public string round;
    [TextArea(3, 20)]
    public string startDialogue;
    public Conversation[] conversations;
}
[System.Serializable]
public class Conversation // contain a specific conversation between player and a specific character
{
    public string convCharacter;//character that is having the conversation
    public Dialogue[] dialogues;
}

[System.Serializable]
public class Dialogue   // single dialogue entry containing all its sentences
{
    public string name;//character name/player name
    
    [TextArea(3,10)]
    public string[] sentences;
}

