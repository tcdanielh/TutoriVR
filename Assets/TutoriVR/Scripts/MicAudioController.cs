using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicAudioController : MonoBehaviour
{
    public const string audioName = "mic.wav";

    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        //soundPath = Application.persistentDataPath + "/recording/capture/";
        //StartCoroutine(LoadAudio());
    }

    private void Update()
    {
        //Debug.Log(audioSource.time);
    }

    public IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile("file://" + soundPath, audioName);
        yield return request;
        audioClip = request.GetAudioClip(false, true);
        audioClip.name = audioName;
        audioSource.clip = audioClip;
        Debug.Log(audioSource.clip);
        PlayAudioFile();
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        Debug.Log(audioToLoad);
        WWW request = new WWW(audioToLoad);
        return request;
    }

    public void PlayAudioFile()
    {
        Debug.Log("in play audio");
        //audioSource.clip = audioClip;
        SetTime(0);
        audioSource.Play();
    }

    public void SetTime(float t)
    {
        //Debug.Log(t);
        audioSource.Pause();
        audioSource.time = t;
        //Debug.Log(audioSource.time);
        //Debug.Log("settime");
    }

}
