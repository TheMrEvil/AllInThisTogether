using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public sealed class ColorParameter : ParameterOverride<Color>
	{
		// Token: 0x06000103 RID: 259 RVA: 0x0000B078 File Offset: 0x00009278
		public override void Interp(Color from, Color to, float t)
		{
			this.value.r = from.r + (to.r - from.r) * t;
			this.value.g = from.g + (to.g - from.g) * t;
			this.value.b = from.b + (to.b - from.b) * t;
			this.value.a = from.a + (to.a - from.a) * t;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000B109 File Offset: 0x00009309
		public static implicit operator Vector4(ColorParameter prop)
		{
			return prop.value;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000B116 File Offset: 0x00009316
		public ColorParameter()
		{
		}
	}
}
