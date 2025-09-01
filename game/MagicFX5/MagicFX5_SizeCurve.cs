using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200001E RID: 30
	public class MagicFX5_SizeCurve : MagicFX5_IScriptInstance
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000572C File Offset: 0x0000392C
		internal override void OnEnableExtended()
		{
			this._startTime = Time.time;
			this._frozen = false;
			this._randomScaleMultiplier = UnityEngine.Random.Range(this.RandomRangeMultiplier.x, this.RandomRangeMultiplier.y);
			this._startSize = base.transform.localScale * this._randomScaleMultiplier;
			Vector3 vector = this.SizeOverLifeTime.Evaluate(0f) * this._startSize;
			vector = this.LerpAxis(this._startSize, vector, this.AffectedAxis);
			base.transform.localScale = vector;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000057C4 File Offset: 0x000039C4
		internal override void OnDisableExtended()
		{
			base.transform.localScale = this._startSize;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000057D8 File Offset: 0x000039D8
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
			Vector3 vector = this.SizeOverLifeTime.Evaluate(num / this.Duration) * this._startSize;
			vector = this.LerpAxis(this._startSize, vector, this.AffectedAxis);
			base.transform.localScale = vector;
			if (!this.Loop && num > this.Duration)
			{
				this._frozen = true;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005864 File Offset: 0x00003A64
		private Vector3 LerpAxis(Vector3 a, Vector3 b, Vector3 axis)
		{
			a.x = Mathf.Lerp(a.x, b.x, axis.x);
			a.y = Mathf.Lerp(a.y, b.y, axis.y);
			a.z = Mathf.Lerp(a.z, b.z, axis.z);
			return a;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000058CC File Offset: 0x00003ACC
		public MagicFX5_SizeCurve()
		{
		}

		// Token: 0x040000EC RID: 236
		public AnimationCurve SizeOverLifeTime = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x040000ED RID: 237
		public Vector2 RandomRangeMultiplier = new Vector2(0.75f, 1.25f);

		// Token: 0x040000EE RID: 238
		public Vector3 AffectedAxis = new Vector3(1f, 1f, 1f);

		// Token: 0x040000EF RID: 239
		public float Duration = 1f;

		// Token: 0x040000F0 RID: 240
		public bool Loop;

		// Token: 0x040000F1 RID: 241
		private float _startTime;

		// Token: 0x040000F2 RID: 242
		private Vector3 _startSize;

		// Token: 0x040000F3 RID: 243
		private bool _frozen;

		// Token: 0x040000F4 RID: 244
		private float _randomScaleMultiplier;
	}
}
