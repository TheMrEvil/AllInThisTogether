using System;
using System.Collections;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	/// <summary>Provides static methods for creating value tuples.</summary>
	// Token: 0x020001AD RID: 429
	[Serializable]
	public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
	{
		/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple" /> instance is equal to a specified object.</summary>
		/// <param name="obj">The object to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.ValueTuple" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600126C RID: 4716 RVA: 0x0004851B File Offset: 0x0004671B
		public override bool Equals(object obj)
		{
			return obj is ValueTuple;
		}

		/// <summary>Determines whether two <see cref="T:System.ValueTuple" /> instances are equal. This method always returns <see langword="true" />.</summary>
		/// <param name="other">The value tuple to compare with the current instance.</param>
		/// <returns>This method always returns <see langword="true" />.</returns>
		// Token: 0x0600126D RID: 4717 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool Equals(ValueTuple other)
		{
			return true;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.ValueTuple" /> instance is equal to a specified object based on a specified comparison method.</summary>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is equal to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600126E RID: 4718 RVA: 0x0004851B File Offset: 0x0004671B
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is ValueTuple;
		}

		/// <summary>Compares this <see cref="T:System.ValueTuple" /> instance with a specified object and returns an indication of their relative values.</summary>
		/// <param name="other">The object to compare with the current instance</param>
		/// <returns>0 if <paramref name="other" /> is a <see cref="T:System.ValueTuple" /> instance; otherwise, 1 if <paramref name="other" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
		// Token: 0x0600126F RID: 4719 RVA: 0x00048526 File Offset: 0x00046726
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		/// <summary>Compares the current <see cref="T:System.ValueTuple" /> instance to a specified <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <returns>This method always returns 0.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
		// Token: 0x06001270 RID: 4720 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int CompareTo(ValueTuple other)
		{
			return 0;
		}

		/// <summary>Compares the current <see cref="T:System.ValueTuple" /> instance to a specified object.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison. This parameter is ignored.</param>
		/// <returns>Returns 0 if <paramref name="other" /> is a <see cref="T:System.ValueTuple" /> instance and 1 if <paramref name="other" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.ValueTuple" /> instance.</exception>
		// Token: 0x06001271 RID: 4721 RVA: 0x00048526 File Offset: 0x00046726
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <returns>This method always return 0.</returns>
		// Token: 0x06001272 RID: 4722 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" /> method computes the hash code. This parameter is ignored.</param>
		/// <returns>The hash code for this <see cref="T:System.ValueTuple" /> instance.</returns>
		// Token: 0x06001273 RID: 4723 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		/// <summary>Returns the string representation of this <see cref="T:System.ValueTuple" /> instance.</summary>
		/// <returns>This method always returns "()".</returns>
		// Token: 0x06001275 RID: 4725 RVA: 0x00048560 File Offset: 0x00046760
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00048567 File Offset: 0x00046767
		string IValueTupleInternal.ToStringEnd()
		{
			return ")";
		}

		/// <summary>Gets the length of this <see langword="ValueTuple" /> instance, which is always 0. There are no elements in a <see langword="ValueTuple" />.</summary>
		/// <returns>0, the number of elements in this <see langword="ValueTuple" /> instance.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		int ITuple.Length
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Returns an <see cref="T:System.IndexOutOfRangeException" />. There are no elements in a <see langword="ValueTuple" />.</summary>
		/// <param name="index">There is no acceptable value for <paramref name="index" />.</param>
		/// <returns>An <see cref="T:System.IndexOutOfRangeException" />.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no acceptable value for <paramref name="index" />.</exception>
		// Token: 0x170001B3 RID: 435
		object ITuple.this[int index]
		{
			get
			{
				throw new IndexOutOfRangeException();
			}
		}

		/// <summary>Creates a new value tuple with zero components.</summary>
		/// <returns>A new value tuple with no components.</returns>
		// Token: 0x06001279 RID: 4729 RVA: 0x00048578 File Offset: 0x00046778
		public static ValueTuple Create()
		{
			return default(ValueTuple);
		}

		/// <summary>Creates a new value tuple with 1 component (a singleton).</summary>
		/// <param name="item1">The value of the value tuple's only component.</param>
		/// <typeparam name="T1">The type of the value tuple's only component.</typeparam>
		/// <returns>A value tuple with 1 component.</returns>
		// Token: 0x0600127A RID: 4730 RVA: 0x0004858E File Offset: 0x0004678E
		public static ValueTuple<T1> Create<T1>(T1 item1)
		{
			return new ValueTuple<T1>(item1);
		}

		/// <summary>Creates a new value tuple with 2 components (a pair).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <returns>A value tuple with 2 components.</returns>
		// Token: 0x0600127B RID: 4731 RVA: 0x00048596 File Offset: 0x00046796
		public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new ValueTuple<T1, T2>(item1, item2);
		}

		/// <summary>Creates a new value tuple with 3 components (a triple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <returns>A value tuple with 3 components.</returns>
		// Token: 0x0600127C RID: 4732 RVA: 0x0004859F File Offset: 0x0004679F
		public static ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new ValueTuple<T1, T2, T3>(item1, item2, item3);
		}

		/// <summary>Creates a new value tuple with 4 components (a quadruple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <returns>A value tuple with 4 components.</returns>
		// Token: 0x0600127D RID: 4733 RVA: 0x000485A9 File Offset: 0x000467A9
		public static ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		/// <summary>Creates a new value tuple with 5 components (a quintuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <returns>A value tuple with 5 components.</returns>
		// Token: 0x0600127E RID: 4734 RVA: 0x000485B4 File Offset: 0x000467B4
		public static ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		/// <summary>Creates a new value tuple with 6 components (a sexuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <param name="item6">The value of the value tuple's sixth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
		/// <returns>A value tuple with 6 components.</returns>
		// Token: 0x0600127F RID: 4735 RVA: 0x000485C1 File Offset: 0x000467C1
		public static ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		/// <summary>Creates a new value tuple with 7 components (a septuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <param name="item6">The value of the value tuple's sixth component.</param>
		/// <param name="item7">The value of the value tuple's seventh component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
		/// <typeparam name="T7">The type of the value tuple's seventh component.</typeparam>
		/// <returns>A value tuple with 7 components.</returns>
		// Token: 0x06001280 RID: 4736 RVA: 0x000485D0 File Offset: 0x000467D0
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		/// <summary>Creates a new value tuple with 8 components (an octuple).</summary>
		/// <param name="item1">The value of the value tuple's first component.</param>
		/// <param name="item2">The value of the value tuple's second component.</param>
		/// <param name="item3">The value of the value tuple's third component.</param>
		/// <param name="item4">The value of the value tuple's fourth component.</param>
		/// <param name="item5">The value of the value tuple's fifth component.</param>
		/// <param name="item6">The value of the value tuple's sixth component.</param>
		/// <param name="item7">The value of the value tuple's seventh component.</param>
		/// <param name="item8">The value of the value tuple's eighth component.</param>
		/// <typeparam name="T1">The type of the value tuple's first component.</typeparam>
		/// <typeparam name="T2">The type of the value tuple's second component.</typeparam>
		/// <typeparam name="T3">The type of the value tuple's third component.</typeparam>
		/// <typeparam name="T4">The type of the value tuple's fourth component.</typeparam>
		/// <typeparam name="T5">The type of the value tuple's fifth component.</typeparam>
		/// <typeparam name="T6">The type of the value tuple's sixth component.</typeparam>
		/// <typeparam name="T7">The type of the value tuple's seventh component.</typeparam>
		/// <typeparam name="T8">The type of the value tuple's eighth component.</typeparam>
		/// <returns>A value tuple with 8 components.</returns>
		// Token: 0x06001281 RID: 4737 RVA: 0x000485E1 File Offset: 0x000467E1
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, ValueTuple.Create<T8>(item8));
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000485F9 File Offset: 0x000467F9
		internal static int CombineHashCodes(int h1, int h2)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(System.Numerics.Hashing.HashHelpers.Combine(System.Numerics.Hashing.HashHelpers.RandomSeed, h1), h2);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004860C File Offset: 0x0004680C
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004861B File Offset: 0x0004681B
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004862B File Offset: 0x0004682B
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004863D File Offset: 0x0004683D
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00048651 File Offset: 0x00046851
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00048667 File Offset: 0x00046867
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
