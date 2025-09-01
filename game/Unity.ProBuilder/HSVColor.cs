using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000010 RID: 16
	internal sealed class HSVColor
	{
		// Token: 0x06000085 RID: 133 RVA: 0x0000454E File Offset: 0x0000274E
		public HSVColor(float h, float s, float v)
		{
			this.h = h;
			this.s = s;
			this.v = v;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000456B File Offset: 0x0000276B
		public HSVColor(float h, float s, float v, float sv_modifier)
		{
			this.h = h;
			this.s = s * sv_modifier;
			this.v = v * sv_modifier;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000458E File Offset: 0x0000278E
		public static HSVColor FromRGB(Color col)
		{
			return ColorUtility.RGBtoHSV(col);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004596 File Offset: 0x00002796
		public override string ToString()
		{
			return string.Format("( {0}, {1}, {2} )", this.h, this.s, this.v);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000045C3 File Offset: 0x000027C3
		public float SqrDistance(HSVColor InColor)
		{
			return InColor.h / 360f - this.h / 360f + (InColor.s - this.s) + (InColor.v - this.v);
		}

		// Token: 0x04000041 RID: 65
		public float h;

		// Token: 0x04000042 RID: 66
		public float s;

		// Token: 0x04000043 RID: 67
		public float v;
	}
}
