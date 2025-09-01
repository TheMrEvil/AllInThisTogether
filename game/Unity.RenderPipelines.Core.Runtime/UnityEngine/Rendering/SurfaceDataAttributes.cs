using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000087 RID: 135
	[AttributeUsage(AttributeTargets.Field)]
	public class SurfaceDataAttributes : Attribute
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000143A0 File Offset: 0x000125A0
		public SurfaceDataAttributes(string displayName = "", bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
			this.checkIsNormalized = checkIsNormalized;
			this.preprocessor = preprocessor;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000143EE File Offset: 0x000125EE
		public SurfaceDataAttributes(string[] displayNames, bool isDirection = false, bool sRGBDisplay = false, FieldPrecision precision = FieldPrecision.Default, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = displayNames;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.precision = precision;
			this.checkIsNormalized = checkIsNormalized;
			this.preprocessor = preprocessor;
		}

		// Token: 0x040002CB RID: 715
		public string[] displayNames;

		// Token: 0x040002CC RID: 716
		public bool isDirection;

		// Token: 0x040002CD RID: 717
		public bool sRGBDisplay;

		// Token: 0x040002CE RID: 718
		public FieldPrecision precision;

		// Token: 0x040002CF RID: 719
		public bool checkIsNormalized;

		// Token: 0x040002D0 RID: 720
		public string preprocessor;
	}
}
