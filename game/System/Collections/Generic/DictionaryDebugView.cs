using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004F0 RID: 1264
	internal sealed class DictionaryDebugView<K, V>
	{
		// Token: 0x06002959 RID: 10585 RVA: 0x0008E898 File Offset: 0x0008CA98
		public DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x0008E8B8 File Offset: 0x0008CAB8
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

		// Token: 0x040015E2 RID: 5602
		private readonly IDictionary<K, V> _dict;
	}
}
