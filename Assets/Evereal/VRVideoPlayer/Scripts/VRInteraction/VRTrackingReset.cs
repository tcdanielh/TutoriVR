/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;
using System.Collections.Generic;

namespace Evereal.VRVideoPlayer
{
  // This class simply insures the head tracking behaves correctly when the application is paused.
  public class VRTrackingReset : MonoBehaviour
  {
    private void OnApplicationPause(bool pauseStatus)
    {
#if UNITY_2019_3_OR_NEWER
      List<UnityEngine.XR.XRInputSubsystem> subsystems = new List<UnityEngine.XR.XRInputSubsystem>();
      UnityEngine.SubsystemManager.GetInstances<UnityEngine.XR.XRInputSubsystem>(subsystems);
      for (int i = 0; i < subsystems.Count; i++)
      {
        subsystems[i].TrySetTrackingOriginMode(UnityEngine.XR.TrackingOriginModeFlags.Device);
        subsystems[i].TryRecenter();
      }
#elif UNITY_2017_2_OR_NEWER
      UnityEngine.XR.InputTracking.Recenter();
#else
      UnityEngine.VR.InputTracking.Recenter();
#endif
    }
  }
}
