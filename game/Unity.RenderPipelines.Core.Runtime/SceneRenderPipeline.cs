using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000004 RID: 4
[ExecuteAlways]
public class SceneRenderPipeline : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	private void OnEnable()
	{
		GraphicsSettings.renderPipelineAsset = this.renderPipelineAsset;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000206D File Offset: 0x0000026D
	private void OnValidate()
	{
		GraphicsSettings.renderPipelineAsset = this.renderPipelineAsset;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000207A File Offset: 0x0000027A
	public SceneRenderPipeline()
	{
	}

	// Token: 0x04000001 RID: 1
	public RenderPipelineAsset renderPipelineAsset;
}
