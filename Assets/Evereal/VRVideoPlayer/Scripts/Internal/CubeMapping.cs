/* Copyright (c) 2017-present Evereal. All rights reserved. */

using UnityEngine;

namespace Evereal.VRVideoPlayer
{
  [RequireComponent(typeof(MeshFilter))]
  public class CubeMapping : MonoBehaviour
  {
    // This value comes from the facebook transform ffmpeg filter and is used to prevent seams appearing along the edges due to bilinear filtering
    [SerializeField]
    private float expansionCoeff = 1.01f;
    // Log message format template
    private const string LOG_FORMAT = "[CubeMapping] {0}";

    // Use this for initialization
    void Start()
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

      //Vector2[] uvs = mesh.uv;
      //// Front
      //uvs[1] = new Vector2(0.33333f, 0.0f);
      //uvs[0] = new Vector2(0.66667f, 0.0f);
      //uvs[3] = new Vector2(0.33333f, 0.5f);
      //uvs[2] = new Vector2(0.66667f, 0.5f);
      //// Top
      //uvs[9] = new Vector2(0.66667f, 0.5f);
      //uvs[5] = new Vector2(0.66667f, 1f);
      //uvs[4] = new Vector2(1.0f, 1f);
      //uvs[8] = new Vector2(1.0f, 0.5f);
      //// Back
      //uvs[6] = new Vector2(0.66667f, 0.0f);
      //uvs[7] = new Vector2(1.0f, 0.0f);
      //uvs[10] = new Vector2(0.66667f, 0.5f);
      //uvs[11] = new Vector2(1.0f, 0.5f);
      //// Bottom
      //uvs[14] = new Vector2(0.0f, 0.0f);
      //uvs[15] = new Vector2(0.33333f, 0.0f);
      //uvs[13] = new Vector2(0.0f, 0.5f);
      //uvs[12] = new Vector2(0.33333f, 0.5f);
      //// Left
      //uvs[19] = new Vector2(0.33333f, 0.5f);
      //uvs[18] = new Vector2(0.33333f, 1f);
      //uvs[17] = new Vector2(0.66667f, 1f);
      //uvs[16] = new Vector2(0.66667f, 0.5f);
      //// Right
      //uvs[23] = new Vector2(0.0f, 0.5f);
      //uvs[22] = new Vector2(0.0f, 1f);
      //uvs[21] = new Vector2(0.33333f, 1f);
      //uvs[20] = new Vector2(0.33333f, 0.5f);

      //mesh.uv = uvs;

      BuildMesh(mesh);
    }

    private void BuildMesh(Mesh mesh)
    {
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

      Vector2[] uv = new Vector2[] {
        //left
        new Vector2(third+wO,1f-hO),
        new Vector2((third*2f)-wO, 1f-hO),
        new Vector2((third*2f)-wO, half+hO),
        new Vector2(third+wO, half+hO),

        //front
        new Vector2(third+wO, half-hO),
        new Vector2((third*2f)-wO, half-hO),
        new Vector2((third*2f)-wO, 0f+hO),
        new Vector2(third+wO, 0f+hO),

        //right
        new Vector2(0f+wO, 1f-hO),
        new Vector2(third-wO, 1f-hO),
        new Vector2(third-wO, half+hO),
        new Vector2(0f+wO, half+hO),

        //back
        new Vector2((third*2f)+wO, half-hO),
        new Vector2(1f-wO, half-hO),
        new Vector2(1f-wO, 0f+hO),
        new Vector2((third*2f)+wO, 0f+hO),

        //bottom
        new Vector2(0f+wO, 0f+hO),
        new Vector2(0f+wO, half-hO),
        new Vector2(third-wO, half-hO),
        new Vector2(third-wO, 0f+hO),

        //top
        new Vector2(1f-wO, 1f-hO),
        new Vector2(1f-wO, half+hO),
        new Vector2((third*2f)+wO, half+hO),
        new Vector2((third*2f)+wO, 1f-hO)
      };

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