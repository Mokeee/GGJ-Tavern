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

	/// <summary>
	/// Returns the answer at the given index.
	/// </summary>
	/// <param name="id">Index of answer</param>
	public void GetAnswer(int id)
	{

	}
}
