using System;
using System.Collections.Generic;
using System.Threading;

using MassAnimation.Adapters.PhotoAdapter;
using MassAnimation.Avatar.Entities;
using MassAnimation.AvatarService.AvatarResource;
using MassAnimation.Resources;
using MassAnimation.Resources.Entities;
using MassAnimation.UnityPluginConnector;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFScript
{

    [ExecuteInEditMode]
    public class PluginManager : MonoBehaviour
    {


        #region members & properties

        private PluginConnector _plugin;
        private volatile bool animationRunning = false;

        [SerializeField]
        private Thread _animationThread;
		
		internal static PluginManager instance;
        public static PluginManager Instance 
		{
			get 
			{
                if (instance == null)
                {
                    instance = (PluginManager)GameObject.FindObjectOfType<PluginManager>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<PluginManager>();
                        instance.name = "PluginManager";
                        instance.Initialize();
                    }
                }
                return instance;
            }
			set
			{
				instance = value;
			}
		}

        private IntuitiveProModel currentModel;
        public IntuitiveProModel CurrentModel 
		{
			get 
			{
				return currentModel;
			}
			private set 
			{
				currentModel = value;
			}
		}

        public AnimatableUnity Model 
		{ 
			get 
			{ 
				return _plugin.Model;  
			}
			set
			{
             
                _plugin.Model = value;
			}
		}

        public bool AnimationRunning
        {
            get
            {
                return animationRunning;
            }
            set
            {
                animationRunning = value;
            }
        }

        #endregion


        #region events

        public event EventHandler<AvatarEventArgs> AvatarUpdated;

        private void OnAvatarUpdated(AvatarEventArgs e)
        {
            EventHandler<AvatarEventArgs> temp = AvatarUpdated;

            if (temp != null)
            {
                temp(this, e);
            }
        }


        #endregion


        #region methods

        private void Initialize() 
		{
			_plugin = PluginConnector.Instance;

            AvatarUpdated += UIController.OnAvatarUpdated;

			EditorApplication.update += Update;

		}


        public void Update()
		{
            try
            { 

			    if (null == CurrentModel || !animationRunning)
			    {
				    return;
			    }
			
			    CurrentModel.UpdateMeshes();
            }
            catch (UnityException exp)
            {
                UnityEngine.Debug.LogError(exp.Message);
            }

        }


        public bool CreateAdapter(string avatarID, string outputPathName, IEnumerable<Tuple<Texture2D, Point[]>> images, ModelDensity meshDensity, string logFileName, bool triGenerationEnabled)
        {
            bool success = false;

            try
            {
                if (_plugin != null)
                {
                    success = _plugin.CreateAdapter(avatarID, outputPathName, images, meshDensity, logFileName, triGenerationEnabled);

                }

            }
            catch (Exception exp)
            {
                Debug.Log(exp);
                throw;
            }

            return success;
        }

        public IAvatar RunAdapter(string hairFileName, EyeColor eyeColor, int speedUp)
        {
            IAvatar avatar = null;

            try
            {

                if (_plugin != null)
                {
                    avatar = _plugin.RunAdapter(hairFileName, eyeColor, speedUp);

                }

            }
            catch (Exception exp)
            {
                Debug.Log(exp);
                throw;
            }

            return avatar;
        }


        public void ChangePose(PoseConfigViewModel poseViewModel, ControlLever ctrlLever)
		{
			try
			{

				if (_plugin != null)
				{
                    animationRunning = true;
                    
                    _plugin.Pose(poseViewModel, ctrlLever);

				}
			}
			catch(UnityException exp)
			{
				UnityEngine.Debug.LogError(exp.Message);
			}
		}

        public void ChangeEyes(float eyeHorizontalMovement, float eyeVerticalMovement)
        {
            try
            {

                if (_plugin != null)
                {
                    animationRunning = true;

                    _plugin.MoveEyes(eyeHorizontalMovement, eyeVerticalMovement);

                }
            }
            catch (UnityException exp)
            {
                UnityEngine.Debug.LogError(exp.Message);
            }
        }
		

        public bool IsAnimating()
		{
			return animationRunning;
		}
		
		
        public bool HasGeneratedModel()
		{
			return CurrentModel != null;
		}
		
        public void GenerateModel()
		{
            try
            { 
			    MeshGenerator gen = new MeshGenerator();
			    var go = gen.GenerateGameObjects(PluginManager.Instance.Model);
			    CurrentModel = go.GetComponent<IntuitiveProModel>();
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }

        #endregion


        #region modeling
   
        public void ProcessTexture(StrEventArgs strArgs)
        {
            try
            {          
                AvatarResourceManager.CopyTexture(strArgs.StrVar);
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }

        }
       

        #endregion


    }

}