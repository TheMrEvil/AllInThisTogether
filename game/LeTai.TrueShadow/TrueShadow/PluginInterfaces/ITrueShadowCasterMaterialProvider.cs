using System;
using UnityEngine;

namespace LeTai.TrueShadow.PluginInterfaces
{
	// Token: 0x0200001F RID: 31
	public interface ITrueShadowCasterMaterialProvider
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600011B RID: 283
		// (remove) Token: 0x0600011C RID: 284
		event Action materialReplaced;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600011D RID: 285
		// (remove) Token: 0x0600011E RID: 286
		event Action materialModified;

		// Token: 0x0600011F RID: 287
		Material GetTrueShadowCasterMaterial();
	}
}
