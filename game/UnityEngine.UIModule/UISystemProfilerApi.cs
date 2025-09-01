using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[StaticAccessor("UI::SystemProfilerApi", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/UI/Canvas.h")]
	public static class UISystemProfilerApi
	{
		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(UISystemProfilerApi.SampleType type);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(UISystemProfilerApi.SampleType type);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddMarker(string name, Object obj);

		// Token: 0x0200000B RID: 11
		public enum SampleType
		{
			// Token: 0x04000014 RID: 20
			Layout,
			// Token: 0x04000015 RID: 21
			Render
		}
	}
}
