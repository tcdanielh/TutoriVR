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
        audioSource.Play();
        audioSource.loop = true;
    }

    public void SetTime(float t)
    {
        audioSource.time = t;
    }

}
