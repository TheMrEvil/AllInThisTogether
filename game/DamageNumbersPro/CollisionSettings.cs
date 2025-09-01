using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public struct CollisionSettings
	{
		// Token: 0x0600009B RID: 155 RVA: 0x0000676B File Offset: 0x0000496B
		public CollisionSettings(float customDefault)
		{
			this.radius = 0.5f;
			this.pushFactor = 1f;
			this.desiredDirection = new Vector3(0f, 0f);
		}

		// Token: 0x040000D9 RID: 217
		[Header("Main:")]
		public float radius;

		// Token: 0x040000DA RID: 218
		public float pushFactor;

		// Token: 0x040000DB RID: 219
		[Header("Direction:")]
		public Vector3 desiredDirection;
	}
}
