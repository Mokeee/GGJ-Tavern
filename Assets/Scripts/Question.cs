using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReactionType
{
    Annoying,
    Okay,
    Comforting
}

public class Question
{
	public Personality Personality;
	public string Text;
    public Dictionary<ReactionType, Snippet> Answers;

    
    /// <param name="character">
    /// The character type to get a reaction for.
    /// </param>
    /// 
    /// <returns>
    /// Returns the ReactionType of a given character to this question.
    /// </returns>
    public ReactionType GetReactionFor(Personality character)
    {
        if (Personality == character)
        {
            return ReactionType.Comforting;
        }

        return ReactionType.Annoying;
    }


    /// <returns>
    /// Returns the Answer that is used for the given type of reaction.
    /// </returns>
    public Snippet GetAnswer(ReactionType reaction)
    {
        return Answers[reaction];
    }

}
