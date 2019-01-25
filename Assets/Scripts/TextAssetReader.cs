using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextAssetReader : MonoBehaviour
{
	public TextAssets TextAssets;

	/// <summary>
	/// Initializes TextAssets.
	/// </summary>
	private void Start()
	{
	}

	/// <summary>
	/// Starts the json reading process.
	/// </summary>
	public void StartReadingTextAssets()
	{
		TextAssets = new TextAssets();

		TextAssets.Answers = ReadAnswers();
		TextAssets.Questions = ReadQuestions(TextAssets.Answers);
		TextAssets.Greetings = ReadGreetings();
		TextAssets.Adventures = ReadAdventures();
	}

	/// <summary>
	/// Reads the questions json and stores them into text assets.
	/// </summary>
	List<Question> ReadQuestions(List<Snippet> answers)
	{
		var loadedData = new QuestionData();
		string filePath = Path.Combine(Application.streamingAssetsPath, "Questions.json");
		Debug.Log("Path:" + filePath);
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			loadedData = JsonUtility.FromJson<QuestionData>(dataAsJson);
		}
		else
		{
			Debug.LogError("Cannot find file!");
		}

		return loadedData.ConvertToQuestions(answers);
	}

	/// <summary>
	/// Reads the answers json and stores them into text assets.
	/// </summary>
	List<Snippet> ReadAnswers()
	{
		AnswerData loadedData = new AnswerData();
		string filePath = Path.Combine(Application.streamingAssetsPath, "Answers.json");
		Debug.Log("Path:" + filePath);
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			loadedData = JsonUtility.FromJson<AnswerData>(dataAsJson);
		}
		else
		{
			Debug.LogError("Cannot find file!");
		}

		return loadedData.ConvertToAnswers();
	}

	/// <summary>
	/// Reads the adventures json and stores them into text assets.
	/// </summary>
	List<Adventure> ReadAdventures()
	{
		AdventureData loadedData = new AdventureData();
		string filePath = Path.Combine(Application.streamingAssetsPath, "Adventures.json");
		Debug.Log("Path:" + filePath);
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			loadedData = JsonUtility.FromJson<AdventureData>(dataAsJson);
		}
		else
		{
			Debug.LogError("Cannot find file!");
		}

		return loadedData.ConvertToAdventure();
	}

	/// <summary>
	/// Reads the greetings json and stores them into text assets.
	/// </summary>
	List<Greeting> ReadGreetings()
	{
		GreetingData loadedData = new GreetingData();
		string filePath = Path.Combine(Application.streamingAssetsPath, "Greetings.json");
		Debug.Log("Path:" + filePath);
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			loadedData = JsonUtility.FromJson<GreetingData>(dataAsJson);
		}
		else
		{
			Debug.LogError("Cannot find file!");
		}

		return loadedData.ConvertToGreeting();
	}
}
