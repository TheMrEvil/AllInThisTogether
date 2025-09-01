using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fluxy
{
	// Token: 0x02000015 RID: 21
	[DisallowMultipleComponent]
	[RequireComponent(typeof(FluxyContainer))]
	public abstract class FluxyTargetProvider : MonoBehaviour, IFluxyTargetProvider
	{
		// Token: 0x0600007B RID: 123
		public abstract List<FluxyTarget> GetTargets();

		// Token: 0x0600007C RID: 124 RVA: 0x00005FB8 File Offset: 0x000041B8
		protected FluxyTargetProvider()
		{
		}
	}
}
