using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Mesh mesh;
    public Vector3[] vertices;
    public int[] triangles;

    //Properties
    public int sizeX = 20;
    public int sizeZ = 20;
    public float perlinDensity = 0.3f;
    public float perlinMagnitude = 2f;

    void Start()
    {
        //transform.position = new Vector3(-sizeX/2f, 0f, -sizeZ/2f);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        MeshDefine();
        MeshCreate();
    }

    private void MeshDefine()
    {
        vertices = new Vector3[(sizeX + 1) * (sizeZ + 1)];

        int i = 0;
        for (int z = 0; z <= sizeZ; z++)
        {
            for (int x = 0; x <= sizeX; x++) {
                //Perlin noise
                float y = Mathf.PerlinNoise(x * perlinDensity, z * perlinDensity) * perlinMagnitude;

                //Def verts
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[sizeX * sizeZ * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < sizeZ; z++)
        {
            for (int x = 0; x <  sizeX; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + sizeX + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + sizeX + 1;
                triangles[tris + 5] = vert + sizeX + 2;

                tris += 6;
                vert++;
            }
            vert++;
        }
    }

    private void MeshCreate()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    //Draw verts in edit mode
    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
