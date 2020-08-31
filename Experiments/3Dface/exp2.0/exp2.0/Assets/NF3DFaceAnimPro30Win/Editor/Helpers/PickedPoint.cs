
using System;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{
	public class PickedPoint
	{
		internal Vector2 Location { get; set;}
		internal Vector2 RelativeLocation { get; set;}

		internal PickedPoint()
		{
			Location = Vector2.zero;
			RelativeLocation = Vector2.zero;
		}

		internal PickedPoint(Vector2 loc, Vector2 relativeLoc  )
		{
			Location = loc;
			RelativeLocation = relativeLoc;
		}

	}
}

