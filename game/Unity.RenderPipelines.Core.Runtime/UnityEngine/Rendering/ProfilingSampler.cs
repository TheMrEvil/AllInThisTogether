using System;
using System.Runtime.CompilerServices;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x02000071 RID: 113
	public class ProfilingSampler
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00011320 File Offset: 0x0000F520
		public static ProfilingSampler Get<TEnum>(TEnum marker) where TEnum : Enum
		{
			TProfilingSampler<TEnum> result;
			TProfilingSampler<TEnum>.samples.TryGetValue(marker, out result);
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001133C File Offset: 0x0000F53C
		public ProfilingSampler(string name)
		{
			this.sampler = CustomSampler.Create(name, true);
			this.inlineSampler = CustomSampler.Create("Inl_" + name, false);
			this.name = name;
			this.m_Recorder = this.sampler.GetRecorder();
			this.m_Recorder.enabled = false;
			this.m_InlineRecorder = this.inlineSampler.GetRecorder();
			this.m_InlineRecorder.enabled = false;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000113B4 File Offset: 0x0000F5B4
		public void Begin(CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (this.sampler != null && this.sampler.isValid)
				{
					cmd.BeginSample(this.sampler);
					return;
				}
				cmd.BeginSample(this.name);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000113E7 File Offset: 0x0000F5E7
		public void End(CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (this.sampler != null && this.sampler.isValid)
				{
					cmd.EndSample(this.sampler);
					return;
				}
				cmd.EndSample(this.name);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001141A File Offset: 0x0000F61A
		internal bool IsValid()
		{
			return this.sampler != null && this.inlineSampler != null;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001142F File Offset: 0x0000F62F
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00011437 File Offset: 0x0000F637
		internal CustomSampler sampler
		{
			[CompilerGenerated]
			get
			{
				return this.<sampler>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<sampler>k__BackingField = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00011440 File Offset: 0x0000F640
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00011448 File Offset: 0x0000F648
		internal CustomSampler inlineSampler
		{
			[CompilerGenerated]
			get
			{
				return this.<inlineSampler>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<inlineSampler>k__BackingField = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00011451 File Offset: 0x0000F651
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00011459 File Offset: 0x0000F659
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<name>k__BackingField = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00011462 File Offset: 0x0000F662
		public bool enableRecording
		{
			set
			{
				this.m_Recorder.enabled = value;
				this.m_InlineRecorder.enabled = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001147C File Offset: 0x0000F67C
		public float gpuElapsedTime
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_Recorder.gpuElapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000114A3 File Offset: 0x0000F6A3
		public int gpuSampleCount
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0;
				}
				return this.m_Recorder.gpuSampleBlockCount;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000114BF File Offset: 0x0000F6BF
		public float cpuElapsedTime
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_Recorder.elapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000114E6 File Offset: 0x0000F6E6
		public int cpuSampleCount
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0;
				}
				return this.m_Recorder.sampleBlockCount;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00011502 File Offset: 0x0000F702
		public float inlineCpuElapsedTime
		{
			get
			{
				if (!this.m_InlineRecorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_InlineRecorder.elapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00011529 File Offset: 0x0000F729
		public int inlineCpuSampleCount
		{
			get
			{
				if (!this.m_InlineRecorder.enabled)
				{
					return 0;
				}
				return this.m_InlineRecorder.sampleBlockCount;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011545 File Offset: 0x0000F745
		private ProfilingSampler()
		{
		}

		// Token: 0x0400024D RID: 589
		[CompilerGenerated]
		private CustomSampler <sampler>k__BackingField;

		// Token: 0x0400024E RID: 590
		[CompilerGenerated]
		private CustomSampler <inlineSampler>k__BackingField;

		// Token: 0x0400024F RID: 591
		[CompilerGenerated]
		private string <name>k__BackingField;

		// Token: 0x04000250 RID: 592
		private Recorder m_Recorder;

		// Token: 0x04000251 RID: 593
		private Recorder m_InlineRecorder;
	}
}
