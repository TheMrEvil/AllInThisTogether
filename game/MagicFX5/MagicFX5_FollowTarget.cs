using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200000F RID: 15
	public class MagicFX5_FollowTarget : MagicFX5_IScriptInstance
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003779 File Offset: 0x00001979
		internal override void OnEnableExtended()
		{
			this._startPos = base.transform.position;
			this._lastTarget = this._startPos;
			this.ManualUpdate();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000379E File Offset: 0x0000199E
		internal override void OnDisableExtended()
		{
			base.transform.localPosition = Vector3.zero;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000037B0 File Offset: 0x000019B0
		internal override void ManualUpdate()
		{
			if (this.UseRotation)
			{
				base.transform.rotation = this.Target.rotation;
			}
			if (this.LookAtTarget && (this.Target.position - this._startPos).sqrMagnitude > 0.001f)
			{
				Vector3 normalized = (this.Target.position - this._startPos).normalized;
				base.transform.rotation = Quaternion.LookRotation(normalized);
			}
			if (this.LookAtMotionVector)
			{
				if ((this.Target.position - this._lastTarget).sqrMagnitude > 0.001f)
				{
					Vector3 normalized2 = (this.Target.position - this._lastTarget).normalized;
					base.transform.rotation = Quaternion.LookRotation(normalized2);
				}
				this._lastTarget = this.Target.position;
			}
			if (this.UsePosition)
			{
				Vector3 position = this.Target.position;
				if (this.FreezeYPos)
				{
					position.y = this._startPos.y;
				}
				base.transform.position = position;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000038E0 File Offset: 0x00001AE0
		public MagicFX5_FollowTarget()
		{
		}

		// Token: 0x0400005D RID: 93
		public Transform Target;

		// Token: 0x0400005E RID: 94
		public bool UsePosition = true;

		// Token: 0x0400005F RID: 95
		public bool UseRotation = true;

		// Token: 0x04000060 RID: 96
		public bool FreezeYPos;

		// Token: 0x04000061 RID: 97
		public bool LookAtTarget;

		// Token: 0x04000062 RID: 98
		public bool LookAtMotionVector;

		// Token: 0x04000063 RID: 99
		private Vector3 _startPos;

		// Token: 0x04000064 RID: 100
		private Vector3 _lastTarget;
	}
}
