using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct ConversationOverData
{
    public NPC npc;
    public bool isLeaving;
}

public class ConversationOverEvent : UnityEvent<ConversationOverData> { }

public class DialogSystem : MonoBehaviour
{
    public DialogDisplayer DialogDisplayer;

    public ConversationOverEvent ConversationOverEvent = new ConversationOverEvent();

    public TextAssetReader TAR;

    private int MaxQuestions = 3;
    private int QuestionSelectionSize = 3;



    private NPC NPC;

    private Adventure StoryArc;

    private Queue<string> Adventure;
    private Queue<QuestionSet> QuestionSets;
    private Snippet Farewell;


    public void Start()
    {
        DialogDisplayer.QuestionChosenEvent.AddListener(NextAnswer);
        TAR.StartReadingTextAssets();
    }


    public void StartDialog(NPC npc, bool isLeaving)
    {
        NPC = npc;

        DialogDisplayer.SetVisible();

        Adventure = new Queue<string>();
        QuestionSets = new Queue<QuestionSet>();

        DialogDisplayer.RequestNextEvent.RemoveAllListeners();
        DialogDisplayer.RequestNextEvent.AddListener(delegate { Next(isLeaving); });

        if (!isLeaving)
        {
            StoryArc = TAR.TextAssets.GetRandomAdventure();
            GenerateGreeting();
            GenerateAdventure();
            GenerateQuestions();
            Next(isLeaving);
        }
        else
        {
            NPC.StayDuration = Random.Range(-GameClock.MaxStayDuration, 0);
            Adventure.Enqueue("ciao bella!");
            Next(isLeaving);
        }
    }


    private void GenerateAdventure()
    {
        Snippet pre = StoryArc.GetRandomSnippet(SnippetType.Pre);

        if (pre.Need != Need.None)
        {
            NPC.Needs.Add(pre.Need);
            if (pre.Need == Need.Tired)
                NPC.StayDuration = Random.Range(1, GameClock.MaxStayDuration);
        }

        Adventure.Enqueue(pre.Text);

        Snippet main = StoryArc.GetRandomSnippet(SnippetType.Main);

        if (main.Need != Need.None)
        {
            NPC.Needs.Add(main.Need);
            if (main.Need == Need.Tired)
                NPC.StayDuration = Random.Range(1, GameClock.MaxStayDuration);
        }

        Adventure.Enqueue(main.Text);


        Snippet post = StoryArc.GetRandomSnippet(SnippetType.Post);

        if (post.Need != Need.None)
        {
            NPC.Needs.Add(post.Need);
            if (post.Need == Need.Tired)
                NPC.StayDuration = Random.Range(1, GameClock.MaxStayDuration);
        }

        Adventure.Enqueue(post.Text);
    }


    public void GenerateGreeting()
    {
        int index = Random.Range(0, TAR.TextAssets.Greetings.Count);

        while (TAR.TextAssets.Greetings[index].Personality != NPC.Character)
        {
            index = (index + 1) % TAR.TextAssets.Greetings.Count;
        }

        Adventure.Enqueue(TAR.TextAssets.Greetings[index].Text);
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
        int index = Random.Range(0, TAR.TextAssets.Questions.Count);
        
        while (TAR.TextAssets.Questions[index].GetReactionFor(NPC.Character) != type)
        {
            index = (index + 1) % TAR.TextAssets.Questions.Count;
        }

        return TAR.TextAssets.Questions[index];
    }


    /// <summary>
    /// Tell the dialog system to do its next step.
    /// </summary>
    public void Next(bool isLeaving)
    {
        if (Adventure.Count > 0)
        {
            NextSnippet();
        }
        else
        {
            if (QuestionSets.Count > 0 && NPC.ComfortLevel > 0)
            {
                NextQuestions();
            }
            else
            {
                EndDialog(isLeaving);
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
            if (answer.Need == Need.Tired)
                NPC.StayDuration = Random.Range(0, GameClock.MaxStayDuration);
        }

        //Make the NPC react to the question:
        switch (reaction)
        {
            case ReactionType.Annoying:
                NPC.ComfortLevel -= 1f;
                break;
            case ReactionType.Okay:
                NPC.ComfortLevel -= 0.5f;
                break;
            case ReactionType.Comforting:
                NPC.ComfortLevel += 0.5f;
                break;
        }

        if (NPC.ComfortLevel <= 0 || QuestionSets.Count == 0)
        {
            //Only show the answer. The dialog will end when "..." is pressed and Next ist executed.
            DialogDisplayer.DisplaySnippet(answer.Text);
        }
        else
        {
            //Take the matching reponse to the chosen question and discard the question. Also display the next questions.
            DialogDisplayer.DisplayAnswer(answer.Text, QuestionSets.Peek().ToStringList());
        }
    }


    private void EndDialog(bool isLeaving)
    {
        var cod = new ConversationOverData();
        cod.npc = NPC;
        cod.isLeaving = isLeaving;
        ConversationOverEvent.Invoke(cod);
        DialogDisplayer.SetInvisible();
    }

}
