using System;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class EmissionCurve
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00004795 File Offset: 0x00002995
		public float GetMaxDuration()
		{
			if (this.cycles <= 0)
			{
				return -1f;
			}
			return this.duration * (float)this.cycles;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000047B4 File Offset: 0x000029B4
		public EmissionCurve()
		{
			this.cycles = -1;
			this.duration = 1f;
			this.weightOverTime = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(1f, 1f)
			});
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004816 File Offset: 0x00002A16
		public EmissionCurve(params Keyframe[] keyframes)
		{
			this.cycles = -1;
			this.duration = 1f;
			this.weightOverTime = new AnimationCurve(keyframes);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000483C File Offset: 0x00002A3C
		public float Evaluate(float timePassed)
		{
			if (this.cycles > 0 && timePassed > this.duration * (float)this.cycles)
			{
				return 0f;
			}
			return this.weightOverTime.Evaluate(timePassed % this.duration);
		}

		// Token: 0x0400008E RID: 142
		public int cycles;

		// Token: 0x0400008F RID: 143
		public float duration;

		// Token: 0x04000090 RID: 144
		[SerializeField]
		public AnimationCurve weightOverTime;
	}
}
