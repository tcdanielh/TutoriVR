/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  public class MouseLook : MonoBehaviour
  {
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -60f;
    public float maximumY = 60f;
    float rotationX = 0f;
    float rotationY = 0f;
    Quaternion originalRotation;
    void Update()
    {
      if (axes == RotationAxes.MouseXAndY)
      {
        // Read the mouse input axis
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion * yQuaternion;
      }
      else if (axes == RotationAxes.MouseX)
      {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation = originalRotation * xQuaternion;
      }
      else
      {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
        transform.localRotation = originalRotation * yQuaternion;
      }
    }
    void Start()
    {
      originalRotation = transform.localRotation;
    }
    public static float ClampAngle(float angle, float min, float max)
    {
      if (angle < -360f)
        angle += 360f;
      if (angle > 360f)
        angle -= 360f;
      return Mathf.Clamp(angle, min, max);
    }
  }
}