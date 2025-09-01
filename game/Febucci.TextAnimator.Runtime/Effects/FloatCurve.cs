using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public struct FloatCurve
	{
		// Token: 0x0600009E RID: 158 RVA: 0x0000487C File Offset: 0x00002A7C
		public FloatCurve(float amplitude, float waveSize, float defaultAmplitude)
		{
			this.defaultAmplitude = defaultAmplitude;
			this.enabled = false;
			this.amplitude = amplitude;
			this.weightOverTime = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.5f, 0.5f),
				new Keyframe(1f, 0f)
			});
			this.weightOverTime.preWrapMode = WrapMode.Loop;
			this.weightOverTime.postWrapMode = WrapMode.Loop;
			this.waveSize = 0f;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004914 File Offset: 0x00002B14
		public float Evaluate(float passedTime, int charIndex)
		{
			if (!this.enabled)
			{
				return this.defaultAmplitude;
			}
			return Mathf.LerpUnclamped(this.defaultAmplitude, this.amplitude, this.weightOverTime.Evaluate(passedTime + this.waveSize * (float)charIndex));
		}

		// Token: 0x04000091 RID: 145
		public bool enabled;

		// Token: 0x04000092 RID: 146
		private readonly float defaultAmplitude;

		// Token: 0x04000093 RID: 147
		public AnimationCurve weightOverTime;

		// Token: 0x04000094 RID: 148
		public float amplitude;

		// Token: 0x04000095 RID: 149
		public float waveSize;
	}
}
