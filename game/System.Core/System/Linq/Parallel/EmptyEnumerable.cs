using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000F1 RID: 241
	internal class EmptyEnumerable<T> : ParallelQuery<T>
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x0001D030 File Offset: 0x0001B230
		private EmptyEnumerable() : base(QuerySettings.Empty)
		{
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001D03D File Offset: 0x0001B23D
		internal static EmptyEnumerable<T> Instance
		{
			get
			{
				if (EmptyEnumerable<T>.s_instance == null)
				{
					EmptyEnumerable<T>.s_instance = new EmptyEnumerable<T>();
				}
				return EmptyEnumerable<T>.s_instance;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001D05B File Offset: 0x0001B25B
		public override IEnumerator<T> GetEnumerator()
		{
			if (EmptyEnumerable<T>.s_enumeratorInstance == null)
			{
				EmptyEnumerable<T>.s_enumeratorInstance = new EmptyEnumerator<T>();
			}
			return EmptyEnumerable<T>.s_enumeratorInstance;
		}

		// Token: 0x040005D8 RID: 1496
		private static volatile EmptyEnumerable<T> s_instance;

		// Token: 0x040005D9 RID: 1497
		private static volatile EmptyEnumerator<T> s_enumeratorInstance;
	}
}
