using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public sealed class IntParameter : ParameterOverride<int>
	{
		// Token: 0x06000100 RID: 256 RVA: 0x0000B053 File Offset: 0x00009253
		public override void Interp(int from, int to, float t)
		{
			this.value = (int)((float)from + (float)(to - from) * t);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000B065 File Offset: 0x00009265
		public IntParameter()
		{
		}
	}
}
