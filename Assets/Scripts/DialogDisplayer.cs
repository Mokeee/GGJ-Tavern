using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This event will be invoked when the player requests the next line to pop up.
/// </summary>
public class RequestNextEvent : UnityEvent { }

/// <summary>
/// this event will be invoked when the player has chosen the next question.
/// </summary>
public class QuestionChosenEvent : UnityEvent<int> { }


public class DialogDisplayer : MonoBehaviour
{
    public RequestNextEvent RequestNextEvent;
    public QuestionChosenEvent QuestionChosenEvent;

    public TextMeshPro TextMesh;

    public Button Next;

    public Button[] QuestionButtons;
    private Text[] Texts;

    public void Start()
    {
        Texts = new Text[QuestionButtons.Length];

        for (int i = 0; i < QuestionButtons.Length; ++i)
        {
            QuestionButtons[i].onClick.AddListener(delegate { ChooseNextQuestion(i); });
            Texts[i] = QuestionButtons[i].GetComponentInChildren<Text>();
        }

        Next.onClick.AddListener(RequestNext);
    }


    public void ChooseNextQuestion(int question)
    {
        QuestionChosenEvent.Invoke(question);
    }


    private void RequestNext()
    {
        RequestNextEvent.Invoke();
    }


    /// <summary>
    /// Displays the snippet and activates the Nex-Button.
    /// </summary>
    /// <param name="snippet">
    /// The snippet to display.
    /// </param>
    public void DisplaySnippet(string snippet)
    {
        TextMesh.SetText(snippet);
        Next.enabled = true;
        DisableQuestionButtons();
    }


    public void DisplayQuestions(List<string> questions)
    {
        for (int i = 0; i < questions.Count; ++i)
        {
            QuestionButtons[i].enabled = true;
            Texts[i].text = questions[i];
        }
    }


    public void DisplayAnswer(string answer, List<string> questions)
    {
        TextMesh.SetText(answer);
        DisplayQuestions(questions);
    }


    private void DisableQuestionButtons()
    {
        foreach (Button button in QuestionButtons)
        {
            button.enabled = false;
        }
    }

}
