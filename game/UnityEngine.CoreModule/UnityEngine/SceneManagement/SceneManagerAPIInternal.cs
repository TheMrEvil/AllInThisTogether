using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E3 RID: 739
	[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	internal static class SceneManagerAPIInternal
	{
		// Token: 0x06001E2F RID: 7727
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNumScenesInBuildSettings();

		// Token: 0x06001E30 RID: 7728 RVA: 0x0003114C File Offset: 0x0002F34C
		[NativeThrows]
		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			Scene result;
			SceneManagerAPIInternal.GetSceneByBuildIndex_Injected(buildIndex, out result);
			return result;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00031162 File Offset: 0x0002F362
		[NativeThrows]
		public static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			return SceneManagerAPIInternal.LoadSceneAsyncNameIndexInternal_Injected(sceneName, sceneBuildIndex, ref parameters, mustCompleteNextFrame);
		}

		// Token: 0x06001E32 RID: 7730
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess);

		// Token: 0x06001E33 RID: 7731
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByBuildIndex_Injected(int buildIndex, out Scene ret);

		// Token: 0x06001E34 RID: 7732
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation LoadSceneAsyncNameIndexInternal_Injected(string sceneName, int sceneBuildIndex, ref LoadSceneParameters parameters, bool mustCompleteNextFrame);
	}
}
