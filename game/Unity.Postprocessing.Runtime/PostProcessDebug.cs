using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000054 RID: 84
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Post-process Debug", 1002)]
	public sealed class PostProcessDebug : MonoBehaviour
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000B5F3 File Offset: 0x000097F3
		private void OnEnable()
		{
			this.m_CmdAfterEverything = new CommandBuffer
			{
				name = "Post-processing Debug Overlay"
			};
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000B60B File Offset: 0x0000980B
		private void OnDisable()
		{
			if (this.m_CurrentCamera != null)
			{
				this.m_CurrentCamera.RemoveCommandBuffer(CameraEvent.AfterImageEffects, this.m_CmdAfterEverything);
			}
			this.m_CurrentCamera = null;
			this.m_PreviousPostProcessLayer = null;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000B63C File Offset: 0x0000983C
		private void Update()
		{
			this.UpdateStates();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000B644 File Offset: 0x00009844
		private void Reset()
		{
			this.postProcessLayer = base.GetComponent<PostProcessLayer>();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000B654 File Offset: 0x00009854
		private void UpdateStates()
		{
			if (this.m_PreviousPostProcessLayer != this.postProcessLayer)
			{
				if (this.m_CurrentCamera != null)
				{
					this.m_CurrentCamera.RemoveCommandBuffer(CameraEvent.AfterImageEffects, this.m_CmdAfterEverything);
					this.m_CurrentCamera = null;
				}
				this.m_PreviousPostProcessLayer = this.postProcessLayer;
				if (this.postProcessLayer != null)
				{
					this.m_CurrentCamera = this.postProcessLayer.GetComponent<Camera>();
					this.m_CurrentCamera.AddCommandBuffer(CameraEvent.AfterImageEffects, this.m_CmdAfterEverything);
				}
			}
			if (this.postProcessLayer == null || !this.postProcessLayer.enabled)
			{
				return;
			}
			if (this.lightMeter)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.LightMeter);
			}
			if (this.histogram)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.Histogram);
			}
			if (this.waveform)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.Waveform);
			}
			if (this.vectorscope)
			{
				this.postProcessLayer.debugLayer.RequestMonitorPass(MonitorType.Vectorscope);
			}
			this.postProcessLayer.debugLayer.RequestDebugOverlay(this.debugOverlay);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000B770 File Offset: 0x00009970
		private void OnPostRender()
		{
			this.m_CmdAfterEverything.Clear();
			if (this.postProcessLayer == null || !this.postProcessLayer.enabled || !this.postProcessLayer.debugLayer.debugOverlayActive)
			{
				return;
			}
			this.m_CmdAfterEverything.Blit(this.postProcessLayer.debugLayer.debugOverlayTarget, BuiltinRenderTextureType.CameraTarget);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000B7D8 File Offset: 0x000099D8
		private void OnGUI()
		{
			if (this.postProcessLayer == null || !this.postProcessLayer.enabled)
			{
				return;
			}
			RenderTexture.active = null;
			Rect rect = new Rect(5f, 5f, 0f, 0f);
			PostProcessDebugLayer debugLayer = this.postProcessLayer.debugLayer;
			this.DrawMonitor(ref rect, debugLayer.lightMeter, this.lightMeter);
			this.DrawMonitor(ref rect, debugLayer.histogram, this.histogram);
			this.DrawMonitor(ref rect, debugLayer.waveform, this.waveform);
			this.DrawMonitor(ref rect, debugLayer.vectorscope, this.vectorscope);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000B880 File Offset: 0x00009A80
		private void DrawMonitor(ref Rect rect, Monitor monitor, bool enabled)
		{
			if (!enabled || monitor.output == null)
			{
				return;
			}
			rect.width = (float)monitor.output.width;
			rect.height = (float)monitor.output.height;
			GUI.DrawTexture(rect, monitor.output);
			rect.x += (float)monitor.output.width + 5f;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000B8F3 File Offset: 0x00009AF3
		public PostProcessDebug()
		{
		}

		// Token: 0x04000170 RID: 368
		public PostProcessLayer postProcessLayer;

		// Token: 0x04000171 RID: 369
		private PostProcessLayer m_PreviousPostProcessLayer;

		// Token: 0x04000172 RID: 370
		public bool lightMeter;

		// Token: 0x04000173 RID: 371
		public bool histogram;

		// Token: 0x04000174 RID: 372
		public bool waveform;

		// Token: 0x04000175 RID: 373
		public bool vectorscope;

		// Token: 0x04000176 RID: 374
		public DebugOverlay debugOverlay;

		// Token: 0x04000177 RID: 375
		private Camera m_CurrentCamera;

		// Token: 0x04000178 RID: 376
		private CommandBuffer m_CmdAfterEverything;
	}
}
