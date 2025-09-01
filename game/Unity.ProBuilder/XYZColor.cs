using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000011 RID: 17
	internal sealed class XYZColor
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000045FA File Offset: 0x000027FA
		public XYZColor(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004617 File Offset: 0x00002817
		public static XYZColor FromRGB(Color col)
		{
			return ColorUtility.RGBToXYZ(col);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000461F File Offset: 0x0000281F
		public static XYZColor FromRGB(float R, float G, float B)
		{
			return ColorUtility.RGBToXYZ(R, G, B);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004629 File Offset: 0x00002829
		public override string ToString()
		{
			return string.Format("( {0}, {1}, {2} )", this.x, this.y, this.z);
		}

		// Token: 0x04000044 RID: 68
		public float x;

		// Token: 0x04000045 RID: 69
		public float y;

		// Token: 0x04000046 RID: 70
		public float z;
	}
}
