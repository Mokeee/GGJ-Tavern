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

public class Adventure
{
	public Dictionary<SnippetType, List<AdventureSnippet>> AdventureSnippetDictionary;

	public AdventureSnippet GetRandomSnippet(SnippetType)
	{

	}
}
