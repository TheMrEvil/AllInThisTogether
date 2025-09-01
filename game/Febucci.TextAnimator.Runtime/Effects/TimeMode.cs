using System;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public struct TimeMode
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000494C File Offset: 0x00002B4C
		public TimeMode(bool useUniformTime)
		{
			this.useUniformTime = useUniformTime;
			this.waveSize = 0f;
			this.timeSpeed = 1f;
			this.startDelay = 0f;
			this.tempTime = 0f;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004984 File Offset: 0x00002B84
		public float GetTime(float animatorTime, float charTime, int charIndex)
		{
			this.tempTime = ((this.useUniformTime ? animatorTime : charTime) - this.startDelay) * this.timeSpeed - this.waveSize * (float)charIndex;
			if (this.tempTime < this.startDelay)
			{
				return -1f;
			}
			return this.tempTime;
		}

		// Token: 0x04000096 RID: 150
		public float startDelay;

		// Token: 0x04000097 RID: 151
		public bool useUniformTime;

		// Token: 0x04000098 RID: 152
		public float waveSize;

		// Token: 0x04000099 RID: 153
		public float timeSpeed;

		// Token: 0x0400009A RID: 154
		private float tempTime;
	}
}
