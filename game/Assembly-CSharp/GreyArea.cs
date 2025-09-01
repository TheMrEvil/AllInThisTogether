using System;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000007 RID: 7
[PostProcess(typeof(GreyAreaRenderer), PostProcessEvent.AfterStack, "Vellum/Grey Area", true)]
[Serializable]
public class GreyArea : PostProcessEffectSettings
{
	// Token: 0x06000021 RID: 33 RVA: 0x000042CA File Offset: 0x000024CA
	public override bool IsEnabledAndSupported(PostProcessRenderContext context)
	{
		return this.enabled.value;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000042DC File Offset: 0x000024DC
	public GreyArea()
	{
	}
}
