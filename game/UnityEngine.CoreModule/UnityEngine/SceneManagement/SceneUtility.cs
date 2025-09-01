using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002EB RID: 747
	[NativeHeader("Runtime/Export/SceneManager/SceneUtility.bindings.h")]
	public static class SceneUtility
	{
		// Token: 0x06001E84 RID: 7812
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetScenePathByBuildIndex(int buildIndex);

		// Token: 0x06001E85 RID: 7813
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetBuildIndexByScenePath(string scenePath);
	}
}
