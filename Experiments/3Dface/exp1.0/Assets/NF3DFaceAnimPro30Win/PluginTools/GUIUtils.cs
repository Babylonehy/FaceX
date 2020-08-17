
using UnityEngine;

namespace Assets.Scripts.NFScript
{

    public class GUIUtils
	{

	    public static Rect DrawTouchableTexture(Rect boxRect, Texture2D texture, out TouchInfo touchInfo)
	    {
	        Event e = Event.current;
	        Rect textureRect = new Rect(boxRect);
	        textureRect.x++;
	        textureRect.y++;
	        textureRect.width -= 2;
	        textureRect.height -= 2;

	        GUI.DrawTexture(textureRect, texture, ScaleMode.ScaleToFit);

	        bool wider = texture.width > texture.height;
	        Vector2 newTextureSize = new Vector2(wider ? textureRect.width : (texture.width * textureRect.height / (float)texture.height),
	            wider ? (texture.height * textureRect.width / (float)texture.width) : textureRect.height);

	        textureRect.Set(textureRect.center.x - newTextureSize.x / 2f,
	            textureRect.center.y - newTextureSize.y / 2f, newTextureSize.x, newTextureSize.y);

			Vector2 relTouchPos = e.mousePosition - textureRect.position;
			Vector2 relPercTouchPos = relTouchPos;
			relPercTouchPos.x = relPercTouchPos.x/textureRect.width;
			relPercTouchPos.y = relPercTouchPos.y/textureRect.height;
			Vector2 textureTouchPos = new Vector2((int)(relPercTouchPos.x*texture.width), (int)(relPercTouchPos.y*texture.height));
			if(e.button == 0 && textureRect.Contains(e.mousePosition))
			{
				touchInfo = new TouchInfo(e.type == EventType.MouseDown, true, textureTouchPos, relPercTouchPos,relTouchPos,e.mousePosition);
			}
			else
			{
				touchInfo = new TouchInfo(e.type == EventType.MouseDown, false, textureTouchPos, relPercTouchPos,relTouchPos,e.mousePosition);
			}

			return textureRect;
	    }


	    public class TouchInfo 
		{
	        public bool isTouching;
			public bool touchInside;
			public Vector2 textureTouchPosition;
			public Vector2 relativePercentageTouchPosition;
	        public Vector2 relativeTouchPosition;
	        public Vector2 absoluteTouchPosition;

			public TouchInfo(): this(false,false,Vector2.zero,Vector2.zero,Vector2.zero,Vector2.zero) 
			{ 
			}

			public TouchInfo(bool isTouching, bool touchInside, Vector2 textureTouchPosition,
			                 Vector2 relativePercentageTouchPosition, 
			                 Vector2 relativeTouchPosition, Vector2 absoluteToucePosition) 
			{
	            this.isTouching = isTouching;
	            this.touchInside = touchInside;
				this.textureTouchPosition = textureTouchPosition;
				this.relativePercentageTouchPosition = relativePercentageTouchPosition;
	            this.relativeTouchPosition = relativeTouchPosition;
	            this.absoluteTouchPosition = absoluteToucePosition;
	        }
	    }

	}

}
