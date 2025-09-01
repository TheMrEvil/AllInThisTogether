using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000215 RID: 533
	[VisibleToOtherModules]
	[NativeHeader("Runtime/Export/Scripting/ScriptingRuntime.h")]
	internal class ScriptingRuntime
	{
		// Token: 0x06001763 RID: 5987
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetAllUserAssemblies();

		// Token: 0x06001764 RID: 5988 RVA: 0x00002072 File Offset: 0x00000272
		public ScriptingRuntime()
		{
		}
	}
}
