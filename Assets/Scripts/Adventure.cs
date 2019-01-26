using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnippetType
{
	Greeting,
	Pre,
	Main,
	Post
}

/// <summary>
/// Class that contains adventure text snippets.
/// </summary>
public class Adventure
{
	public Dictionary<SnippetType, List<Snippet>> AdventureSnippetDictionary;

	public Adventure()
	{
		AdventureSnippetDictionary = new Dictionary<SnippetType, List<Snippet>>();
		AdventureSnippetDictionary.Add(SnippetType.Pre, new List<Snippet>());
		AdventureSnippetDictionary.Add(SnippetType.Main, new List<Snippet>());
		AdventureSnippetDictionary.Add(SnippetType.Post, new List<Snippet>());
	}

	/// <summary>
	/// Returns a random snippet for given snippet type.
	/// </summary>
	/// <param name="type">Type of the snippet</param>
	/// <returns></returns>
	public Snippet GetRandomSnippet(SnippetType type)
	{
		var snippets = AdventureSnippetDictionary[type];

        var randomIndex = Random.Range(0, snippets.Count);

		return snippets[randomIndex];
	}
}
