using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Runtime/Export/Graphics/GraphicsDeviceDebug.bindings.h")]
	[StaticAccessor("GraphicsDeviceDebug", StaticAccessorType.DoubleColon)]
	internal static class GraphicsDeviceDebug
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000020A0 File Offset: 0x000002A0
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000020B5 File Offset: 0x000002B5
		internal static GraphicsDeviceDebugSettings settings
		{
			get
			{
				GraphicsDeviceDebugSettings result;
				GraphicsDeviceDebug.get_settings_Injected(out result);
				return result;
			}
			set
			{
				GraphicsDeviceDebug.set_settings_Injected(ref value);
			}
		}

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_settings_Injected(out GraphicsDeviceDebugSettings ret);

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_settings_Injected(ref GraphicsDeviceDebugSettings value);
	}
}
