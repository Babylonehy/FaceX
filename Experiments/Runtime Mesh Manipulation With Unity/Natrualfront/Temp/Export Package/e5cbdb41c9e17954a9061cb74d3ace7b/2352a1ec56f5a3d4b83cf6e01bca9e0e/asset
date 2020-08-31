using System.Linq;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Assets.Scripts.NFScript
{

    public class MeshGenerator
    {

        const string TextureFolder = "Assets/NF3DFaceAnimPro30Win/_Textures/";
        const string MaterialsFolder = "Assets/NF3DFaceAnimPro30Win/_Materials/";

        public GameObject AnimatableMeshPrefab;


	    public MeshGenerator()
	    {

#if UNITY_EDITOR
            AnimatableMeshPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/NF3DFaceAnimPro30Win/_Prefabs/AnimatableMesh.prefab", typeof(GameObject));
#endif

        }


        public GameObject GenerateGameObjects(MassAnimation.Avatar.Entities.AnimatableUnity model)
	    {
		    var result = new GameObject("Model");
		    var ipm = result.AddComponent<IntuitiveProModel>();
		    foreach (var shape in model.ModelShapes)
		    {
			    GenerateGameObject(shape, result);
		    }
		    ipm.CacheModelShapeUpdater();
		    return result;
	    }

	    private Texture2D LoadTexture(string texName)
	    {
            Texture2D tex = null;

#if UNITY_EDITOR
            tex = AssetDatabase.LoadAssetAtPath(TextureFolder + texName, typeof(Texture2D)) as Texture2D;
#endif

            return tex;
	    }

        private Material CreateNewMaterial(Material shared, string texName)
	    {
            Material material = null;

            try
            {

                material = new Material(shared);
                var name = texName;

                Texture2D tex2D = LoadTexture(texName);
#if UNITY_EDITOR
                AssetDatabase.CreateAsset(material, MaterialsFolder + name + ".mat");
#endif

                material.mainTexture = tex2D;
            }
            catch(System.Exception exp)
            {
                Debug.Log(exp.Message);
                throw;
            }

		    return material;
	    }


	    void GenerateMaterial(MassAnimation.Avatar.Entities.ShapeUnity shape, GameObject go)
	    {
		    if (null != shape.TextureEntity)
		    {
			    var texName = shape.TextureEntity.ImageName;
			    go.GetComponent<Renderer>().material = CreateNewMaterial(go.GetComponent<Renderer>().sharedMaterial, texName);
		    }
	    }

	    void GenerateMesh(MassAnimation.Avatar.Entities.ShapeUnity shape, GameObject go)
	    {
		    if (null != shape.GeoModel)
		    {
			    var indices = shape.GeoModel.Indices;
			    var vertices = shape.GeoModel.Vertices;

			    var mesh = new Mesh();
			    var mf = go.GetComponent<MeshFilter>();
			    mf.mesh = mesh;

			    mesh.vertices = vertices.AllPoints.Select(p => new Vector3(p.X, p.Y, p.Z)).ToArray();

			    mesh.uv = shape.TexureMapping.UVs.AllPoints.Select(uv => new Vector2(uv.X, uv.Y)).ToArray();

			    mesh.triangles = indices.AllCoordIndices.SelectMany(idx => idx).ToArray();

			    mesh.RecalculateNormals();

			    var umfs = go.GetComponent<UpdateMeshFromShape>();
			    umfs.SetShape(shape);
		    }
	    }

	    void GenerateGameObject(MassAnimation.Avatar.Entities.ShapeUnity shape, GameObject parent)
	    {
		    try
		    {
			    var go = (GameObject)GameObject.Instantiate(AnimatableMeshPrefab);
			    go.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
			    go.transform.parent = parent.transform;
			    GenerateMaterial(shape, go);
			    GenerateMesh(shape, go);
		    }
		    catch(UnityException exp)
		    {
			    Debug.LogError(exp.Message);
                throw;
		    }
	    }

    }

}