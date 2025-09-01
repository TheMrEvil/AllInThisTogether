using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RendererUtils
{
	// Token: 0x0200042C RID: 1068
	public struct RendererListDesc
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x0003EE5F File Offset: 0x0003D05F
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x0003EE67 File Offset: 0x0003D067
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

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x0003EE70 File Offset: 0x0003D070
		// (set) Token: 0x06002538 RID: 9528 RVA: 0x0003EE78 File Offset: 0x0003D078
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

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x0003EE81 File Offset: 0x0003D081
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x0003EE89 File Offset: 0x0003D089
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

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x0003EE92 File Offset: 0x0003D092
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x0003EE9A File Offset: 0x0003D09A
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

		// Token: 0x0600253D RID: 9533 RVA: 0x0003EEA3 File Offset: 0x0003D0A3
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

		// Token: 0x0600253E RID: 9534 RVA: 0x0003EEDB File Offset: 0x0003D0DB
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

		// Token: 0x0600253F RID: 9535 RVA: 0x0003EF18 File Offset: 0x0003D118
		public bool IsValid()
		{
			bool flag = this.camera == null || (this.passName == ShaderTagId.none && (this.passNames == null || this.passNames.Length == 0));
			return !flag;
		}

		// Token: 0x04000DD9 RID: 3545
		public SortingCriteria sortingCriteria;

		// Token: 0x04000DDA RID: 3546
		public PerObjectData rendererConfiguration;

		// Token: 0x04000DDB RID: 3547
		public RenderQueueRange renderQueueRange;

		// Token: 0x04000DDC RID: 3548
		public RenderStateBlock? stateBlock;

		// Token: 0x04000DDD RID: 3549
		public Material overrideMaterial;

		// Token: 0x04000DDE RID: 3550
		public bool excludeObjectMotionVectors;

		// Token: 0x04000DDF RID: 3551
		public int layerMask;

		// Token: 0x04000DE0 RID: 3552
		public int overrideMaterialPassIndex;

		// Token: 0x04000DE1 RID: 3553
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private CullingResults <cullingResult>k__BackingField;

		// Token: 0x04000DE2 RID: 3554
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Camera <camera>k__BackingField;

		// Token: 0x04000DE3 RID: 3555
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ShaderTagId <passName>k__BackingField;

		// Token: 0x04000DE4 RID: 3556
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ShaderTagId[] <passNames>k__BackingField;
	}
}
