using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RendererUtils
{
	// Token: 0x0200042D RID: 1069
	internal struct RendererListParams
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x0003EF6E File Offset: 0x0003D16E
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x0003EF76 File Offset: 0x0003D176
		public bool isValid
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<isValid>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isValid>k__BackingField = value;
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0003EF80 File Offset: 0x0003D180
		internal static RendererListParams Create(in RendererListDesc desc)
		{
			RendererListParams rendererListParams = default(RendererListParams);
			RendererListDesc rendererListDesc = desc;
			bool flag = !rendererListDesc.IsValid();
			RendererListParams result;
			if (flag)
			{
				result = rendererListParams;
			}
			else
			{
				SortingSettings sortingSettings = new SortingSettings(desc.camera)
				{
					criteria = desc.sortingCriteria
				};
				DrawingSettings drawingSettings = new DrawingSettings(RendererListParams.s_EmptyName, sortingSettings)
				{
					perObjectData = desc.rendererConfiguration
				};
				bool flag2 = desc.passName != ShaderTagId.none;
				if (flag2)
				{
					Debug.Assert(desc.passNames == null);
					drawingSettings.SetShaderPassName(0, desc.passName);
				}
				else
				{
					for (int i = 0; i < desc.passNames.Length; i++)
					{
						drawingSettings.SetShaderPassName(i, desc.passNames[i]);
					}
				}
				bool flag3 = desc.overrideMaterial != null;
				if (flag3)
				{
					drawingSettings.overrideMaterial = desc.overrideMaterial;
					drawingSettings.overrideMaterialPassIndex = desc.overrideMaterialPassIndex;
				}
				FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(desc.renderQueueRange), desc.layerMask, uint.MaxValue, 0)
				{
					excludeMotionVectorObjects = desc.excludeObjectMotionVectors
				};
				rendererListParams.isValid = true;
				rendererListParams.cullingResult = desc.cullingResult;
				rendererListParams.drawSettings = drawingSettings;
				rendererListParams.filteringSettings = filteringSettings;
				rendererListParams.stateBlock = desc.stateBlock;
				result = rendererListParams;
			}
			return result;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0003F0F7 File Offset: 0x0003D2F7
		// Note: this type is marked as 'beforefieldinit'.
		static RendererListParams()
		{
		}

		// Token: 0x04000DE5 RID: 3557
		private static readonly ShaderTagId s_EmptyName = new ShaderTagId("");

		// Token: 0x04000DE6 RID: 3558
		public static readonly RendererListParams nullRendererList = default(RendererListParams);

		// Token: 0x04000DE7 RID: 3559
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <isValid>k__BackingField;

		// Token: 0x04000DE8 RID: 3560
		internal CullingResults cullingResult;

		// Token: 0x04000DE9 RID: 3561
		internal DrawingSettings drawSettings;

		// Token: 0x04000DEA RID: 3562
		internal FilteringSettings filteringSettings;

		// Token: 0x04000DEB RID: 3563
		internal RenderStateBlock? stateBlock;
	}
}
