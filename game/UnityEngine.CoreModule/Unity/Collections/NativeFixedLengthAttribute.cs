using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x02000089 RID: 137
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class NativeFixedLengthAttribute : Attribute
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00004394 File Offset: 0x00002594
		public NativeFixedLengthAttribute(int fixedLength)
		{
			this.FixedLength = fixedLength;
		}

		// Token: 0x0400020A RID: 522
		public int FixedLength;
	}
}
