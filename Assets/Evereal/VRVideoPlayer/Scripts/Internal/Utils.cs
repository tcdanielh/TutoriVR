/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System.IO;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class Utils
  {
    public static string GetFilePath(string path, VideoSource source)
    {
      string filePath = string.Empty;
      switch (source)
      {
        case VideoSource.ABSOLUTE_URL:
          filePath = path;
          break;
        case VideoSource.FROM_PROJECT_FOLDER:
          filePath = Path.Combine(Application.dataPath, path);
          break;
        case VideoSource.FROM_STREAMING_ASSETS:
          filePath = Path.Combine(Application.streamingAssetsPath, path);
          break;
      }
      return filePath;
    }
  }
}