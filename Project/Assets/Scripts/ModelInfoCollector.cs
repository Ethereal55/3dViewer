using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AccurateMeshCounter : MonoBehaviour
{
    [SerializeField] private Text _resultText;

    public void CalculateVerticles()
    {
        int blenderVertices = 0;
        int blenderTriangles = 0;

        foreach (var filter in FindObjectsOfType<MeshFilter>())
        {
            if (filter.sharedMesh != null)
                ProcessMesh(filter.sharedMesh, ref blenderVertices, ref blenderTriangles);
        }

        foreach (var skinned in FindObjectsOfType<SkinnedMeshRenderer>())
        {
            if (skinned.sharedMesh != null)
                ProcessMesh(skinned.sharedMesh, ref blenderVertices, ref blenderTriangles);
        }

        _resultText.text = $"Vertices: {blenderVertices}\nTriangles: {blenderTriangles}";
    }

    void ProcessMesh(Mesh mesh, ref int vertices, ref int triangles)
    {
        // —читаем уникальные вершины как в Blender
        HashSet<Vector3> uniqueVerts = new HashSet<Vector3>();
        foreach (var vertex in mesh.vertices)
        {
            uniqueVerts.Add(vertex);
        }
        vertices += uniqueVerts.Count;

        // “реугольники считаем оригинальные
        triangles += mesh.triangles.Length / 3;
    }
}