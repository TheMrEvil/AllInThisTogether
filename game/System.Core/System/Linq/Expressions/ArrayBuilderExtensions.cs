using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200023B RID: 571
	internal static class ArrayBuilderExtensions
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x000356A0 File Offset: 0x000338A0
		public static ReadOnlyCollection<T> ToReadOnly<T>(this ArrayBuilder<T> builder)
		{
			return new TrueReadOnlyCollection<T>(builder.ToArray());
		}
	}
}
