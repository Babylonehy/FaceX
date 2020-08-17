using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.NFScript
{

    [ExecuteInEditMode]
    public class IntuitiveProModel : MonoBehaviour
    {
        public List<UpdateMeshFromShape> modelShapeUpdater = new List<UpdateMeshFromShape>();

        void Start()
        {
            UnityEvents uv = UnityEvents.Instance;

            uv.pauseListeners = new List<Component>();
            uv.resumeListeners = new List<Component>();
            uv.playListeners = new List<Component>();
            uv.stopListeners = new List<Component>();
            uv.saveSceneListeners = new List<Component>();
            uv.loadProjectListeners = new List<Component>();

            if (!uv.playListeners.Contains(this))
            {
                uv.addEventListener(this, uv.playListeners);
                uv.addEventListener(this, uv.stopListeners);
                uv.addEventListener(this, uv.pauseListeners);
                uv.addEventListener(this, uv.saveSceneListeners);
                uv.addEventListener(this, uv.loadProjectListeners);
            }

    
        }

        void Awake()
        {

        }

        public void Update()
        {

        }

        public void CacheModelShapeUpdater()
        {
            modelShapeUpdater.AddRange(GetComponentsInChildren<UpdateMeshFromShape>());
        }

        public void UpdateMeshes()
        {
            foreach (var msu in modelShapeUpdater)
            {
                msu.UpdateMesh();
            }
        }

        public void OnEditorPlay()
        {
            try
            { 

 
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }

        }

        public void OnEditorPaused()
        {

        }

        public void OnEditorStop()
        {

        }

        public void OnSceneSaved()
        {

        }

        public void OnProjectLoad()
        {

        }
    }


}
