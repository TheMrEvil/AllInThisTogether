using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200001C RID: 28
	public class MagicFX5_ShaderFloatCurve : MagicFX5_IScriptInstance
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000552E File Offset: 0x0000372E
		private void Awake()
		{
			this._rend = base.GetComponent<Renderer>();
			this._shaderID = Shader.PropertyToID(this.ShaderName.ToString());
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005558 File Offset: 0x00003758
		internal override void OnEnableExtended()
		{
			this._leftTime = 0f;
			this._frozen = false;
			this._startFloat = this._rend.sharedMaterial.GetFloat(this._shaderID);
			float value = this.OverrideDefault ? this.DefaultValue : this._startFloat;
			this._rend.SetFloatPropertyBlock(this._shaderID, value);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000055BC File Offset: 0x000037BC
		internal override void OnDisableExtended()
		{
			this._rend.SetFloatPropertyBlock(this._shaderID, this._startFloat);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000055D8 File Offset: 0x000037D8
		internal override void ManualUpdate()
		{
			if (this._frozen)
			{
				return;
			}
			this._leftTime += Time.deltaTime;
			if (this.Loop && this._leftTime >= this.Duration)
			{
				this._leftTime = 0f;
			}
			float num = this.OverrideDefault ? this.DefaultValue : this._startFloat;
			float value = this.FloatOverTime.Evaluate(this._leftTime / this.Duration) * num;
			this._rend.SetFloatPropertyBlock(this._shaderID, value);
			if (!this.Loop && this._leftTime >= this.Duration)
			{
				this._frozen = true;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005684 File Offset: 0x00003884
		public MagicFX5_ShaderFloatCurve()
		{
		}

		// Token: 0x040000E1 RID: 225
		public MagicFX5_ShaderFloatCurve.ME2_ShaderPropertyName ShaderName;

		// Token: 0x040000E2 RID: 226
		public AnimationCurve FloatOverTime = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(0.5f, 1f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x040000E3 RID: 227
		public float Duration = 2f;

		// Token: 0x040000E4 RID: 228
		public bool Loop;

		// Token: 0x040000E5 RID: 229
		[Space]
		public bool OverrideDefault;

		// Token: 0x040000E6 RID: 230
		public float DefaultValue = 1f;

		// Token: 0x040000E7 RID: 231
		private Renderer _rend;

		// Token: 0x040000E8 RID: 232
		private float _leftTime;

		// Token: 0x040000E9 RID: 233
		private float _startFloat;

		// Token: 0x040000EA RID: 234
		private bool _frozen;

		// Token: 0x040000EB RID: 235
		private int _shaderID;

		// Token: 0x02000036 RID: 54
		public enum ME2_ShaderPropertyName
		{
			// Token: 0x04000186 RID: 390
			_Cutoff,
			// Token: 0x04000187 RID: 391
			_Cutout,
			// Token: 0x04000188 RID: 392
			_CustomTime,
			// Token: 0x04000189 RID: 393
			_CutoutOffset,
			// Token: 0x0400018A RID: 394
			_VertexCutout
		}
	}
}
