using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This event will be invoked when the player requests the next line to pop up.
/// </summary>
public class RequestNextEvent : UnityEvent<bool> { }

/// <summary>
/// this event will be invoked when the player has chosen the next question.
/// </summary>
public class QuestionChosenEvent : UnityEvent<int> { }


public class DialogDisplayer : MonoBehaviour
{
    public RequestNextEvent RequestNextEvent = new RequestNextEvent();
    public QuestionChosenEvent QuestionChosenEvent = new QuestionChosenEvent();

    public TextMeshProUGUI TextMesh;
    public Button Next;
    public Button[] QuestionButtons = new Button[3];

    [Header("Animation")]
    public CanvasGroup CG;
    public float Speed;

    private Text[] Texts;

    public void Start()
    {
        Texts = new Text[QuestionButtons.Length];

        for (int i = 0; i < QuestionButtons.Length; i++)
        {
            var index = i;
            QuestionButtons[i].onClick.AddListener(delegate { ChooseNextQuestion(index); });
            Texts[i] = QuestionButtons[i].GetComponentInChildren<Text>();
        }

        Next.onClick.AddListener(delegate { RequestNext(false); });
    }


    public void ChooseNextQuestion(int question)
    {
        QuestionChosenEvent.Invoke(question);
    }


    private void RequestNext(bool isLeaving)
    {
        RequestNextEvent.Invoke(isLeaving);
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
        Next.gameObject.SetActive(true);
        DisableQuestionButtons();
    }


    public void DisplayQuestions(List<string> questions)
    {
        for (int i = 0; i < questions.Count; ++i)
        {
            QuestionButtons[i].gameObject.SetActive(true);
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
            button.gameObject.SetActive(false);
        }
    }

    public void SetInvisible()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    public void SetVisible()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (CG.alpha < 1.0f)
        {
            CG.alpha += Time.deltaTime * Speed;
            yield return null;
        }

        CG.alpha = 1.0f;
        CG.blocksRaycasts = true;
    }


    private IEnumerator FadeOut()
    {
        while (CG.alpha > 0.0f)
        {
            CG.alpha -= Time.deltaTime * Speed;
            yield return null;
        }

        CG.alpha = 0.0f;
        CG.blocksRaycasts = false;
    }
}
