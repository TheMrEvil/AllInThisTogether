using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000026 RID: 38
	public abstract class Property<T, U> : Property<T>
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000307A File Offset: 0x0000127A
		public Property(Uniform uniform, params string[] keywords) : base(uniform, keywords)
		{
		}

		// Token: 0x0600000B RID: 11
		public abstract void SetValue(Material material, T valueM, U valueS);
	}
}
