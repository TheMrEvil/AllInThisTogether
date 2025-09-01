using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003B RID: 59
	[Preserve]
	[Serializable]
	public sealed class TemporalAntialiasing
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00009C33 File Offset: 0x00007E33
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00009C3B File Offset: 0x00007E3B
		public Vector2 jitter
		{
			[CompilerGenerated]
			get
			{
				return this.<jitter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<jitter>k__BackingField = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00009C44 File Offset: 0x00007E44
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00009C4C File Offset: 0x00007E4C
		public int sampleIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<sampleIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<sampleIndex>k__BackingField = value;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009C55 File Offset: 0x00007E55
		public bool IsSupported()
		{
			return SystemInfo.supportedRenderTargetCount >= 2 && SystemInfo.supportsMotionVectors && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009C73 File Offset: 0x00007E73
		internal DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009C76 File Offset: 0x00007E76
		internal void ResetHistory()
		{
			this.m_ResetHistory = true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00009C80 File Offset: 0x00007E80
		private Vector2 GenerateRandomOffset()
		{
			Vector2 result = new Vector2(HaltonSeq.Get((this.sampleIndex & 1023) + 1, 2) - 0.5f, HaltonSeq.Get((this.sampleIndex & 1023) + 1, 3) - 0.5f);
			int num = this.sampleIndex + 1;
			this.sampleIndex = num;
			if (num >= 8)
			{
				this.sampleIndex = 0;
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public Matrix4x4 GetJitteredProjectionMatrix(Camera camera)
		{
			this.jitter = this.GenerateRandomOffset();
			this.jitter *= this.jitterSpread;
			Matrix4x4 result;
			if (this.jitteredMatrixFunc != null)
			{
				result = this.jitteredMatrixFunc(camera, this.jitter);
			}
			else
			{
				result = (camera.orthographic ? RuntimeUtilities.GetJitteredOrthographicProjectionMatrix(camera, this.jitter) : RuntimeUtilities.GetJitteredPerspectiveProjectionMatrix(camera, this.jitter));
			}
			this.jitter = new Vector2(this.jitter.x / (float)camera.pixelWidth, this.jitter.y / (float)camera.pixelHeight);
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009D88 File Offset: 0x00007F88
		public void ConfigureJitteredProjectionMatrix(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			camera.nonJitteredProjectionMatrix = camera.projectionMatrix;
			camera.projectionMatrix = this.GetJitteredProjectionMatrix(camera);
			camera.useJitteredProjectionMatrixForTransparentRendering = false;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009DBC File Offset: 0x00007FBC
		public void ConfigureStereoJitteredProjectionMatrices(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			this.jitter = this.GenerateRandomOffset();
			this.jitter *= this.jitterSpread;
			for (Camera.StereoscopicEye stereoscopicEye = Camera.StereoscopicEye.Left; stereoscopicEye <= Camera.StereoscopicEye.Right; stereoscopicEye++)
			{
				context.camera.CopyStereoDeviceProjectionMatrixToNonJittered(stereoscopicEye);
				Matrix4x4 stereoNonJitteredProjectionMatrix = context.camera.GetStereoNonJitteredProjectionMatrix(stereoscopicEye);
				Matrix4x4 matrix = RuntimeUtilities.GenerateJitteredProjectionMatrixFromOriginal(context, stereoNonJitteredProjectionMatrix, this.jitter);
				context.camera.SetStereoProjectionMatrix(stereoscopicEye, matrix);
			}
			this.jitter = new Vector2(this.jitter.x / (float)context.screenWidth, this.jitter.y / (float)context.screenHeight);
			camera.useJitteredProjectionMatrixForTransparentRendering = false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009E6C File Offset: 0x0000806C
		private void GenerateHistoryName(RenderTexture rt, int id, PostProcessRenderContext context)
		{
			rt.name = "Temporal Anti-aliasing History id #" + id.ToString();
			if (context.stereoActive)
			{
				rt.name = rt.name + " for eye " + context.xrActiveEye.ToString();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009EBC File Offset: 0x000080BC
		private RenderTexture CheckHistory(int id, PostProcessRenderContext context)
		{
			int xrActiveEye = context.xrActiveEye;
			if (this.m_HistoryTextures[xrActiveEye] == null)
			{
				this.m_HistoryTextures[xrActiveEye] = new RenderTexture[2];
			}
			RenderTexture renderTexture = this.m_HistoryTextures[xrActiveEye][id];
			if (this.m_ResetHistory || renderTexture == null || !renderTexture.IsCreated())
			{
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = context.GetScreenSpaceTemporaryRT(0, context.sourceFormat, RenderTextureReadWrite.Default, 0, 0);
				this.GenerateHistoryName(renderTexture, id, context);
				renderTexture.filterMode = FilterMode.Bilinear;
				this.m_HistoryTextures[xrActiveEye][id] = renderTexture;
				context.command.BlitFullscreenTriangle(context.source, renderTexture, false, null, false);
			}
			else if (renderTexture.width != context.width || renderTexture.height != context.height)
			{
				RenderTexture screenSpaceTemporaryRT = context.GetScreenSpaceTemporaryRT(0, context.sourceFormat, RenderTextureReadWrite.Default, 0, 0);
				this.GenerateHistoryName(screenSpaceTemporaryRT, id, context);
				screenSpaceTemporaryRT.filterMode = FilterMode.Bilinear;
				this.m_HistoryTextures[xrActiveEye][id] = screenSpaceTemporaryRT;
				context.command.BlitFullscreenTriangle(renderTexture, screenSpaceTemporaryRT, false, null, false);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			return this.m_HistoryTextures[xrActiveEye][id];
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009FDC File Offset: 0x000081DC
		internal void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.temporalAntialiasing);
			CommandBuffer command = context.command;
			command.BeginSample("TemporalAntialiasing");
			int num = this.m_HistoryPingPong[context.xrActiveEye];
			RenderTexture value = this.CheckHistory(++num % 2, context);
			RenderTexture tex = this.CheckHistory(++num % 2, context);
			this.m_HistoryPingPong[context.xrActiveEye] = (num + 1) % 2;
			propertySheet.properties.SetVector(ShaderIDs.Jitter, this.jitter);
			propertySheet.properties.SetFloat(ShaderIDs.Sharpness, this.sharpness);
			propertySheet.properties.SetVector(ShaderIDs.FinalBlendParameters, new Vector4(this.stationaryBlending, this.motionBlending, 6000f, 0f));
			propertySheet.properties.SetTexture(ShaderIDs.HistoryTex, value);
			int pass = context.camera.orthographic ? 1 : 0;
			this.m_Mrt[0] = context.destination;
			this.m_Mrt[1] = tex;
			command.BlitFullscreenTriangle(context.source, this.m_Mrt, context.source, propertySheet, pass, false, null);
			command.EndSample("TemporalAntialiasing");
			this.m_ResetHistory = false;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000A134 File Offset: 0x00008334
		internal void Release()
		{
			if (this.m_HistoryTextures != null)
			{
				for (int i = 0; i < this.m_HistoryTextures.Length; i++)
				{
					if (this.m_HistoryTextures[i] != null)
					{
						for (int j = 0; j < this.m_HistoryTextures[i].Length; j++)
						{
							RenderTexture.ReleaseTemporary(this.m_HistoryTextures[i][j]);
							this.m_HistoryTextures[i][j] = null;
						}
						this.m_HistoryTextures[i] = null;
					}
				}
			}
			this.sampleIndex = 0;
			this.m_HistoryPingPong[0] = 0;
			this.m_HistoryPingPong[1] = 0;
			this.ResetHistory();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public TemporalAntialiasing()
		{
		}

		// Token: 0x0400012D RID: 301
		[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable, but blurrier, output.")]
		[Range(0.1f, 1f)]
		public float jitterSpread = 0.75f;

		// Token: 0x0400012E RID: 302
		[Tooltip("Controls the amount of sharpening applied to the color buffer. High values may introduce dark-border artifacts.")]
		[Range(0f, 3f)]
		public float sharpness = 0.25f;

		// Token: 0x0400012F RID: 303
		[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
		[Range(0f, 0.99f)]
		public float stationaryBlending = 0.95f;

		// Token: 0x04000130 RID: 304
		[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
		[Range(0f, 0.99f)]
		public float motionBlending = 0.85f;

		// Token: 0x04000131 RID: 305
		public Func<Camera, Vector2, Matrix4x4> jitteredMatrixFunc;

		// Token: 0x04000132 RID: 306
		[CompilerGenerated]
		private Vector2 <jitter>k__BackingField;

		// Token: 0x04000133 RID: 307
		private readonly RenderTargetIdentifier[] m_Mrt = new RenderTargetIdentifier[2];

		// Token: 0x04000134 RID: 308
		private bool m_ResetHistory = true;

		// Token: 0x04000135 RID: 309
		private const int k_SampleCount = 8;

		// Token: 0x04000136 RID: 310
		[CompilerGenerated]
		private int <sampleIndex>k__BackingField;

		// Token: 0x04000137 RID: 311
		private const int k_NumEyes = 2;

		// Token: 0x04000138 RID: 312
		private const int k_NumHistoryTextures = 2;

		// Token: 0x04000139 RID: 313
		private readonly RenderTexture[][] m_HistoryTextures = new RenderTexture[2][];

		// Token: 0x0400013A RID: 314
		private readonly int[] m_HistoryPingPong = new int[2];

		// Token: 0x02000083 RID: 131
		private enum Pass
		{
			// Token: 0x0400032E RID: 814
			SolverDilate,
			// Token: 0x0400032F RID: 815
			SolverNoDilate
		}
	}
}
