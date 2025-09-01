using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006B0 RID: 1712
	internal sealed class SerObjectInfoInit
	{
		// Token: 0x06003F23 RID: 16163 RVA: 0x000DA836 File Offset: 0x000D8A36
		public SerObjectInfoInit()
		{
		}

		// Token: 0x04002921 RID: 10529
		internal Hashtable seenBeforeTable = new Hashtable();

		// Token: 0x04002922 RID: 10530
		internal int objectInfoIdCount = 1;

		// Token: 0x04002923 RID: 10531
		internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
	}
}
