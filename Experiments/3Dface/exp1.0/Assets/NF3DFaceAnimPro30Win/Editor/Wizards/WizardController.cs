
using Assets.Scripts.NFScript;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{

    internal static class WizardController
	{

		#region constants

		internal const float ButtonWidth = 70;	
		internal const float ButtonHeight = 20;
		
		private const int dotSize = 8;
		private const int halfDotSize = 4;

		private const double TouchThresholdCoeff = 0.02;

		#endregion
		
		
		#region private members
		
		private static bool addingPoint = false;
		private static bool removingPoint = false;
        private static bool undoPoint = false;
		private static Rect lastTextureRect;

		private static UnityEngine.Texture pointTexture;
			
		#endregion
			
			
		#region properties
			
		internal static EditorWindow ActiveWindow;

		private static UnityEngine.Texture PointTexture
		{
			get
			{
				if (pointTexture == null)
				{
					pointTexture = (UnityEngine.Texture)Resources.Load("dot");
				}
				return pointTexture;
			}
		}	
		
		#endregion
		
		
		
		#region constructors
		
		
		
		#endregion
		
		
		
		#region events & handlers
		
		
		#endregion
		
		
		#region internal methods

		internal static void GoToPage<T>(EditorWindow currentWindow) where T : EditorWindow
		{
			try
			{
				GoToPage<T>(currentWindow, currentWindow.position);
			}
			catch(UnityException exp)
			{
				Debug.LogError(exp.Message);
			}
		}
		
		internal static void GoToPage<T>(EditorWindow currentWindow, Rect rect) where T : EditorWindow
		{
			try
			{
				addingPoint = true;
				removingPoint = false;
				
				var window = EditorWindow.GetWindowWithRect<T>(rect, true, "");
				window.position = rect;
				ActiveWindow = window;
				currentWindow.Close();
			}
			catch(UnityException exp)
			{
				Debug.LogError(exp.Message);
			}
		}
		
		internal static void DrawFeaturePointsSection(IWizardFeaturePointPage wizardPage, Texture2D texture)
		{
			try
			{
				DrawResetBackNext(wizardPage, true, "Next");
				DrawEditorButtons();
				var box = DrawPhotoBox(wizardPage, texture);
				if(wizardPage is IWizardFeaturePointPage)
				{
					DrawPoints((IWizardFeaturePointPage)wizardPage);
				}
				DrawExamplePhoto(box, wizardPage);
			}
			catch(UnityException exp)
			{
				Debug.LogError(exp.Message);
			}
		}
		
		internal static void DrawResetBackNext(IWizardPage wizardPage, bool isResetButton, string nextLabel )
		{
			var top = ((EditorWindow)wizardPage).maxSize.y - 30;
            var resetRect = new Rect(20, top, ButtonWidth, ButtonHeight);
            var backRect = new Rect((isResetButton ? resetRect.xMax : 0) + 20, top, ButtonWidth, ButtonHeight);
            var nextRect = new Rect(backRect.xMax + 20, top, ButtonWidth, ButtonHeight);
			if(isResetButton)
			{
                if (GUI.Button(resetRect, "Reset"))
                {
                    wizardPage.OnReset();
                }
			}
			if (GUI.Button(backRect, "Back"))
			{
				wizardPage.OnBack();
			}
			if (GUI.Button(nextRect, nextLabel))
			{
				wizardPage.OnNext();
			}
		}

        internal static void DrawNext(IWizardPage wizardPage, string nextLabel)     
        {
            var top = ((EditorWindow)wizardPage).maxSize.y - 30;
            var firstRect = new Rect(150, top, ButtonWidth, ButtonHeight);


            if (GUI.Button(firstRect, nextLabel))
            {
                wizardPage.OnNext();
            }
        }
		
		#endregion
		
		
		#region private methods

	    private static void DrawExamplePhoto(Rect photoBox, IWizardFeaturePointPage wizardPage)
	    {
	        GUI.DrawTexture(new Rect(photoBox.xMax + 10, photoBox.yMin, 170, 185), wizardPage.ExampleTexture, ScaleMode.ScaleToFit);
	    }

		private static Rect DrawPhotoBox(IWizardPage wizardPage, Texture2D texture)
	    {
            var boxRect = new Rect(110, 70, 400, 400);      

			try
			{
	        
		        GUI.Box(boxRect,"");
				GUIUtils.TouchInfo touchInfo;
				lastTextureRect = GUIUtils.DrawTouchableTexture(boxRect, texture, out touchInfo);

                
                if (wizardPage is IWizardFeaturePointPage)
                {
                    IWizardFeaturePointPage pointPage = (IWizardFeaturePointPage)wizardPage;

                    if (touchInfo.isTouching && touchInfo.touchInside)
                    {
                       

                        bool collidingExistingPoint = false;
                        PickedPoint collidingPoint = new PickedPoint();

                        foreach (PickedPoint existingPoint in pointPage.Points)
                        {
                            Rect testRect = new Rect(boxRect);
                            if (IsTouchExisting(existingPoint, touchInfo.textureTouchPosition, testRect))
                            {
                                collidingExistingPoint = true;
                                collidingPoint = existingPoint;
                                break;
                            }
                        }

                        if (!collidingExistingPoint && addingPoint)
                        {
                            PickedPoint pickedPt = new PickedPoint(touchInfo.textureTouchPosition, touchInfo.relativePercentageTouchPosition);
                            pointPage.Points.Add(pickedPt);

                            

                        }
                        else if (collidingExistingPoint && removingPoint)
                        {
                            pointPage.Points.Remove(collidingPoint);

                            Debug.Log("Removing point at " + collidingPoint.Location);

                        }

                    }   
                    else if (touchInfo.isTouching && !touchInfo.touchInside)
                    {
                        
                    }

 
                    if (undoPoint)
                    {
                        int ptCnt = pointPage.Points.Count;

                        if (1 <= ptCnt)
                        {
                            pointPage.Points.RemoveAt(ptCnt - 1);
                        }

                        undoPoint = false;
                    }

                    ActiveWindow.Repaint();

                } 


			}
			catch(UnityException exp)
			{
				Debug.LogError(exp.Message);
			}

	        return boxRect;
	    }
		

		private static void DrawPoints (IWizardFeaturePointPage pointPage)
		{
			if(pointPage.PointsNumber < 1)
			{
				return;
			}

			Rect pointRect;
			Vector2 halfPointSize = new Vector2(halfDotSize,halfDotSize);
			foreach(PickedPoint point in pointPage.Points)
			{

				pointRect = new Rect( lastTextureRect.x + lastTextureRect.width*point.RelativeLocation.x - halfPointSize.x,
				                     lastTextureRect.y + lastTextureRect.height*point.RelativeLocation.y - halfPointSize.y,
				                     dotSize,
				                     dotSize);			

				GUI.DrawTexture(pointRect, PointTexture);
			}
		}

	    private static void DrawEditorButtons()
	    {
	        const float left = 10;
            float top = 70; 
	        const float width = 90;
	        const float height = 60;
	        
	        var selectMoveRect = new Rect(left, top, width, height);
	        var style = GUI.skin.GetStyle("button");
	        style.alignment = TextAnchor.MiddleCenter;
	        style.imagePosition = ImagePosition.ImageAbove;
	        var selectMoveContent = new GUIContent
	                                    {
	                                        image = (UnityEngine.Texture)Resources.Load("MovePointPointer"),
	                                        text = "Select Move"
	                                    };
	        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
	        
	        GUI.Button(selectMoveRect, selectMoveContent, style);

	        top += height + 50;
	        var addPointRect = new Rect(left, top, width, height);
	        var addPointContent = new GUIContent("Add Point");

			addingPoint = GUI.Toggle(addPointRect, addingPoint, addPointContent);
			if(addingPoint)
			{
				removingPoint = false;
			}
	        
	        top += height + 2;
	        var deletePointRect = new Rect(left, top, width, height);
	        var deletePointContent = new GUIContent("Delete Point");
	        
			removingPoint = GUI.Toggle(deletePointRect, removingPoint, deletePointContent);
			if(removingPoint)
			{
				addingPoint = false;
			}

            
            top += height + 30;
            var undoRect = new Rect(left, top, (float)(width * 0.67), height / 2);
            GUI.skin.button.alignment = TextAnchor.MiddleCenter;
            if (GUI.Button(undoRect, "Undo"))
            {
                undoPoint = true;
            }

	    }

		private static bool IsTouchExisting(PickedPoint existingPoint, Vector2 touchPoint, Rect rect)
		{
			bool touched = false;

			try
			{
				double threshold = ( (double)rect.width * TouchThresholdCoeff + rect.height * TouchThresholdCoeff ) /2;
				if(Vector2.Distance(existingPoint.Location, touchPoint) <= threshold )
				{
					touched = true;
				}
			}
			catch(UnityException exp)
			{
				Debug.LogError(exp.Message);
			}

			return touched;
		}
				
		#endregion	

	}

}
