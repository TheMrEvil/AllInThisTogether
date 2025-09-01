using System;
using UnityEngine;

namespace LeTai.Effects
{
	// Token: 0x0200002A RID: 42
	public class ScalableBlurConfig : BlurConfig
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006AAA File Offset: 0x00004CAA
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006AB2 File Offset: 0x00004CB2
		public float Radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				this.radius = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006AC5 File Offset: 0x00004CC5
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006ACD File Offset: 0x00004CCD
		public int Iteration
		{
			get
			{
				return this.iteration;
			}
			set
			{
				this.iteration = Mathf.Max(0, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006ADC File Offset: 0x00004CDC
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public int MaxDepth
		{
			get
			{
				return this.maxDepth;
			}
			set
			{
				this.maxDepth = Mathf.Max(1, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006AF4 File Offset: 0x00004CF4
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00006B27 File Offset: 0x00004D27
		public float Strength
		{
			get
			{
				return this.strength = this.radius * (float)(3 * (1 << this.iteration) - 2) / ScalableBlurConfig.UNIT_VARIANCE;
			}
			set
			{
				this.strength = Mathf.Max(0f, value);
				this.SetAdvancedFieldFromSimple();
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006B40 File Offset: 0x00004D40
		protected virtual void SetAdvancedFieldFromSimple()
		{
			if ((double)this.strength < 0.01)
			{
				this.iteration = 0;
				this.radius = 0f;
				return;
			}
			float num = this.strength * ScalableBlurConfig.UNIT_VARIANCE;
			this.iteration = Mathf.CeilToInt(Mathf.Log(0.16666667f * (Mathf.Sqrt(12f * num + 1f) + 5f)) / Mathf.Log(2f));
			this.radius = num / (float)(3 * (1 << this.iteration) - 2);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006BD0 File Offset: 0x00004DD0
		private void OnValidate()
		{
			this.SetAdvancedFieldFromSimple();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006BD8 File Offset: 0x00004DD8
		public ScalableBlurConfig()
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006BF9 File Offset: 0x00004DF9
		// Note: this type is marked as 'beforefieldinit'.
		static ScalableBlurConfig()
		{
		}

		// Token: 0x040000B9 RID: 185
		[SerializeField]
		private float radius = 4f;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private int iteration = 4;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		private int maxDepth = 6;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		[Range(0f, 256f)]
		private float strength;

		// Token: 0x040000BD RID: 189
		private static readonly float UNIT_VARIANCE = 1f + Mathf.Sqrt(2f) / 2f;
	}
}
