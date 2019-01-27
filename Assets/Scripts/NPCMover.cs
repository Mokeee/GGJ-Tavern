using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    public Vector3 TargetPosition;

    public float Duration = 4;


    public void MoveToTargetPosition()
    {
        StartCoroutine(MoveAnimation());
    }

    private IEnumerator MoveAnimation()
    {
        Vector2 startPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;

        float startTime = Time.time;
        float timePassed = 0f;

        while ((timePassed = Time.time - startTime) < Duration)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPosition, TargetPosition, timePassed / Duration);

            yield return new WaitForFixedUpdate();
        }
    }

}
