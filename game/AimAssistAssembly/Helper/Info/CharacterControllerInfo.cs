using System;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Info
{
	// Token: 0x02000008 RID: 8
	public class CharacterControllerInfo : IPlayerPhysicsInfo
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002379 File Offset: 0x00000579
		public Vector3 Velocity
		{
			get
			{
				return this.controller.velocity;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002386 File Offset: 0x00000586
		public CharacterControllerInfo(CharacterController controller)
		{
			this.controller = controller;
		}

		// Token: 0x04000012 RID: 18
		private readonly CharacterController controller;
	}
}
