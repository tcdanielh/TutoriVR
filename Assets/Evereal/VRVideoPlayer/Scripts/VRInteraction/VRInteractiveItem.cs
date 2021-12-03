/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  // This class should be added to any gameobject in the scene
  // that should react to input based on the user's gaze.
  // It contains events that can be subscribed to by classes that
  // need to know about input specifics to this gameobject.
  public class VRInteractiveItem : MonoBehaviour
  {
    // Called when the gaze moves over this object
    public event Action<Vector3> OnOver;
    // Called when the gaze leaves this object
    public event Action OnOut;
    // Called when click input is detected whilst the gaze is over this object.
    public event Action OnClick;
    // Called when double click input is detected whilst the gaze is over this object.
    public event Action OnDoubleClick;
    // Called when Fire1 is released whilst the gaze is over this object.
    public event Action OnUp;
    // Called when Fire1 is pressed whilst the gaze is over this object.
    public event Action OnDown;

    protected bool isOver;

    // Is the gaze currently over this object?
    public bool IsOver
    {
      get { return isOver; }
    }

    // The below functions are called by the VREyeRaycaster when the appropriate input is detected.
    // They in turn call the appropriate events should they have subscribers.
    public void Over(Vector3 point)
    {
      isOver = true;

      if (OnOver != null)
        OnOver(point);
    }

    public void Out()
    {
      isOver = false;

      if (OnOut != null)
        OnOut();
    }

    public void Click()
    {
      if (OnClick != null)
        OnClick();
    }

    public void DoubleClick()
    {
      if (OnDoubleClick != null)
        OnDoubleClick();
    }

    public void Up()
    {
      if (OnUp != null)
        OnUp();
    }

    public void Down()
    {
      if (OnDown != null)
        OnDown();
    }
  }
}