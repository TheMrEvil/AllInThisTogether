using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 6-tuple, or sextuple.</summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	// Token: 0x0200019D RID: 413
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's first component.</returns>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x000432E6 File Offset: 0x000414E6
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's second component.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000432EE File Offset: 0x000414EE
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's third component.</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x000432F6 File Offset: 0x000414F6
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's fourth component.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x000432FE File Offset: 0x000414FE
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's fifth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's fifth  component.</returns>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00043306 File Offset: 0x00041506
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`6" /> object's sixth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`6" /> object's sixth component.</returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0004330E File Offset: 0x0004150E
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`6" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		/// <param name="item5">The value of the tuple's fifth component.</param>
		/// <param name="item6">The value of the tuple's sixth component.</param>
		// Token: 0x06001095 RID: 4245 RVA: 0x00043316 File Offset: 0x00041516
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`6" /> object is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001096 RID: 4246 RVA: 0x0004270D File Offset: 0x0004090D
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`6" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001097 RID: 4247 RVA: 0x0004334C File Offset: 0x0004154C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5)) && comparer.Equals(this.m_Item6, tuple.m_Item6);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`6" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
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
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`6" /> object.</exception>
		// Token: 0x06001098 RID: 4248 RVA: 0x00042756 File Offset: 0x00040956
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`6" /> object to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
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
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`6" /> object.</exception>
		// Token: 0x06001099 RID: 4249 RVA: 0x00043424 File Offset: 0x00041624
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
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
			return comparer.Compare(this.m_Item6, tuple.m_Item6);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`6" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600109A RID: 4250 RVA: 0x000427BC File Offset: 0x000409BC
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`6" /> object by using a specified computation method.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.Tuple`6" /> object.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600109B RID: 4251 RVA: 0x00043528 File Offset: 0x00041728
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6));
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000427DC File Offset: 0x000409DC
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`6" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`6" /> object.</returns>
		// Token: 0x0600109D RID: 4253 RVA: 0x000435A0 File Offset: 0x000417A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000435C4 File Offset: 0x000417C4
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
			sb.Append(')');
			return sb.ToString();
		}

		/// <summary>Gets the number of elements in the <see langword="Tuple" />.</summary>
		/// <returns>6, the number of elements in a <see cref="T:System.Tuple`6" /> object.</returns>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x000224A3 File Offset: 0x000206A3
		int ITuple.Length
		{
			get
			{
				return 6;
			}
		}

		/// <summary>Gets the value of the specified <see langword="Tuple" /> element.</summary>
		/// <param name="index">The index of the specified <see langword="Tuple" /> element. <paramref name="index" /> can range from 0 to 5.</param>
		/// <returns>The value of the <see langword="Tuple" /> element at the specified position.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than 5.</exception>
		// Token: 0x1700014F RID: 335
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x04001328 RID: 4904
		private readonly T1 m_Item1;

		// Token: 0x04001329 RID: 4905
		private readonly T2 m_Item2;

		// Token: 0x0400132A RID: 4906
		private readonly T3 m_Item3;

		// Token: 0x0400132B RID: 4907
		private readonly T4 m_Item4;

		// Token: 0x0400132C RID: 4908
		private readonly T5 m_Item5;

		// Token: 0x0400132D RID: 4909
		private readonly T6 m_Item6;
	}
}
