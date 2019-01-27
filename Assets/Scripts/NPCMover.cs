using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    public Vector3 TargetPosition;

    public float Duration = 1;


    public void MoveToTargetPosition()
    {
        StartCoroutine(MoveAnimation());
    }

    private IEnumerator MoveAnimation()
    {
        Vector3 startPosition = transform.localPosition;

        float startTime = Time.time;
        float timePassed = 0f;

        while ((timePassed = Time.time - startTime) < Duration)
        {
            transform.position = Vector3.Lerp(startPosition, TargetPosition, timePassed / Duration);

            yield return new WaitForFixedUpdate();
        }
    }

}
