using System;
using UnityEngine;

namespace Febucci.Attributes
{
	// Token: 0x02000003 RID: 3
	public class MinValueAttribute : PropertyAttribute
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public MinValueAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04000001 RID: 1
		public float min;
	}
}
