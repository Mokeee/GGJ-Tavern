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
		StartCoroutine(Display());
    }

	private IEnumerator Display()
	{
        int greetingsIndex = 0;
        int questionsIndex = 0;
		while(true)
		{
			UIText.text = TextAssetReader.TextAssets.Greetings[greetingsIndex].Personality.ToString();
			greetingsIndex = (greetingsIndex + 1) % TextAssetReader.TextAssets.Greetings.Count;
            UIText.text += "\n\n" + TextAssetReader.TextAssets.Questions[questionsIndex].Text 
            + "\n" + TextAssetReader.TextAssets.Questions[questionsIndex].Personality.ToString()
            + "\n" + TextAssetReader.TextAssets.Questions[questionsIndex].GetAnswer(ReactionType.Annoying).Text
            + "\n" + TextAssetReader.TextAssets.Questions[questionsIndex].GetAnswer(ReactionType.Okay).Text
            + "\n" + TextAssetReader.TextAssets.Questions[questionsIndex].GetAnswer(ReactionType.Comforting).Text;
            greetingsIndex = (greetingsIndex + 1) % TextAssetReader.TextAssets.Greetings.Count;
            questionsIndex = (questionsIndex + 1) % TextAssetReader.TextAssets.Questions.Count;
            yield return new WaitForSeconds(1.0f);
		}
	}
}
