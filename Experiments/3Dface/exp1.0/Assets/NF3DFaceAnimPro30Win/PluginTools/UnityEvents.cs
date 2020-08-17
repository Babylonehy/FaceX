using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFScript
{

[InitializeOnLoad] 
    public class UnityEvents : MonoBehaviour
    {

	    #region Singleton Pattern

	    private static UnityEvents instance;
	    public static UnityEvents Instance{
		    get 
		    {
			    if(instance==null){
				    instance = (UnityEvents) GameObject.FindObjectOfType<UnityEvents>();
				    if(instance==null){
					    instance = new GameObject().AddComponent<UnityEvents>();
					    instance.name = "UnityEvents";
				    }
			    }
			    return instance;
		    }
	    }

	    #endregion


	    #region public attributes

	    public bool showDebugMsgs = true;

	    public List<Component> pauseListeners = new List<Component>();
	    public List<Component> resumeListeners = new List<Component>();
	    public List<Component> playListeners = new List<Component>();
	    public List<Component> stopListeners = new List<Component>();
	    public List<Component> saveSceneListeners = new List<Component>();
	    public List<Component> loadProjectListeners = new List<Component>();

	    #endregion


	    #region private attributes

	    private bool gameStarted = false;
	    private bool wasPaused = false;

	    #endregion


	    #region AddListener

	    public void addEventListener(Component newListener, List<Component> listenerList){
		    if(!listenerList.Contains(newListener))
			    listenerList.Add(newListener);
	    }

	    #endregion


	    #region Initialization

	    static UnityEvents(){
		
			    EditorApplication.update += RunOnce; 
	    }

	    void Start () {
		    EditorApplication.playmodeStateChanged = HandleOnPlayModeChanged;
	    }

	    #endregion


	    #region PlayPauseResumeStop

	    void HandleOnPlayModeChanged()
	    {
		    
            try
            { 
		
		        if (gameStarted && EditorApplication.isPaused && EditorApplication.isPlayingOrWillChangePlaymode)
		        {
			        wasPaused = true;
			        OnEditorPaused();
		        }
		        else if (EditorApplication.isPlayingOrWillChangePlaymode)
		        {
			        if(wasPaused)
                    {
				        wasPaused = false;
				        OnEditorResume();
			        }else if(!gameStarted)
                    {
				        gameStarted = true;
				        OnEditorPlay();
			        }
		        }
		        else if(EditorApplication.isPlaying)
                {
			        gameStarted = false;
			        OnEditorStop();
		        }

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
	    }


	    public virtual void OnEditorPaused()
	    {
            if (showDebugMsgs)
            {
                //Debug.Log("Game in editor is being paused");
            }
		    foreach(Component pauseListener in pauseListeners)
            {
			    pauseListener.SendMessage(MTD_ON_EDITOR_PAUSE, SendMessageOptions.DontRequireReceiver);
		    }
	    }


	    public virtual void OnEditorPlay()
	    {
            if (showDebugMsgs)
            {
                //Debug.Log("Game in editor starts");
            }
		    foreach(Component playListener in playListeners)
            {
			    playListener.SendMessage(MTD_ON_EDITOR_PLAY, SendMessageOptions.DontRequireReceiver);
		    }
	    }


	    public virtual void OnEditorResume()
	    {
            if (showDebugMsgs)
            {
                //Debug.Log("Game in editor is being resumed");
            }
		    foreach(Component resumeListener in resumeListeners)
            {
			    resumeListener.SendMessage(MTD_ON_EDITOR_RESUME, SendMessageOptions.DontRequireReceiver);
		    }
	    }


	    public virtual void OnEditorStop()
	    {
            if (showDebugMsgs)
            {
                //Debug.Log("Game in editor is being stopped");
            }
		    foreach(Component stopListener in stopListeners)
            {
			    stopListener.SendMessage(MTD_ON_EDITOR_STOP, SendMessageOptions.DontRequireReceiver);
		    }
	    }

	    #endregion


	    #region Save Scene

        public class MyAssetModificationProcessor : UnityEditor.AssetModificationProcessor
	    {
		    public static string[] OnWillSaveAssets(string[] paths)
		    {
			    
			    string sceneName = string.Empty;
			    foreach (string path in paths)
			    {
				    if (path.Contains(".unity"))
				    {
					  
					    sceneName = Path.GetFileNameWithoutExtension(path);
				    }
			    }
			    if (sceneName.Length == 0)
			    {
				    return paths;
			    }
			    Instance.OnSaveScene();

			    return paths;
		    }
	    }


	    public void OnSaveScene()
        {
            try 
            { 

                if (Instance.showDebugMsgs)
                {
                    //Debug.Log("Saving scene");
                }
		        foreach(Component saveSceneListener in Instance.saveSceneListeners)
                {
			        saveSceneListener.SendMessage(MTD_ON_SCENE_SAVED, SendMessageOptions.DontRequireReceiver);
		        }


            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
	    }

	    #endregion


	    #region Load Project

	    static void RunOnce()
	    {
		    UnityEvents instantiate = Instance;
		    Instance.OnProjectLoad();
		    EditorApplication.update -= RunOnce;
	    }


	    public void OnProjectLoad()
        {
            try 
            { 
                if (Instance.showDebugMsgs)
                {
                    
                }
		        foreach(Component loadProjectListener in Instance.loadProjectListeners)
                {
			        loadProjectListener.SendMessage(MTD_ON_PROJECT_LOAD, SendMessageOptions.DontRequireReceiver);
		        }

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
	    }

	    #endregion


	    #region Events to Overwrite

	    public const string MTD_ON_SCENE_SAVED = "OnSceneSaved";
	    public const string MTD_ON_EDITOR_STOP = "OnEditorStop";
	    public const string MTD_ON_EDITOR_RESUME = "OnEditorResume";
	    public const string MTD_ON_EDITOR_PLAY = "OnEditorPlay";
	    public const string MTD_ON_EDITOR_PAUSE = "OnEditorPaused";
	    public const string MTD_ON_PROJECT_LOAD = "OnProjectLoad";

	    #endregion

    }

}