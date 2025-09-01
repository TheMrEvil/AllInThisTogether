using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	internal static class SubsystemDescriptorBindings
	{
		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create(IntPtr descriptorPtr);

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetId(IntPtr descriptorPtr);
	}
}
