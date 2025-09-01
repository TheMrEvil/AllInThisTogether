using System;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x0200027B RID: 635
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[UsedByNativeCode]
	public class Sampler
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x00008CBB File Offset: 0x00006EBB
		internal Sampler()
		{
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0002C803 File Offset: 0x0002AA03
		internal Sampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0002C814 File Offset: 0x0002AA14
		public bool isValid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0002C838 File Offset: 0x0002AA38
		public Recorder GetRecorder()
		{
			ProfilerRecorderHandle handle = new ProfilerRecorderHandle((ulong)this.m_Ptr.ToInt64());
			return new Recorder(handle);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0002C864 File Offset: 0x0002AA64
		public static Sampler Get(string name)
		{
			IntPtr marker = ProfilerUnsafeUtility.GetMarker(name);
			bool flag = marker == IntPtr.Zero;
			Sampler result;
			if (flag)
			{
				result = Sampler.s_InvalidSampler;
			}
			else
			{
				result = new Sampler(marker);
			}
			return result;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0002C89C File Offset: 0x0002AA9C
		public static int GetNames(List<string> names)
		{
			List<ProfilerRecorderHandle> list = new List<ProfilerRecorderHandle>();
			ProfilerRecorderHandle.GetAvailable(list);
			bool flag = names != null;
			if (flag)
			{
				bool flag2 = names.Count < list.Count;
				if (flag2)
				{
					names.Capacity = list.Count;
					for (int i = names.Count; i < list.Count; i++)
					{
						names.Add(null);
					}
				}
				int num = 0;
				foreach (ProfilerRecorderHandle handle in list)
				{
					names[num] = ProfilerRecorderHandle.GetDescription(handle).Name;
					num++;
				}
			}
			return list.Count;
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0002C978 File Offset: 0x0002AB78
		public string name
		{
			get
			{
				return ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
			}
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0002C995 File Offset: 0x0002AB95
		// Note: this type is marked as 'beforefieldinit'.
		static Sampler()
		{
		}

		// Token: 0x04000911 RID: 2321
		internal IntPtr m_Ptr;

		// Token: 0x04000912 RID: 2322
		internal static Sampler s_InvalidSampler = new Sampler();
	}
}
