using System;
using UnityEngine;

namespace TeleportFX
{
	// Token: 0x02000006 RID: 6
	[AddComponentMenu("")]
	internal class TeleportFX_PositionCurve : TeleportFX_IScriptInstance
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002B64 File Offset: 0x00000D64
		internal override void OnEnableExtended()
		{
			this._startTime = Time.time;
			this._frozen = false;
			this._currentAxis = this.Axis;
			if (this.MultiplyTransformScale != null)
			{
				this._currentAxis = Vector3.Scale(this._currentAxis, this.MultiplyTransformScale.lossyScale);
			}
			this._startPosition = base.transform.position;
			base.transform.position = this.PositionOverLifeTime.Evaluate(0f) * this._currentAxis + this._startPosition;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002BFB File Offset: 0x00000DFB
		internal override void OnDisableExtended()
		{
			base.transform.position = this._startPosition;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C10 File Offset: 0x00000E10
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
			Vector3 position = this.PositionOverLifeTime.Evaluate(num / this.Duration) * this._currentAxis + this._startPosition;
			base.transform.position = position;
			if (!this.Loop && num > this.Duration)
			{
				this._frozen = true;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C94 File Offset: 0x00000E94
		public TeleportFX_PositionCurve()
		{
		}

		// Token: 0x0400002A RID: 42
		public AnimationCurve PositionOverLifeTime = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x0400002B RID: 43
		public Vector3 Axis = new Vector3(0f, 1f, 0f);

		// Token: 0x0400002C RID: 44
		public float Duration = 1f;

		// Token: 0x0400002D RID: 45
		public bool Loop;

		// Token: 0x0400002E RID: 46
		public Transform MultiplyTransformScale;

		// Token: 0x0400002F RID: 47
		private float _startTime;

		// Token: 0x04000030 RID: 48
		private Vector3 _startPosition;

		// Token: 0x04000031 RID: 49
		private bool _frozen;

		// Token: 0x04000032 RID: 50
		private Vector3 _currentAxis;
	}
}
