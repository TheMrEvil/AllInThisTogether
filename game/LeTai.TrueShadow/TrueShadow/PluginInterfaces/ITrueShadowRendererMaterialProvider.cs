using System;
using UnityEngine;

namespace LeTai.TrueShadow.PluginInterfaces
{
	// Token: 0x02000023 RID: 35
	public interface ITrueShadowRendererMaterialProvider
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000123 RID: 291
		// (remove) Token: 0x06000124 RID: 292
		event Action materialReplaced;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000125 RID: 293
		// (remove) Token: 0x06000126 RID: 294
		event Action materialModified;

		// Token: 0x06000127 RID: 295
		Material GetTrueShadowRendererMaterial();
	}
}
