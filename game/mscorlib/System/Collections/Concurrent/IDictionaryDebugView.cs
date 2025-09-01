using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x02000A5D RID: 2653
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06005F4E RID: 24398 RVA: 0x0014094C File Offset: 0x0013EB4C
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dictionary = dictionary;
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06005F4F RID: 24399 RVA: 0x0014096C File Offset: 0x0013EB6C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dictionary.Count];
				this._dictionary.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003955 RID: 14677
		private readonly IDictionary<K, V> _dictionary;
	}
}
