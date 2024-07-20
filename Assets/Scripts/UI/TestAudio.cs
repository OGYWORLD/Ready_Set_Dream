using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnEnable()
    {
        if(audioSource != null)
        {
            audioSource.Play();
            print("����� �����");
        }
    }

    private void OnDisable()
    {
        audioSource.Pause();
        print("����� ����");
    }

}
