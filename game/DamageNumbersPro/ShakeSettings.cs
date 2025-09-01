using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public struct ShakeSettings
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000697F File Offset: 0x00004B7F
		public ShakeSettings(Vector2 customDefault)
		{
			this.offset = customDefault;
			this.frequency = 50f;
		}

		// Token: 0x040000F1 RID: 241
		[Tooltip("Moves back and fourth from negative offset to positive offset.")]
		public Vector2 offset;

		// Token: 0x040000F2 RID: 242
		[Tooltip("Changes the speed at which the number moves back and fourth.\nUsed in a sinus function.")]
		public float frequency;
	}
}
