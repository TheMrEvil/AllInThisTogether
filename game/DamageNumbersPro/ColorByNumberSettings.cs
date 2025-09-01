using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public struct ColorByNumberSettings
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00006798 File Offset: 0x00004998
		public ColorByNumberSettings(float customValue)
		{
			this.colorGradient = new Gradient();
			this.colorGradient.SetKeys(new GradientColorKey[]
			{
				new GradientColorKey(new Color(1f, 0.8f, 0.5f), 0f),
				new GradientColorKey(new Color(1f, 0f, 0f), 1f)
			}, new GradientAlphaKey[]
			{
				new GradientAlphaKey(1f, 0f)
			});
			this.fromNumber = 10f;
			this.toNumber = 100f;
		}

		// Token: 0x040000DC RID: 220
		public Gradient colorGradient;

		// Token: 0x040000DD RID: 221
		public float fromNumber;

		// Token: 0x040000DE RID: 222
		public float toNumber;
	}
}
