using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x02000327 RID: 807
	internal static class EmptyReadOnlyCollection<T>
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x0005236F File Offset: 0x0005056F
		// Note: this type is marked as 'beforefieldinit'.
		static EmptyReadOnlyCollection()
		{
		}

		// Token: 0x04000BDE RID: 3038
		public static readonly ReadOnlyCollection<T> Instance = new TrueReadOnlyCollection<T>(Array.Empty<T>());
	}
}
