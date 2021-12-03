/* Copyright (c) 2020-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(MeshFilter))]
  public class EACMapping : MonoBehaviour
  {
    // This value comes from the facebook transform ffmpeg filter and is used to prevent seams appearing along the edges due to bilinear filtering
    [SerializeField]
    private float expansionCoeff = 1.01f;
    // Log message format template
    private const string LOG_FORMAT = "[EACMapping] {0}";

    public StereoMode stereoMode = StereoMode.NONE;

    // Use this for initialization
    void Start()
    {
      BuildMesh();
    }

    // Set video stereo mode.
    public void SetStereoMode(StereoMode sm)
    {
      stereoMode = sm;
      BuildMesh();
    }

    private void BuildMesh()
    {
      MeshFilter meshFilter = GetComponent<MeshFilter>();
      Mesh mesh = meshFilter.mesh;
      if (meshFilter != null)
        mesh = meshFilter.mesh;
      if (mesh == null || mesh.uv.Length != 24)
      {
        Debug.LogWarningFormat(LOG_FORMAT, "Script needs to be attached to built-in cube");
        return;
      }

      Vector3 offset = new Vector3(-0.5f, -0.5f, -0.5f);
      Vector3[] v = new Vector3[]
      {
        // Left
        new Vector3(0f,-1f,0f) - offset,
        new Vector3(0f,0f,0f) - offset,
        new Vector3(0f,0f,-1f) - offset,
        new Vector3(0f,-1f,-1f) - offset,
        // Front
        new Vector3(0f,0f,0f) - offset,
        new Vector3(-1f,0f,0f) - offset,
        new Vector3(-1f,0f,-1f) - offset,
        new Vector3(0f,0f,-1f) - offset,
        // Right
        new Vector3(-1f,0f,0f) - offset,
        new Vector3(-1f,-1f,0f) - offset,
        new Vector3(-1f,-1f,-1f) - offset,
        new Vector3(-1f,0f,-1f) - offset,
        // Back
        new Vector3(-1f,-1f,0f) - offset,
        new Vector3(0f,-1f,0f) - offset,
        new Vector3(0f,-1f,-1f) - offset,
        new Vector3(-1f,-1f,-1f) - offset,
        // Bottom
        new Vector3(0f,-1f,-1f) - offset,
        new Vector3(0f,0f,-1f) - offset,
        new Vector3(-1f,0f,-1f) - offset,
        new Vector3(-1f,-1f,-1f) - offset,
        // Top
        new Vector3(-1f,-1f,0f) - offset,
        new Vector3(-1f,0f,0f) - offset,
        new Vector3(0f,0f,0f) - offset,
        new Vector3(0f,-1f,0f) - offset,
      };

      Matrix4x4 rot = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90f, Vector3.right), Vector3.one);
      for (int i = 0; i < v.Length; i++)
      {
        v[i] = rot.MultiplyPoint(v[i]);
      }

      mesh.vertices = v;

      mesh.triangles = new int[]
      {
        0,1,2,
        0,2,3,
        4,5,6,
        4,6,7,
        8,9,10,
        8,10,11,
        12,13,14,
        12,14,15,
        16,17,18,
        16,18,19,
        20,21,22,
        20,22,23,
      };

      mesh.normals = new Vector3[]
      {
        // Left
        new Vector3(-1f,0f,0f),
        new Vector3(-1f,0f,0f),
        new Vector3(-1f,0f,0f),
        new Vector3(-1f,0f,0f),
        // Front
        new Vector3(0f,-1f,0f),
        new Vector3(0f,-1f,0f),
        new Vector3(0f,-1f,0f),
        new Vector3(0f,-1f,0f),
        // Right
        new Vector3(1f,0f,0f),
        new Vector3(1f,0f,0f),
        new Vector3(1f,0f,0f),
        new Vector3(1f,0f,0f),
        // Back
        new Vector3(0f,1f,0f),
        new Vector3(0f,1f,0f),
        new Vector3(0f,1f,0f),
        new Vector3(0f,1f,0f),
        // Bottom
        new Vector3(0f,0f,1f),
        new Vector3(0f,0f,1f),
        new Vector3(0f,0f,1f),
        new Vector3(0f,0f,1f),
        // Top
        new Vector3(0f,0f,-1f),
        new Vector3(0f,0f,-1f),
        new Vector3(0f,0f,-1f),
        new Vector3(0f,0f,-1f)
      };

      UpdateMeshUV(mesh, 512, 512, false);
    }

    private void UpdateMeshUV(Mesh mesh, int textureWidth, int textureHeight, bool flipY)
    {
      float texWidth = textureWidth;
      float texHeight = textureHeight;

      float blockWidth = texWidth / 3f;
      float pixelOffset = Mathf.Floor(((expansionCoeff * blockWidth) - blockWidth) / 2f);

      float wO = pixelOffset / texWidth;
      float hO = pixelOffset / texHeight;

      const float third = 1f / 3f;
      const float half = 0.5f;

      Vector2[] uv = { };
      if (stereoMode == StereoMode.LEFT_RIGHT)
      {
        uv = new Vector2[] {
          //left
          new Vector2(0f+wO, 0f+hO),
          new Vector2(0f+wO, third-hO),
          new Vector2(half-wO, third-hO),
          new Vector2(half-wO, 0f+hO),

          //front
          new Vector2(0f+wO, third+hO),
          new Vector2(0f+wO, (third*2)-hO),
          new Vector2(half-wO, (third*2)-hO),
          new Vector2(half-wO, third+hO),

          //right
          new Vector2(0f+wO, (third*2)+hO),
          new Vector2(0f+wO, 1f-hO),
          new Vector2(half-wO, 1f-hO),
          new Vector2(half-wO, (third*2)+hO),
        
          //back
          new Vector2(half+wO, (third*2)-hO),
          new Vector2(1f-wO, (third*2)-hO),
          new Vector2(1f-wO, third+hO),
          new Vector2(half+wO, third+hO),

          //bottom
          new Vector2(1f-wO, third-hO),
          new Vector2(1f-wO, 0f+hO),
          new Vector2(half+wO, 0f+hO),
          new Vector2(half+wO, third-hO),

          //top
          new Vector2(half+wO, (third*2)+hO),
          new Vector2(half+wO, 1f-hO),
          new Vector2(1f-wO, 1f-hO),
          new Vector2(1f-wO, (third*2)+hO),
        };
      }
      else
      {
        uv = new Vector2[] {
          //left
          new Vector2(0f+wO, 1f-hO),
          new Vector2(third-wO, 1f-hO),
          new Vector2(third-wO, half+hO),
          new Vector2(0f+wO, half+hO),

          //front
          new Vector2(third+wO,1f-hO),
          new Vector2((third*2f)-wO, 1f-hO),
          new Vector2((third*2f)-wO, half+hO),
          new Vector2(third+wO, half+hO),

          //right
          new Vector2((third*2f)+wO, 1f-hO),
          new Vector2(1f-wO, 1f-hO),
          new Vector2(1f-wO, half+hO),
          new Vector2((third*2f)+wO, half+hO),
        
          //back
          new Vector2((third*2f)-wO, half-hO),
          new Vector2((third*2f)-wO, 0f+hO),
          new Vector2(third+wO, 0f+hO),
          new Vector2(third+wO, half-hO),

          //bottom
          new Vector2(third-wO, 0f+hO),
          new Vector2(0f+wO, 0f+hO),
          new Vector2(0f+wO, half-hO),
          new Vector2(third-wO, half-hO),

          //top
          new Vector2((third*2f)+wO, half-hO),
          new Vector2(1f-wO, half-hO),
          new Vector2(1f-wO, 0f+hO),
          new Vector2((third*2f)+wO, 0f+hO),
        };
      }

      if (flipY)
      {
        for (int i = 0; i < uv.Length; i++)
        {
          uv[i].y = 1f - uv[i].y;
        }
      }

      mesh.uv = uv;
      mesh.UploadMeshData(false);
    }
  }
}