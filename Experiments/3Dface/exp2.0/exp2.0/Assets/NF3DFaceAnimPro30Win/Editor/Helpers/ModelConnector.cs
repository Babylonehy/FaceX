using System;

using MassAnimation.Adapters.PhotoAdapter;
using MassAnimation.Avatar.Entities;
using MassAnimation.Resources;
using MassAnimation.Resources.Entities;
using MassAnimation.Modeling;

using UnityEngine;

using Assets.Scripts.NFScript;

namespace Assets.Scripts.NFEditor
{

    public class ModelConnector 
	{

		public string _frontImgPath; 

		internal ModelConnector ()
		{
		}

		internal ModelConnector (string frontalImagePath)
		{
			_frontImgPath = frontalImagePath;
		}


        internal Animatable BuildAvatarFromFrontImage(string frontImagePath, Point[] pointLocations, EyeColor eyeColor, int speedUp)
        {

            Animatable anim = null;

            try
            {

                Texture2D frontImg = FGCVT.FimPngIo.loadTexture2DFromPngOrJpgFile(frontImagePath);          

                Tuple<Texture2D, Point[]> imgPtPair =
                    new Tuple<Texture2D, Point[]>(frontImg, pointLocations);

                Tuple<Texture2D, Point[]>[] mdls =
                    new Tuple<Texture2D, Point[]>[1];

                mdls[0] = imgPtPair;
         
                string outputPath = ResourceDirectories.TempModelDirectory;

                bool adapterCreated = false;


                try
                {

                    adapterCreated = PluginManager.Instance.CreateAdapter("avatar",
                                                      outputPath,
                                                      mdls,
                                                      ModelDensity.HIGH,
                                                      "",
                                                      false);

                }
                catch (Exception adExp)
                {
                    Debug.LogException(adExp);
                    Debug.Log("AvatarAdapter creation failed. ");
                    throw;
                }

                if (adapterCreated)
                {
                    IAvatar avatar = null;

                    try
                    {

                        avatar = PluginManager.Instance.RunAdapter("", eyeColor, speedUp);
                    }
                    catch (AvatarGenerationException agExp)
                    {
                        
                        string fitErrorMsg = null;                       
                        if (string.Equals(agExp.Message, "User fiducial points not properly placed.", StringComparison.InvariantCultureIgnoreCase))
                        {
                            fitErrorMsg = "Sorry, you did not position the points properly. Please follow the demo video(s) exactly.";
                            throw new ApplicationException(fitErrorMsg);
                        }
                        else
                        {
                            throw new ApplicationException(agExp.Message);
                        }

                    }

                    if (avatar != null)
                    {

                        anim = avatar.ToAnimatable();
                    }
                }  

            }
            catch (Exception exp)
            {
                Debug.LogException(exp);
                throw;
            }

            return anim;

        }


    }

}