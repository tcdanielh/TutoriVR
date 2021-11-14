/* Copyright (c) 2017-present Evereal. All rights reserved. */

using System;

namespace Evereal.VRVideoPlayer
{
  /// <summary>
  /// Video source, where is video load from
  /// </summary>
  [Serializable]
  public enum VideoSource
  {
    ABSOLUTE_URL,
    FROM_STREAMING_ASSETS,
    FROM_PROJECT_FOLDER,
  }

  /// <summary>
  /// Media player type
  /// </summary>
  public enum MediaPlayerType
  {
    UnityVideoPlayer,
    AVProVideo,
    AVProVideo2,
    EasyMovieTexture,
  }

  /// <summary>
  /// VR type
  /// </summary>
  public enum VRDeviceType
  {
    None,
    Oculus,
    SteamVR,
  }

  /// <summary>
  /// Video render mode
  /// </summary>
  [Serializable]
  public enum RenderMode
  {
    NORMAL,
    _360,
    _180,
  }

  /// <summary>
  /// 360 video projection type
  /// </summary>
  [Serializable]
  public enum ProjectionType
  {
    NONE,
    // <summary>
    // Equirectangular Projection.
    // https://en.wikipedia.org/wiki/Equirectangular_projection
    // </summary>
    EQUIRECT,
    // <summary>
    // Cubemap Projection.
    // https://en.wikipedia.org/wiki/Cube_mapping
    // https://docs.unity3d.com/Manual/class-Cubemap.html
    // </summary>
    // Cubemap video format layout:
    // +------------------+------------------+------------------+
    // |                  |                  |                  |
    // |                  |                  |                  |
    // |    +X (Right)    |    -X (Left)     |     +Y (Top)     |
    // |                  |                  |                  |
    // |                  |                  |                  |
    // +------------------+------------------+------------------+
    // |                  |                  |                  |
    // |                  |                  |                  |
    // |   +Y (Bottom)    |   +Z (Front)     |    -Z (Back)     |
    // |                  |                  |                  |
    // |                  |                  |                  |
    // +------------------+------------------+------------------+
    //
    CUBEMAP,
    // <summary>
    // Equi-Angular Cubemap Projection.
    // https://en.wikipedia.org/wiki/360_video_projection
    // https://blog.google/products/google-vr/bringing-pixels-front-and-center-vr-video/
    // </summary>
    EAC,
  }

  /// <summary>
  /// Video stereo mode
  /// </summary>
  [Serializable]
  public enum StereoMode
  {
    NONE = 0,
    TOP_BOTTOM = 1,
    LEFT_RIGHT = 2
  }
}