using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x020000CD RID: 205
	internal sealed class SystemCore_EnumerableDebugView
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x0001A985 File Offset: 0x00018B85
		public SystemCore_EnumerableDebugView(IEnumerable enumerable)
		{
			if (enumerable == null)
			{
				throw Error.ArgumentNull("enumerable");
			}
			this._enumerable = enumerable;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public object[] Items
		{
			get
			{
				List<object> list = new List<object>();
				foreach (object item in this._enumerable)
				{
					list.Add(item);
				}
				if (list.Count == 0)
				{
					throw new SystemCore_EnumerableDebugViewEmptyException();
				}
				return list.ToArray();
			}
		}

		// Token: 0x0400056D RID: 1389
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable _enumerable;
	}
}
