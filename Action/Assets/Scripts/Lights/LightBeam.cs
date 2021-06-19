using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(Collider))]

public class LightBeam : MonoBehaviour
{
    //use with blender meshes (not probuilder)
    private Mesh m;

    private Vector3[]       baseVertices;

    private int[]           baseTriangles;
    private List<int>       triangles = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //64 triangles
        m = GetComponent<MeshFilter>().mesh;

        baseVertices = m.vertices;
        baseTriangles = m.triangles;
    }

    // Update is called once per frame
    void Update()
    {
        //number of triangles (192) (EBO)
        //number of baseVertices (129) (VBO)
        Debug.Log(baseVertices.Length);
        Debug.Log(baseTriangles.Length);
    }

    private void OnTriggerStay(Collider other)
    {
        //recreate the mesh
        Vector3[] v = new Vector3[baseVertices.Length];
        Array.Copy(baseVertices, v, baseVertices.Length);

        triangles.Clear();

        //baseTriangles.Length / 2 because there are 64 triangles but we want 32
        for (int i = 0; i < baseTriangles.Length / 2; i += 3)
        {
            int index0 = baseTriangles[i];
            int index1 = baseTriangles[i + 1];
            int index2 = baseTriangles[i + 2];

            //check if the triangles hits an obstacle
            float scale;
            if (rayHits(v[index0], v[index2], out scale))
            {
                v[index0] = baseVertices[index0] * scale;
                v[index1] = baseVertices[index1] * scale;
                v[index2] = baseVertices[index2] * scale;
            }

            triangles.Add(index0);
            triangles.Add(index1);
            triangles.Add(index2);
        }

        m.Clear();

        m.vertices = v;
        m.triangles = triangles.ToArray();

        m.RecalculateNormals();
        m.RecalculateBounds();
    }

    private void OnTriggerExit(Collider other)
    {
        //recreate the original mesh
        m.Clear();

        m.vertices = baseVertices;
        m.triangles = baseTriangles;

        m.RecalculateNormals();
        m.RecalculateBounds();
    }

    private bool rayHits(Vector3 from, Vector3 to, out float mag)
    {
        int layerMask = 1;
        RaycastHit hit;

        Vector3 diff = (to - from) * 15f;
        Vector3 dir = new Vector3(-diff.x, -diff.z, diff.y);

        //set same rotation as parent (player)
        dir = Quaternion.AngleAxis(transform.parent.rotation.eulerAngles.z, Vector3.forward) * dir;
        dir = Quaternion.AngleAxis(transform.parent.rotation.eulerAngles.y, Vector3.up) * dir;
        dir = Quaternion.AngleAxis(transform.parent.rotation.eulerAngles.x, Vector3.right) * dir;

        if (Physics.Raycast(transform.parent.position, dir, out hit, diff.magnitude, layerMask))
        {
            Debug.DrawLine(transform.parent.position, hit.point, Color.red);
            mag = (transform.parent.position - hit.point).magnitude / diff.magnitude;
            return true;
        }
        else
        {
            Debug.DrawLine(transform.parent.position, transform.parent.position + dir, Color.green);
            mag = 1f;
            return false;
        }
    }
}
