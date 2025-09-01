using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Parallel;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq
{
	/// <summary>Represents a parallel sequence.</summary>
	/// <typeparam name="TSource">The type of element in the source sequence.</typeparam>
	// Token: 0x0200007F RID: 127
	public class ParallelQuery<TSource> : ParallelQuery, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x000080F2 File Offset: 0x000062F2
		internal ParallelQuery(QuerySettings settings) : base(settings)
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000080FB File Offset: 0x000062FB
		internal sealed override ParallelQuery<TCastTo> Cast<TCastTo>()
		{
			return from elem in this
			select (TCastTo)((object)elem);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00008124 File Offset: 0x00006324
		internal sealed override ParallelQuery<TCastTo> OfType<TCastTo>()
		{
			return from elem in this
			where elem is TCastTo
			select (TCastTo)((object)elem);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000817A File Offset: 0x0000637A
		internal override IEnumerator GetEnumeratorUntyped()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the sequence.</summary>
		/// <returns>An enumerator that iterates through the sequence.</returns>
		// Token: 0x060002A5 RID: 677 RVA: 0x000080E3 File Offset: 0x000062E3
		public virtual IEnumerator<TSource> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000235B File Offset: 0x0000055B
		internal ParallelQuery()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x02000080 RID: 128
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__1<TCastTo>
		{
			// Token: 0x060002A7 RID: 679 RVA: 0x00008182 File Offset: 0x00006382
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__1()
			{
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__1()
			{
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x0000818E File Offset: 0x0000638E
			internal TCastTo <Cast>b__1_0(TSource elem)
			{
				return (TCastTo)((object)elem);
			}

			// Token: 0x040003B4 RID: 948
			public static readonly ParallelQuery<TSource>.<>c__1<TCastTo> <>9 = new ParallelQuery<TSource>.<>c__1<TCastTo>();

			// Token: 0x040003B5 RID: 949
			public static Func<TSource, TCastTo> <>9__1_0;
		}

		// Token: 0x02000081 RID: 129
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__2<TCastTo>
		{
			// Token: 0x060002AA RID: 682 RVA: 0x0000819B File Offset: 0x0000639B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__2()
			{
			}

			// Token: 0x060002AB RID: 683 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__2()
			{
			}

			// Token: 0x060002AC RID: 684 RVA: 0x000081A7 File Offset: 0x000063A7
			internal bool <OfType>b__2_0(TSource elem)
			{
				return elem is TCastTo;
			}

			// Token: 0x060002AD RID: 685 RVA: 0x0000818E File Offset: 0x0000638E
			internal TCastTo <OfType>b__2_1(TSource elem)
			{
				return (TCastTo)((object)elem);
			}

			// Token: 0x040003B6 RID: 950
			public static readonly ParallelQuery<TSource>.<>c__2<TCastTo> <>9 = new ParallelQuery<TSource>.<>c__2<TCastTo>();

			// Token: 0x040003B7 RID: 951
			public static Func<TSource, bool> <>9__2_0;

			// Token: 0x040003B8 RID: 952
			public static Func<TSource, TCastTo> <>9__2_1;
		}
	}
}
