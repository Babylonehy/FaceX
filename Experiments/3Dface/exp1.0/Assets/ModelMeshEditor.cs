using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using Debug = UnityEngine.Debug;

public class ModelMeshEditor : MonoBehaviour
{

    //控制点的大小
    public float pointScale;
    private float lastPointScale;
    public GameObject editorpoint;

    Mesh mesh;

    //顶点列表
    List<Vector3> positionList = new List<Vector3>();

    //顶点控制物体列表
    List<GameObject> positionObjList = new List<GameObject>();

    /// <summary>
    /// key:顶点字符串
    /// value:顶点在列表中的位置
    /// </summary>
    Dictionary<string, List<int>> pointmap = new Dictionary<string, List<int>>();

    // Use this for initialization
    void Start()
    {
        Debug.LogWarning("pointScale");
        lastPointScale = pointScale;
        Debug.LogWarning("GetComponent<MeshFilter>");
        mesh = GetComponent<MeshFilter>().sharedMesh;
        Debug.LogWarning("CreateEditorPoint");
        CreateEditorPoint();
        Debug.LogWarning("end CreateEditorPoint");
    }


    //创建控制点
    public void CreateEditorPoint()
    {
        positionList = new List<Vector3>(mesh.vertices);

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            string vstr = Vector2String(mesh.vertices[i]);

            if (!pointmap.ContainsKey(vstr))
            {
                pointmap.Add(vstr, new List<int>());
            }
            pointmap[vstr].Add(i);
        }
        Debug.LogWarning("start pointmap");
        foreach (string key in pointmap.Keys)
        {
            editorpoint = Instantiate(editorpoint);
            editorpoint.transform.parent = transform;
            editorpoint.transform.localPosition = String2Vector(key);
            editorpoint.transform.localScale = new Vector3(pointScale, pointScale, pointScale);

            MeshEditorPoint editorPoint = editorpoint.GetComponent<MeshEditorPoint>();
            Debug.Log("editorPoint" + (editorPoint == null));
            editorPoint.onMove = PointMove;
            editorPoint.pointid = key;

            positionObjList.Add(editorpoint);
        }
    }

    //顶点物体被移动时调用此方法
    public void PointMove(string pointid, Vector3 position)
    {
        Debug.Log("Here");
        if (!pointmap.ContainsKey(pointid))
        {
            return;
        }

        List<int> _list = pointmap[pointid];

        for (int i = 0; i < _list.Count; i++)
        {
            positionList[_list[i]] = position;
        }

        mesh.vertices = positionList.ToArray();
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        //检测控制点尺寸是否改变
        Debug.LogWarning("start update");
        if (Math.Abs(lastPointScale - pointScale) > 0.1f)
        {
            lastPointScale = pointScale;
            for (int i = 0; i < positionObjList.Count; i++)
            {
                positionObjList[i].transform.localScale = new Vector3(pointScale, pointScale, pointScale);
            }
        }
    }

        string Vector2String(Vector3 v)
    {
        StringBuilder str = new StringBuilder();
        str.Append(v.x).Append(",").Append(v.y).Append(",").Append(v.z);
        return str.ToString();
    }

    Vector3 String2Vector(string vstr)
    {
        try
        {
            string[] strings = vstr.Split(',');
            return new Vector3(float.Parse(strings[0]), float.Parse(strings[1]), float.Parse(strings[2]));
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return Vector3.zero;
        }
    }
}