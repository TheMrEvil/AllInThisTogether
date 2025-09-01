using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public sealed class FloatParameter : ParameterOverride<float>
	{
		// Token: 0x060000FE RID: 254 RVA: 0x0000B03C File Offset: 0x0000923C
		public override void Interp(float from, float to, float t)
		{
			this.value = from + (to - from) * t;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000B04B File Offset: 0x0000924B
		public FloatParameter()
		{
		}
	}
}
