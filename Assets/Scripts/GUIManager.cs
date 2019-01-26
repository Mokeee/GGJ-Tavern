using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;


    public void Start()
    {
        Instance = this;
    }


    public void DisplayQuestions(List<string> questions)
    {
        //TODO
    }


    public void DisplaySnippet(string text)
    {
        //TODO
    }


    public void HideDialog()
    {
        //TODO
    }
}
