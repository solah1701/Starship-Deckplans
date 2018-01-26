using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHelper : MonoBehaviour {

    public static int SetTriangle(int[] triangles, int i, int v00, int v10, int v01)
    {
        triangles[i] = v00;
        triangles[i + 1] = v01;
        triangles[i + 2] = v10;
        return i + 3;
    }

    public static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }
}
