using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
	/// <summary>Provides a base class for implementations of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface.</summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	// Token: 0x02000AC5 RID: 2757
	[TypeDependency("System.Collections.Generic.ObjectEqualityComparer`1")]
	[Serializable]
	public abstract class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
	{
		/// <summary>Returns a default equality comparer for the type specified by the generic argument.</summary>
		/// <returns>The default instance of the <see cref="T:System.Collections.Generic.EqualityComparer`1" /> class for type <typeparamref name="T" />.</returns>
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06006288 RID: 25224 RVA: 0x00149D2C File Offset: 0x00147F2C
		public static EqualityComparer<T> Default
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				EqualityComparer<T> equalityComparer = EqualityComparer<T>.defaultComparer;
				if (equalityComparer == null)
				{
					equalityComparer = EqualityComparer<T>.CreateComparer();
					EqualityComparer<T>.defaultComparer = equalityComparer;
				}
				return equalityComparer;
			}
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x00149D54 File Offset: 0x00147F54
		[SecuritySafeCritical]
		private static EqualityComparer<T> CreateComparer()
		{
			RuntimeType runtimeType = (RuntimeType)typeof(T);
			if (runtimeType == typeof(byte))
			{
				return (EqualityComparer<T>)new ByteEqualityComparer();
			}
			if (typeof(IEquatable<T>).IsAssignableFrom(runtimeType))
			{
				return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(GenericEqualityComparer<>), runtimeType);
			}
			if (runtimeType.IsGenericType && runtimeType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				RuntimeType runtimeType2 = (RuntimeType)runtimeType.GetGenericArguments()[0];
				if (typeof(IEquatable<>).MakeGenericType(new Type[]
				{
					runtimeType2
				}).IsAssignableFrom(runtimeType2))
				{
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(NullableEqualityComparer<>), runtimeType2);
				}
			}
			if (runtimeType.IsEnum)
			{
				switch (Type.GetTypeCode(Enum.GetUnderlyingType(runtimeType)))
				{
				case TypeCode.SByte:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(SByteEnumEqualityComparer<>), runtimeType);
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(EnumEqualityComparer<>), runtimeType);
				case TypeCode.Int16:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(ShortEnumEqualityComparer<>), runtimeType);
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(LongEnumEqualityComparer<>), runtimeType);
				}
			}
			return new ObjectEqualityComparer<T>();
		}

		/// <summary>When overridden in a derived class, determines whether two objects of type <typeparamref name="T" /> are equal.</summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600628A RID: 25226
		public abstract bool Equals(T x, T y);

		/// <summary>When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.</summary>
		/// <param name="obj">The object for which to get a hash code.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x0600628B RID: 25227
		public abstract int GetHashCode(T obj);

		// Token: 0x0600628C RID: 25228 RVA: 0x00149EB8 File Offset: 0x001480B8
		internal virtual int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x00149EEC File Offset: 0x001480EC
		internal virtual int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.  
		///  -or-  
		///  <paramref name="obj" /> is of a type that cannot be cast to type <typeparamref name="T" />.</exception>
		// Token: 0x0600628E RID: 25230 RVA: 0x00149F1F File Offset: 0x0014811F
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj is T)
			{
				return this.GetHashCode((T)((object)obj));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return 0;
		}

		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="x" /> or <paramref name="y" /> is of a type that cannot be cast to type <typeparamref name="T" />.</exception>
		// Token: 0x0600628F RID: 25231 RVA: 0x00149F42 File Offset: 0x00148142
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is T && y is T)
			{
				return this.Equals((T)((object)x), (T)((object)y));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.EqualityComparer`1" /> class.</summary>
		// Token: 0x06006290 RID: 25232 RVA: 0x0000259F File Offset: 0x0000079F
		protected EqualityComparer()
		{
		}

		// Token: 0x04003A36 RID: 14902
		private static volatile EqualityComparer<T> defaultComparer;
	}
}
