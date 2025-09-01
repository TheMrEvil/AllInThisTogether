using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 1-tuple, or singleton.</summary>
	/// <typeparam name="T1">The type of the tuple's only component.</typeparam>
	// Token: 0x02000198 RID: 408
	[Serializable]
	public class Tuple<T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the <see cref="T:System.Tuple`1" /> object's single component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`1" /> object's single component.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x000426F6 File Offset: 0x000408F6
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`1" /> class.</summary>
		/// <param name="item1">The value of the tuple's only component.</param>
		// Token: 0x06001045 RID: 4165 RVA: 0x000426FE File Offset: 0x000408FE
		public Tuple(T1 item1)
		{
			this.m_Item1 = item1;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`1" /> object is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001046 RID: 4166 RVA: 0x0004270D File Offset: 0x0004090D
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`1" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001047 RID: 4167 RVA: 0x0004271C File Offset: 0x0004091C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`1" /> object to a specified object, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
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
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`1" /> object.</exception>
		// Token: 0x06001048 RID: 4168 RVA: 0x00042756 File Offset: 0x00040956
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`1" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
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
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`1" /> object.</exception>
		// Token: 0x06001049 RID: 4169 RVA: 0x00042764 File Offset: 0x00040964
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return comparer.Compare(this.m_Item1, tuple.m_Item1);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`1" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600104A RID: 4170 RVA: 0x000427BC File Offset: 0x000409BC
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`1" /> object by using a specified computation method.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.Tuple`1" /> object.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600104B RID: 4171 RVA: 0x000427C9 File Offset: 0x000409C9
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.m_Item1);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000427DC File Offset: 0x000409DC
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`1" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`1" /> object.</returns>
		// Token: 0x0600104D RID: 4173 RVA: 0x000427E8 File Offset: 0x000409E8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004280B File Offset: 0x00040A0B
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(')');
			return sb.ToString();
		}

		/// <summary>Gets the number of elements in the <see langword="Tuple" />.</summary>
		/// <returns>1, the number of elements in a <see cref="T:System.Tuple`1" /> object.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000040F7 File Offset: 0x000022F7
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		/// <summary>Gets the value of the <see langword="Tuple" /> element.</summary>
		/// <param name="index">The index of the <see langword="Tuple" /> element. <paramref name="index" /> must be 0.</param>
		/// <returns>The value of the <see langword="Tuple" /> element.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than 0.</exception>
		// Token: 0x17000131 RID: 305
		object ITuple.this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item1;
			}
		}

		// Token: 0x04001319 RID: 4889
		private readonly T1 m_Item1;
	}
}
