using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextAssetReader : MonoBehaviour
{
	TextAssets TextAssets;

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
		TextAssets.Questions = ReadAnswers();
		TextAssets.Answers = ReadAnswers();
	}

	/// <summary>
	/// Reads the questions json and stores them into text assets.
	/// </summary>
	void List<Question> ReadQuestions()
	{

	}

	/// <summary>
	/// Reads the answers json and stores them into text assets.
	/// </summary>
	void List<Answer> ReadAnswers()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, languageFileName);
		Debug.Log("Path:" + filePath);
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			ReaderData loadedData = JsonUtility.FromJson<ReaderData>(dataAsJson);

			for (int i = 0; i < loadedData.items.Length; i++)
			{
				if (localizationDatabase.ContainsKey(loadedData.items[i].key))
				{
					Debug.LogError(loadedData.items[i].key + " localised twice.");
				}
				else
				{
					localizationDatabase.Add(loadedData.items[i].key, loadedData.items[i].value);
				}

			}

			Debug.Log("Data loaded, dictionary contains: " + localizationDatabase.Count + " entries");
			PlayerPrefs.SetString(PreferenceKeys.Locales.Language, languageFileName);
		}
		else
		{
			Debug.LogError("Cannot find file!");
		}
	}

	/// <summary>
	/// Reads the adventures json and stores them into text assets.
	/// </summary>
	void List<Adventure> ReadAdventures()
	{

	}
}
