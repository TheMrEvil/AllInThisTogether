using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200046F RID: 1135
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	internal class BuiltinRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		// Token: 0x06002828 RID: 10280 RVA: 0x00042ED8 File Offset: 0x000410D8
		public bool TickRealtimeProbes()
		{
			return BuiltinRuntimeReflectionSystem.BuiltinUpdate();
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x00042EEF File Offset: 0x000410EF
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x00004563 File Offset: 0x00002763
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x0600282B RID: 10283
		[StaticAccessor("GetReflectionProbes()", Type = StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool BuiltinUpdate();

		// Token: 0x0600282C RID: 10284 RVA: 0x00042EFC File Offset: 0x000410FC
		[RequiredByNativeCode]
		private static BuiltinRuntimeReflectionSystem Internal_BuiltinRuntimeReflectionSystem_New()
		{
			return new BuiltinRuntimeReflectionSystem();
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x00002072 File Offset: 0x00000272
		public BuiltinRuntimeReflectionSystem()
		{
		}
	}
}
