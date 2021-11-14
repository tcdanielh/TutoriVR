using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableUI : MonoBehaviour
{

    public WorldSpaceVideo worldSpaceVideo;

    public abstract void ShotClick();
}