using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public struct PushSettings
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00006939 File Offset: 0x00004B39
		public PushSettings(float customDefault)
		{
			this.radius = 4f;
			this.pushOffset = 0.8f;
		}

		// Token: 0x040000EB RID: 235
		[Header("Main:")]
		public float radius;

		// Token: 0x040000EC RID: 236
		public float pushOffset;
	}
}
