using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003AB RID: 939
	public class FlipAtRightAngle : MonoBehaviour
	{
		// Token: 0x06001F2C RID: 7980 RVA: 0x000BAC51 File Offset: 0x000B8E51
		private void Start()
		{
			this.tr = base.transform;
			this.audioSource = base.GetComponent<AudioSource>();
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000BAC6B File Offset: 0x000B8E6B
		private void OnTriggerEnter(Collider col)
		{
			if (col.GetComponent<Controller>() == null)
			{
				return;
			}
			this.SwitchDirection(this.tr.forward, col.GetComponent<Controller>());
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000BAC94 File Offset: 0x000B8E94
		private void SwitchDirection(Vector3 _newUpDirection, Controller _controller)
		{
			float num = 0.001f;
			if (Vector3.Angle(_newUpDirection, _controller.transform.up) < num)
			{
				return;
			}
			this.audioSource.Play();
			Transform transform = _controller.transform;
			Quaternion lhs = Quaternion.FromToRotation(transform.up, _newUpDirection);
			transform.rotation = lhs * transform.rotation;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000BACED File Offset: 0x000B8EED
		public FlipAtRightAngle()
		{
		}

		// Token: 0x04001F7F RID: 8063
		private AudioSource audioSource;

		// Token: 0x04001F80 RID: 8064
		private Transform tr;
	}
}
