using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public struct FollowSettings
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00006921 File Offset: 0x00004B21
		public FollowSettings(float customDefault)
		{
			this.speed = 10f;
			this.drag = 0f;
		}

		// Token: 0x040000E9 RID: 233
		[Tooltip("Speed at which target is followed.")]
		public float speed;

		// Token: 0x040000EA RID: 234
		[Tooltip("Decreases follow speed over time.")]
		public float drag;
	}
}
