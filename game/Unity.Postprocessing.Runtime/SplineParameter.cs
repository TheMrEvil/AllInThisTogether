using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public sealed class SplineParameter : ParameterOverride<Spline>
	{
		// Token: 0x06000112 RID: 274 RVA: 0x0000B2DB File Offset: 0x000094DB
		protected internal override void OnEnable()
		{
			if (this.value != null)
			{
				this.value.Cache(int.MinValue);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000B2F5 File Offset: 0x000094F5
		internal override void SetValue(ParameterOverride parameter)
		{
			base.SetValue(parameter);
			if (this.value != null)
			{
				this.value.Cache(Time.renderedFrameCount);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000B318 File Offset: 0x00009518
		public override void Interp(Spline from, Spline to, float t)
		{
			if (from == null || to == null)
			{
				base.Interp(from, to, t);
				return;
			}
			int renderedFrameCount = Time.renderedFrameCount;
			from.Cache(renderedFrameCount);
			to.Cache(renderedFrameCount);
			for (int i = 0; i < 128; i++)
			{
				float num = from.cachedData[i];
				float num2 = to.cachedData[i];
				this.value.cachedData[i] = num + (num2 - num) * t;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000B37F File Offset: 0x0000957F
		public SplineParameter()
		{
		}
	}
}
