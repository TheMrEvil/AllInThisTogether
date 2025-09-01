using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000472 RID: 1138
	[NativeHeader("Runtime/Camera/ScriptableRuntimeReflectionSystem.h")]
	[RequiredByNativeCode]
	public static class ScriptableRuntimeReflectionSystemSettings
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x00042F3C File Offset: 0x0004113C
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x00042F54 File Offset: 0x00041154
		public static IScriptableRuntimeReflectionSystem system
		{
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.Internal_ScriptableRuntimeReflectionSystemSettings_system;
			}
			set
			{
				bool flag = value == null || value.Equals(null);
				if (flag)
				{
					Debug.LogError("'null' cannot be assigned to ScriptableRuntimeReflectionSystemSettings.system");
				}
				else
				{
					bool flag2 = !(ScriptableRuntimeReflectionSystemSettings.system is BuiltinRuntimeReflectionSystem) && !(value is BuiltinRuntimeReflectionSystem) && ScriptableRuntimeReflectionSystemSettings.system != value;
					if (flag2)
					{
						Debug.LogWarningFormat("ScriptableRuntimeReflectionSystemSettings.system is assigned more than once. Only a the last instance will be used. (Last instance {0}, New instance {1})", new object[]
						{
							ScriptableRuntimeReflectionSystemSettings.system,
							value
						});
					}
					ScriptableRuntimeReflectionSystemSettings.Internal_ScriptableRuntimeReflectionSystemSettings_system = value;
				}
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x00042FCC File Offset: 0x000411CC
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x00042FE8 File Offset: 0x000411E8
		private static IScriptableRuntimeReflectionSystem Internal_ScriptableRuntimeReflectionSystemSettings_system
		{
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation;
			}
			[RequiredByNativeCode]
			set
			{
				bool flag = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != value;
				if (flag)
				{
					bool flag2 = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != null;
					if (flag2)
					{
						ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation.Dispose();
					}
				}
				ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation = value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x0004303C File Offset: 0x0004123C
		private static ScriptableRuntimeReflectionSystemWrapper Internal_ScriptableRuntimeReflectionSystemSettings_instance
		{
			[RequiredByNativeCode]
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.s_Instance;
			}
		}

		// Token: 0x06002838 RID: 10296
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		[StaticAccessor("ScriptableRuntimeReflectionSystem", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScriptingDirtyReflectionSystemInstance();

		// Token: 0x06002839 RID: 10297 RVA: 0x00043053 File Offset: 0x00041253
		// Note: this type is marked as 'beforefieldinit'.
		static ScriptableRuntimeReflectionSystemSettings()
		{
		}

		// Token: 0x04000ECA RID: 3786
		private static ScriptableRuntimeReflectionSystemWrapper s_Instance = new ScriptableRuntimeReflectionSystemWrapper();
	}
}
