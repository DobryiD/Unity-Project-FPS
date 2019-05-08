using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip highlighted;
    public AudioClip pressed;
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void HighlightedSound() {
        source.PlayOneShot(highlighted);

    }
    public void PressedSound() {
        source.PlayOneShot(pressed);
    }
}
