/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;
using UnityEditor;

namespace Evereal.VRVideoPlayer
{
  public class MenuEditor : MonoBehaviour
  {
    private const string LOG_FORMAT = "[VRVideoPlayer] {0}";

    [MenuItem("Tools/Evereal/VRVideoPlayer/GameObject/VRVideoPlayer")]
    private static void CreateVRVideoPlayerObject(MenuCommand menuCommand)
    {
      GameObject vrVideoPlayerPrefab = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/VRVideoPlayer")) as GameObject;
      vrVideoPlayerPrefab.name = "VRVideoPlayer";
      GameObjectUtility.SetParentAndAlign(vrVideoPlayerPrefab, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(vrVideoPlayerPrefab, "Create " + vrVideoPlayerPrefab.name);
      Selection.activeObject = vrVideoPlayerPrefab;
    }

    [MenuItem("Tools/Evereal/VRVideoPlayer/GameObject/VRVideoPlayer (Simple GUI)")]
    private static void CreateVRVideoPlayerGUIObject(MenuCommand menuCommand)
    {
      GameObject vrVideoPlayerGUIPrefab = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/VRVideoPlayer_SimpleGUI")) as GameObject;
      vrVideoPlayerGUIPrefab.name = "VRVideoPlayer_SimpleGUI";
      GameObjectUtility.SetParentAndAlign(vrVideoPlayerGUIPrefab, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(vrVideoPlayerGUIPrefab, "Create " + vrVideoPlayerGUIPrefab.name);
      Selection.activeObject = vrVideoPlayerGUIPrefab;
    }

    [MenuItem("Tools/Evereal/VRVideoPlayer/GameObject/VRVideoPlayer (VR UI)")]
    private static void CreateVRVideoPlayerUIObject(MenuCommand menuCommand)
    {
      GameObject vrVideoPlayerUIPrefab = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/VRVideoPlayer_UI")) as GameObject;
      vrVideoPlayerUIPrefab.name = "VRVideoPlayer_UI";
      GameObjectUtility.SetParentAndAlign(vrVideoPlayerUIPrefab, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(vrVideoPlayerUIPrefab, "Create " + vrVideoPlayerUIPrefab.name);
      Selection.activeObject = vrVideoPlayerUIPrefab;
    }

    [MenuItem("Tools/Evereal/VRVideoPlayer/GameObject/MainCamera (Fade)")]
    private static void CreateMainCameraFadeObject(MenuCommand menuCommand)
    {
      GameObject mainCameraFade = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/MainCamera_Fade")) as GameObject;
      mainCameraFade.name = "MainCamera_Fade";
      GameObjectUtility.SetParentAndAlign(mainCameraFade, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(mainCameraFade, "Create " + mainCameraFade.name);
      Selection.activeObject = mainCameraFade;
    }

    [MenuItem("Tools/Evereal/VRVideoPlayer/GameObject/MainCamera (Gaze Interaction)")]
    private static void CreateMainCameraGazeObject(MenuCommand menuCommand)
    {
      GameObject mainCameraGaze = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/MainCamera_Gaze")) as GameObject;
      mainCameraGaze.name = "MainCamera_Gaze";
      GameObjectUtility.SetParentAndAlign(mainCameraGaze, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(mainCameraGaze, "Create " + mainCameraGaze.name);
      Selection.activeObject = mainCameraGaze;
    }

    [MenuItem("Tools/Evereal/VRVideoPlayer/GameObject/UICanvas (Gaze Interaction)")]
    private static void CreateUICanvasGazeObject(MenuCommand menuCommand)
    {
      GameObject uiCanvasGaze = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/UICanvas_Gaze")) as GameObject;
      uiCanvasGaze.name = "UICanvas_Gaze";
      GameObjectUtility.SetParentAndAlign(uiCanvasGaze, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(uiCanvasGaze, "Create " + uiCanvasGaze.name);
      Selection.activeObject = uiCanvasGaze;
    }

    [MenuItem("Tools/Evereal/VRVideoPlayer/GameObject/VRHandRaycaster")]
    private static void CreateVRHandRaycasterObject(MenuCommand menuCommand)
    {
      GameObject vrHandRaycaster = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/VRHandRaycaster")) as GameObject;
      vrHandRaycaster.name = "VRHandRaycaster";
      GameObjectUtility.SetParentAndAlign(vrHandRaycaster, menuCommand.context as GameObject);
      Undo.RegisterCreatedObjectUndo(vrHandRaycaster, "Create " + vrHandRaycaster.name);
      Selection.activeObject = vrHandRaycaster;
    }

#if VRVIDEOPLAYER_AVPROVIDEO || VRVIDEOPLAYER_EASYMOVIETEXTURE || VRVIDEOPLAYER_AVPROVIDEO_V2
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/Unity Video Player")]
#else
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/Unity Video Player (Selected)")]
#endif
    private static void UseUnityVideoPlayer()
    {
      PlayerSelector.SelectPlayer(MediaPlayerType.UnityVideoPlayer);
      Debug.LogFormat(LOG_FORMAT, "Set media player to: " + MediaPlayerType.UnityVideoPlayer);
    }

#if VRVIDEOPLAYER_AVPROVIDEO_V2
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/AVPro Video 2.x (Selected)")]
#else
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/AVPro Video 2.x")]
#endif
    private static void UseAVProVideo2()
    {
      PlayerSelector.SelectPlayer(MediaPlayerType.AVProVideo2);
      Debug.LogFormat(LOG_FORMAT, "Set media player to: " + MediaPlayerType.AVProVideo2);
    }

#if VRVIDEOPLAYER_AVPROVIDEO
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/AVPro Video 1.x (Selected)")]
#else
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/AVPro Video 1.x")]
#endif
    private static void UseAVProVideo()
    {
      PlayerSelector.SelectPlayer(MediaPlayerType.AVProVideo);
      Debug.LogFormat(LOG_FORMAT, "Set media player to: " + MediaPlayerType.AVProVideo);
    }

#if VRVIDEOPLAYER_EASYMOVIETEXTURE
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/Easy Movie Texture (Selected)")]
#else
    [MenuItem("Tools/Evereal/VRVideoPlayer/MediaPlayer/Easy Movie Texture")]
#endif
    private static void UseEasyMovieTexture()
    {
      PlayerSelector.SelectPlayer(MediaPlayerType.EasyMovieTexture);
      Debug.LogFormat(LOG_FORMAT, "Set media player to: " + MediaPlayerType.EasyMovieTexture);
    }

#if VRVIDEOPLAYER_OCULUS || VRVIDEOPLAYER_STEAMVR
    [MenuItem("Tools/Evereal/VRVideoPlayer/VR/None")]
#else
    [MenuItem("Tools/Evereal/VRVideoPlayer/VR/None (Selected)")]
#endif
    private static void UseNone()
    {
      VRSelector.SelectVR(VRDeviceType.None);
      Debug.LogFormat(LOG_FORMAT, "Set VR to: " + VRDeviceType.None);
    }

#if VRVIDEOPLAYER_OCULUS
    [MenuItem("Tools/Evereal/VRVideoPlayer/VR/Oculus (Selected)")]
#else
    [MenuItem("Tools/Evereal/VRVideoPlayer/VR/Oculus")]
#endif
    private static void UseOculus()
    {
      VRSelector.SelectVR(VRDeviceType.Oculus);
      Debug.LogFormat(LOG_FORMAT, "Set VR to: " + VRDeviceType.Oculus);
    }

#if VRVIDEOPLAYER_STEAMVR
    [MenuItem("Tools/Evereal/VRVideoPlayer/VR/SteamVR (Selected)")]
#else
    [MenuItem("Tools/Evereal/VRVideoPlayer/VR/SteamVR")]
#endif
    private static void UseSteamVR()
    {
      VRSelector.SelectVR(VRDeviceType.SteamVR);
      Debug.LogFormat(LOG_FORMAT, "Set VR to: " + VRDeviceType.SteamVR);
    }
  }
}