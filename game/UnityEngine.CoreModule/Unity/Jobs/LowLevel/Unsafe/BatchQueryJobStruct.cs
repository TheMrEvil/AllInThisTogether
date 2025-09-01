using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000065 RID: 101
	public struct BatchQueryJobStruct<T> where T : struct
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00003700 File Offset: 0x00001900
		public static IntPtr Initialize()
		{
			bool flag = BatchQueryJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				BatchQueryJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), null, null, null);
			}
			return BatchQueryJobStruct<T>.jobReflectionData;
		}

		// Token: 0x04000182 RID: 386
		internal static IntPtr jobReflectionData;
	}
}
