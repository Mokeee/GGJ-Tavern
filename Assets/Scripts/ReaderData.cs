using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnswerData
{
	public AnswerItem[] answers;

	public List<Snippet> ConvertToAnswers()
	{
		var snippets = new List<Snippet>();

		foreach(var item in answers)
		{
			var snippet = new Snippet();
			snippet.Text = item.text;
			snippet.Need = item.need;

			snippets.Add(snippet);
		}
        Debug.Log(snippets.Count);
		return snippets;
	}
}

[System.Serializable]
public class AnswerItem
{
	public string text;
	public Need need;
}

[System.Serializable]
public class QuestionData
{
	public QuestionItem[] questions;

	public List<Question> ConvertToQuestions(List<Snippet> answers)
	{
		var newQuestions = new List<Question>();

		foreach (var item in this.questions)
		{
			var question = new Question();
			question.Text = item.text;

			for(int i = 0; i < 3; i++)
			{
				var reactionType = (i == 0) ? ReactionType.Annoying : (i == 1) ? ReactionType.Okay : ReactionType.Comforting;
                Debug.Log(item.answers[i] + " : " + answers[item.answers[i]].Text);
				question.Answers.Add(reactionType, answers[item.answers[i]]);
			}

			question.Personality = item.personality;

			newQuestions.Add(question);
		}

		return newQuestions;
	}
}

[System.Serializable]
public class QuestionItem
{
	public string text;
	public Personality personality;
	public int[] answers;
}

//TODO: DESERIALIZE ADVENTURES
[System.Serializable]
public class AdventureData
{
	public AdventureItem[] adventures;

	public List<Adventure> ConvertToAdventure()
	{
		var adventuresData = new List<Adventure>();
		var adventureItemsByID = new Dictionary<int, List<AdventureItem>>();

		foreach (var item in this.adventures)
		{
			var adventureItemList = new List<AdventureItem>();

			if (adventureItemsByID.ContainsKey(item.id))
				adventureItemsByID.TryGetValue(item.id, out adventureItemList);

			adventureItemList.Add(item);

			if(adventureItemsByID.ContainsKey(item.id))
				adventureItemsByID[item.id] = adventureItemList;
			else
				adventureItemsByID.Add(item.id, adventureItemList);
		}

		foreach(var key in adventureItemsByID.Keys)
		{
			var adventure = new Adventure();

			foreach(var item in adventureItemsByID[key])
			{
				var snippet = new Snippet();
				snippet.Text = item.text;
				snippet.Need = item.need;
                Debug.Log(item.type);
				adventure.AdventureSnippetDictionary[item.type].Add(snippet);
			}

			adventuresData.Add(adventure);
		}

		return adventuresData;
	}
}

[System.Serializable]
public class AdventureItem
{
	public int id;
	public string text;
	public Need need;
	public SnippetType type;
}

[System.Serializable]
public class GreetingData
{
	public GreetingItem[] greetings;

	public List<Greeting> ConvertToGreeting()
	{
		var greetings = new List<Greeting>();

		foreach (var item in this.greetings)
		{
			var greeting = new Greeting();
			greeting.Text = item.text;
			greeting.Personality = item.personality;

			greetings.Add(greeting);
		}

		return greetings;
	}
}

[System.Serializable]
public class GreetingItem
{
	public string text;
	public Personality personality;
}