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

	public Question()
	{
		Answers = new Dictionary<ReactionType, Snippet>();
	}
    
    /// <param name="character">
    /// The character type to get a reaction for.
    /// </param>
    /// 
    /// <returns>
    /// Returns the ReactionType of a given character to this question.
    /// </returns>
    public ReactionType GetReactionFor(Personality character)
    {
        if(character == Personality.Heroic)
        {
            switch(Personality)
            {
                case Personality.Heroic:
                    return ReactionType.Comforting;
                case Personality.Noble:
                    return ReactionType.Annoying;
                case Personality.Shy:
                    return ReactionType.Okay;
            }
        }
        else if (character == Personality.Noble)
        {
            switch (Personality)
            {
                case Personality.Heroic:
                    return ReactionType.Okay;
                case Personality.Noble:
                    return ReactionType.Comforting;
                case Personality.Shy:
                    return ReactionType.Annoying;
            }
        }
        else if (character == Personality.Shy)
        {
            switch (Personality)
            {
                case Personality.Heroic:
                    return ReactionType.Annoying;
                case Personality.Noble:
                    return ReactionType.Okay;
                case Personality.Shy:
                    return ReactionType.Comforting;
            }
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


    public Snippet GetAnswerForPersonality(Personality character)
    {
        return GetAnswer(GetReactionFor(character));
    }

}
