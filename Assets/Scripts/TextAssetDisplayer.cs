using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAssetDisplayer : MonoBehaviour
{
	public TextAssetReader TextAssetReader;
	public TextMeshProUGUI UIText;

    // Start is called before the first frame update
    void Start()
    {
		TextAssetReader.StartReadingTextAssets();
		StartCoroutine(DisplayGreetings());
    }

	private IEnumerator DisplayGreetings()
	{
		int index = 0;
		while(true)
		{
			UIText.text = TextAssetReader.TextAssets.Greetings[index].Personality.ToString();
			index = (index + 1) % TextAssetReader.TextAssets.Greetings.Count;
			yield return new WaitForSeconds(1.0f);
		}
	}
}
