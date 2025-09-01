using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A6 RID: 934
	public abstract class Controller : MonoBehaviour
	{
		// Token: 0x06001EF0 RID: 7920
		public abstract Vector3 GetVelocity();

		// Token: 0x06001EF1 RID: 7921
		public abstract Vector3 GetMovementVelocity();

		// Token: 0x06001EF2 RID: 7922
		public abstract bool IsGrounded();

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000B9383 File Offset: 0x000B7583
		protected Controller()
		{
		}

		// Token: 0x04001F39 RID: 7993
		public Controller.VectorEvent OnJump;

		// Token: 0x04001F3A RID: 7994
		public Action<Vector3, Vector3, Vector3> OnLand;

		// Token: 0x02000694 RID: 1684
		// (Invoke) Token: 0x0600280F RID: 10255
		public delegate void VectorEvent(Vector3 v);
	}
}
