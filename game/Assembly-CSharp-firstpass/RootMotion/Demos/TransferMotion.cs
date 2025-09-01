using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200014D RID: 333
	public class TransferMotion : MonoBehaviour
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x000597ED File Offset: 0x000579ED
		private void OnEnable()
		{
			this.lastPosition = base.transform.position;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00059800 File Offset: 0x00057A00
		private void Update()
		{
			Vector3 a = base.transform.position - this.lastPosition;
			this.to.position += a * this.transferMotion;
			this.lastPosition = base.transform.position;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00059857 File Offset: 0x00057A57
		public TransferMotion()
		{
		}

		// Token: 0x04000AE3 RID: 2787
		[Tooltip("The Transform to transfer motion to.")]
		public Transform to;

		// Token: 0x04000AE4 RID: 2788
		[Tooltip("The amount of motion to transfer.")]
		[Range(0f, 1f)]
		public float transferMotion = 0.9f;

		// Token: 0x04000AE5 RID: 2789
		private Vector3 lastPosition;
	}
}
