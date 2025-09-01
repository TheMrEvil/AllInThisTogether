using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200001B RID: 27
	public class MagicFX5_ShaderColorCurve : MagicFX5_IScriptInstance
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000534A File Offset: 0x0000354A
		private void Awake()
		{
			this._rend = base.GetComponent<Renderer>();
			this._shaderID = Shader.PropertyToID(this.ShaderName.ToString());
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005374 File Offset: 0x00003574
		internal override void OnEnableExtended()
		{
			this._startTime = Time.time;
			this._frozen = false;
			if (this.OverrideMaterial != null)
			{
				this._startColor = this.OverrideMaterial.GetColor(this._shaderID);
				this.OverrideMaterial.SetColor(this._shaderID, this.ColorOverTime.Evaluate(0f) * this._startColor);
				return;
			}
			this._startColor = this._rend.sharedMaterial.GetColor(this._shaderID);
			this._rend.SetColorPropertyBlock(this._shaderID, this.ColorOverTime.Evaluate(0f) * this._startColor);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000542D File Offset: 0x0000362D
		internal override void OnDisableExtended()
		{
			if (this.OverrideMaterial != null)
			{
				this.OverrideMaterial.SetColor(this._shaderID, this._startColor);
				return;
			}
			this._rend.SetColorPropertyBlock(this._shaderID, this._startColor);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000546C File Offset: 0x0000366C
		internal override void ManualUpdate()
		{
			if (this._frozen)
			{
				return;
			}
			float num = Time.time - this._startTime;
			if (this.Loop)
			{
				num %= this.Duration;
			}
			Color color = this.ColorOverTime.Evaluate(num / this.Duration) * this._startColor;
			if (this.OverrideMaterial != null)
			{
				this.OverrideMaterial.SetVector(this._shaderID, color);
			}
			else
			{
				this._rend.SetColorPropertyBlock(this._shaderID, color);
			}
			if (!this.Loop && num > this.Duration)
			{
				this._frozen = true;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005510 File Offset: 0x00003710
		public MagicFX5_ShaderColorCurve()
		{
		}

		// Token: 0x040000D7 RID: 215
		public MagicFX5_ShaderColorCurve.ME2_ShaderPropertyName ShaderName;

		// Token: 0x040000D8 RID: 216
		public Material OverrideMaterial;

		// Token: 0x040000D9 RID: 217
		public Gradient ColorOverTime = new Gradient();

		// Token: 0x040000DA RID: 218
		public float Duration = 2f;

		// Token: 0x040000DB RID: 219
		public bool Loop;

		// Token: 0x040000DC RID: 220
		private Renderer _rend;

		// Token: 0x040000DD RID: 221
		private float _startTime;

		// Token: 0x040000DE RID: 222
		private Color _startColor;

		// Token: 0x040000DF RID: 223
		private bool _frozen;

		// Token: 0x040000E0 RID: 224
		private int _shaderID;

		// Token: 0x02000035 RID: 53
		public enum ME2_ShaderPropertyName
		{
			// Token: 0x04000181 RID: 385
			_Color,
			// Token: 0x04000182 RID: 386
			_MainColor,
			// Token: 0x04000183 RID: 387
			_EmissionColor1,
			// Token: 0x04000184 RID: 388
			_FresnelEmissionColor
		}
	}
}
