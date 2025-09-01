using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000042 RID: 66
	public sealed class SketchRenderer : PostProcessEffectRenderer<Sketch>
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000744D File Offset: 0x0000564D
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Sketch");
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000745F File Offset: 0x0000565F
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007468 File Offset: 0x00005668
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, false);
			gpuprojectionMatrix[2, 3] = (gpuprojectionMatrix[3, 2] = 0f);
			gpuprojectionMatrix[3, 3] = 1f;
			Matrix4x4 value = Matrix4x4.Inverse(gpuprojectionMatrix * context.camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, -gpuprojectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value);
			if (base.settings.strokeTex.value)
			{
				propertySheet.properties.SetTexture("_Strokes", base.settings.strokeTex);
			}
			propertySheet.properties.SetVector("_Params", new Vector4(0f, (float)base.settings.blendMode.value, base.settings.intensity, (base.settings.projectionMode.value == Sketch.SketchProjectionMode.ScreenSpace) ? (base.settings.tiling * 0.1f) : base.settings.tiling));
			propertySheet.properties.SetVector("_Brightness", base.settings.brightness);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.projectionMode.value, false, null, false);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007617 File Offset: 0x00005817
		public SketchRenderer()
		{
		}

		// Token: 0x0400013B RID: 315
		private Shader shader;
	}
}
