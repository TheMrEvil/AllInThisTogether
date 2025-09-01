using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000089 RID: 137
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class PackingAttribute : Attribute
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0001443C File Offset: 0x0001263C
		public PackingAttribute(string[] displayNames, FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 32, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = displayNames;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			this.sizeInBits = bitSize;
			this.range = new float[]
			{
				minValue,
				maxValue
			};
			this.preprocessor = preprocessor;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000144A4 File Offset: 0x000126A4
		public PackingAttribute(string displayName = "", FieldPacking packingScheme = FieldPacking.NoPacking, int bitSize = 0, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = new string[1];
			this.displayNames[0] = displayName;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			this.sizeInBits = bitSize;
			this.range = new float[]
			{
				minValue,
				maxValue
			};
			this.preprocessor = preprocessor;
		}

		// Token: 0x040002D3 RID: 723
		public string[] displayNames;

		// Token: 0x040002D4 RID: 724
		public float[] range;

		// Token: 0x040002D5 RID: 725
		public FieldPacking packingScheme;

		// Token: 0x040002D6 RID: 726
		public int offsetInSource;

		// Token: 0x040002D7 RID: 727
		public int sizeInBits;

		// Token: 0x040002D8 RID: 728
		public bool isDirection;

		// Token: 0x040002D9 RID: 729
		public bool sRGBDisplay;

		// Token: 0x040002DA RID: 730
		public bool checkIsNormalized;

		// Token: 0x040002DB RID: 731
		public string preprocessor;
	}
}
