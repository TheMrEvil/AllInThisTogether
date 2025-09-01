using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A7 RID: 935
	public class SidescrollerController : AdvancedWalkerController
	{
		// Token: 0x06001EF4 RID: 7924 RVA: 0x000B938C File Offset: 0x000B758C
		protected override Vector3 CalculateMovementDirection()
		{
			if (this.characterInput == null)
			{
				return Vector3.zero;
			}
			Vector3 vector = Vector3.zero;
			if (this.cameraTransform == null)
			{
				vector += this.tr.right * this.characterInput.movementAxis.x;
			}
			else
			{
				vector += Vector3.ProjectOnPlane(this.cameraTransform.right, this.tr.up).normalized * this.characterInput.movementAxis.x;
			}
			return vector;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000B942A File Offset: 0x000B762A
		public SidescrollerController()
		{
		}
	}
}
