using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

using UnityEngine;
using UnityEditor;


namespace Assets.Scripts.NFAudio
{
    [ExecuteInEditMode]
    public class SoundFileLoader
    {

        const string tempAssetFolder = "_temp";

        public string _tempRelativeFolderPath;
        public string TempRelativeFolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(_tempRelativeFolderPath))
                    _tempRelativeFolderPath = "Assets/NF3DFaceAnimPro30Win/" + tempAssetFolder;
                return _tempRelativeFolderPath;
            }
        }

    
        public string _absoluteTempFolderPath;
        public string AbsoluteTempFolderPath 
        {
            get 
            {
                if(string.IsNullOrEmpty(_absoluteTempFolderPath))
                {
                    _absoluteTempFolderPath = Path.Combine(Application.dataPath,tempAssetFolder);
                }
                return _absoluteTempFolderPath;
            } 
        }

        private SoundAnalyzerManager soundManager;

        private string rootProjectFolder;
        private AudioImporter audioImporter;
        private AudioClip audioClip;

        private AudioImporterSampleSettings audioImporterSettings;
        public SoundFileLoader(SoundAnalyzerManager soundManager)
        {
            try
            {
        
                this.soundManager = soundManager;
            
                audioImporterSettings = new AudioImporterSampleSettings();
                audioImporterSettings.loadType = AudioClipLoadType.DecompressOnLoad;
                audioImporterSettings.compressionFormat = AudioCompressionFormat.PCM;
                audioImporterSettings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

                var regex = new Regex(Regex.Escape("Assets"));
                rootProjectFolder= regex.Replace(Application.dataPath, string.Empty, 1);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
        }


        private Thread loadingThread;

        public void AsyncLoadFile(string fsFilePath)
        {
            if(loadingThread == null)
                loadingThread = new Thread(LoadFileStartFunction);

            PrepareToLoadFile(fsFilePath);
            loadingThread.Start(fsFilePath);
        }

        public void Abort()
        {
            if(loadingThread != null)
                loadingThread.Abort();
        }

        private void  LoadFileStartFunction(System.Object oFsFilePath)
        {

            string fsFilePath = Convert.ToString(oFsFilePath);

            LoadFile(fsFilePath);
        }



        public void SyncLoadFile(string fsFilePath)
        {
            try
            {
                PrepareToLoadFile(fsFilePath);
                LoadFile(fsFilePath);
            }
            catch (System.Exception exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }

        private void PrepareToLoadFile(string fsFilePath)
        {
            try
            {

                soundManager.SetState(StateId.LoadingStarted, "Loading " + fsFilePath);

#if UNITY_EDITOR
                if (!AssetDatabase.IsValidFolder(TempRelativeFolderPath))
                {
                    AssetDatabase.CreateFolder("Assets", tempAssetFolder);
                    AssetDatabase.SaveAssets();
                }
#endif

            }
            catch (System.Exception exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }
        }

        private void LoadFile(string fsFilePath)
        {
            try
            { 

                string sourceFileName = Path.GetFileName(fsFilePath);
                string destAssetPath = Path.Combine(TempRelativeFolderPath,sourceFileName);

                if(!sourceFileName.Contains(TempRelativeFolderPath) )
                {

                    destAssetPath = GeneratePath(TempRelativeFolderPath, sourceFileName);

                    if (string.IsNullOrEmpty(destAssetPath))
                    {
                        throw new ApplicationException("Can't locate audio path. ");
                    }

                    string destFsFilePath = Path.Combine(rootProjectFolder, destAssetPath);

                    soundManager.SetState(StateId.CopyToTempFolderInProgress, "Copy file from " + fsFilePath + " to " + destFsFilePath);

                    try
                    {
                        if (File.Exists(destFsFilePath))
                        {
                            File.Delete(destFsFilePath);
                        }
                        File.Copy(fsFilePath, destFsFilePath);
                    }
                    catch (UnityException exp)
                    {
                        Debug.LogError(exp.Message);
                    }
#if UNITY_EDITOR
                    AssetDatabase.ImportAsset(destAssetPath,ImportAssetOptions.ForceUncompressedImport);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
#endif
                }


                audioImporter = AudioImporter.GetAtPath(destAssetPath) as AudioImporter;
                if(audioImporter  == null)
                {
                    soundManager.SetState(StateId.Abort, "Aborted  as file " + destAssetPath + " has incompatible format");
                    return;
                }

                audioImporter.preloadAudioData = false;
                audioImporter.forceToMono = false;
                audioImporter.loadInBackground = false;
            
                audioImporter.defaultSampleSettings = audioImporterSettings;
                soundManager.SetState(StateId.LoadingAudioData, "Import " + destAssetPath);

                audioImporter.SaveAndReimport();

#if UNITY_EDITOR
                audioClip = AssetDatabase.LoadAssetAtPath(destAssetPath, typeof (AudioClip)) as AudioClip;
#endif
                if (audioClip == null || !audioClip.LoadAudioData())
                {
                    soundManager.SetState(StateId.Abort, "Aborted: Can't load " + destAssetPath + " as audioclip");
                    return;
                }

                soundManager.audioClip = audioClip;
                soundManager.audioImporter = audioImporter;
                soundManager.SetState(StateId.DataLoaded, "Loaded " + destAssetPath);
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
                throw;
            }

        }


        private string GeneratePath(string relativeFolderPath, string sourceFileName)
        {
            string assetPath = null;

#if UNITY_EDITOR
            try
            {
                string tempPath = Path.Combine(relativeFolderPath, sourceFileName);


                string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath(tempPath);


                if (string.IsNullOrEmpty(uniqueAssetPath))
                {
                    assetPath = tempPath;
                }
                else
                {
                    assetPath = uniqueAssetPath;
                }
            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }
            catch (System.Exception exp)
            {
                Debug.Log(exp.Message);
            }
#endif
            return assetPath;
        }

        private static int GetBitRate(string fsFilePath)
        {
            var mp3Header = new Mp3Header();
            mp3Header.ReadMp3Information(fsFilePath);
            return mp3Header.IntBitRate;
        }


    }

}