/* Copyright (c) 2020-present Evereal. All rights reserved. */

using System.Collections;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(Renderer))]
  public class Fade : MonoBehaviour
  {
    public float fadeDuration = 1f;

    public delegate void FadeOutCompletedEvent();
    public event FadeOutCompletedEvent fadeOutCompleted = delegate { };

    public delegate void FadeInCompletedEvent();
    public event FadeInCompletedEvent fadeInCompleted = delegate { };

    public IEnumerator StartFadeOut()
    {
      Renderer fadeRenderer = GetComponent<Renderer>();
      Material fadeMaterial = fadeRenderer.material;
      Color startColor = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, 0f);
      Color targetColor = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, 1f);
      float fadeStartTime = Time.time;
      float fadeProgress;
      bool fading = true;

      while (fading)
      {
        yield return new WaitForEndOfFrame();
        fadeProgress = Time.time - fadeStartTime;
        if (fadeRenderer != null)
        {
          fadeRenderer.material.color = Color.Lerp(startColor, targetColor, fadeProgress / fadeDuration);
        }
        else
        {
          fading = false;
        }

        if (fadeProgress >= fadeDuration)
        {
          fading = false;
        }
      }
      fadeOutCompleted();
    }

    public IEnumerator StartFadeIn()
    {
      Renderer fadeRenderer = GetComponent<Renderer>();
      Material fadeMaterial = fadeRenderer.material;
      Color startColor = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, 1f);
      Color targetColor = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, 0f);
      float fadeStartTime = Time.time;
      float fadeProgress;
      bool fading = true;

      while (fading)
      {
        yield return new WaitForEndOfFrame();
        fadeProgress = Time.time - fadeStartTime;
        if (fadeRenderer != null)
        {
          fadeRenderer.material.color = Color.Lerp(startColor, targetColor, fadeProgress / fadeDuration);
        }
        else
        {
          fading = false;
        }

        if (fadeProgress >= fadeDuration)
        {
          fading = false;
        }
      }
      fadeInCompleted();
    }
  }
}