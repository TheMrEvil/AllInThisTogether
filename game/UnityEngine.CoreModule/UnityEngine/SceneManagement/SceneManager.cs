using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E5 RID: 741
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	[RequiredByNativeCode]
	public class SceneManager
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001E3F RID: 7743
		public static extern int sceneCount { [NativeHeader("Runtime/SceneManager/SceneManager.h")] [NativeMethod("GetSceneCount")] [StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x000311C8 File Offset: 0x0002F3C8
		public static int sceneCountInBuildSettings
		{
			get
			{
				return SceneManagerAPI.ActiveAPI.GetNumScenesInBuildSettings();
			}
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000311E4 File Offset: 0x0002F3E4
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetActiveScene()
		{
			Scene result;
			SceneManager.GetActiveScene_Injected(out result);
			return result;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000311F9 File Offset: 0x0002F3F9
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static bool SetActiveScene(Scene scene)
		{
			return SceneManager.SetActiveScene_Injected(ref scene);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00031204 File Offset: 0x0002F404
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByPath(string scenePath)
		{
			Scene result;
			SceneManager.GetSceneByPath_Injected(scenePath, out result);
			return result;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0003121C File Offset: 0x0002F41C
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByName(string name)
		{
			Scene result;
			SceneManager.GetSceneByName_Injected(name, out result);
			return result;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00031234 File Offset: 0x0002F434
		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			return SceneManagerAPI.ActiveAPI.GetSceneByBuildIndex(buildIndex);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00031254 File Offset: 0x0002F454
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneAt(int index)
		{
			Scene result;
			SceneManager.GetSceneAt_Injected(index, out result);
			return result;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0003126C File Offset: 0x0002F46C
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene CreateScene([NotNull("ArgumentNullException")] string sceneName, CreateSceneParameters parameters)
		{
			Scene result;
			SceneManager.CreateScene_Injected(sceneName, ref parameters, out result);
			return result;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00031284 File Offset: 0x0002F484
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static bool UnloadSceneInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneInternal_Injected(ref scene, options);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0003128E File Offset: 0x0002F48E
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		private static AsyncOperation UnloadSceneAsyncInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal_Injected(ref scene, options);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00031298 File Offset: 0x0002F498
		private static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = SceneManagerAPI.ActiveAPI.LoadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
			}
			return result;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000312C8 File Offset: 0x0002F4C8
		private static AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation result;
			if (flag)
			{
				outSuccess = false;
				result = null;
			}
			else
			{
				result = SceneManagerAPI.ActiveAPI.UnloadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, immediately, options, out outSuccess);
			}
			return result;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000312FF File Offset: 0x0002F4FF
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static void MergeScenes(Scene sourceScene, Scene destinationScene)
		{
			SceneManager.MergeScenes_Injected(ref sourceScene, ref destinationScene);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0003130A File Offset: 0x0002F50A
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static void MoveGameObjectToScene([NotNull("ArgumentNullException")] GameObject go, Scene scene)
		{
			SceneManager.MoveGameObjectToScene_Injected(go, ref scene);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00031314 File Offset: 0x0002F514
		[RequiredByNativeCode]
		internal static AsyncOperation LoadFirstScene_Internal(bool async)
		{
			return SceneManagerAPI.ActiveAPI.LoadFirstScene(async);
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001E4F RID: 7759 RVA: 0x00031334 File Offset: 0x0002F534
		// (remove) Token: 0x06001E50 RID: 7760 RVA: 0x00031368 File Offset: 0x0002F568
		public static event UnityAction<Scene, LoadSceneMode> sceneLoaded
		{
			[CompilerGenerated]
			add
			{
				UnityAction<Scene, LoadSceneMode> unityAction = SceneManager.sceneLoaded;
				UnityAction<Scene, LoadSceneMode> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<Scene, LoadSceneMode> value2 = (UnityAction<Scene, LoadSceneMode>)Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<Scene, LoadSceneMode>>(ref SceneManager.sceneLoaded, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction<Scene, LoadSceneMode> unityAction = SceneManager.sceneLoaded;
				UnityAction<Scene, LoadSceneMode> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<Scene, LoadSceneMode> value2 = (UnityAction<Scene, LoadSceneMode>)Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<Scene, LoadSceneMode>>(ref SceneManager.sceneLoaded, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001E51 RID: 7761 RVA: 0x0003139C File Offset: 0x0002F59C
		// (remove) Token: 0x06001E52 RID: 7762 RVA: 0x000313D0 File Offset: 0x0002F5D0
		public static event UnityAction<Scene> sceneUnloaded
		{
			[CompilerGenerated]
			add
			{
				UnityAction<Scene> unityAction = SceneManager.sceneUnloaded;
				UnityAction<Scene> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<Scene> value2 = (UnityAction<Scene>)Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<Scene>>(ref SceneManager.sceneUnloaded, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction<Scene> unityAction = SceneManager.sceneUnloaded;
				UnityAction<Scene> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<Scene> value2 = (UnityAction<Scene>)Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<Scene>>(ref SceneManager.sceneUnloaded, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001E53 RID: 7763 RVA: 0x00031404 File Offset: 0x0002F604
		// (remove) Token: 0x06001E54 RID: 7764 RVA: 0x00031438 File Offset: 0x0002F638
		public static event UnityAction<Scene, Scene> activeSceneChanged
		{
			[CompilerGenerated]
			add
			{
				UnityAction<Scene, Scene> unityAction = SceneManager.activeSceneChanged;
				UnityAction<Scene, Scene> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<Scene, Scene> value2 = (UnityAction<Scene, Scene>)Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<Scene, Scene>>(ref SceneManager.activeSceneChanged, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction<Scene, Scene> unityAction = SceneManager.activeSceneChanged;
				UnityAction<Scene, Scene> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<Scene, Scene> value2 = (UnityAction<Scene, Scene>)Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<Scene, Scene>>(ref SceneManager.activeSceneChanged, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0003146C File Offset: 0x0002F66C
		[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
		public static Scene[] GetAllScenes()
		{
			Scene[] array = new Scene[SceneManager.sceneCount];
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				array[i] = SceneManager.GetSceneAt(i);
			}
			return array;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000314B0 File Offset: 0x0002F6B0
		public static Scene CreateScene(string sceneName)
		{
			CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.None);
			return SceneManager.CreateScene(sceneName, parameters);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000314D4 File Offset: 0x0002F6D4
		public static void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneName, parameters);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000314F4 File Offset: 0x0002F6F4
		[ExcludeFromDocs]
		public static void LoadScene(string sceneName)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneName, parameters);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00031514 File Offset: 0x0002F714
		public static Scene LoadScene(string sceneName, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0003153C File Offset: 0x0002F73C
		public static void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneBuildIndex, parameters);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0003155C File Offset: 0x0002F75C
		[ExcludeFromDocs]
		public static void LoadScene(int sceneBuildIndex)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneBuildIndex, parameters);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0003157C File Offset: 0x0002F77C
		public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000315A4 File Offset: 0x0002F7A4
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, parameters);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000315C8 File Offset: 0x0002F7C8
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, parameters);
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000315EC File Offset: 0x0002F7EC
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, false);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00031608 File Offset: 0x0002F808
		public static AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneName, parameters);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0003162C File Offset: 0x0002F82C
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneName, parameters);
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x00031650 File Offset: 0x0002F850
		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, false);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0003166C File Offset: 0x0002F86C
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(Scene scene)
		{
			return SceneManager.UnloadSceneInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00031688 File Offset: 0x0002F888
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(int sceneBuildIndex)
		{
			bool result;
			SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, true, UnloadSceneOptions.None, out result);
			return result;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000316AC File Offset: 0x0002F8AC
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(string sceneName)
		{
			bool result;
			SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, true, UnloadSceneOptions.None, out result);
			return result;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000316CC File Offset: 0x0002F8CC
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000316F0 File Offset: 0x0002F8F0
		public static AsyncOperation UnloadSceneAsync(string sceneName)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00031710 File Offset: 0x0002F910
		public static AsyncOperation UnloadSceneAsync(Scene scene)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0003172C File Offset: 0x0002F92C
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, options, out flag);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00031750 File Offset: 0x0002F950
		public static AsyncOperation UnloadSceneAsync(string sceneName, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, options, out flag);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00031770 File Offset: 0x0002F970
		public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, options);
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0003178C File Offset: 0x0002F98C
		[RequiredByNativeCode]
		private static void Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			bool flag = SceneManager.sceneLoaded != null;
			if (flag)
			{
				SceneManager.sceneLoaded(scene, mode);
			}
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000317B8 File Offset: 0x0002F9B8
		[RequiredByNativeCode]
		private static void Internal_SceneUnloaded(Scene scene)
		{
			bool flag = SceneManager.sceneUnloaded != null;
			if (flag)
			{
				SceneManager.sceneUnloaded(scene);
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000317E0 File Offset: 0x0002F9E0
		[RequiredByNativeCode]
		private static void Internal_ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
		{
			bool flag = SceneManager.activeSceneChanged != null;
			if (flag)
			{
				SceneManager.activeSceneChanged(previousActiveScene, newActiveScene);
			}
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00002072 File Offset: 0x00000272
		public SceneManager()
		{
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00031809 File Offset: 0x0002FA09
		// Note: this type is marked as 'beforefieldinit'.
		static SceneManager()
		{
		}

		// Token: 0x06001E71 RID: 7793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveScene_Injected(out Scene ret);

		// Token: 0x06001E72 RID: 7794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetActiveScene_Injected(ref Scene scene);

		// Token: 0x06001E73 RID: 7795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByPath_Injected(string scenePath, out Scene ret);

		// Token: 0x06001E74 RID: 7796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByName_Injected(string name, out Scene ret);

		// Token: 0x06001E75 RID: 7797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneAt_Injected(int index, out Scene ret);

		// Token: 0x06001E76 RID: 7798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScene_Injected(string sceneName, ref CreateSceneParameters parameters, out Scene ret);

		// Token: 0x06001E77 RID: 7799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnloadSceneInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06001E78 RID: 7800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation UnloadSceneAsyncInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06001E79 RID: 7801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MergeScenes_Injected(ref Scene sourceScene, ref Scene destinationScene);

		// Token: 0x06001E7A RID: 7802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveGameObjectToScene_Injected(GameObject go, ref Scene scene);

		// Token: 0x040009E7 RID: 2535
		internal static bool s_AllowLoadScene = true;

		// Token: 0x040009E8 RID: 2536
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static UnityAction<Scene, LoadSceneMode> sceneLoaded;

		// Token: 0x040009E9 RID: 2537
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static UnityAction<Scene> sceneUnloaded;

		// Token: 0x040009EA RID: 2538
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static UnityAction<Scene, Scene> activeSceneChanged;
	}
}
