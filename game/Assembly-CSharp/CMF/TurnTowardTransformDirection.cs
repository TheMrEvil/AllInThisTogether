using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A2 RID: 930
	public class TurnTowardTransformDirection : MonoBehaviour
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x000B77A3 File Offset: 0x000B59A3
		private void Start()
		{
			this.tr = base.transform;
			this.parentTransform = base.transform.parent;
			if (this.targetTransform == null)
			{
				Debug.LogWarning("No target transform has been assigned to this script.", this);
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000B77DC File Offset: 0x000B59DC
		private void LateUpdate()
		{
			if (!this.targetTransform)
			{
				return;
			}
			Vector3 normalized = Vector3.ProjectOnPlane(this.targetTransform.forward, this.parentTransform.up).normalized;
			Vector3 up = this.parentTransform.up;
			this.tr.rotation = Quaternion.LookRotation(normalized, up);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000B7839 File Offset: 0x000B5A39
		public TurnTowardTransformDirection()
		{
		}

		// Token: 0x04001EF3 RID: 7923
		public Transform targetTransform;

		// Token: 0x04001EF4 RID: 7924
		private Transform tr;

		// Token: 0x04001EF5 RID: 7925
		private Transform parentTransform;
	}
}
