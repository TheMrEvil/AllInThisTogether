using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B8 RID: 184
	public class VolumeComponentMenuForRenderPipeline : VolumeComponentMenu
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001C8EB File Offset: 0x0001AAEB
		public Type[] pipelineTypes
		{
			[CompilerGenerated]
			get
			{
				return this.<pipelineTypes>k__BackingField;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		public VolumeComponentMenuForRenderPipeline(string menu, params Type[] pipelineTypes) : base(menu)
		{
			if (pipelineTypes == null)
			{
				throw new Exception("Specify a list of supported pipeline");
			}
			foreach (Type type in pipelineTypes)
			{
				if (!typeof(RenderPipeline).IsAssignableFrom(type))
				{
					throw new Exception(string.Format("You can only specify types that inherit from {0}, please check {1}", typeof(RenderPipeline), type));
				}
			}
			this.pipelineTypes = pipelineTypes;
		}

		// Token: 0x0400039A RID: 922
		[CompilerGenerated]
		private readonly Type[] <pipelineTypes>k__BackingField;
	}
}
