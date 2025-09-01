using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents an n-value tuple, where n is 8 or greater.</summary>
	/// <typeparam name="T1">The type of the value tuple's first element.</typeparam>
	/// <typeparam name="T2">The type of the value tuple's second element.</typeparam>
	/// <typeparam name="T3">The type of the value tuple's third element.</typeparam>
	/// <typeparam name="T4">The type of the value tuple's fourth element.</typeparam>
	/// <typeparam name="T5">The type of the value tuple's fifth element.</typeparam>
	/// <typeparam name="T6">The type of the value tuple's sixth element.</typeparam>
	/// <typeparam name="T7">The type of the value tuple's seventh element.</typeparam>
	/// <typeparam name="TRest">Any generic value tuple instance that defines the types of the tuple's remaining elements.</typeparam>
	// Token: 0x020001B5 RID: 437
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IValueTupleInternal, ITuple where TRest : struct
	{
		/// <summary>Initializes a new <see cref="T:System.ValueTuple`8" /> instance.</summary>
		/// <param name="item1">The value tuple's first element.</param>
		/// <param name="item2">The value tuple's second element.</param>
		/// <param name="item3">The value tuple's third element.</param>
		/// <param name="item4">The value tuple's fourth element.</param>
		/// <param name="item5">The value tuple's fifth element.</param>
		/// <param name="item6">The value tuple's sixth element.</param>
		/// <param name="item7">The value tuple's seventh element.</param>
		/// <param name="rest">An instance of any value tuple type that contains the values of the value's tuple's remaining elements.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rest" /> is not a generic value tuple type.</exception>
		// Token: 0x060012F1 RID: 4849 RVA: 0x0004B52C File Offset: 0x0004972C
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
		{
			if (!(rest is IValueTupleInternal))
			{
				throw new ArgumentException("The last element of an eight element ValueTuple must be a ValueTuple.");
			}
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
			this.Rest = rest;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`8" /> instance is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012F2 RID: 4850 RVA: 0x0004B58F File Offset: 0x0004978F
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> && this.Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)obj);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`8" /> instance is equal to a specified <see cref="T:System.ValueTuple`8" /> instance.</summary>
		/// <param name="other">The value tuple to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified tuple; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012F3 RID: 4851 RVA: 0x0004B5A8 File Offset: 0x000497A8
		public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(this.Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(this.Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(this.Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(this.Item7, other.Item7) && EqualityComparer<TRest>.Default.Equals(this.Rest, other.Rest);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple`8" /> instance is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified objects; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012F4 RID: 4852 RVA: 0x0004B67C File Offset: 0x0004987C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4) && comparer.Equals(this.Item5, valueTuple.Item5) && comparer.Equals(this.Item6, valueTuple.Item6) && comparer.Equals(this.Item7, valueTuple.Item7) && comparer.Equals(this.Rest, valueTuple.Rest);
		}

		/// <summary>Compares the current <see cref="T:System.ValueTuple`8" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.  
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
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple`8" /> object.</exception>
		// Token: 0x060012F5 RID: 4853 RVA: 0x0004B798 File Offset: 0x00049998
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other);
		}

		/// <summary>Compares the current <see cref="T:System.ValueTuple`8" /> instance to a specified <see cref="T:System.ValueTuple`8" /> instance</summary>
		/// <param name="other">The tuple to compare with this instance.</param>
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
		// Token: 0x060012F6 RID: 4854 RVA: 0x0004B7E8 File Offset: 0x000499E8
		public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T2>.Default.Compare(this.Item2, other.Item2);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T3>.Default.Compare(this.Item3, other.Item3);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T4>.Default.Compare(this.Item4, other.Item4);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T5>.Default.Compare(this.Item5, other.Item5);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T6>.Default.Compare(this.Item6, other.Item6);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T7>.Default.Compare(this.Item7, other.Item7);
			if (num != 0)
			{
				return num;
			}
			return Comparer<TRest>.Default.Compare(this.Rest, other.Rest);
		}

		/// <summary>Compares the current <see cref="T:System.ValueTuple`8" /> instance to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
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
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple`8" /> object.</exception>
		// Token: 0x060012F7 RID: 4855 RVA: 0x0004B8D0 File Offset: 0x00049AD0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item2, valueTuple.Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item3, valueTuple.Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item4, valueTuple.Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item5, valueTuple.Item5);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item6, valueTuple.Item6);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item7, valueTuple.Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Rest, valueTuple.Rest);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`8" /> instance.</summary>
		/// <returns>The hash code for the current <see cref="T:System.ValueTuple`8" /> instance.</returns>
		// Token: 0x060012F8 RID: 4856 RVA: 0x0004BA28 File Offset: 0x00049C28
		public override int GetHashCode()
		{
			IValueTupleInternal valueTupleInternal = this.Rest as IValueTupleInternal;
			if (valueTupleInternal == null)
			{
				ref T1 ptr = ref this.Item1;
				T1 t = default(T1);
				int h;
				if (t == null)
				{
					t = this.Item1;
					ptr = ref t;
					if (t == null)
					{
						h = 0;
						goto IL_4C;
					}
				}
				h = ptr.GetHashCode();
				IL_4C:
				ref T2 ptr2 = ref this.Item2;
				T2 t2 = default(T2);
				int h2;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr2 = ref t2;
					if (t2 == null)
					{
						h2 = 0;
						goto IL_84;
					}
				}
				h2 = ptr2.GetHashCode();
				IL_84:
				ref T3 ptr3 = ref this.Item3;
				T3 t3 = default(T3);
				int h3;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr3 = ref t3;
					if (t3 == null)
					{
						h3 = 0;
						goto IL_BC;
					}
				}
				h3 = ptr3.GetHashCode();
				IL_BC:
				ref T4 ptr4 = ref this.Item4;
				T4 t4 = default(T4);
				int h4;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr4 = ref t4;
					if (t4 == null)
					{
						h4 = 0;
						goto IL_F4;
					}
				}
				h4 = ptr4.GetHashCode();
				IL_F4:
				ref T5 ptr5 = ref this.Item5;
				T5 t5 = default(T5);
				int h5;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr5 = ref t5;
					if (t5 == null)
					{
						h5 = 0;
						goto IL_12C;
					}
				}
				h5 = ptr5.GetHashCode();
				IL_12C:
				ref T6 ptr6 = ref this.Item6;
				T6 t6 = default(T6);
				int h6;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr6 = ref t6;
					if (t6 == null)
					{
						h6 = 0;
						goto IL_164;
					}
				}
				h6 = ptr6.GetHashCode();
				IL_164:
				ref T7 ptr7 = ref this.Item7;
				T7 t7 = default(T7);
				int h7;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr7 = ref t7;
					if (t7 == null)
					{
						h7 = 0;
						goto IL_19C;
					}
				}
				h7 = ptr7.GetHashCode();
				IL_19C:
				return ValueTuple.CombineHashCodes(h, h2, h3, h4, h5, h6, h7);
			}
			int length = valueTupleInternal.Length;
			if (length >= 8)
			{
				return valueTupleInternal.GetHashCode();
			}
			switch (8 - length)
			{
			case 1:
			{
				ref T7 ptr8 = ref this.Item7;
				T7 t7 = default(T7);
				int h8;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr8 = ref t7;
					if (t7 == null)
					{
						h8 = 0;
						goto IL_21D;
					}
				}
				h8 = ptr8.GetHashCode();
				IL_21D:
				return ValueTuple.CombineHashCodes(h8, valueTupleInternal.GetHashCode());
			}
			case 2:
			{
				ref T6 ptr9 = ref this.Item6;
				T6 t6 = default(T6);
				int h9;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr9 = ref t6;
					if (t6 == null)
					{
						h9 = 0;
						goto IL_261;
					}
				}
				h9 = ptr9.GetHashCode();
				IL_261:
				ref T7 ptr10 = ref this.Item7;
				T7 t7 = default(T7);
				int h10;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr10 = ref t7;
					if (t7 == null)
					{
						h10 = 0;
						goto IL_299;
					}
				}
				h10 = ptr10.GetHashCode();
				IL_299:
				return ValueTuple.CombineHashCodes(h9, h10, valueTupleInternal.GetHashCode());
			}
			case 3:
			{
				ref T5 ptr11 = ref this.Item5;
				T5 t5 = default(T5);
				int h11;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr11 = ref t5;
					if (t5 == null)
					{
						h11 = 0;
						goto IL_2DD;
					}
				}
				h11 = ptr11.GetHashCode();
				IL_2DD:
				ref T6 ptr12 = ref this.Item6;
				T6 t6 = default(T6);
				int h12;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr12 = ref t6;
					if (t6 == null)
					{
						h12 = 0;
						goto IL_315;
					}
				}
				h12 = ptr12.GetHashCode();
				IL_315:
				ref T7 ptr13 = ref this.Item7;
				T7 t7 = default(T7);
				int h13;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr13 = ref t7;
					if (t7 == null)
					{
						h13 = 0;
						goto IL_34D;
					}
				}
				h13 = ptr13.GetHashCode();
				IL_34D:
				return ValueTuple.CombineHashCodes(h11, h12, h13, valueTupleInternal.GetHashCode());
			}
			case 4:
			{
				ref T4 ptr14 = ref this.Item4;
				T4 t4 = default(T4);
				int h14;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr14 = ref t4;
					if (t4 == null)
					{
						h14 = 0;
						goto IL_391;
					}
				}
				h14 = ptr14.GetHashCode();
				IL_391:
				ref T5 ptr15 = ref this.Item5;
				T5 t5 = default(T5);
				int h15;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr15 = ref t5;
					if (t5 == null)
					{
						h15 = 0;
						goto IL_3C9;
					}
				}
				h15 = ptr15.GetHashCode();
				IL_3C9:
				ref T6 ptr16 = ref this.Item6;
				T6 t6 = default(T6);
				int h16;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr16 = ref t6;
					if (t6 == null)
					{
						h16 = 0;
						goto IL_401;
					}
				}
				h16 = ptr16.GetHashCode();
				IL_401:
				ref T7 ptr17 = ref this.Item7;
				T7 t7 = default(T7);
				int h17;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr17 = ref t7;
					if (t7 == null)
					{
						h17 = 0;
						goto IL_439;
					}
				}
				h17 = ptr17.GetHashCode();
				IL_439:
				return ValueTuple.CombineHashCodes(h14, h15, h16, h17, valueTupleInternal.GetHashCode());
			}
			case 5:
			{
				ref T3 ptr18 = ref this.Item3;
				T3 t3 = default(T3);
				int h18;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr18 = ref t3;
					if (t3 == null)
					{
						h18 = 0;
						goto IL_47D;
					}
				}
				h18 = ptr18.GetHashCode();
				IL_47D:
				ref T4 ptr19 = ref this.Item4;
				T4 t4 = default(T4);
				int h19;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr19 = ref t4;
					if (t4 == null)
					{
						h19 = 0;
						goto IL_4B5;
					}
				}
				h19 = ptr19.GetHashCode();
				IL_4B5:
				ref T5 ptr20 = ref this.Item5;
				T5 t5 = default(T5);
				int h20;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr20 = ref t5;
					if (t5 == null)
					{
						h20 = 0;
						goto IL_4ED;
					}
				}
				h20 = ptr20.GetHashCode();
				IL_4ED:
				ref T6 ptr21 = ref this.Item6;
				T6 t6 = default(T6);
				int h21;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr21 = ref t6;
					if (t6 == null)
					{
						h21 = 0;
						goto IL_525;
					}
				}
				h21 = ptr21.GetHashCode();
				IL_525:
				ref T7 ptr22 = ref this.Item7;
				T7 t7 = default(T7);
				int h22;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr22 = ref t7;
					if (t7 == null)
					{
						h22 = 0;
						goto IL_55D;
					}
				}
				h22 = ptr22.GetHashCode();
				IL_55D:
				return ValueTuple.CombineHashCodes(h18, h19, h20, h21, h22, valueTupleInternal.GetHashCode());
			}
			case 6:
			{
				ref T2 ptr23 = ref this.Item2;
				T2 t2 = default(T2);
				int h23;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr23 = ref t2;
					if (t2 == null)
					{
						h23 = 0;
						goto IL_5A1;
					}
				}
				h23 = ptr23.GetHashCode();
				IL_5A1:
				ref T3 ptr24 = ref this.Item3;
				T3 t3 = default(T3);
				int h24;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr24 = ref t3;
					if (t3 == null)
					{
						h24 = 0;
						goto IL_5D9;
					}
				}
				h24 = ptr24.GetHashCode();
				IL_5D9:
				ref T4 ptr25 = ref this.Item4;
				T4 t4 = default(T4);
				int h25;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr25 = ref t4;
					if (t4 == null)
					{
						h25 = 0;
						goto IL_611;
					}
				}
				h25 = ptr25.GetHashCode();
				IL_611:
				ref T5 ptr26 = ref this.Item5;
				T5 t5 = default(T5);
				int h26;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr26 = ref t5;
					if (t5 == null)
					{
						h26 = 0;
						goto IL_649;
					}
				}
				h26 = ptr26.GetHashCode();
				IL_649:
				ref T6 ptr27 = ref this.Item6;
				T6 t6 = default(T6);
				int h27;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr27 = ref t6;
					if (t6 == null)
					{
						h27 = 0;
						goto IL_681;
					}
				}
				h27 = ptr27.GetHashCode();
				IL_681:
				ref T7 ptr28 = ref this.Item7;
				T7 t7 = default(T7);
				int h28;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr28 = ref t7;
					if (t7 == null)
					{
						h28 = 0;
						goto IL_6B9;
					}
				}
				h28 = ptr28.GetHashCode();
				IL_6B9:
				return ValueTuple.CombineHashCodes(h23, h24, h25, h26, h27, h28, valueTupleInternal.GetHashCode());
			}
			case 7:
			case 8:
			{
				ref T1 ptr29 = ref this.Item1;
				T1 t = default(T1);
				int h29;
				if (t == null)
				{
					t = this.Item1;
					ptr29 = ref t;
					if (t == null)
					{
						h29 = 0;
						goto IL_6FA;
					}
				}
				h29 = ptr29.GetHashCode();
				IL_6FA:
				ref T2 ptr30 = ref this.Item2;
				T2 t2 = default(T2);
				int h30;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr30 = ref t2;
					if (t2 == null)
					{
						h30 = 0;
						goto IL_732;
					}
				}
				h30 = ptr30.GetHashCode();
				IL_732:
				ref T3 ptr31 = ref this.Item3;
				T3 t3 = default(T3);
				int h31;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr31 = ref t3;
					if (t3 == null)
					{
						h31 = 0;
						goto IL_76A;
					}
				}
				h31 = ptr31.GetHashCode();
				IL_76A:
				ref T4 ptr32 = ref this.Item4;
				T4 t4 = default(T4);
				int h32;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr32 = ref t4;
					if (t4 == null)
					{
						h32 = 0;
						goto IL_7A2;
					}
				}
				h32 = ptr32.GetHashCode();
				IL_7A2:
				ref T5 ptr33 = ref this.Item5;
				T5 t5 = default(T5);
				int h33;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr33 = ref t5;
					if (t5 == null)
					{
						h33 = 0;
						goto IL_7DA;
					}
				}
				h33 = ptr33.GetHashCode();
				IL_7DA:
				ref T6 ptr34 = ref this.Item6;
				T6 t6 = default(T6);
				int h34;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr34 = ref t6;
					if (t6 == null)
					{
						h34 = 0;
						goto IL_812;
					}
				}
				h34 = ptr34.GetHashCode();
				IL_812:
				ref T7 ptr35 = ref this.Item7;
				T7 t7 = default(T7);
				int h35;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr35 = ref t7;
					if (t7 == null)
					{
						h35 = 0;
						goto IL_84A;
					}
				}
				h35 = ptr35.GetHashCode();
				IL_84A:
				return ValueTuple.CombineHashCodes(h29, h30, h31, h32, h33, h34, h35, valueTupleInternal.GetHashCode());
			}
			default:
				return -1;
			}
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.ValueTuple`8" /> instance by using a specified computation method.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method calculates the hash code of the current <see cref="T:System.ValueTuple`8" /> instance.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060012F9 RID: 4857 RVA: 0x0004C28C File Offset: 0x0004A48C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004C298 File Offset: 0x0004A498
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			IValueTupleInternal valueTupleInternal = this.Rest as IValueTupleInternal;
			if (valueTupleInternal == null)
			{
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7));
			}
			int length = valueTupleInternal.Length;
			if (length >= 8)
			{
				return valueTupleInternal.GetHashCode(comparer);
			}
			switch (8 - length)
			{
			case 1:
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item7), valueTupleInternal.GetHashCode(comparer));
			case 2:
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), valueTupleInternal.GetHashCode(comparer));
			case 3:
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), valueTupleInternal.GetHashCode(comparer));
			case 4:
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), valueTupleInternal.GetHashCode(comparer));
			case 5:
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), valueTupleInternal.GetHashCode(comparer));
			case 6:
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), valueTupleInternal.GetHashCode(comparer));
			case 7:
			case 8:
				return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), valueTupleInternal.GetHashCode(comparer));
			default:
				return -1;
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004C28C File Offset: 0x0004A48C
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.ValueTuple`8" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.ValueTuple`8" /> instance.</returns>
		// Token: 0x060012FC RID: 4860 RVA: 0x0004C5B4 File Offset: 0x0004A7B4
		public override string ToString()
		{
			IValueTupleInternal valueTupleInternal = this.Rest as IValueTupleInternal;
			T1 t;
			T2 t2;
			T3 t3;
			T4 t4;
			T5 t5;
			T6 t6;
			T7 t7;
			if (valueTupleInternal == null)
			{
				string[] array = new string[17];
				array[0] = "(";
				int num = 1;
				ref T1 ptr = ref this.Item1;
				t = default(T1);
				string text;
				if (t == null)
				{
					t = this.Item1;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_5D;
					}
				}
				text = ptr.ToString();
				IL_5D:
				array[num] = text;
				array[2] = ", ";
				int num2 = 3;
				ref T2 ptr2 = ref this.Item2;
				t2 = default(T2);
				string text2;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr2 = ref t2;
					if (t2 == null)
					{
						text2 = null;
						goto IL_9D;
					}
				}
				text2 = ptr2.ToString();
				IL_9D:
				array[num2] = text2;
				array[4] = ", ";
				int num3 = 5;
				ref T3 ptr3 = ref this.Item3;
				t3 = default(T3);
				string text3;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr3 = ref t3;
					if (t3 == null)
					{
						text3 = null;
						goto IL_DD;
					}
				}
				text3 = ptr3.ToString();
				IL_DD:
				array[num3] = text3;
				array[6] = ", ";
				int num4 = 7;
				ref T4 ptr4 = ref this.Item4;
				t4 = default(T4);
				string text4;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr4 = ref t4;
					if (t4 == null)
					{
						text4 = null;
						goto IL_120;
					}
				}
				text4 = ptr4.ToString();
				IL_120:
				array[num4] = text4;
				array[8] = ", ";
				int num5 = 9;
				ref T5 ptr5 = ref this.Item5;
				t5 = default(T5);
				string text5;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr5 = ref t5;
					if (t5 == null)
					{
						text5 = null;
						goto IL_164;
					}
				}
				text5 = ptr5.ToString();
				IL_164:
				array[num5] = text5;
				array[10] = ", ";
				int num6 = 11;
				ref T6 ptr6 = ref this.Item6;
				t6 = default(T6);
				string text6;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr6 = ref t6;
					if (t6 == null)
					{
						text6 = null;
						goto IL_1A9;
					}
				}
				text6 = ptr6.ToString();
				IL_1A9:
				array[num6] = text6;
				array[12] = ", ";
				int num7 = 13;
				ref T7 ptr7 = ref this.Item7;
				t7 = default(T7);
				string text7;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr7 = ref t7;
					if (t7 == null)
					{
						text7 = null;
						goto IL_1EE;
					}
				}
				text7 = ptr7.ToString();
				IL_1EE:
				array[num7] = text7;
				array[14] = ", ";
				array[15] = this.Rest.ToString();
				array[16] = ")";
				return string.Concat(array);
			}
			string[] array2 = new string[16];
			array2[0] = "(";
			int num8 = 1;
			ref T1 ptr8 = ref this.Item1;
			t = default(T1);
			string text8;
			if (t == null)
			{
				t = this.Item1;
				ptr8 = ref t;
				if (t == null)
				{
					text8 = null;
					goto IL_262;
				}
			}
			text8 = ptr8.ToString();
			IL_262:
			array2[num8] = text8;
			array2[2] = ", ";
			int num9 = 3;
			ref T2 ptr9 = ref this.Item2;
			t2 = default(T2);
			string text9;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr9 = ref t2;
				if (t2 == null)
				{
					text9 = null;
					goto IL_2A2;
				}
			}
			text9 = ptr9.ToString();
			IL_2A2:
			array2[num9] = text9;
			array2[4] = ", ";
			int num10 = 5;
			ref T3 ptr10 = ref this.Item3;
			t3 = default(T3);
			string text10;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr10 = ref t3;
				if (t3 == null)
				{
					text10 = null;
					goto IL_2E2;
				}
			}
			text10 = ptr10.ToString();
			IL_2E2:
			array2[num10] = text10;
			array2[6] = ", ";
			int num11 = 7;
			ref T4 ptr11 = ref this.Item4;
			t4 = default(T4);
			string text11;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr11 = ref t4;
				if (t4 == null)
				{
					text11 = null;
					goto IL_325;
				}
			}
			text11 = ptr11.ToString();
			IL_325:
			array2[num11] = text11;
			array2[8] = ", ";
			int num12 = 9;
			ref T5 ptr12 = ref this.Item5;
			t5 = default(T5);
			string text12;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr12 = ref t5;
				if (t5 == null)
				{
					text12 = null;
					goto IL_369;
				}
			}
			text12 = ptr12.ToString();
			IL_369:
			array2[num12] = text12;
			array2[10] = ", ";
			int num13 = 11;
			ref T6 ptr13 = ref this.Item6;
			t6 = default(T6);
			string text13;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr13 = ref t6;
				if (t6 == null)
				{
					text13 = null;
					goto IL_3AE;
				}
			}
			text13 = ptr13.ToString();
			IL_3AE:
			array2[num13] = text13;
			array2[12] = ", ";
			int num14 = 13;
			ref T7 ptr14 = ref this.Item7;
			t7 = default(T7);
			string text14;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr14 = ref t7;
				if (t7 == null)
				{
					text14 = null;
					goto IL_3F3;
				}
			}
			text14 = ptr14.ToString();
			IL_3F3:
			array2[num14] = text14;
			array2[14] = ", ";
			array2[15] = valueTupleInternal.ToStringEnd();
			return string.Concat(array2);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004C9D0 File Offset: 0x0004ABD0
		string IValueTupleInternal.ToStringEnd()
		{
			IValueTupleInternal valueTupleInternal = this.Rest as IValueTupleInternal;
			T1 t;
			T2 t2;
			T3 t3;
			T4 t4;
			T5 t5;
			T6 t6;
			T7 t7;
			if (valueTupleInternal == null)
			{
				string[] array = new string[16];
				int num = 0;
				ref T1 ptr = ref this.Item1;
				t = default(T1);
				string text;
				if (t == null)
				{
					t = this.Item1;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_55;
					}
				}
				text = ptr.ToString();
				IL_55:
				array[num] = text;
				array[1] = ", ";
				int num2 = 2;
				ref T2 ptr2 = ref this.Item2;
				t2 = default(T2);
				string text2;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr2 = ref t2;
					if (t2 == null)
					{
						text2 = null;
						goto IL_95;
					}
				}
				text2 = ptr2.ToString();
				IL_95:
				array[num2] = text2;
				array[3] = ", ";
				int num3 = 4;
				ref T3 ptr3 = ref this.Item3;
				t3 = default(T3);
				string text3;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr3 = ref t3;
					if (t3 == null)
					{
						text3 = null;
						goto IL_D5;
					}
				}
				text3 = ptr3.ToString();
				IL_D5:
				array[num3] = text3;
				array[5] = ", ";
				int num4 = 6;
				ref T4 ptr4 = ref this.Item4;
				t4 = default(T4);
				string text4;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr4 = ref t4;
					if (t4 == null)
					{
						text4 = null;
						goto IL_118;
					}
				}
				text4 = ptr4.ToString();
				IL_118:
				array[num4] = text4;
				array[7] = ", ";
				int num5 = 8;
				ref T5 ptr5 = ref this.Item5;
				t5 = default(T5);
				string text5;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr5 = ref t5;
					if (t5 == null)
					{
						text5 = null;
						goto IL_15B;
					}
				}
				text5 = ptr5.ToString();
				IL_15B:
				array[num5] = text5;
				array[9] = ", ";
				int num6 = 10;
				ref T6 ptr6 = ref this.Item6;
				t6 = default(T6);
				string text6;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr6 = ref t6;
					if (t6 == null)
					{
						text6 = null;
						goto IL_1A0;
					}
				}
				text6 = ptr6.ToString();
				IL_1A0:
				array[num6] = text6;
				array[11] = ", ";
				int num7 = 12;
				ref T7 ptr7 = ref this.Item7;
				t7 = default(T7);
				string text7;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr7 = ref t7;
					if (t7 == null)
					{
						text7 = null;
						goto IL_1E5;
					}
				}
				text7 = ptr7.ToString();
				IL_1E5:
				array[num7] = text7;
				array[13] = ", ";
				array[14] = this.Rest.ToString();
				array[15] = ")";
				return string.Concat(array);
			}
			string[] array2 = new string[15];
			int num8 = 0;
			ref T1 ptr8 = ref this.Item1;
			t = default(T1);
			string text8;
			if (t == null)
			{
				t = this.Item1;
				ptr8 = ref t;
				if (t == null)
				{
					text8 = null;
					goto IL_251;
				}
			}
			text8 = ptr8.ToString();
			IL_251:
			array2[num8] = text8;
			array2[1] = ", ";
			int num9 = 2;
			ref T2 ptr9 = ref this.Item2;
			t2 = default(T2);
			string text9;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr9 = ref t2;
				if (t2 == null)
				{
					text9 = null;
					goto IL_291;
				}
			}
			text9 = ptr9.ToString();
			IL_291:
			array2[num9] = text9;
			array2[3] = ", ";
			int num10 = 4;
			ref T3 ptr10 = ref this.Item3;
			t3 = default(T3);
			string text10;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr10 = ref t3;
				if (t3 == null)
				{
					text10 = null;
					goto IL_2D1;
				}
			}
			text10 = ptr10.ToString();
			IL_2D1:
			array2[num10] = text10;
			array2[5] = ", ";
			int num11 = 6;
			ref T4 ptr11 = ref this.Item4;
			t4 = default(T4);
			string text11;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr11 = ref t4;
				if (t4 == null)
				{
					text11 = null;
					goto IL_314;
				}
			}
			text11 = ptr11.ToString();
			IL_314:
			array2[num11] = text11;
			array2[7] = ", ";
			int num12 = 8;
			ref T5 ptr12 = ref this.Item5;
			t5 = default(T5);
			string text12;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr12 = ref t5;
				if (t5 == null)
				{
					text12 = null;
					goto IL_357;
				}
			}
			text12 = ptr12.ToString();
			IL_357:
			array2[num12] = text12;
			array2[9] = ", ";
			int num13 = 10;
			ref T6 ptr13 = ref this.Item6;
			t6 = default(T6);
			string text13;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr13 = ref t6;
				if (t6 == null)
				{
					text13 = null;
					goto IL_39C;
				}
			}
			text13 = ptr13.ToString();
			IL_39C:
			array2[num13] = text13;
			array2[11] = ", ";
			int num14 = 12;
			ref T7 ptr14 = ref this.Item7;
			t7 = default(T7);
			string text14;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr14 = ref t7;
				if (t7 == null)
				{
					text14 = null;
					goto IL_3E1;
				}
			}
			text14 = ptr14.ToString();
			IL_3E1:
			array2[num14] = text14;
			array2[13] = ", ";
			array2[14] = valueTupleInternal.ToStringEnd();
			return string.Concat(array2);
		}

		/// <summary>Gets the number of elements in the <see langword="ValueTuple" />.</summary>
		/// <returns>The number of elements in the <see langword="ValueTuple" />.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0004CDD8 File Offset: 0x0004AFD8
		int ITuple.Length
		{
			get
			{
				IValueTupleInternal valueTupleInternal = this.Rest as IValueTupleInternal;
				if (valueTupleInternal != null)
				{
					return 7 + valueTupleInternal.Length;
				}
				return 8;
			}
		}

		/// <summary>Gets the value of the specified <see langword="ValueTuple" /> element.</summary>
		/// <param name="index">The value of the specified <see langword="ValueTuple" /> element. <paramref name="index" /> can range from 0 for <see langword="Item1" /> to one less than the number of elements in the <see langword="ValueTuple" />.</param>
		/// <returns>The value of the <see langword="ValueTuple" /> element at the specified position.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than or equal to <see cref="P:System.ValueTuple`8.System#Runtime#CompilerServices#ITuple#Length" />.</exception>
		// Token: 0x170001C3 RID: 451
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
				{
					IValueTupleInternal valueTupleInternal = this.Rest as IValueTupleInternal;
					if (valueTupleInternal != null)
					{
						return valueTupleInternal[index - 7];
					}
					if (index == 7)
					{
						return this.Rest;
					}
					throw new IndexOutOfRangeException();
				}
				}
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's first element.</summary>
		// Token: 0x04001382 RID: 4994
		public T1 Item1;

		/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's second element.</summary>
		// Token: 0x04001383 RID: 4995
		public T2 Item2;

		/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's third element.</summary>
		// Token: 0x04001384 RID: 4996
		public T3 Item3;

		/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's fourth element.</summary>
		// Token: 0x04001385 RID: 4997
		public T4 Item4;

		/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's fifth element.</summary>
		// Token: 0x04001386 RID: 4998
		public T5 Item5;

		/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's sixth element.</summary>
		// Token: 0x04001387 RID: 4999
		public T6 Item6;

		/// <summary>Gets the value of the current <see cref="T:System.ValueTuple`8" /> instance's seventh element.</summary>
		// Token: 0x04001388 RID: 5000
		public T7 Item7;

		/// <summary>Gets the current <see cref="T:System.ValueTuple`8" /> instance's remaining elements.</summary>
		// Token: 0x04001389 RID: 5001
		public TRest Rest;
	}
}
