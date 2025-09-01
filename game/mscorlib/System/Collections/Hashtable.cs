using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are organized based on the hash code of the key.</summary>
	// Token: 0x02000A48 RID: 2632
	[DebuggerTypeProxy(typeof(Hashtable.HashtableDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
	{
		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x0013C4D0 File Offset: 0x0013A6D0
		private static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable
		{
			get
			{
				return LazyInitializer.EnsureInitialized<ConditionalWeakTable<object, SerializationInfo>>(ref Hashtable.s_serializationInfoTable);
			}
		}

		/// <summary>Gets or sets the object that can dispense hash codes.</summary>
		/// <returns>The object that can dispense hash codes.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value, but the hash table was created using an <see cref="T:System.Collections.IEqualityComparer" />.</exception>
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x0013C4DC File Offset: 0x0013A6DC
		// (set) Token: 0x06005E45 RID: 24133 RVA: 0x0013C510 File Offset: 0x0013A710
		[Obsolete("Please use EqualityComparer property.")]
		protected IHashCodeProvider hcp
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).HashCodeProvider;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(value, compatibleComparer.Comparer);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(value, null);
					return;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Collections.IComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value, but the hash table was created using an <see cref="T:System.Collections.IEqualityComparer" />.</exception>
		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x0013C569 File Offset: 0x0013A769
		// (set) Token: 0x06005E47 RID: 24135 RVA: 0x0013C5A0 File Offset: 0x0013A7A0
		[Obsolete("Please use KeyComparer properties.")]
		protected IComparer comparer
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).Comparer;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(compatibleComparer.HashCodeProvider, value);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(null, value);
					return;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEqualityComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IEqualityComparer" /> to use for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to a value, but the hash table was created using an <see cref="T:System.Collections.IHashCodeProvider" /> and an <see cref="T:System.Collections.IComparer" />.</exception>
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x0013C5F9 File Offset: 0x0013A7F9
		protected IEqualityComparer EqualityComparer
		{
			get
			{
				return this._keycomparer;
			}
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x0000259F File Offset: 0x0000079F
		internal Hashtable(bool trash)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity, load factor, hash code provider, and comparer.</summary>
		// Token: 0x06005E4A RID: 24138 RVA: 0x0013C601 File Offset: 0x0013A801
		public Hashtable() : this(0, 1f)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, and the default load factor, hash code provider, and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06005E4B RID: 24139 RVA: 0x0013C60F File Offset: 0x0013A80F
		public Hashtable(int capacity) : this(capacity, 1f)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity and load factor, and the default hash code provider and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="loadFactor" /> is less than 0.1.  
		/// -or-  
		/// <paramref name="loadFactor" /> is greater than 1.0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="capacity" /> is causing an overflow.</exception>
		// Token: 0x06005E4C RID: 24140 RVA: 0x0013C620 File Offset: 0x0013A820
		public Hashtable(int capacity, float loadFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if (loadFactor < 0.1f || loadFactor > 1f)
			{
				throw new ArgumentOutOfRangeException("loadFactor", SR.Format("Load factor needs to be between 0.1 and 1.0.", 0.1, 1.0));
			}
			this._loadFactor = 0.72f * loadFactor;
			double num = (double)((float)capacity / this._loadFactor);
			if (num > 2147483647.0)
			{
				throw new ArgumentException("Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.", "capacity");
			}
			int num2 = (num > 3.0) ? HashHelpers.GetPrime((int)num) : 3;
			this._buckets = new Hashtable.bucket[num2];
			this._loadsize = (int)(this._loadFactor * (float)num2);
			this._isWriterInProgress = false;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, load factor, and <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="loadFactor" /> is less than 0.1.  
		/// -or-  
		/// <paramref name="loadFactor" /> is greater than 1.0.</exception>
		// Token: 0x06005E4D RID: 24141 RVA: 0x0013C6F8 File Offset: 0x0013A8F8
		public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer) : this(capacity, loadFactor)
		{
			this._keycomparer = equalityComparer;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity and load factor, and the specified hash code provider and comparer.</summary>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" /> object.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x06005E4E RID: 24142 RVA: 0x0013C709 File Offset: 0x0013A909
		[Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
		public Hashtable(IHashCodeProvider hcp, IComparer comparer) : this(0, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity and load factor, and the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" /> object.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x06005E4F RID: 24143 RVA: 0x0013C719 File Offset: 0x0013A919
		public Hashtable(IEqualityComparer equalityComparer) : this(0, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, hash code provider, comparer, and the default load factor.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06005E50 RID: 24144 RVA: 0x0013C728 File Offset: 0x0013A928
		[Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
		public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer) : this(capacity, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity and <see cref="T:System.Collections.IEqualityComparer" />, and the default load factor.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06005E51 RID: 24145 RVA: 0x0013C738 File Offset: 0x0013A938
		public Hashtable(int capacity, IEqualityComparer equalityComparer) : this(capacity, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor, hash code provider, and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x06005E52 RID: 24146 RVA: 0x0013C747 File Offset: 0x0013A947
		public Hashtable(IDictionary d) : this(d, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor, and the default hash code provider and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.  
		/// -or-  
		/// <paramref name="loadFactor" /> is greater than 1.0.</exception>
		// Token: 0x06005E53 RID: 24147 RVA: 0x0013C755 File Offset: 0x0013A955
		public Hashtable(IDictionary d, float loadFactor) : this(d, loadFactor, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor, and the specified hash code provider and comparer. This API is obsolete. For an alternative, see <see cref="M:System.Collections.Hashtable.#ctor(System.Collections.IDictionary,System.Collections.IEqualityComparer)" />.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x06005E54 RID: 24148 RVA: 0x0013C760 File Offset: 0x0013A960
		[Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer) : this(d, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to a new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor and the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		// Token: 0x06005E55 RID: 24149 RVA: 0x0013C770 File Offset: 0x0013A970
		public Hashtable(IDictionary d, IEqualityComparer equalityComparer) : this(d, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, load factor, hash code provider, and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.  
		/// -or-  
		/// <paramref name="loadFactor" /> is less than 0.1.  
		/// -or-  
		/// <paramref name="loadFactor" /> is greater than 1.0.</exception>
		// Token: 0x06005E56 RID: 24150 RVA: 0x0013C77F File Offset: 0x0013A97F
		[Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
		public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer) : this(capacity, loadFactor)
		{
			if (hcp != null || comparer != null)
			{
				this._keycomparer = new CompatibleComparer(hcp, comparer);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor, hash code provider, and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.  
		/// -or-  
		/// <paramref name="loadFactor" /> is greater than 1.0.</exception>
		// Token: 0x06005E57 RID: 24151 RVA: 0x0013C7A0 File Offset: 0x0013A9A0
		[Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer) : this((d != null) ? d.Count : 0, loadFactor, hcp, comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor and <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.  
		///  -or-  
		///  <see langword="null" /> to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.  
		/// -or-  
		/// <paramref name="loadFactor" /> is greater than 1.0.</exception>
		// Token: 0x06005E58 RID: 24152 RVA: 0x0013C7FC File Offset: 0x0013A9FC
		public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer) : this((d != null) ? d.Count : 0, loadFactor, equalityComparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06005E59 RID: 24153 RVA: 0x0013C853 File Offset: 0x0013AA53
		protected Hashtable(SerializationInfo info, StreamingContext context)
		{
			Hashtable.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x0013C868 File Offset: 0x0013AA68
		private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(this.GetHash(key) & int.MaxValue);
			seed = num;
			incr = 1U + seed * 101U % (uint)(hashsize - 1);
			return num;
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Hashtable" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Hashtable" /> has a fixed size.</exception>
		// Token: 0x06005E5B RID: 24155 RVA: 0x0013C895 File Offset: 0x0013AA95
		public virtual void Add(object key, object value)
		{
			this.Insert(key, value, true);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only.</exception>
		// Token: 0x06005E5C RID: 24156 RVA: 0x0013C8A0 File Offset: 0x0013AAA0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public virtual void Clear()
		{
			if (this._count == 0 && this._occupancy == 0)
			{
				return;
			}
			this._isWriterInProgress = true;
			for (int i = 0; i < this._buckets.Length; i++)
			{
				this._buckets[i].hash_coll = 0;
				this._buckets[i].key = null;
				this._buckets[i].val = null;
			}
			this._count = 0;
			this._occupancy = 0;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Hashtable" />.</returns>
		// Token: 0x06005E5D RID: 24157 RVA: 0x0013C930 File Offset: 0x0013AB30
		public virtual object Clone()
		{
			Hashtable.bucket[] buckets = this._buckets;
			Hashtable hashtable = new Hashtable(this._count, this._keycomparer);
			hashtable._version = this._version;
			hashtable._loadFactor = this._loadFactor;
			hashtable._count = 0;
			int i = buckets.Length;
			while (i > 0)
			{
				i--;
				object key = buckets[i].key;
				if (key != null && key != buckets)
				{
					hashtable[key] = buckets[i].val;
				}
			}
			return hashtable;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Hashtable" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06005E5E RID: 24158 RVA: 0x0013C9AF File Offset: 0x0013ABAF
		public virtual bool Contains(object key)
		{
			return this.ContainsKey(key);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Hashtable" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06005E5F RID: 24159 RVA: 0x0013C9B8 File Offset: 0x0013ABB8
		public virtual bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			Hashtable.bucket[] buckets = this._buckets;
			uint num2;
			uint num3;
			uint num = this.InitHash(key, buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = buckets[num5];
				if (bucket.key == null)
				{
					break;
				}
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					return true;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
				{
					return false;
				}
			}
			return false;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Hashtable" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified <paramref name="value" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005E60 RID: 24160 RVA: 0x0013CA58 File Offset: 0x0013AC58
		public virtual bool ContainsValue(object value)
		{
			if (value == null)
			{
				int num = this._buckets.Length;
				while (--num >= 0)
				{
					if (this._buckets[num].key != null && this._buckets[num].key != this._buckets && this._buckets[num].val == null)
					{
						return true;
					}
				}
			}
			else
			{
				int num2 = this._buckets.Length;
				while (--num2 >= 0)
				{
					object val = this._buckets[num2].val;
					if (val != null && val.Equals(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x0013CAF4 File Offset: 0x0013ACF4
		private void CopyKeys(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(key, arrayIndex++);
				}
			}
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x0013CB3C File Offset: 0x0013AD3C
		private void CopyEntries(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(key, buckets[num].val);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Hashtable" /> elements to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Hashtable" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Hashtable" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Hashtable" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005E63 RID: 24163 RVA: 0x0013CBA0 File Offset: 0x0013ADA0
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Array cannot be null.");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			this.CopyEntries(array, arrayIndex);
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x0013CC10 File Offset: 0x0013AE10
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this._count];
			int num = 0;
			Hashtable.bucket[] buckets = this._buckets;
			int num2 = buckets.Length;
			while (--num2 >= 0)
			{
				object key = buckets[num2].key;
				if (key != null && key != this._buckets)
				{
					array[num++] = new KeyValuePairs(key, buckets[num2].val);
				}
			}
			return array;
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x0013CC78 File Offset: 0x0013AE78
		private void CopyValues(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(buckets[num].val, arrayIndex++);
				}
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new element using the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Hashtable" /> is read-only.  
		///  -or-  
		///  The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.Hashtable" /> has a fixed size.</exception>
		// Token: 0x17001079 RID: 4217
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				Hashtable.bucket[] buckets = this._buckets;
				uint num2;
				uint num3;
				uint num = this.InitHash(key, buckets.Length, out num2, out num3);
				int num4 = 0;
				int num5 = (int)(num2 % (uint)buckets.Length);
				Hashtable.bucket bucket;
				for (;;)
				{
					SpinWait spinWait = default(SpinWait);
					for (;;)
					{
						int version = this._version;
						bucket = buckets[num5];
						if (!this._isWriterInProgress && version == this._version)
						{
							break;
						}
						spinWait.SpinOnce();
					}
					if (bucket.key == null)
					{
						break;
					}
					if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
					{
						goto Block_5;
					}
					num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
					if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
					{
						goto IL_CA;
					}
				}
				return null;
				Block_5:
				return bucket.val;
				IL_CA:
				return null;
			}
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x0013CDB0 File Offset: 0x0013AFB0
		private void expand()
		{
			int newsize = HashHelpers.ExpandPrime(this._buckets.Length);
			this.rehash(newsize);
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x0013CDD2 File Offset: 0x0013AFD2
		private void rehash()
		{
			this.rehash(this._buckets.Length);
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0013CDE2 File Offset: 0x0013AFE2
		private void UpdateVersion()
		{
			this._version++;
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x0013CDF8 File Offset: 0x0013AFF8
		private void rehash(int newsize)
		{
			this._occupancy = 0;
			Hashtable.bucket[] array = new Hashtable.bucket[newsize];
			for (int i = 0; i < this._buckets.Length; i++)
			{
				Hashtable.bucket bucket = this._buckets[i];
				if (bucket.key != null && bucket.key != this._buckets)
				{
					int hashcode = bucket.hash_coll & int.MaxValue;
					this.putEntry(array, bucket.key, bucket.val, hashcode);
				}
			}
			this._isWriterInProgress = true;
			this._buckets = array;
			this._loadsize = (int)(this._loadFactor * (float)newsize);
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06005E6C RID: 24172 RVA: 0x0013CE99 File Offset: 0x0013B099
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Hashtable" />.</returns>
		// Token: 0x06005E6D RID: 24173 RVA: 0x0013CE99 File Offset: 0x0013B099
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		/// <summary>Returns the hash code for the specified key.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>The hash code for <paramref name="key" />.</returns>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06005E6E RID: 24174 RVA: 0x0013CEA2 File Offset: 0x0013B0A2
		protected virtual int GetHash(object key)
		{
			if (this._keycomparer != null)
			{
				return this._keycomparer.GetHashCode(key);
			}
			return key.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Hashtable" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Hashtable" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06005E6F RID: 24175 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Hashtable" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Hashtable" /> has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06005E70 RID: 24176 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Hashtable" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.Hashtable" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06005E71 RID: 24177 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Compares a specific <see cref="T:System.Object" /> with a specific key in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="item">The <see cref="T:System.Object" /> to compare with <paramref name="key" />.</param>
		/// <param name="key">The key in the <see cref="T:System.Collections.Hashtable" /> to compare with <paramref name="item" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> and <paramref name="key" /> are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06005E72 RID: 24178 RVA: 0x0013CEBF File Offset: 0x0013B0BF
		protected virtual bool KeyEquals(object item, object key)
		{
			if (this._buckets == item)
			{
				return false;
			}
			if (item == key)
			{
				return true;
			}
			if (this._keycomparer != null)
			{
				return this._keycomparer.Equals(item, key);
			}
			return item != null && item.Equals(key);
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</returns>
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06005E73 RID: 24179 RVA: 0x0013CEF4 File Offset: 0x0013B0F4
		public virtual ICollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Hashtable.KeyCollection(this);
				}
				return this._keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Hashtable" />.</returns>
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06005E74 RID: 24180 RVA: 0x0013CF10 File Offset: 0x0013B110
		public virtual ICollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Hashtable.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x0013CF2C File Offset: 0x0013B12C
		private void Insert(object key, object nvalue, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			if (this._count >= this._loadsize)
			{
				this.expand();
			}
			else if (this._occupancy > this._loadsize && this._count > 100)
			{
				this.rehash();
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = -1;
			int num6 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				if (num5 == -1 && this._buckets[num6].key == this._buckets && this._buckets[num6].hash_coll < 0)
				{
					num5 = num6;
				}
				if (this._buckets[num6].key == null || (this._buckets[num6].key == this._buckets && ((long)this._buckets[num6].hash_coll & (long)((ulong)-2147483648)) == 0L))
				{
					break;
				}
				if ((long)(this._buckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(this._buckets[num6].key, key))
				{
					goto Block_12;
				}
				if (num5 == -1 && this._buckets[num6].hash_coll >= 0)
				{
					Hashtable.bucket[] buckets = this._buckets;
					int num7 = num6;
					buckets[num7].hash_coll = (buckets[num7].hash_coll | int.MinValue);
					this._occupancy++;
				}
				num6 = (int)(((long)num6 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (++num4 >= this._buckets.Length)
				{
					goto Block_16;
				}
			}
			if (num5 != -1)
			{
				num6 = num5;
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this._buckets[num6].key = key;
			Hashtable.bucket[] buckets2 = this._buckets;
			int num8 = num6;
			buckets2[num8].hash_coll = (buckets2[num8].hash_coll | (int)num);
			this._count++;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_12:
			if (add)
			{
				throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", this._buckets[num6].key, key));
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_16:
			if (num5 != -1)
			{
				this._isWriterInProgress = true;
				this._buckets[num5].val = nvalue;
				this._buckets[num5].key = key;
				Hashtable.bucket[] buckets3 = this._buckets;
				int num9 = num5;
				buckets3[num9].hash_coll = (buckets3[num9].hash_coll | (int)num);
				this._count++;
				this.UpdateVersion();
				this._isWriterInProgress = false;
				return;
			}
			throw new InvalidOperationException("Hashtable insert failed. Load factor too high. The most common cause is multiple threads writing to the Hashtable simultaneously.");
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x0013D1FC File Offset: 0x0013B3FC
		private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
		{
			uint num = (uint)(1 + hashcode * 101 % (newBuckets.Length - 1));
			int num2 = hashcode % newBuckets.Length;
			while (newBuckets[num2].key != null && newBuckets[num2].key != this._buckets)
			{
				if (newBuckets[num2].hash_coll >= 0)
				{
					int num3 = num2;
					newBuckets[num3].hash_coll = (newBuckets[num3].hash_coll | int.MinValue);
					this._occupancy++;
				}
				num2 = (int)(((long)num2 + (long)((ulong)num)) % (long)((ulong)newBuckets.Length));
			}
			newBuckets[num2].val = nvalue;
			newBuckets[num2].key = key;
			int num4 = num2;
			newBuckets[num4].hash_coll = (newBuckets[num4].hash_coll | hashcode);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Hashtable" /> has a fixed size.</exception>
		// Token: 0x06005E77 RID: 24183 RVA: 0x0013D2B0 File Offset: 0x0013B4B0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = this._buckets[num5];
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					break;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= this._buckets.Length)
				{
					return;
				}
			}
			this._isWriterInProgress = true;
			Hashtable.bucket[] buckets = this._buckets;
			int num6 = num5;
			buckets[num6].hash_coll = (buckets[num6].hash_coll & int.MinValue);
			if (this._buckets[num5].hash_coll != 0)
			{
				this._buckets[num5].key = this._buckets;
			}
			else
			{
				this._buckets[num5].key = null;
			}
			this._buckets[num5].val = null;
			this._count--;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Hashtable" />.</returns>
		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06005E78 RID: 24184 RVA: 0x0013D3EE File Offset: 0x0013B5EE
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Hashtable" />.</returns>
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x0013D410 File Offset: 0x0013B610
		public virtual int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="table">The <see cref="T:System.Collections.Hashtable" /> to synchronize.</param>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="table" /> is <see langword="null" />.</exception>
		// Token: 0x06005E7A RID: 24186 RVA: 0x0013D418 File Offset: 0x0013B618
		public static Hashtable Synchronized(Hashtable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			return new Hashtable.SyncHashtable(table);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified.</exception>
		// Token: 0x06005E7B RID: 24187 RVA: 0x0013D430 File Offset: 0x0013B630
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				int version = this._version;
				info.AddValue("LoadFactor", this._loadFactor);
				info.AddValue("Version", this._version);
				IEqualityComparer keycomparer = this._keycomparer;
				if (keycomparer == null)
				{
					info.AddValue("Comparer", null, typeof(IComparer));
					info.AddValue("HashCodeProvider", null, typeof(IHashCodeProvider));
				}
				else if (keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = keycomparer as CompatibleComparer;
					info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
					info.AddValue("HashCodeProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				}
				else
				{
					info.AddValue("KeyComparer", keycomparer, typeof(IEqualityComparer));
				}
				info.AddValue("HashSize", this._buckets.Length);
				object[] array = new object[this._count];
				object[] array2 = new object[this._count];
				this.CopyKeys(array, 0);
				this.CopyValues(array2, 0);
				info.AddValue("Keys", array, typeof(object[]));
				info.AddValue("Values", array2, typeof(object[]));
				if (this._version != version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Hashtable" /> is invalid.</exception>
		// Token: 0x06005E7C RID: 24188 RVA: 0x0013D5CC File Offset: 0x0013B7CC
		public virtual void OnDeserialization(object sender)
		{
			if (this._buckets != null)
			{
				return;
			}
			SerializationInfo serializationInfo;
			Hashtable.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			int num = 0;
			IComparer comparer = null;
			IHashCodeProvider hashCodeProvider = null;
			object[] array = null;
			object[] array2 = null;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1613443821U)
				{
					if (num2 != 891156946U)
					{
						if (num2 != 1228509323U)
						{
							if (num2 == 1613443821U)
							{
								if (name == "Keys")
								{
									array = (object[])serializationInfo.GetValue("Keys", typeof(object[]));
								}
							}
						}
						else if (name == "KeyComparer")
						{
							this._keycomparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
						}
					}
					else if (name == "Comparer")
					{
						comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
					}
				}
				else if (num2 <= 2484309429U)
				{
					if (num2 != 2370642523U)
					{
						if (num2 == 2484309429U)
						{
							if (name == "HashCodeProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashCodeProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Values")
					{
						array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
					}
				}
				else if (num2 != 3356145248U)
				{
					if (num2 == 3483216242U)
					{
						if (name == "LoadFactor")
						{
							this._loadFactor = serializationInfo.GetSingle("LoadFactor");
						}
					}
				}
				else if (name == "HashSize")
				{
					num = serializationInfo.GetInt32("HashSize");
				}
			}
			this._loadsize = (int)(this._loadFactor * (float)num);
			if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
			{
				this._keycomparer = new CompatibleComparer(hashCodeProvider, comparer);
			}
			this._buckets = new Hashtable.bucket[num];
			if (array == null)
			{
				throw new SerializationException("The keys for this dictionary are missing.");
			}
			if (array2 == null)
			{
				throw new SerializationException("The values for this dictionary are missing.");
			}
			if (array.Length != array2.Length)
			{
				throw new SerializationException("The keys and values arrays have different sizes.");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new SerializationException("One of the serialized keys is null.");
				}
				this.Insert(array[i], array2[i], true);
			}
			this._version = serializationInfo.GetInt32("Version");
			Hashtable.SerializationInfoTable.Remove(this);
		}

		// Token: 0x040038F5 RID: 14581
		internal const int HashPrime = 101;

		// Token: 0x040038F6 RID: 14582
		private const int InitialSize = 3;

		// Token: 0x040038F7 RID: 14583
		private const string LoadFactorName = "LoadFactor";

		// Token: 0x040038F8 RID: 14584
		private const string VersionName = "Version";

		// Token: 0x040038F9 RID: 14585
		private const string ComparerName = "Comparer";

		// Token: 0x040038FA RID: 14586
		private const string HashCodeProviderName = "HashCodeProvider";

		// Token: 0x040038FB RID: 14587
		private const string HashSizeName = "HashSize";

		// Token: 0x040038FC RID: 14588
		private const string KeysName = "Keys";

		// Token: 0x040038FD RID: 14589
		private const string ValuesName = "Values";

		// Token: 0x040038FE RID: 14590
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x040038FF RID: 14591
		private Hashtable.bucket[] _buckets;

		// Token: 0x04003900 RID: 14592
		private int _count;

		// Token: 0x04003901 RID: 14593
		private int _occupancy;

		// Token: 0x04003902 RID: 14594
		private int _loadsize;

		// Token: 0x04003903 RID: 14595
		private float _loadFactor;

		// Token: 0x04003904 RID: 14596
		private volatile int _version;

		// Token: 0x04003905 RID: 14597
		private volatile bool _isWriterInProgress;

		// Token: 0x04003906 RID: 14598
		private ICollection _keys;

		// Token: 0x04003907 RID: 14599
		private ICollection _values;

		// Token: 0x04003908 RID: 14600
		private IEqualityComparer _keycomparer;

		// Token: 0x04003909 RID: 14601
		private object _syncRoot;

		// Token: 0x0400390A RID: 14602
		private static ConditionalWeakTable<object, SerializationInfo> s_serializationInfoTable;

		// Token: 0x02000A49 RID: 2633
		private struct bucket
		{
			// Token: 0x0400390B RID: 14603
			public object key;

			// Token: 0x0400390C RID: 14604
			public object val;

			// Token: 0x0400390D RID: 14605
			public int hash_coll;
		}

		// Token: 0x02000A4A RID: 2634
		[Serializable]
		private class KeyCollection : ICollection, IEnumerable
		{
			// Token: 0x06005E7D RID: 24189 RVA: 0x0013D8B2 File Offset: 0x0013BAB2
			internal KeyCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06005E7E RID: 24190 RVA: 0x0013D8C4 File Offset: 0x0013BAC4
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._hashtable.CopyKeys(array, arrayIndex);
			}

			// Token: 0x06005E7F RID: 24191 RVA: 0x0013D939 File Offset: 0x0013BB39
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 1);
			}

			// Token: 0x17001081 RID: 4225
			// (get) Token: 0x06005E80 RID: 24192 RVA: 0x0013D947 File Offset: 0x0013BB47
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x17001082 RID: 4226
			// (get) Token: 0x06005E81 RID: 24193 RVA: 0x0013D954 File Offset: 0x0013BB54
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x17001083 RID: 4227
			// (get) Token: 0x06005E82 RID: 24194 RVA: 0x0013D961 File Offset: 0x0013BB61
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x0400390E RID: 14606
			private Hashtable _hashtable;
		}

		// Token: 0x02000A4B RID: 2635
		[Serializable]
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06005E83 RID: 24195 RVA: 0x0013D96E File Offset: 0x0013BB6E
			internal ValueCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06005E84 RID: 24196 RVA: 0x0013D980 File Offset: 0x0013BB80
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._hashtable.CopyValues(array, arrayIndex);
			}

			// Token: 0x06005E85 RID: 24197 RVA: 0x0013D9F5 File Offset: 0x0013BBF5
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 2);
			}

			// Token: 0x17001084 RID: 4228
			// (get) Token: 0x06005E86 RID: 24198 RVA: 0x0013DA03 File Offset: 0x0013BC03
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x17001085 RID: 4229
			// (get) Token: 0x06005E87 RID: 24199 RVA: 0x0013DA10 File Offset: 0x0013BC10
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x17001086 RID: 4230
			// (get) Token: 0x06005E88 RID: 24200 RVA: 0x0013DA1D File Offset: 0x0013BC1D
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x0400390F RID: 14607
			private Hashtable _hashtable;
		}

		// Token: 0x02000A4C RID: 2636
		[Serializable]
		private class SyncHashtable : Hashtable, IEnumerable
		{
			// Token: 0x06005E89 RID: 24201 RVA: 0x0013DA2A File Offset: 0x0013BC2A
			internal SyncHashtable(Hashtable table) : base(false)
			{
				this._table = table;
			}

			// Token: 0x06005E8A RID: 24202 RVA: 0x0013DA3A File Offset: 0x0013BC3A
			internal SyncHashtable(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06005E8B RID: 24203 RVA: 0x0001B98B File Offset: 0x00019B8B
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x17001087 RID: 4231
			// (get) Token: 0x06005E8C RID: 24204 RVA: 0x0013DA49 File Offset: 0x0013BC49
			public override int Count
			{
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x17001088 RID: 4232
			// (get) Token: 0x06005E8D RID: 24205 RVA: 0x0013DA56 File Offset: 0x0013BC56
			public override bool IsReadOnly
			{
				get
				{
					return this._table.IsReadOnly;
				}
			}

			// Token: 0x17001089 RID: 4233
			// (get) Token: 0x06005E8E RID: 24206 RVA: 0x0013DA63 File Offset: 0x0013BC63
			public override bool IsFixedSize
			{
				get
				{
					return this._table.IsFixedSize;
				}
			}

			// Token: 0x1700108A RID: 4234
			// (get) Token: 0x06005E8F RID: 24207 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700108B RID: 4235
			public override object this[object key]
			{
				get
				{
					return this._table[key];
				}
				set
				{
					object syncRoot = this._table.SyncRoot;
					lock (syncRoot)
					{
						this._table[key] = value;
					}
				}
			}

			// Token: 0x1700108C RID: 4236
			// (get) Token: 0x06005E92 RID: 24210 RVA: 0x0013DACC File Offset: 0x0013BCCC
			public override object SyncRoot
			{
				get
				{
					return this._table.SyncRoot;
				}
			}

			// Token: 0x06005E93 RID: 24211 RVA: 0x0013DADC File Offset: 0x0013BCDC
			public override void Add(object key, object value)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Add(key, value);
				}
			}

			// Token: 0x06005E94 RID: 24212 RVA: 0x0013DB28 File Offset: 0x0013BD28
			public override void Clear()
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Clear();
				}
			}

			// Token: 0x06005E95 RID: 24213 RVA: 0x0013DB74 File Offset: 0x0013BD74
			public override bool Contains(object key)
			{
				return this._table.Contains(key);
			}

			// Token: 0x06005E96 RID: 24214 RVA: 0x0013DB82 File Offset: 0x0013BD82
			public override bool ContainsKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				return this._table.ContainsKey(key);
			}

			// Token: 0x06005E97 RID: 24215 RVA: 0x0013DBA4 File Offset: 0x0013BDA4
			public override bool ContainsValue(object key)
			{
				object syncRoot = this._table.SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this._table.ContainsValue(key);
				}
				return result;
			}

			// Token: 0x06005E98 RID: 24216 RVA: 0x0013DBF4 File Offset: 0x0013BDF4
			public override void CopyTo(Array array, int arrayIndex)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06005E99 RID: 24217 RVA: 0x0013DC40 File Offset: 0x0013BE40
			public override object Clone()
			{
				object syncRoot = this._table.SyncRoot;
				object result;
				lock (syncRoot)
				{
					result = Hashtable.Synchronized((Hashtable)this._table.Clone());
				}
				return result;
			}

			// Token: 0x06005E9A RID: 24218 RVA: 0x0013DC98 File Offset: 0x0013BE98
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x06005E9B RID: 24219 RVA: 0x0013DC98 File Offset: 0x0013BE98
			public override IDictionaryEnumerator GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x1700108D RID: 4237
			// (get) Token: 0x06005E9C RID: 24220 RVA: 0x0013DCA8 File Offset: 0x0013BEA8
			public override ICollection Keys
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection keys;
					lock (syncRoot)
					{
						keys = this._table.Keys;
					}
					return keys;
				}
			}

			// Token: 0x1700108E RID: 4238
			// (get) Token: 0x06005E9D RID: 24221 RVA: 0x0013DCF4 File Offset: 0x0013BEF4
			public override ICollection Values
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection values;
					lock (syncRoot)
					{
						values = this._table.Values;
					}
					return values;
				}
			}

			// Token: 0x06005E9E RID: 24222 RVA: 0x0013DD40 File Offset: 0x0013BF40
			public override void Remove(object key)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Remove(key);
				}
			}

			// Token: 0x06005E9F RID: 24223 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public override void OnDeserialization(object sender)
			{
			}

			// Token: 0x06005EA0 RID: 24224 RVA: 0x0013DD8C File Offset: 0x0013BF8C
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._table.ToKeyValuePairsArray();
			}

			// Token: 0x04003910 RID: 14608
			protected Hashtable _table;
		}

		// Token: 0x02000A4D RID: 2637
		[Serializable]
		private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06005EA1 RID: 24225 RVA: 0x0013DD99 File Offset: 0x0013BF99
			internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
			{
				this._hashtable = hashtable;
				this._bucket = hashtable._buckets.Length;
				this._version = hashtable._version;
				this._current = false;
				this._getObjectRetType = getObjRetType;
			}

			// Token: 0x06005EA2 RID: 24226 RVA: 0x000231CD File Offset: 0x000213CD
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x1700108F RID: 4239
			// (get) Token: 0x06005EA3 RID: 24227 RVA: 0x0013DDD2 File Offset: 0x0013BFD2
			public virtual object Key
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					return this._currentKey;
				}
			}

			// Token: 0x06005EA4 RID: 24228 RVA: 0x0013DDF0 File Offset: 0x0013BFF0
			public virtual bool MoveNext()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				while (this._bucket > 0)
				{
					this._bucket--;
					object key = this._hashtable._buckets[this._bucket].key;
					if (key != null && key != this._hashtable._buckets)
					{
						this._currentKey = key;
						this._currentValue = this._hashtable._buckets[this._bucket].val;
						this._current = true;
						return true;
					}
				}
				this._current = false;
				return false;
			}

			// Token: 0x17001090 RID: 4240
			// (get) Token: 0x06005EA5 RID: 24229 RVA: 0x0013DE9A File Offset: 0x0013C09A
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x17001091 RID: 4241
			// (get) Token: 0x06005EA6 RID: 24230 RVA: 0x0013DEC0 File Offset: 0x0013C0C0
			public virtual object Current
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getObjectRetType == 1)
					{
						return this._currentKey;
					}
					if (this._getObjectRetType == 2)
					{
						return this._currentValue;
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x17001092 RID: 4242
			// (get) Token: 0x06005EA7 RID: 24231 RVA: 0x0013DF16 File Offset: 0x0013C116
			public virtual object Value
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentValue;
				}
			}

			// Token: 0x06005EA8 RID: 24232 RVA: 0x0013DF34 File Offset: 0x0013C134
			public virtual void Reset()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._current = false;
				this._bucket = this._hashtable._buckets.Length;
				this._currentKey = null;
				this._currentValue = null;
			}

			// Token: 0x04003911 RID: 14609
			private Hashtable _hashtable;

			// Token: 0x04003912 RID: 14610
			private int _bucket;

			// Token: 0x04003913 RID: 14611
			private int _version;

			// Token: 0x04003914 RID: 14612
			private bool _current;

			// Token: 0x04003915 RID: 14613
			private int _getObjectRetType;

			// Token: 0x04003916 RID: 14614
			private object _currentKey;

			// Token: 0x04003917 RID: 14615
			private object _currentValue;

			// Token: 0x04003918 RID: 14616
			internal const int Keys = 1;

			// Token: 0x04003919 RID: 14617
			internal const int Values = 2;

			// Token: 0x0400391A RID: 14618
			internal const int DictEntry = 3;
		}

		// Token: 0x02000A4E RID: 2638
		internal class HashtableDebugView
		{
			// Token: 0x06005EA9 RID: 24233 RVA: 0x0013DF89 File Offset: 0x0013C189
			public HashtableDebugView(Hashtable hashtable)
			{
				if (hashtable == null)
				{
					throw new ArgumentNullException("hashtable");
				}
				this._hashtable = hashtable;
			}

			// Token: 0x17001093 RID: 4243
			// (get) Token: 0x06005EAA RID: 24234 RVA: 0x0013DFA6 File Offset: 0x0013C1A6
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this._hashtable.ToKeyValuePairsArray();
				}
			}

			// Token: 0x0400391B RID: 14619
			private Hashtable _hashtable;
		}
	}
}
