using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B1 RID: 177
	public class CameraControllerFPS : MonoBehaviour
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00036D6C File Offset: 0x00034F6C
		private void Awake()
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			this.x = eulerAngles.y;
			this.y = eulerAngles.x;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00036DA0 File Offset: 0x00034FA0
		public void LateUpdate()
		{
			Cursor.lockState = CursorLockMode.Locked;
			this.x += Input.GetAxis("Mouse X") * this.rotationSensitivity;
			this.y = this.ClampAngle(this.y - Input.GetAxis("Mouse Y") * this.rotationSensitivity, this.yMinLimit, this.yMaxLimit);
			base.transform.rotation = Quaternion.AngleAxis(this.x, Vector3.up) * Quaternion.AngleAxis(this.y, Vector3.right);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00036E31 File Offset: 0x00035031
		private float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
			return Mathf.Clamp(angle, min, max);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00036E5D File Offset: 0x0003505D
		public CameraControllerFPS()
		{
		}

		// Token: 0x04000675 RID: 1653
		public float rotationSensitivity = 3f;

		// Token: 0x04000676 RID: 1654
		public float yMinLimit = -89f;

		// Token: 0x04000677 RID: 1655
		public float yMaxLimit = 89f;

		// Token: 0x04000678 RID: 1656
		private float x;

		// Token: 0x04000679 RID: 1657
		private float y;
	}
}
