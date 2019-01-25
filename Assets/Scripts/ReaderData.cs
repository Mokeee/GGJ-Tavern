using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnswerData
{
	public AnswerItem[] items;

	public List<Snippet> ConvertToAnswers()
	{
		var snippets = new List<Snippet>();

		foreach(var item in items)
		{
			var snippet = new Snippet();
			snippet.Text = item.text;
			snippet.Need = item.need;

			snippets.Add(snippet);
		}

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
	public QuestionItem[] items;

	public List<Question> ConvertToQuestions(List<Snippet> answers)
	{
		var questions = new List<Question>();

		foreach (var item in items)
		{
			var question = new Question();
			question.Text = item.text;

			for(int i = 0; i < 3; i++)
			{
				var reactionType = (i == 0) ? ReactionType.Annoying : (i == 1) ? ReactionType.Okay : ReactionType.Comforting;
				question.Answers.Add(reactionType, answers[item.answers[i]]);
			}

			question.Personality = item.personality;

			questions.Add(question);
		}

		return questions;
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
	public AdventureItem[] items;

	public List<Snippet> ConvertToAnswers()
	{
		var snippets = new List<Snippet>();

		foreach (var item in items)
		{
			var snippet = new Snippet();
			snippet.Text = item.text;
			snippet.Need = item.need;

			snippets.Add(snippet);
		}

		return snippets;
	}
}

[System.Serializable]
public class AdventureItem
{
	public string text;
	public Need need;
}