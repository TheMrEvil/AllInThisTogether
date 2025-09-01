using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000023 RID: 35
	public class MagicFX5_TrailRendererFeatures : MagicFX5_IScriptInstance
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00006168 File Offset: 0x00004368
		internal override void OnEnableExtended()
		{
			if (this._renderer == null)
			{
				this._renderer = base.GetComponent<Renderer>();
			}
			if (this._lineRenderer == null)
			{
				this._lineRenderer = base.GetComponent<LineRenderer>();
			}
			if (this._trailRenderer == null)
			{
				this._trailRenderer = base.GetComponent<TrailRenderer>();
			}
			this._uvOffset = Vector2.zero;
			this._lastPos = base.transform.position;
			if (this.ApplyColorCurveWhenStoped != null)
			{
				this.ApplyColorCurveWhenStoped.enabled = false;
			}
			if (this.AutomaticEmittingStop && this._trailRenderer != null)
			{
				this._emittingStopCurrentFrames = 0;
				this._trailRenderer.Clear();
				this._trailRenderer.emitting = true;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000622D File Offset: 0x0000442D
		internal override void OnDisableExtended()
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006230 File Offset: 0x00004430
		internal override void ManualUpdate()
		{
			Vector3 position = base.transform.position;
			if (this._lineRenderer != null && this._lineRenderer.positionCount > 1)
			{
				position = this._lineRenderer.GetPosition(0);
			}
			float num = Vector3.Distance(position, this._lastPos);
			this._lastPos = position;
			if (this.UseWorldSpaceUV)
			{
				this._uvOffset.x = this._uvOffset.x - num;
				this._renderer.SetVectorPropertyBlock(this._shaderID, this._uvOffset);
			}
			if (this.ApplyColorCurveWhenStoped != null)
			{
				this.ApplyColorCurveWhenStoped.enabled = (num < 0.001f);
			}
			if (this.AutomaticEmittingStop && this._trailRenderer != null && num < 0.001f)
			{
				this._emittingStopCurrentFrames++;
				if (this._emittingStopCurrentFrames > 10)
				{
					this._trailRenderer.emitting = false;
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000631D File Offset: 0x0000451D
		public MagicFX5_TrailRendererFeatures()
		{
		}

		// Token: 0x04000121 RID: 289
		public bool UseWorldSpaceUV = true;

		// Token: 0x04000122 RID: 290
		public MagicFX5_ShaderColorCurve ApplyColorCurveWhenStoped;

		// Token: 0x04000123 RID: 291
		public bool AutomaticEmittingStop;

		// Token: 0x04000124 RID: 292
		private Vector2 _uvOffset;

		// Token: 0x04000125 RID: 293
		private Vector3 _lastPos;

		// Token: 0x04000126 RID: 294
		private Renderer _renderer;

		// Token: 0x04000127 RID: 295
		private LineRenderer _lineRenderer;

		// Token: 0x04000128 RID: 296
		private TrailRenderer _trailRenderer;

		// Token: 0x04000129 RID: 297
		private int _shaderID = Shader.PropertyToID("_TrailWorldUvOffset");

		// Token: 0x0400012A RID: 298
		private int _emittingStopCurrentFrames;

		// Token: 0x0400012B RID: 299
		private const int EmittingStopFramesThreshold = 10;
	}
}
