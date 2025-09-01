using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 7-tuple, or septuple.</summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
	// Token: 0x0200019E RID: 414
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's first component.</returns>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00043702 File Offset: 0x00041902
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's second component.</returns>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0004370A File Offset: 0x0004190A
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's third component.</returns>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00043712 File Offset: 0x00041912
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's fourth component.</returns>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0004371A File Offset: 0x0004191A
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's fifth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's fifth component.</returns>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00043722 File Offset: 0x00041922
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's sixth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's sixth component.</returns>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0004372A File Offset: 0x0004192A
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`7" /> object's seventh component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`7" /> object's seventh component.</returns>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00043732 File Offset: 0x00041932
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`7" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		/// <param name="item5">The value of the tuple's fifth component.</param>
		/// <param name="item6">The value of the tuple's sixth component.</param>
		/// <param name="item7">The value of the tuple's seventh component.</param>
		// Token: 0x060010A8 RID: 4264 RVA: 0x0004373A File Offset: 0x0004193A
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`7" /> object is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010A9 RID: 4265 RVA: 0x0004270D File Offset: 0x0004090D
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`7" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010AA RID: 4266 RVA: 0x00043778 File Offset: 0x00041978
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6)) && comparer.Equals(this.m_Item7, tuple.m_Item7);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`7" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance precedes <paramref name="obj" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="obj" /> have the same position in the sort order.  
		///
		///   A positive integer  
		///
		///   This instance follows <paramref name="obj" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`7" /> object.</exception>
		// Token: 0x060010AB RID: 4267 RVA: 0x00042756 File Offset: 0x00040956
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`7" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance precedes <paramref name="other" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="other" /> have the same position in the sort order.  
		///
		///   A positive integer  
		///
		///   This instance follows <paramref name="other" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`7" /> object.</exception>
		// Token: 0x060010AC RID: 4268 RVA: 0x00043870 File Offset: 0x00041A70
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item4, tuple.m_Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item5, tuple.m_Item5);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item6, tuple.m_Item6);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item7, tuple.m_Item7);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`7" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060010AD RID: 4269 RVA: 0x000427BC File Offset: 0x000409BC
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`7" /> object by using a specified computation method.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.Tuple`7" /> object.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060010AE RID: 4270 RVA: 0x00043998 File Offset: 0x00041B98
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7));
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000427DC File Offset: 0x000409DC
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`7" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`7" /> object.</returns>
		// Token: 0x060010B0 RID: 4272 RVA: 0x00043A24 File Offset: 0x00041C24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00043A48 File Offset: 0x00041C48
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(", ");
			sb.Append(this.m_Item5);
			sb.Append(", ");
			sb.Append(this.m_Item6);
			sb.Append(", ");
			sb.Append(this.m_Item7);
			sb.Append(')');
			return sb.ToString();
		}

		/// <summary>Gets the number of elements in the <see langword="Tuple" />.</summary>
		/// <returns>7, the number of elements in a <see cref="T:System.Tuple`7" /> object.</returns>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0003227E File Offset: 0x0003047E
		int ITuple.Length
		{
			get
			{
				return 7;
			}
		}

		/// <summary>Gets the value of the specified <see langword="Tuple" /> element.</summary>
		/// <param name="index">The index of the specified <see langword="Tuple" /> element. <paramref name="index" /> can range from 0 to 6.</param>
		/// <returns>The value of the <see langword="Tuple" /> element at the specified position.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than 6.</exception>
		// Token: 0x17000158 RID: 344
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Item1;
				case 1:
					return this.Item2;
				case 2:
					return this.Item3;
				case 3:
					return this.Item4;
				case 4:
					return this.Item5;
				case 5:
					return this.Item6;
				case 6:
					return this.Item7;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x0400132E RID: 4910
		private readonly T1 m_Item1;

		// Token: 0x0400132F RID: 4911
		private readonly T2 m_Item2;

		// Token: 0x04001330 RID: 4912
		private readonly T3 m_Item3;

		// Token: 0x04001331 RID: 4913
		private readonly T4 m_Item4;

		// Token: 0x04001332 RID: 4914
		private readonly T5 m_Item5;

		// Token: 0x04001333 RID: 4915
		private readonly T6 m_Item6;

		// Token: 0x04001334 RID: 4916
		private readonly T7 m_Item7;
	}
}
