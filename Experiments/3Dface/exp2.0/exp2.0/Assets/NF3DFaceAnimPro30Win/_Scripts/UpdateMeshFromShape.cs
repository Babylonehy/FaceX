using System.Collections;
using System.Linq;

using UnityEngine;

using MassAnimation.Avatar.Entities;

namespace Assets.Scripts.NFScript
{

    [ExecuteInEditMode]
    public class UpdateMeshFromShape : MonoBehaviour 
    {
	    MeshFilter mf;
        ShapeUnity shape;

	    public void SetShape(ShapeUnity shape)
	    {
		    this.shape = shape;
	    }

	    void Start() 
	    {
		    mf = this.GetComponent<MeshFilter>();
	    }

	    public void UpdateMesh() 
	    {
		    if (null == shape)
		    {
			    return;
		    }

            if (null != mf)
            {

                var mesh = mf.sharedMesh;

                if (null != mesh)
                {
                    var shapeVertices = shape.GeoModel.Vertices;

                    mesh.vertices = shapeVertices.AllPoints.Select(p => new Vector3(p.X, p.Y, p.Z)).ToArray();
                    mesh.RecalculateNormals();
                }
            }

        }

    }

}