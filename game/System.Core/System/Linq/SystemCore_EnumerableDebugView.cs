using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000CB RID: 203
	internal sealed class SystemCore_EnumerableDebugView<T>
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x0001A941 File Offset: 0x00018B41
		public SystemCore_EnumerableDebugView(IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				throw Error.ArgumentNull("enumerable");
			}
			this._enumerable = enumerable;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001A95F File Offset: 0x00018B5F
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = this._enumerable.ToArray<T>();
				if (array.Length == 0)
				{
					throw new SystemCore_EnumerableDebugViewEmptyException();
				}
				return array;
			}
		}

		// Token: 0x0400056C RID: 1388
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable<T> _enumerable;
	}
}
