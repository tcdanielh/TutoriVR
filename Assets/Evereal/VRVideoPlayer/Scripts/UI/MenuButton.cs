/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class MenuButton : ButtonBase, IRunnable
  {
    public GameObject menuPanel;
    public GameObject menuIcon;
    public GameObject closeIcon;

    private bool isMenuPanelOpen = false;

    protected new const string LOG_FORMAT = "[MenuButton] {0}";

    protected override void OnClick()
    {
      menuPanel.SetActive(!isMenuPanelOpen);
      isMenuPanelOpen = !isMenuPanelOpen;
      Toggle();
    }

    public void Run(Vector3 currentPoint)
    {
            menuPanel.SetActive(!isMenuPanelOpen);
            isMenuPanelOpen = !isMenuPanelOpen;
            Toggle();
        }

    public void Toggle()
    {
      if (menuIcon == null)
      {
        Debug.LogWarningFormat(LOG_FORMAT, "MenuIcon not attached!");
      }
      if (closeIcon == null)
      {
        Debug.LogWarningFormat(LOG_FORMAT, "CloseIcon not attached!");
      }

      menuIcon.SetActive(!isMenuPanelOpen);
      closeIcon.SetActive(isMenuPanelOpen);
    }
  }
}