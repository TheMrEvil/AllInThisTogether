using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000019 RID: 25
	[Obsolete("Use the updated RendererList API which is defined in the UnityEngine.Rendering.RendererUtils namespace.")]
	public struct RendererList
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006DAD File Offset: 0x00004FAD
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00006DB5 File Offset: 0x00004FB5
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

		// Token: 0x060000CB RID: 203 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public static RendererList Create(in RendererListDesc desc)
		{
			RendererList result = default(RendererList);
			RendererListDesc rendererListDesc = desc;
			if (!rendererListDesc.IsValid())
			{
				return result;
			}
			SortingSettings sortingSettings = new SortingSettings(desc.camera)
			{
				criteria = desc.sortingCriteria
			};
			DrawingSettings drawingSettings = new DrawingSettings(RendererList.s_EmptyName, sortingSettings)
			{
				perObjectData = desc.rendererConfiguration
			};
			if (desc.passName != ShaderTagId.none)
			{
				drawingSettings.SetShaderPassName(0, desc.passName);
			}
			else
			{
				for (int i = 0; i < desc.passNames.Length; i++)
				{
					drawingSettings.SetShaderPassName(i, desc.passNames[i]);
				}
			}
			if (desc.overrideMaterial != null)
			{
				drawingSettings.overrideMaterial = desc.overrideMaterial;
				drawingSettings.overrideMaterialPassIndex = desc.overrideMaterialPassIndex;
			}
			FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(desc.renderQueueRange), desc.layerMask, uint.MaxValue, 0)
			{
				excludeMotionVectorObjects = desc.excludeObjectMotionVectors
			};
			result.isValid = true;
			result.cullingResult = desc.cullingResult;
			result.drawSettings = drawingSettings;
			result.filteringSettings = filteringSettings;
			result.stateBlock = desc.stateBlock;
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006EF6 File Offset: 0x000050F6
		// Note: this type is marked as 'beforefieldinit'.
		static RendererList()
		{
		}

		// Token: 0x040000AF RID: 175
		private static readonly ShaderTagId s_EmptyName = new ShaderTagId("");

		// Token: 0x040000B0 RID: 176
		public static readonly RendererList nullRendererList = default(RendererList);

		// Token: 0x040000B1 RID: 177
		[CompilerGenerated]
		private bool <isValid>k__BackingField;

		// Token: 0x040000B2 RID: 178
		public CullingResults cullingResult;

		// Token: 0x040000B3 RID: 179
		public DrawingSettings drawSettings;

		// Token: 0x040000B4 RID: 180
		public FilteringSettings filteringSettings;

		// Token: 0x040000B5 RID: 181
		public RenderStateBlock? stateBlock;
	}
}
