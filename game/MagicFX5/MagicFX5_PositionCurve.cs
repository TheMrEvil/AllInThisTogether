using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000018 RID: 24
	public class MagicFX5_PositionCurve : MagicFX5_IScriptInstance
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000463C File Offset: 0x0000283C
		internal override void OnEnableExtended()
		{
			MagicFX5_GlobalUpdate.CreateInstanceIfRequired();
			MagicFX5_GlobalUpdate.ScriptInstances.Add(this);
			this._startTime = Time.time;
			this._frozen = false;
			this._startPosition = base.transform.position;
			base.transform.position = this.PositionOverLifeTime.Evaluate(0f) * this.Axis + this._startPosition;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000046AD File Offset: 0x000028AD
		internal override void OnDisableExtended()
		{
			MagicFX5_GlobalUpdate.ScriptInstances.Remove(this);
			base.transform.position = this._startPosition;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000046CC File Offset: 0x000028CC
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
			Vector3 position = this.PositionOverLifeTime.Evaluate(num / this.Duration) * this.Axis + this._startPosition;
			base.transform.position = position;
			if (!this.Loop && num > this.Duration)
			{
				this._frozen = true;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004750 File Offset: 0x00002950
		public MagicFX5_PositionCurve()
		{
		}

		// Token: 0x040000A6 RID: 166
		public AnimationCurve PositionOverLifeTime = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x040000A7 RID: 167
		public Vector3 Axis = new Vector3(0f, 1f, 0f);

		// Token: 0x040000A8 RID: 168
		public float Duration = 1f;

		// Token: 0x040000A9 RID: 169
		public bool Loop;

		// Token: 0x040000AA RID: 170
		private float _startTime;

		// Token: 0x040000AB RID: 171
		private Vector3 _startPosition;

		// Token: 0x040000AC RID: 172
		private bool _frozen;
	}
}
