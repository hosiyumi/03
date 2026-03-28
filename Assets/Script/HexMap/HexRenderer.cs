using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单格渲染

public struct Face//单格绘制
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//请求组件

public class HexRenderer : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

    public List<Face> m_faces;

    public Material material;
    public float innersize, outersize, height;
    public bool isFlatTopped;

    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }

    private void OnEnable()
    {
        DrawMesh();
    }

    // public void OnValidate()//测试单个六边形用代码
    // {
    //     if(Application.isPlaying)
    //     {
    //         DrawMesh();
    //     }
    // }

    public void DrawMesh()//网格绘制
    {
        DrawFaces();
        CombineFaces();
    }

    public void DrawFaces()
    {
        m_faces = new List<Face>();
        //顶面
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CrateFace(innersize, outersize, height / 2f, height / 2f, point));
        }
        //底面
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CrateFace(innersize, outersize, -height / 2f, -height / 2f, point, true));
        }
        //外面
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CrateFace(outersize, outersize, height / 2f, -height / 2f, point, true));
        }
        //内面
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CrateFace(innersize, innersize, height / 2f, -height / 2f, point, false));
        }

    }

    private Face CrateFace(float innerRad, float outerRead, float heightA, float heightB, int point, bool revese = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRead, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRead, heightA, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        if (revese)
        {
            vertices.Reverse();
        }

        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, float height, int index)
    {
        float angle_deg = isFlatTopped ? 60 * index : 60 * index - 30;
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3((size * Mathf.Cos(angle_rad)), height, size * Mathf.Sin(angle_rad));
    }



    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < m_faces.Count; i++)
        {
            vertices.AddRange(m_faces[i].vertices);
            uvs.AddRange(m_faces[i].uvs);
            int offset = (4 * i);
            foreach (int triangles in m_faces[i].triangles)
            {
                tris.Add(triangles + offset);
            }
        }

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = tris.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();
    }
}


