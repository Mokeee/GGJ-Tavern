using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSlider : MonoBehaviour
{
    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
