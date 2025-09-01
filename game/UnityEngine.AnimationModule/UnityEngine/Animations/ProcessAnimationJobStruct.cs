using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Animations
{
	// Token: 0x02000047 RID: 71
	internal struct ProcessAnimationJobStruct<T> where T : struct, IAnimationJob
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000045E4 File Offset: 0x000027E4
		public static IntPtr GetJobReflectionData()
		{
			bool flag = ProcessAnimationJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ProcessAnimationJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ProcessAnimationJobStruct<T>.ExecuteJobFunction(ProcessAnimationJobStruct<T>.Execute), null, null);
			}
			return ProcessAnimationJobStruct<T>.jobReflectionData;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00004634 File Offset: 0x00002834
		public unsafe static void Execute(ref T data, IntPtr animationStreamPtr, IntPtr methodIndex, ref JobRanges ranges, int jobIndex)
		{
			AnimationStream stream;
			UnsafeUtility.CopyPtrToStructure<AnimationStream>((void*)animationStreamPtr, out stream);
			JobMethodIndex jobMethodIndex = (JobMethodIndex)methodIndex.ToInt32();
			JobMethodIndex jobMethodIndex2 = jobMethodIndex;
			JobMethodIndex jobMethodIndex3 = jobMethodIndex2;
			if (jobMethodIndex3 != JobMethodIndex.ProcessRootMotionMethodIndex)
			{
				if (jobMethodIndex3 != JobMethodIndex.ProcessAnimationMethodIndex)
				{
					throw new NotImplementedException("Invalid Animation jobs method index.");
				}
				data.ProcessAnimation(stream);
			}
			else
			{
				data.ProcessRootMotion(stream);
			}
		}

		// Token: 0x0400013F RID: 319
		private static IntPtr jobReflectionData;

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x060002AC RID: 684
		public delegate void ExecuteJobFunction(ref T data, IntPtr animationStreamPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);
	}
}
