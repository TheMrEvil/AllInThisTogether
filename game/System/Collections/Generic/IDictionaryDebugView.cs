using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004C7 RID: 1223
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06002799 RID: 10137 RVA: 0x000891F4 File Offset: 0x000873F4
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x00089214 File Offset: 0x00087414
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001559 RID: 5465
		private readonly IDictionary<K, V> _dict;
	}
}
