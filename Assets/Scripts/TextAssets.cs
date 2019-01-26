using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAssets
{
	[SerializeField]
	public List<Question> Questions;
	[SerializeField]
	public List<Snippet> Answers;
	[SerializeField]
	public List<Adventure> Adventures;
	[SerializeField]
	public List<Greeting> Greetings;
    [SerializeField]
    public List<Snippet> Farewells;

	/// <summary>
	/// Returns the answer at the given index.
	/// </summary>
	/// <param name="id">Index of answer</param>
	public void GetAnswer(int id)
	{

	}

    public Adventure GetRandomAdventure()
    {
        return Adventures[Random.Range(0, Adventures.Count)];
    }


    public Greeting GetRandomGreeting()
    {
        return Greetings[Random.Range(0, Greetings.Count)];
    }


    public Snippet GetRandomFarewell()
    {
        return Farewells[Random.Range(0, Farewells.Count)];
    }
}
