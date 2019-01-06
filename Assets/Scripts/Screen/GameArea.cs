using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameArea : MonoBehaviour
{
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }
}