using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSet
{
    public List<Question> Questions;

    public QuestionSet(Question q1, Question q2, Question q3)
    {
        Questions = new List<Question>();
        Questions.Add(q1);
        Questions.Add(q2);
        Questions.Add(q3);
    }

    public void Shuffle()
    {
        for (int i = 0; i < Questions.Count; ++i)
        {
            Swap(i, Random.Range(0, Questions.Count));
        }
    }


    private void Swap(int a, int b)
    {
        Question temp = Questions[a];
        Questions[a] = Questions[b];
        Questions[b] = temp;
    }


    public List<string> ToStringList()
    {
        List<string> list = new List<string>();

        for (int i = 0; i < Questions.Count; ++i)
        {
            list.Add(Questions[i].Text);
        }

        return list;
    }


    public List<Personality> ToPersonalityList()
    {
        List<Personality> list = new List<Personality>();

        for (int i = 0; i < Questions.Count; ++i)
        {
            list.Add(Questions[i].Personality);
        }

        return list;
    }

}
