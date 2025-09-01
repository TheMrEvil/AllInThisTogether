using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	public sealed class PostProcessDebugLayer
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000B8FB File Offset: 0x00009AFB
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000B903 File Offset: 0x00009B03
		public RenderTexture debugOverlayTarget
		{
			[CompilerGenerated]
			get
			{
				return this.<debugOverlayTarget>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<debugOverlayTarget>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000B90C File Offset: 0x00009B0C
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000B914 File Offset: 0x00009B14
		public bool debugOverlayActive
		{
			[CompilerGenerated]
			get
			{
				return this.<debugOverlayActive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<debugOverlayActive>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000B91D File Offset: 0x00009B1D
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000B925 File Offset: 0x00009B25
		public DebugOverlay debugOverlay
		{
			[CompilerGenerated]
			get
			{
				return this.<debugOverlay>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<debugOverlay>k__BackingField = value;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000B930 File Offset: 0x00009B30
		internal void OnEnable()
		{
			RuntimeUtilities.CreateIfNull<LightMeterMonitor>(ref this.lightMeter);
			RuntimeUtilities.CreateIfNull<HistogramMonitor>(ref this.histogram);
			RuntimeUtilities.CreateIfNull<WaveformMonitor>(ref this.waveform);
			RuntimeUtilities.CreateIfNull<VectorscopeMonitor>(ref this.vectorscope);
			RuntimeUtilities.CreateIfNull<PostProcessDebugLayer.OverlaySettings>(ref this.overlaySettings);
			this.m_Monitors = new Dictionary<MonitorType, Monitor>
			{
				{
					MonitorType.LightMeter,
					this.lightMeter
				},
				{
					MonitorType.Histogram,
					this.histogram
				},
				{
					MonitorType.Waveform,
					this.waveform
				},
				{
					MonitorType.Vectorscope,
					this.vectorscope
				}
			};
			foreach (KeyValuePair<MonitorType, Monitor> keyValuePair in this.m_Monitors)
			{
				keyValuePair.Value.OnEnable();
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000BA00 File Offset: 0x00009C00
		internal void OnDisable()
		{
			foreach (KeyValuePair<MonitorType, Monitor> keyValuePair in this.m_Monitors)
			{
				keyValuePair.Value.OnDisable();
			}
			this.DestroyDebugOverlayTarget();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000BA60 File Offset: 0x00009C60
		private void DestroyDebugOverlayTarget()
		{
			RuntimeUtilities.Destroy(this.debugOverlayTarget);
			this.debugOverlayTarget = null;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000BA74 File Offset: 0x00009C74
		public void RequestMonitorPass(MonitorType monitor)
		{
			this.m_Monitors[monitor].requested = true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000BA88 File Offset: 0x00009C88
		public void RequestDebugOverlay(DebugOverlay mode)
		{
			this.debugOverlay = mode;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000BA91 File Offset: 0x00009C91
		internal void SetFrameSize(int width, int height)
		{
			this.frameWidth = width;
			this.frameHeight = height;
			this.debugOverlayActive = false;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000BAA8 File Offset: 0x00009CA8
		public void PushDebugOverlay(CommandBuffer cmd, RenderTargetIdentifier source, PropertySheet sheet, int pass)
		{
			if (this.debugOverlayTarget == null || !this.debugOverlayTarget.IsCreated() || this.debugOverlayTarget.width != this.frameWidth || this.debugOverlayTarget.height != this.frameHeight)
			{
				RuntimeUtilities.Destroy(this.debugOverlayTarget);
				this.debugOverlayTarget = new RenderTexture(this.frameWidth, this.frameHeight, 0, RenderTextureFormat.ARGB32)
				{
					name = "Debug Overlay Target",
					anisoLevel = 1,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					hideFlags = HideFlags.HideAndDontSave
				};
				this.debugOverlayTarget.Create();
			}
			cmd.BlitFullscreenTriangle(source, this.debugOverlayTarget, sheet, pass, false, null, false);
			this.debugOverlayActive = true;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000BB75 File Offset: 0x00009D75
		internal DepthTextureMode GetCameraFlags()
		{
			if (this.debugOverlay == DebugOverlay.Depth)
			{
				return DepthTextureMode.Depth;
			}
			if (this.debugOverlay == DebugOverlay.Normals)
			{
				return DepthTextureMode.DepthNormals;
			}
			if (this.debugOverlay == DebugOverlay.MotionVectors)
			{
				return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
			}
			return DepthTextureMode.None;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000BB9C File Offset: 0x00009D9C
		internal void RenderMonitors(PostProcessRenderContext context)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (KeyValuePair<MonitorType, Monitor> keyValuePair in this.m_Monitors)
			{
				bool flag3 = keyValuePair.Value.IsRequestedAndSupported(context);
				flag = (flag || flag3);
				flag2 |= (flag3 && keyValuePair.Value.NeedsHalfRes());
			}
			if (!flag)
			{
				return;
			}
			CommandBuffer command = context.command;
			command.BeginSample("Monitors");
			if (flag2)
			{
				command.GetTemporaryRT(ShaderIDs.HalfResFinalCopy, context.width / 2, context.height / 2, 0, FilterMode.Bilinear, context.sourceFormat);
				command.Blit(context.destination, ShaderIDs.HalfResFinalCopy);
			}
			foreach (KeyValuePair<MonitorType, Monitor> keyValuePair2 in this.m_Monitors)
			{
				Monitor value = keyValuePair2.Value;
				if (value.requested)
				{
					value.Render(context);
				}
			}
			if (flag2)
			{
				command.ReleaseTemporaryRT(ShaderIDs.HalfResFinalCopy);
			}
			command.EndSample("Monitors");
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		internal void RenderSpecialOverlays(PostProcessRenderContext context)
		{
			if (this.debugOverlay == DebugOverlay.Depth)
			{
				PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.debugOverlays);
				propertySheet.properties.SetVector(ShaderIDs.Params, new Vector4(this.overlaySettings.linearDepth ? 1f : 0f, 0f, 0f, 0f));
				this.PushDebugOverlay(context.command, BuiltinRenderTextureType.None, propertySheet, 0);
				return;
			}
			if (this.debugOverlay == DebugOverlay.Normals)
			{
				PropertySheet propertySheet2 = context.propertySheets.Get(context.resources.shaders.debugOverlays);
				propertySheet2.ClearKeywords();
				if (context.camera.actualRenderingPath == RenderingPath.DeferredLighting)
				{
					propertySheet2.EnableKeyword("SOURCE_GBUFFER");
				}
				this.PushDebugOverlay(context.command, BuiltinRenderTextureType.None, propertySheet2, 1);
				return;
			}
			if (this.debugOverlay == DebugOverlay.MotionVectors)
			{
				PropertySheet propertySheet3 = context.propertySheets.Get(context.resources.shaders.debugOverlays);
				propertySheet3.properties.SetVector(ShaderIDs.Params, new Vector4(this.overlaySettings.motionColorIntensity, (float)this.overlaySettings.motionGridSize, 0f, 0f));
				this.PushDebugOverlay(context.command, context.source, propertySheet3, 2);
				return;
			}
			if (this.debugOverlay == DebugOverlay.NANTracker)
			{
				PropertySheet sheet = context.propertySheets.Get(context.resources.shaders.debugOverlays);
				this.PushDebugOverlay(context.command, context.source, sheet, 3);
				return;
			}
			if (this.debugOverlay == DebugOverlay.ColorBlindnessSimulation)
			{
				PropertySheet propertySheet4 = context.propertySheets.Get(context.resources.shaders.debugOverlays);
				propertySheet4.properties.SetVector(ShaderIDs.Params, new Vector4(this.overlaySettings.colorBlindnessStrength, 0f, 0f, 0f));
				this.PushDebugOverlay(context.command, context.source, propertySheet4, (int)(4 + this.overlaySettings.colorBlindnessType));
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		internal void EndFrame()
		{
			foreach (KeyValuePair<MonitorType, Monitor> keyValuePair in this.m_Monitors)
			{
				keyValuePair.Value.requested = false;
			}
			if (!this.debugOverlayActive)
			{
				this.DestroyDebugOverlayTarget();
			}
			this.debugOverlay = DebugOverlay.None;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000BF48 File Offset: 0x0000A148
		public PostProcessDebugLayer()
		{
		}

		// Token: 0x04000189 RID: 393
		public LightMeterMonitor lightMeter;

		// Token: 0x0400018A RID: 394
		public HistogramMonitor histogram;

		// Token: 0x0400018B RID: 395
		public WaveformMonitor waveform;

		// Token: 0x0400018C RID: 396
		public VectorscopeMonitor vectorscope;

		// Token: 0x0400018D RID: 397
		private Dictionary<MonitorType, Monitor> m_Monitors;

		// Token: 0x0400018E RID: 398
		private int frameWidth;

		// Token: 0x0400018F RID: 399
		private int frameHeight;

		// Token: 0x04000190 RID: 400
		[CompilerGenerated]
		private RenderTexture <debugOverlayTarget>k__BackingField;

		// Token: 0x04000191 RID: 401
		[CompilerGenerated]
		private bool <debugOverlayActive>k__BackingField;

		// Token: 0x04000192 RID: 402
		[CompilerGenerated]
		private DebugOverlay <debugOverlay>k__BackingField;

		// Token: 0x04000193 RID: 403
		public PostProcessDebugLayer.OverlaySettings overlaySettings;

		// Token: 0x02000085 RID: 133
		[Serializable]
		public class OverlaySettings
		{
			// Token: 0x06000271 RID: 625 RVA: 0x00013148 File Offset: 0x00011348
			public OverlaySettings()
			{
			}

			// Token: 0x04000335 RID: 821
			public bool linearDepth;

			// Token: 0x04000336 RID: 822
			[Range(0f, 16f)]
			public float motionColorIntensity = 4f;

			// Token: 0x04000337 RID: 823
			[Range(4f, 128f)]
			public int motionGridSize = 64;

			// Token: 0x04000338 RID: 824
			public ColorBlindnessType colorBlindnessType;

			// Token: 0x04000339 RID: 825
			[Range(0f, 1f)]
			public float colorBlindnessStrength = 1f;
		}
	}
}
