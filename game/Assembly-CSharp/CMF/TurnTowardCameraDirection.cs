using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A0 RID: 928
	public class TurnTowardCameraDirection : MonoBehaviour
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x000B7583 File Offset: 0x000B5783
		private void Start()
		{
			this.tr = base.transform;
			if (this.cameraController == null)
			{
				Debug.LogWarning("No camera controller reference has been assigned to this script.", this);
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000B75AC File Offset: 0x000B57AC
		private void LateUpdate()
		{
			if (!this.cameraController)
			{
				return;
			}
			Vector3 facingDirection = this.cameraController.GetFacingDirection();
			Vector3 upDirection = this.cameraController.GetUpDirection();
			this.tr.rotation = Quaternion.LookRotation(facingDirection, upDirection);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000B75F1 File Offset: 0x000B57F1
		public TurnTowardCameraDirection()
		{
		}

		// Token: 0x04001EEA RID: 7914
		public CameraController cameraController;

		// Token: 0x04001EEB RID: 7915
		private Transform tr;
	}
}
