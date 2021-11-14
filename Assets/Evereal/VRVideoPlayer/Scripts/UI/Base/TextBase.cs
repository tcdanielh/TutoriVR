/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(TextMesh))]
  public class TextBase : MonoBehaviour
  {
    private TextMesh textMesh;
    public int lengthLimit = -1;
    // private string LOG_FORMAT = "[TextBase] {0}";

    private void Awake()
    {
      textMesh = GetComponent<TextMesh>();
    }

    public void SetText(string text)
    {
      if (lengthLimit > 0 && text.Length > lengthLimit)
      {
        text = text.Substring(0, lengthLimit) + "...";
      }
      if (textMesh == null)
        textMesh = GetComponent<TextMesh>();
      textMesh.text = text;
    }
  }
}
