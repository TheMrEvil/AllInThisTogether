using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000012 RID: 18
	internal sealed class CIELabColor
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004656 File Offset: 0x00002856
		public CIELabColor(float L, float a, float b)
		{
			this.L = L;
			this.a = a;
			this.b = b;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004673 File Offset: 0x00002873
		public static CIELabColor FromXYZ(XYZColor xyz)
		{
			return ColorUtility.XYZToCIE_Lab(xyz);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000467B File Offset: 0x0000287B
		public static CIELabColor FromRGB(Color col)
		{
			return ColorUtility.XYZToCIE_Lab(XYZColor.FromRGB(col));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004688 File Offset: 0x00002888
		public override string ToString()
		{
			return string.Format("( {0}, {1}, {2} )", this.L, this.a, this.b);
		}

		// Token: 0x04000047 RID: 71
		public float L;

		// Token: 0x04000048 RID: 72
		public float a;

		// Token: 0x04000049 RID: 73
		public float b;
	}
}
