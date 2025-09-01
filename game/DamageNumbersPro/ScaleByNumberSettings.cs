using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public struct ScaleByNumberSettings
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00006951 File Offset: 0x00004B51
		public ScaleByNumberSettings(float customDefault)
		{
			this.fromNumber = 0f;
			this.fromScale = 1f;
			this.toNumber = 1000f;
			this.toScale = 2f;
		}

		// Token: 0x040000ED RID: 237
		[Header("Number Range:")]
		[Tooltip("The number at which scaling starts.")]
		public float fromNumber;

		// Token: 0x040000EE RID: 238
		[Tooltip("The number at which scaling caps.")]
		public float toNumber;

		// Token: 0x040000EF RID: 239
		[Header("Scale Range:")]
		[Tooltip("The scale when the number is smaller of equal 'From Number'.")]
		public float fromScale;

		// Token: 0x040000F0 RID: 240
		[Tooltip("The scale when the number is bigger of equal 'To Number'.")]
		public float toScale;
	}
}
