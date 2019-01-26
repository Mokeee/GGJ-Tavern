using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ConversationOverEvent : UnityEvent<NPC> { }

public class DialogSystem : MonoBehaviour
{
    public DialogDisplayer DialogDisplayer;

    public ConversationOverEvent ConversationOverEvent;

    private int MaxQuestions = 3;
    private int QuestionSelectionSize = 3;

    private TextAssets TextAssets;

    private NPC NPC;

    private Adventure StoryArc;

    private Queue<string> Adventure;
    private Queue<QuestionSet> QuestionSets;
    private Snippet Farewell;


    public void StartDialog(NPC npc, bool isLeaving)
    {
        NPC = npc;

        Adventure = new Queue<string>();
        QuestionSets = new Queue<QuestionSet>();

        if (!isLeaving)
        {
            StoryArc = TextAssets.GetRandomAdventure();
            GenerateGreeting();
            GenerateAdventure();
            GenerateQuestions();
        }
        else
        {
            //Display farewell and leave#
            EndDialog();
        }
    }


    private void GenerateAdventure()
    {
        Snippet pre = StoryArc.GetRandomSnippet(SnippetType.Pre);

        if (pre.Need != Need.None)
        {
            NPC.Needs.Add(pre.Need);
        }

        Adventure.Enqueue(pre.Text);

        Snippet main = StoryArc.GetRandomSnippet(SnippetType.Main);

        if (main.Need != Need.None)
        {
            NPC.Needs.Add(main.Need);
        }

        Adventure.Enqueue(main.Text);


        Snippet post = StoryArc.GetRandomSnippet(SnippetType.Post);

        if (post.Need != Need.None)
        {
            NPC.Needs.Add(post.Need);
        }

        Adventure.Enqueue(post.Text);
    }


    public void GenerateGreeting()
    {
        int index = Random.Range(0, TextAssets.Greetings.Count);

        while (TextAssets.Greetings[index].Personality != NPC.Character)
        {
            index = ++index % TextAssets.Greetings.Count;
        }

        Adventure.Enqueue(TextAssets.Greetings[index].Text);
    }


    private void GenerateQuestions()
    {
        for (int i = 0; i < MaxQuestions; ++i)
        {
            Question annoying = GetRandomQuestion(ReactionType.Annoying);
            Question okay = GetRandomQuestion(ReactionType.Okay);
            Question comforting = GetRandomQuestion(ReactionType.Comforting);

            QuestionSet questionSet = new QuestionSet(annoying, okay, comforting);
            questionSet.Shuffle();

            QuestionSets.Enqueue(questionSet);
        }

    }


    private Question GetRandomQuestion(ReactionType type)
    {
        int index = Random.Range(0, TextAssets.Questions.Count);

        while (TextAssets.Questions[index].Personality != NPC.Character)
        {
            index = ++index % TextAssets.Questions.Count;
        }

        return TextAssets.Questions[index];
    }


    /// <summary>
    /// Tell the dialog system to do its next step.
    /// </summary>
    public void Next()
    {
        if (Adventure.Count > 0)
        {
            NextSnippet();
        }
        else
        {
            if (QuestionSets.Count > 0 && NPC.ComfortLevel != 0)
            {
                NextQuestions();
            }
            else
            {
                EndDialog();
            }
        }
    }


    /// <summary>
    /// Reqeuest the next adventure snippet
    /// </summary>
    private void NextSnippet()
    {
        DialogDisplayer.DisplaySnippet(Adventure.Dequeue());
    }


    private void NextQuestions()
    {
        DialogDisplayer.DisplayQuestions(QuestionSets.Peek().ToStringList());
    }


    /// <summary>
    /// An interface to pass the chosen question to the DialogSystem.
    /// It will react by telling the GUI to display the answer.
    /// </summary>
    /// <param name="questionIndex">
    /// The index of the chosen question.
    /// </param>
    public void NextAnswer(int questionIndex)
    {
        //The question chosen by the player
        Question chosen = QuestionSets.Dequeue().Questions[questionIndex];

        //The reaction the question would provoke
        ReactionType reaction = chosen.GetReactionFor(NPC.Character);

        //The answer that our npc would give to the chosen question.
        Snippet answer = chosen.GetAnswer(reaction);

        //If the answer contains a special need, add it to the NPC.
        if (answer.Need != Need.None)
        {
            NPC.AddSpecialNeed(answer.Need);
        }

        //Make the NPC react to the question:
        switch (reaction)
        {
            case ReactionType.Annoying:
                NPC.ComfortLevel -= 1f;
                break;
            case ReactionType.Okay:
                NPC.ComfortLevel -= 0.5;
                break;
            default:
                break;
        }

        if (NPC.ComfortLevel == 0 || QuestionSets.Count == 0)
        {
            //Only show the answer. The dilog will end when "..." is pressed and Next ist executed.
            DialogDisplayer.DisplaySnippet(answer.Text);
        }
        else
        {
            //Take the matching reponse to the chosen question and discard the question. Also display the next questions.
            DialogDisplayer.DisplayAnswer(answer.Text, QuestionSets.Peek().ToStringList());
        }
    }


    private void EndDialog()
    {
        ConversationOverEvent.Invoke(NPC);
    }

}
