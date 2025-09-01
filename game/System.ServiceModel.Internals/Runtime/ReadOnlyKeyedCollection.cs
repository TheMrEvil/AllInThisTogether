using System;
using System.Collections.ObjectModel;

namespace System.Runtime
{
	// Token: 0x02000028 RID: 40
	internal class ReadOnlyKeyedCollection<TKey, TValue> : ReadOnlyCollection<TValue>
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000583C File Offset: 0x00003A3C
		public ReadOnlyKeyedCollection(KeyedCollection<TKey, TValue> innerCollection) : base(innerCollection)
		{
			this.innerCollection = innerCollection;
		}

		// Token: 0x1700002E RID: 46
		public TValue this[TKey key]
		{
			get
			{
				return this.innerCollection[key];
			}
		}

		// Token: 0x040000C8 RID: 200
		private KeyedCollection<TKey, TValue> innerCollection;
	}
}
