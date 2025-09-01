using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000008 RID: 8
public class GreyAreaRenderer : PostProcessEffectRenderer<GreyArea>
{
	// Token: 0x06000023 RID: 35 RVA: 0x000042E4 File Offset: 0x000024E4
	public override void Init()
	{
		this.shader = Shader.Find("Hidden/PostProcess/Greyscale");
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000042F6 File Offset: 0x000024F6
	public override void Release()
	{
		base.Release();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00004300 File Offset: 0x00002500
	public override void Render(PostProcessRenderContext context)
	{
		PropertySheet propertySheet = context.propertySheets.Get(this.shader);
		Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, false);
		gpuprojectionMatrix[2, 3] = (gpuprojectionMatrix[3, 2] = 0f);
		gpuprojectionMatrix[3, 3] = 1f;
		Matrix4x4 value = Matrix4x4.Inverse(gpuprojectionMatrix * context.camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, -gpuprojectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
		propertySheet.properties.SetMatrix("clipToWorld", value);
		context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000043D3 File Offset: 0x000025D3
	public GreyAreaRenderer()
	{
	}

	// Token: 0x04000013 RID: 19
	private Shader shader;
}
