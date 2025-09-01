using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents an n-tuple, where n is 8 or greater.</summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
	/// <typeparam name="TRest">Any generic <see langword="Tuple" /> object that defines the types of the tuple's remaining components.</typeparam>
	// Token: 0x0200019F RID: 415
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's first component.</returns>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00043BB6 File Offset: 0x00041DB6
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's second component.</returns>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00043BBE File Offset: 0x00041DBE
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's third component.</returns>
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00043BC6 File Offset: 0x00041DC6
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's fourth component.</returns>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00043BCE File Offset: 0x00041DCE
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's fifth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's fifth component.</returns>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00043BD6 File Offset: 0x00041DD6
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's sixth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's sixth component.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00043BDE File Offset: 0x00041DDE
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`8" /> object's seventh component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's seventh component.</returns>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00043BE6 File Offset: 0x00041DE6
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Tuple`8" /> object's remaining components.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`8" /> object's remaining components.</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00043BEE File Offset: 0x00041DEE
		public TRest Rest
		{
			get
			{
				return this.m_Rest;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`8" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		/// <param name="item5">The value of the tuple's fifth component.</param>
		/// <param name="item6">The value of the tuple's sixth component.</param>
		/// <param name="item7">The value of the tuple's seventh component.</param>
		/// <param name="rest">Any generic <see langword="Tuple" /> object that contains the values of the tuple's remaining components.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rest" /> is not a generic <see langword="Tuple" /> object.</exception>
		// Token: 0x060010BC RID: 4284 RVA: 0x00043BF8 File Offset: 0x00041DF8
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
		{
			if (!(rest is ITupleInternal))
			{
				throw new ArgumentException("The last element of an eight element tuple must be a Tuple.");
			}
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
			this.m_Rest = rest;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`8" /> object is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010BD RID: 4285 RVA: 0x0004270D File Offset: 0x0004090D
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`8" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010BE RID: 4286 RVA: 0x00043C64 File Offset: 0x00041E64
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6) && comparer.Equals(this.m_Item7, tuple.m_Item7)) && comparer.Equals(this.m_Rest, tuple.m_Rest);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`8" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
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
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`8" /> object.</exception>
		// Token: 0x060010BF RID: 4287 RVA: 0x00042756 File Offset: 0x00040956
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`8" /> object to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
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
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`8" /> object.</exception>
		// Token: 0x060010C0 RID: 4288 RVA: 0x00043D7C File Offset: 0x00041F7C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
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
			num = comparer.Compare(this.m_Item7, tuple.m_Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Rest, tuple.m_Rest);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`8" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060010C1 RID: 4289 RVA: 0x000427BC File Offset: 0x000409BC
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`8" /> object by using a specified computation method.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.Tuple`8" /> object.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060010C2 RID: 4290 RVA: 0x00043EC4 File Offset: 0x000420C4
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ITupleInternal tupleInternal = (ITupleInternal)((object)this.m_Rest);
			if (tupleInternal.Length >= 8)
			{
				return tupleInternal.GetHashCode(comparer);
			}
			switch (8 - tupleInternal.Length)
			{
			case 1:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 2:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 3:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 4:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 5:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 6:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 7:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			default:
				return -1;
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000427DC File Offset: 0x000409DC
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`8" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`8" /> object.</returns>
		// Token: 0x060010C4 RID: 4292 RVA: 0x00044160 File Offset: 0x00042360
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00044184 File Offset: 0x00042384
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
			sb.Append(", ");
			return ((ITupleInternal)((object)this.m_Rest)).ToString(sb);
		}

		/// <summary>Gets the number of elements in the <see langword="Tuple" />.</summary>
		/// <returns>The number of elements in the <see langword="Tuple" />.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00044279 File Offset: 0x00042479
		int ITuple.Length
		{
			get
			{
				return 7 + ((ITupleInternal)((object)this.Rest)).Length;
			}
		}

		/// <summary>Gets the value of the specified <see langword="Tuple" /> element.</summary>
		/// <param name="index">The index of the specified <see langword="Tuple" /> element. <paramref name="index" /> can range from 0 for <see langword="Item1" /> to one less than the number of elements in the <see langword="Tuple" />.</param>
		/// <returns>The value of the <see langword="Tuple" /> element at the specified position.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to <see cref="P:System.Tuple`8.System#Runtime#CompilerServices#ITuple#Length" />.</exception>
		// Token: 0x17000162 RID: 354
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
					return ((ITupleInternal)((object)this.Rest))[index - 7];
				}
			}
		}

		// Token: 0x04001335 RID: 4917
		private readonly T1 m_Item1;

		// Token: 0x04001336 RID: 4918
		private readonly T2 m_Item2;

		// Token: 0x04001337 RID: 4919
		private readonly T3 m_Item3;

		// Token: 0x04001338 RID: 4920
		private readonly T4 m_Item4;

		// Token: 0x04001339 RID: 4921
		private readonly T5 m_Item5;

		// Token: 0x0400133A RID: 4922
		private readonly T6 m_Item6;

		// Token: 0x0400133B RID: 4923
		private readonly T7 m_Item7;

		// Token: 0x0400133C RID: 4924
		private readonly TRest m_Rest;
	}
}
