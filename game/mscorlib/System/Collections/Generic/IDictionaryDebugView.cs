using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A96 RID: 2710
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06006121 RID: 24865 RVA: 0x001450E8 File Offset: 0x001432E8
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x00145108 File Offset: 0x00143308
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

		// Token: 0x040039E3 RID: 14819
		private readonly IDictionary<K, V> _dict;
	}
}
