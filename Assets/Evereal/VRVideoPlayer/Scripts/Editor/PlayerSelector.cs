/* Copyright (c) 2020-present Evereal. All rights reserved. */

using System.Collections.Generic;
using UnityEditor;

namespace Evereal.VRVideoPlayer
{
  public class PlayerSelector
  {
    private static readonly Dictionary<MediaPlayerType, string> PLAYER_DEFINE = new Dictionary<MediaPlayerType, string>()
    {
      {
        MediaPlayerType.UnityVideoPlayer, "" // The default case will use Unity Video Player
      },
      {
        MediaPlayerType.AVProVideo, "VRVIDEOPLAYER_AVPROVIDEO"
      },
      {
        MediaPlayerType.AVProVideo2, "VRVIDEOPLAYER_AVPROVIDEO_V2"
      },
      {
        MediaPlayerType.EasyMovieTexture, "VRVIDEOPLAYER_EASYMOVIETEXTURE"
      },
    };

    public static void SelectPlayer(MediaPlayerType player)
    {
      string playerDefine = PLAYER_DEFINE[player];
#if UNITY_STANDALONE
      UpdateDefineSymbols(playerDefine, BuildTargetGroup.Standalone);
#elif UNITY_ANDROID
      UpdateDefineSymbols(playerDefine, BuildTargetGroup.Android);
#elif UNITY_IOS
      UpdateDefineSymbols(playerDefine, BuildTargetGroup.iOS);
#elif UNITY_WEBGL
			UpdateDefineSymbols(playerDefine, BuildTargetGroup.WebGL);
#endif
    }

    public static void ResetPlayer()
    {
      string defaultDefine = PLAYER_DEFINE[MediaPlayerType.UnityVideoPlayer];
#if UNITY_STANDALONE
      UpdateDefineSymbols(defaultDefine, BuildTargetGroup.Standalone);
#elif UNITY_ANDROID
      UpdateDefineSymbols(defaultDefine, BuildTargetGroup.Android);
#elif UNITY_IOS
			UpdateDefineSymbols(defaultDefine, BuildTargetGroup.iOS);
#elif UNITY_WEBGL
			UpdateDefineSymbols(defaultDefine, BuildTargetGroup.WebGL);
#endif
    }

    private static void UpdateDefineSymbols(string playerDefine, BuildTargetGroup target)
    {
      string currentDefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);
      string updateDefine = currentDefine;
      if (currentDefine.Length > 0)
      {
        string[] defines = currentDefine.Split(';');
        bool updated = false;
        foreach (string define in defines)
        {
          // Check if define is player define
          foreach (string existedPlayer in PLAYER_DEFINE.Values)
          {
            if (define == existedPlayer)
            {
              updateDefine = currentDefine.Replace(existedPlayer, playerDefine);
              updated = true;
              break;
            }
          }
          if (updated)
          {
            break;
          }
        }
        if (!updated)
        {
          if (playerDefine != "")
          {
            updateDefine = currentDefine + ";" + playerDefine;
          }
        }
      }
      else
      {
        updateDefine = playerDefine;
      }
      PlayerSettings.SetScriptingDefineSymbolsForGroup(target, updateDefine);
    }
  }
}