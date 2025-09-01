using System;
using Unity.Profiling;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x0200027A RID: 634
	[UsedByNativeCode]
	public sealed class Recorder
	{
		// Token: 0x06001BAB RID: 7083 RVA: 0x00008CBB File Offset: 0x00006EBB
		internal Recorder()
		{
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0002C4E8 File Offset: 0x0002A6E8
		internal Recorder(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (!flag)
			{
				this.m_RecorderCPU = new ProfilerRecorder(handle, 1, (ProfilerRecorderOptions)153);
				bool flag2 = (ProfilerRecorderHandle.GetDescription(handle).Flags & MarkerFlags.SampleGPU) > MarkerFlags.Default;
				if (flag2)
				{
					this.m_RecorderGPU = new ProfilerRecorder(handle, 1, (ProfilerRecorderOptions)217);
				}
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0002C54C File Offset: 0x0002A74C
		~Recorder()
		{
			this.m_RecorderCPU.Dispose();
			this.m_RecorderGPU.Dispose();
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0002C590 File Offset: 0x0002A790
		public static Recorder Get(string samplerName)
		{
			ProfilerRecorderHandle handle = ProfilerRecorderHandle.Get(ProfilerCategory.Any, samplerName);
			bool flag = !handle.Valid;
			Recorder result;
			if (flag)
			{
				result = Recorder.s_InvalidRecorder;
			}
			else
			{
				result = new Recorder(handle);
			}
			return result;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0002C5CC File Offset: 0x0002A7CC
		public bool isValid
		{
			get
			{
				return this.m_RecorderCPU.handle > 0UL;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0002C5F0 File Offset: 0x0002A7F0
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x0002C60D File Offset: 0x0002A80D
		public bool enabled
		{
			get
			{
				return this.m_RecorderCPU.IsRunning;
			}
			set
			{
				this.SetEnabled(value);
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0002C618 File Offset: 0x0002A818
		public long elapsedNanoseconds
		{
			get
			{
				bool flag = !this.m_RecorderCPU.Valid;
				long result;
				if (flag)
				{
					result = 0L;
				}
				else
				{
					result = this.m_RecorderCPU.LastValue;
				}
				return result;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0002C64C File Offset: 0x0002A84C
		public long gpuElapsedNanoseconds
		{
			get
			{
				bool flag = !this.m_RecorderGPU.Valid;
				long result;
				if (flag)
				{
					result = 0L;
				}
				else
				{
					result = this.m_RecorderGPU.LastValue;
				}
				return result;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0002C680 File Offset: 0x0002A880
		public int sampleBlockCount
		{
			get
			{
				bool flag = !this.m_RecorderCPU.Valid;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = this.m_RecorderCPU.Count != 1;
					if (flag2)
					{
						result = 0;
					}
					else
					{
						result = (int)this.m_RecorderCPU.GetSample(0).Count;
					}
				}
				return result;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0002C6D8 File Offset: 0x0002A8D8
		public int gpuSampleBlockCount
		{
			get
			{
				bool flag = !this.m_RecorderGPU.Valid;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = this.m_RecorderGPU.Count != 1;
					if (flag2)
					{
						result = 0;
					}
					else
					{
						result = (int)this.m_RecorderGPU.GetSample(0).Count;
					}
				}
				return result;
			}
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0002C730 File Offset: 0x0002A930
		public void FilterToCurrentThread()
		{
			bool flag = !this.m_RecorderCPU.Valid;
			if (!flag)
			{
				this.m_RecorderCPU.FilterToCurrentThread();
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0002C760 File Offset: 0x0002A960
		public void CollectFromAllThreads()
		{
			bool flag = !this.m_RecorderCPU.Valid;
			if (!flag)
			{
				this.m_RecorderCPU.CollectFromAllThreads();
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0002C790 File Offset: 0x0002A990
		private void SetEnabled(bool state)
		{
			if (state)
			{
				this.m_RecorderCPU.Start();
				bool valid = this.m_RecorderGPU.Valid;
				if (valid)
				{
					this.m_RecorderGPU.Start();
				}
			}
			else
			{
				this.m_RecorderCPU.Stop();
				bool valid2 = this.m_RecorderGPU.Valid;
				if (valid2)
				{
					this.m_RecorderGPU.Stop();
				}
			}
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0002C7F7 File Offset: 0x0002A9F7
		// Note: this type is marked as 'beforefieldinit'.
		static Recorder()
		{
		}

		// Token: 0x0400090D RID: 2317
		private const ProfilerRecorderOptions s_RecorderDefaultOptions = (ProfilerRecorderOptions)153;

		// Token: 0x0400090E RID: 2318
		internal static Recorder s_InvalidRecorder = new Recorder();

		// Token: 0x0400090F RID: 2319
		private ProfilerRecorder m_RecorderCPU;

		// Token: 0x04000910 RID: 2320
		private ProfilerRecorder m_RecorderGPU;
	}
}
