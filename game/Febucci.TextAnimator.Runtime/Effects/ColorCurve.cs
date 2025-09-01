using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public struct ColorCurve
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000046AC File Offset: 0x000028AC
		public ColorCurve(float waveSize)
		{
			this.enabled = false;
			this.waveSize = waveSize;
			this.duration = 1f;
			this.colorOverTime = new Gradient();
			this.colorOverTime.SetKeys(new GradientColorKey[]
			{
				new GradientColorKey(Color.white, 0f),
				new GradientColorKey(Color.cyan, 0.5f),
				new GradientColorKey(Color.white, 1f)
			}, new GradientAlphaKey[]
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004762 File Offset: 0x00002962
		public Color32 Evaluate(float time, int charIndex)
		{
			time = Mathf.Repeat(time + (float)charIndex * this.waveSize, this.duration);
			return this.colorOverTime.Evaluate(time);
		}

		// Token: 0x0400008A RID: 138
		public bool enabled;

		// Token: 0x0400008B RID: 139
		public Gradient colorOverTime;

		// Token: 0x0400008C RID: 140
		public float waveSize;

		// Token: 0x0400008D RID: 141
		public float duration;
	}
}
