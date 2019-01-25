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
		TextAssets = new TextAssets();
	}

	/// <summary>
	/// Starts the json reading process.
	/// </summary>
	public void StartReadingTextAssets()
	{
		TextAssets.Answers = ReadAnswers();
		TextAssets.Questions = ReadQuestions(TextAssets.Answers);
		TextAssets.Adventures = ReadAdventures();
	}

	/// <summary>
	/// Reads the questions json and stores them into text assets.
	/// </summary>
	List<Question> ReadQuestions(List<Snippet> answers)
	{
		var loadedData = new QuestionData();
		string filePath = Path.Combine(Application.streamingAssetsPath, "answers.json");
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
		string filePath = Path.Combine(Application.streamingAssetsPath, "answers.json");
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
		AnswerData loadedData = new AnswerData();
		string filePath = Path.Combine(Application.streamingAssetsPath, "answers.json");
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
}
