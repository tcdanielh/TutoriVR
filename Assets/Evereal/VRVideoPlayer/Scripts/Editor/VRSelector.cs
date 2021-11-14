/* Copyright (c) 2020-present Evereal. All rights reserved. */

using System.Collections.Generic;
using UnityEditor;

namespace Evereal.VRVideoPlayer
{
  public class VRSelector
  {
    private static readonly Dictionary<VRDeviceType, string> VR_DEFINE = new Dictionary<VRDeviceType, string>()
    {
      {
        VRDeviceType.None, "" // The default case will use Unity Video Player
      },
      {
        VRDeviceType.Oculus, "VRVIDEOPLAYER_OCULUS"
      },
      {
        VRDeviceType.SteamVR, "VRVIDEOPLAYER_STEAMVR"
      },
    };

    public static void SelectVR(VRDeviceType vrDeviceType)
    {
      string vrDeviceDefine = VR_DEFINE[vrDeviceType];
#if UNITY_STANDALONE
      UpdateDefineSymbols(vrDeviceDefine, BuildTargetGroup.Standalone);
#elif UNITY_ANDROID
      UpdateDefineSymbols(vrDeviceDefine, BuildTargetGroup.Android);
#elif UNITY_IOS
      UpdateDefineSymbols(vrDeviceDefine, BuildTargetGroup.iOS);
#elif UNITY_WEBGL
			UpdateDefineSymbols(vrDeviceDefine, BuildTargetGroup.WebGL);
#endif
    }

    public static void ResetVR()
    {
      string defaultDefine = VR_DEFINE[VRDeviceType.None];
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

    private static void UpdateDefineSymbols(string vrDefine, BuildTargetGroup target)
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
          foreach (string existedPlayer in VR_DEFINE.Values)
          {
            if (define == existedPlayer)
            {
              updateDefine = currentDefine.Replace(existedPlayer, vrDefine);
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
          if (vrDefine != "")
          {
            updateDefine = currentDefine + ";" + vrDefine;
          }
        }
      }
      else
      {
        updateDefine = vrDefine;
      }
      PlayerSettings.SetScriptingDefineSymbolsForGroup(target, updateDefine);
    }
  }
}