using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaybuttonControl : ShootableUI
{
    public override void ShotClick()
    {
        worldSpaceVideo.PlayPause();
    }
}
