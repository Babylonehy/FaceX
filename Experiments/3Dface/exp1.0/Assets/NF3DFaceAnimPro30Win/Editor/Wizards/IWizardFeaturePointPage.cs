
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.NFEditor
{

	public interface IWizardFeaturePointPage: IWizardPage
	{
		Texture ExampleTexture { get; }
		
		List<PickedPoint> Points { get; }

		int PointsNumber { get; }

		bool CheckPoints();
	    
	}
}