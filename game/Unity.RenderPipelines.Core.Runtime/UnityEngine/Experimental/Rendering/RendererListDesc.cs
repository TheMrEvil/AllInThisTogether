using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200001A RID: 26
	[Obsolete("Use the updated RendererList API which is defined in the UnityEngine.Rendering.RendererUtils namespace.")]
	public struct RendererListDesc
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006F12 File Offset: 0x00005112
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00006F1A File Offset: 0x0000511A
		internal CullingResults cullingResult
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<cullingResult>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<cullingResult>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00006F23 File Offset: 0x00005123
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00006F2B File Offset: 0x0000512B
		internal Camera camera
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<camera>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<camera>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006F34 File Offset: 0x00005134
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006F3C File Offset: 0x0000513C
		internal ShaderTagId passName
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<passName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<passName>k__BackingField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006F45 File Offset: 0x00005145
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006F4D File Offset: 0x0000514D
		internal ShaderTagId[] passNames
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<passNames>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<passNames>k__BackingField = value;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006F56 File Offset: 0x00005156
		public RendererListDesc(ShaderTagId passName, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passName = passName;
			this.passNames = null;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.overrideMaterialPassIndex = 0;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006F89 File Offset: 0x00005189
		public RendererListDesc(ShaderTagId[] passNames, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passNames = passNames;
			this.passName = ShaderTagId.none;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.overrideMaterialPassIndex = 0;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006FC0 File Offset: 0x000051C0
		public bool IsValid()
		{
			return !(this.camera == null) && (!(this.passName == ShaderTagId.none) || (this.passNames != null && this.passNames.Length != 0));
		}

		// Token: 0x040000B6 RID: 182
		public SortingCriteria sortingCriteria;

		// Token: 0x040000B7 RID: 183
		public PerObjectData rendererConfiguration;

		// Token: 0x040000B8 RID: 184
		public RenderQueueRange renderQueueRange;

		// Token: 0x040000B9 RID: 185
		public RenderStateBlock? stateBlock;

		// Token: 0x040000BA RID: 186
		public Material overrideMaterial;

		// Token: 0x040000BB RID: 187
		public bool excludeObjectMotionVectors;

		// Token: 0x040000BC RID: 188
		public int layerMask;

		// Token: 0x040000BD RID: 189
		public int overrideMaterialPassIndex;

		// Token: 0x040000BE RID: 190
		[CompilerGenerated]
		private CullingResults <cullingResult>k__BackingField;

		// Token: 0x040000BF RID: 191
		[CompilerGenerated]
		private Camera <camera>k__BackingField;

		// Token: 0x040000C0 RID: 192
		[CompilerGenerated]
		private ShaderTagId <passName>k__BackingField;

		// Token: 0x040000C1 RID: 193
		[CompilerGenerated]
		private ShaderTagId[] <passNames>k__BackingField;
	}
}
