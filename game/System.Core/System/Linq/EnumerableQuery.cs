using System;
using System.Collections;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Represents an <see cref="T:System.Collections.IEnumerable" /> as an <see cref="T:System.Linq.EnumerableQuery" /> data source. </summary>
	// Token: 0x0200008E RID: 142
	public abstract class EnumerableQuery
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000410 RID: 1040
		internal abstract Expression Expression { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000411 RID: 1041
		internal abstract IEnumerable Enumerable { get; }

		// Token: 0x06000412 RID: 1042 RVA: 0x0000BE41 File Offset: 0x0000A041
		internal static IQueryable Create(Type elementType, IEnumerable sequence)
		{
			return (IQueryable)Activator.CreateInstance(typeof(EnumerableQuery<>).MakeGenericType(new Type[]
			{
				elementType
			}), new object[]
			{
				sequence
			});
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000BE41 File Offset: 0x0000A041
		internal static IQueryable Create(Type elementType, Expression expression)
		{
			return (IQueryable)Activator.CreateInstance(typeof(EnumerableQuery<>).MakeGenericType(new Type[]
			{
				elementType
			}), new object[]
			{
				expression
			});
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.EnumerableQuery" /> class.</summary>
		// Token: 0x06000414 RID: 1044 RVA: 0x00002162 File Offset: 0x00000362
		protected EnumerableQuery()
		{
		}
	}
}
