using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x02000677 RID: 1655
	internal class SurrogateHashtable : Hashtable
	{
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000D5920 File Offset: 0x000D3B20
		internal SurrogateHashtable(int size) : base(size)
		{
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000D592C File Offset: 0x000D3B2C
		protected override bool KeyEquals(object key, object item)
		{
			SurrogateKey surrogateKey = (SurrogateKey)item;
			SurrogateKey surrogateKey2 = (SurrogateKey)key;
			return surrogateKey2.m_type == surrogateKey.m_type && (surrogateKey2.m_context.m_state & surrogateKey.m_context.m_state) == surrogateKey.m_context.m_state && surrogateKey2.m_context.Context == surrogateKey.m_context.Context;
		}
	}
}
