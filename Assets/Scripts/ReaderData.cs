using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ReaderData
{
	public ReaderItem[] items;
}

[System.Serializable]
public class ReaderItem
{
	public string key;
	public string value;
}