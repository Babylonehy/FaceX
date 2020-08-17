/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

[CustomEditor(typeof(MeshStudy))]
public class MeshInspector : Editor
{
    private MeshStudy mesh;
    private Transform handleTransform;
    private Quaternion handleRotation;
    string triangleIdx;

    void OnSceneGUI()
    {
        mesh = target as MeshStudy;
        // Debug.Log("Custom editor is running");
        EditMesh();
    }

    void EditMesh()
    {
        handleTransform = mesh.transform; //1
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
        handleTransform.rotation : Quaternion.identity; //2
        // Debug.Log("mesh.vertices.Length: "+mesh.vertices.Length);
        for (int i = 0; i < mesh.vertices.Length; i++) //3
        {
            ShowPoint(i);
        }
    }

    private void ShowPoint(int index)
    {
        if (mesh.moveVertexPoint)
        {
            Vector3 point = handleTransform.TransformPoint(mesh.vertices[index]); //1
            Handles.color = Color.red;
            point = Handles.FreeMoveHandle(point, handleRotation, mesh.handleSize,
                Vector3.zero, Handles.DotHandleCap); //2

            if (GUI.changed) //3
            {
                mesh.DoAction(index, handleTransform.InverseTransformPoint(point)); //4
            }
        }
        else
        {
            //click
        }
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        mesh = target as MeshStudy;

        if (GUILayout.Button("Reset")) //1
        {
            Debug.Log("Press Reset");
            mesh.Reset(); //2
        }

        // For testing Reset function
        if (mesh.isCloned)
        {
            if (GUILayout.Button("Test Edit"))
            {
                Debug.Log("Press Test Edit");
                mesh.EditMesh();
            }
        }
    }


}
