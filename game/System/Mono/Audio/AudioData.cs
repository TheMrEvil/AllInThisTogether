using System;

namespace Mono.Audio
{
	// Token: 0x02000035 RID: 53
	internal abstract class AudioData
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B4 RID: 180
		public abstract int Channels { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B5 RID: 181
		public abstract int Rate { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B6 RID: 182
		public abstract AudioFormat Format { get; }

		// Token: 0x060000B7 RID: 183 RVA: 0x000031E9 File Offset: 0x000013E9
		public virtual void Setup(AudioDevice dev)
		{
			dev.SetFormat(this.Format, this.Channels, this.Rate);
		}

		// Token: 0x060000B8 RID: 184
		public abstract void Play(AudioDevice dev);

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003204 File Offset: 0x00001404
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000320C File Offset: 0x0000140C
		public virtual bool IsStopped
		{
			get
			{
				return this.stopped;
			}
			set
			{
				this.stopped = value;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000219B File Offset: 0x0000039B
		protected AudioData()
		{
		}

		// Token: 0x04000125 RID: 293
		protected const int buffer_size = 4096;

		// Token: 0x04000126 RID: 294
		private bool stopped;
	}
}
