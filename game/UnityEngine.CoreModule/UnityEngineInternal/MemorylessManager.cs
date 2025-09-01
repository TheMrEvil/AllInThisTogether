using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	public class MemorylessManager
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000207C File Offset: 0x0000027C
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002093 File Offset: 0x00000293
		public static MemorylessMode depthMemorylessMode
		{
			get
			{
				return MemorylessManager.GetFramebufferDepthMemorylessMode();
			}
			set
			{
				MemorylessManager.SetFramebufferDepthMemorylessMode(value);
			}
		}

		// Token: 0x06000014 RID: 20
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetFramebufferDepthMemorylessMode")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MemorylessMode GetFramebufferDepthMemorylessMode();

		// Token: 0x06000015 RID: 21
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "SetFramebufferDepthMemorylessMode")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetFramebufferDepthMemorylessMode(MemorylessMode mode);

		// Token: 0x06000016 RID: 22 RVA: 0x00002072 File Offset: 0x00000272
		public MemorylessManager()
		{
		}
	}
}
