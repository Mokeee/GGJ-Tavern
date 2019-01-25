using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    private string Text { get; }
    private Need SpecialNeed { get; }

    public Answer(string text, Need specialNeed)
    {
        Text = text;
        SpecialNeed = specialNeed;
    }

    /// <summary>
    /// Sets the SpecialNeed to None.
    /// </summary>
    /// 
    /// <param name="text">
    /// The text to use for this answer.
    /// </param>
    public Answer(string text)
    {
        Text = text;
        SpecialNeed = Need.FOO; //Replace with None
    }

}
