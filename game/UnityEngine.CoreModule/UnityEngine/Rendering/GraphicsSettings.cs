using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003E2 RID: 994
	[StaticAccessor("GetGraphicsSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Camera/GraphicsSettings.h")]
	public sealed class GraphicsSettings : Object
	{
		// Token: 0x06001FA7 RID: 8103 RVA: 0x0000E886 File Offset: 0x0000CA86
		private GraphicsSettings()
		{
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001FA8 RID: 8104
		// (set) Token: 0x06001FA9 RID: 8105
		public static extern TransparencySortMode transparencySortMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x00033E40 File Offset: 0x00032040
		// (set) Token: 0x06001FAB RID: 8107 RVA: 0x00033E55 File Offset: 0x00032055
		public static Vector3 transparencySortAxis
		{
			get
			{
				Vector3 result;
				GraphicsSettings.get_transparencySortAxis_Injected(out result);
				return result;
			}
			set
			{
				GraphicsSettings.set_transparencySortAxis_Injected(ref value);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001FAC RID: 8108
		// (set) Token: 0x06001FAD RID: 8109
		public static extern bool realtimeDirectRectangularAreaLights { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001FAE RID: 8110
		// (set) Token: 0x06001FAF RID: 8111
		public static extern bool lightsUseLinearIntensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001FB0 RID: 8112
		// (set) Token: 0x06001FB1 RID: 8113
		public static extern bool lightsUseColorTemperature { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001FB2 RID: 8114
		// (set) Token: 0x06001FB3 RID: 8115
		public static extern uint defaultRenderingLayerMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001FB4 RID: 8116
		// (set) Token: 0x06001FB5 RID: 8117
		public static extern bool useScriptableRenderPipelineBatching { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001FB6 RID: 8118
		// (set) Token: 0x06001FB7 RID: 8119
		public static extern bool logWhenShaderIsCompiled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001FB8 RID: 8120
		// (set) Token: 0x06001FB9 RID: 8121
		public static extern bool disableBuiltinCustomRenderTextureUpdate { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001FBA RID: 8122
		public static extern VideoShadersIncludeMode videoShadersIncludeMode { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001FBB RID: 8123
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasShaderDefine(GraphicsTier tier, BuiltinShaderDefine defineHash);

		// Token: 0x06001FBC RID: 8124 RVA: 0x00033E60 File Offset: 0x00032060
		public static bool HasShaderDefine(BuiltinShaderDefine defineHash)
		{
			return GraphicsSettings.HasShaderDefine(Graphics.activeTier, defineHash);
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001FBD RID: 8125
		[NativeName("CurrentRenderPipeline")]
		private static extern ScriptableObject INTERNAL_currentRenderPipeline { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x00033E80 File Offset: 0x00032080
		public static RenderPipelineAsset currentRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_currentRenderPipeline as RenderPipelineAsset;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x00033E9C File Offset: 0x0003209C
		// (set) Token: 0x06001FC0 RID: 8128 RVA: 0x00033EB3 File Offset: 0x000320B3
		public static RenderPipelineAsset renderPipelineAsset
		{
			get
			{
				return GraphicsSettings.defaultRenderPipeline;
			}
			set
			{
				GraphicsSettings.defaultRenderPipeline = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001FC1 RID: 8129
		// (set) Token: 0x06001FC2 RID: 8130
		[NativeName("DefaultRenderPipeline")]
		private static extern ScriptableObject INTERNAL_defaultRenderPipeline { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x00033EC0 File Offset: 0x000320C0
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x00033EDC File Offset: 0x000320DC
		public static RenderPipelineAsset defaultRenderPipeline
		{
			get
			{
				return GraphicsSettings.INTERNAL_defaultRenderPipeline as RenderPipelineAsset;
			}
			set
			{
				GraphicsSettings.INTERNAL_defaultRenderPipeline = value;
			}
		}

		// Token: 0x06001FC5 RID: 8133
		[NativeName("GetAllConfiguredRenderPipelinesForScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject[] GetAllConfiguredRenderPipelines();

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00033EE8 File Offset: 0x000320E8
		public static RenderPipelineAsset[] allConfiguredRenderPipelines
		{
			get
			{
				return GraphicsSettings.GetAllConfiguredRenderPipelines().Cast<RenderPipelineAsset>().ToArray<RenderPipelineAsset>();
			}
		}

		// Token: 0x06001FC7 RID: 8135
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object GetGraphicsSettings();

		// Token: 0x06001FC8 RID: 8136
		[NativeName("SetShaderModeScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShaderMode(BuiltinShaderType type, BuiltinShaderMode mode);

		// Token: 0x06001FC9 RID: 8137
		[NativeName("GetShaderModeScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern BuiltinShaderMode GetShaderMode(BuiltinShaderType type);

		// Token: 0x06001FCA RID: 8138
		[NativeName("SetCustomShaderScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCustomShader(BuiltinShaderType type, Shader shader);

		// Token: 0x06001FCB RID: 8139
		[NativeName("GetCustomShaderScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Shader GetCustomShader(BuiltinShaderType type);

		// Token: 0x06001FCC RID: 8140 RVA: 0x00033F09 File Offset: 0x00032109
		public static void RegisterRenderPipelineSettings<T>(RenderPipelineGlobalSettings settings) where T : RenderPipeline
		{
			GraphicsSettings.RegisterRenderPipeline(typeof(T).FullName, settings);
		}

		// Token: 0x06001FCD RID: 8141
		[NativeName("RegisterRenderPipelineSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterRenderPipeline(string renderpipelineName, Object settings);

		// Token: 0x06001FCE RID: 8142 RVA: 0x00033F22 File Offset: 0x00032122
		public static void UnregisterRenderPipelineSettings<T>() where T : RenderPipeline
		{
			GraphicsSettings.UnregisterRenderPipeline(typeof(T).FullName);
		}

		// Token: 0x06001FCF RID: 8143
		[NativeName("UnregisterRenderPipelineSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterRenderPipeline(string renderpipelineName);

		// Token: 0x06001FD0 RID: 8144 RVA: 0x00033F3C File Offset: 0x0003213C
		public static RenderPipelineGlobalSettings GetSettingsForRenderPipeline<T>() where T : RenderPipeline
		{
			return GraphicsSettings.GetSettingsForRenderPipeline(typeof(T).FullName) as RenderPipelineGlobalSettings;
		}

		// Token: 0x06001FD1 RID: 8145
		[NativeName("GetSettingsForRenderPipeline")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object GetSettingsForRenderPipeline(string renderpipelineName);

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001FD2 RID: 8146
		// (set) Token: 0x06001FD3 RID: 8147
		public static extern bool cameraRelativeLightCulling { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001FD4 RID: 8148
		// (set) Token: 0x06001FD5 RID: 8149
		public static extern bool cameraRelativeShadowCulling { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001FD6 RID: 8150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_transparencySortAxis_Injected(out Vector3 ret);

		// Token: 0x06001FD7 RID: 8151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transparencySortAxis_Injected(ref Vector3 value);
	}
}
