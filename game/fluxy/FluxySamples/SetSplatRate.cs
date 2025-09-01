using System;
using Fluxy;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x0200001F RID: 31
	public class SetSplatRate : MonoBehaviour
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00006D4E File Offset: 0x00004F4E
		public void SetRate(float value)
		{
			base.GetComponent<FluxyTarget>().rateOverTime = (float)((int)value);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006D5E File Offset: 0x00004F5E
		public SetSplatRate()
		{
		}
	}
}
