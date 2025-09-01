using System;
using System.Collections.ObjectModel;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020002E8 RID: 744
	internal sealed class TrueReadOnlyCollection<T> : ReadOnlyCollection<T>
	{
		// Token: 0x060016A8 RID: 5800 RVA: 0x0004C7C1 File Offset: 0x0004A9C1
		public TrueReadOnlyCollection(params T[] list) : base(list)
		{
		}
	}
}
