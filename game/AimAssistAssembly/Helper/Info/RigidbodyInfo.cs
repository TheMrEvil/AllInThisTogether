using System;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Info
{
	// Token: 0x0200000A RID: 10
	public class RigidbodyInfo : IPlayerPhysicsInfo
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002395 File Offset: 0x00000595
		public Vector3 Velocity
		{
			get
			{
				return this.playerBody.velocity;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023A2 File Offset: 0x000005A2
		public RigidbodyInfo(Rigidbody playerBody)
		{
			this.playerBody = playerBody;
		}

		// Token: 0x04000013 RID: 19
		private readonly Rigidbody playerBody;
	}
}
