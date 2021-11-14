/* Copyright (c) 2020-present Evereal. All rights reserved. */

using System;
using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public interface IMediaPlayer
  {
    // Game object media player will attached to.
    GameObject gameObject { get; }

    // The file or HTTP URL that the MediaPlayer reads content from.
    string url { get; set; }

    // Whether content is being played. (Read Only)
    bool isPlaying { get; }

    // Whether the media player has successfully prepared the content to be played. (Read Only)
    bool isPrepared { get; }

    // Whether playback is paused. (Read Only)
    bool isPaused { get; }

    // The MediaPlayer current time in seconds.
    double time { get; set; }

    // The length of the media, or the URL, in seconds. (Read Only)
    double length { get; }

    // Invoked when the media player preparation is complete.
    event MediaEventHandler prepareCompleted;

    // Invoked immediately after Play is called.
    event MediaEventHandler started;

    // Invoked when first frame is ready.
    event MediaEventHandler firstFrameReady;

    // Invoked when the media player reaches the end of the content to play.
    event MediaEventHandler loopPointReached;

    // The frame index currently being displayed by the MediaPlayer.
    // long frame { get; }

    // Number of frames in the current video content.
    // ulong frameCount { get; }

    // The frame rate of the clip or URL in frames/second. (Read Only)
    float frameRate { get; }

    // The width of the images in the media, or URL, in pixels. (Read Only)
    int width { get; }

    // The height of the images in the media, or URL, in pixels. (Read Only)
    int height { get; }

    // Internal texture in which video content is placed. (Read Only)
    Texture texture { get; }

    // Init media player instance.
    void Init();

    // Load a new video file or url.
    void Load(string url, bool play);

    // Starts media playback.
    void Play();

    // Pauses the playback and leaves the current time intact.
    void Pause();

    // Stops the playback and sets the current time to 0.
    void Stop();

    // Set media playback looping.
    void SetLooping(bool loop);

    // Determines whether the media player restarts from the beginning when it reaches the end of the clip.
    bool IsLooping();

    // Gets the audio mute status for the specified track.
    bool IsAudioMute(ushort track);

    // Set the audio mute status for the specified track.
    void SetAudioMute(ushort track, bool mute);

    // Return the direct-output volume for specified track.
    float GetAudioVolume(ushort track);

    // Set the audio volume for the specified track.
    void SetAudioVolume(ushort track, float volume);

    // Set media target renderer.
    void SetTargetRenderer(Renderer renderer);

    // Set player game object.
    void SetGameObject(GameObject obj);

    // Should be called when game object becomes enabled.
    void OnEnable();

    // Should be called when game object becomes disabled.
    void OnDisable();
  }
}