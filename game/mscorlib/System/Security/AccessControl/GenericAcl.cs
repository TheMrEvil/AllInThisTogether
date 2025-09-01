using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.CommonAcl" />, <see cref="T:System.Security.AccessControl.DiscretionaryAcl" />, <see cref="T:System.Security.AccessControl.RawAcl" />, and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x0200052D RID: 1325
	public abstract class GenericAcl : ICollection, IEnumerable
	{
		// Token: 0x06003478 RID: 13432 RVA: 0x000BF4A7 File Offset: 0x000BD6A7
		static GenericAcl()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.GenericAcl" /> class.</summary>
		// Token: 0x06003479 RID: 13433 RVA: 0x0000259F File Offset: 0x0000079F
		protected GenericAcl()
		{
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericAcl.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</returns>
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600347A RID: 13434
		public abstract int BinaryLength { get; }

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</returns>
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600347B RID: 13435
		public abstract int Count { get; }

		/// <summary>This property is always set to <see langword="false" />. It is implemented only because it is required for the implementation of the <see cref="T:System.Collections.ICollection" /> interface.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.GenericAce" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.GenericAce" /> to get or set.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> at the specified index.</returns>
		// Token: 0x1700072B RID: 1835
		public abstract GenericAce this[int index]
		{
			get;
			set;
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.GenericAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.GenericAcl" />.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600347F RID: 13439
		public abstract byte Revision { get; }

		/// <summary>This property always returns <see langword="null" />. It is implemented only because it is required for the implementation of the <see cref="T:System.Collections.ICollection" /> interface.</summary>
		/// <returns>Always returns <see langword="null" />.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x0000270D File Offset: 0x0000090D
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies each <see cref="T:System.Security.AccessControl.GenericAce" /> of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> into the specified array.</summary>
		/// <param name="array">The array into which copies of the <see cref="T:System.Security.AccessControl.GenericAce" /> objects contained by the current <see cref="T:System.Security.AccessControl.GenericAcl" /> are placed.</param>
		/// <param name="index">The zero-based index of <paramref name="array" /> where the copying begins.</param>
		// Token: 0x06003481 RID: 13441 RVA: 0x000BF4C0 File Offset: 0x000BD6C0
		public void CopyTo(GenericAce[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || array.Length - index < this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be non-negative integer and must not exceed array length - count");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Copies each <see cref="T:System.Security.AccessControl.GenericAce" /> of the current <see cref="T:System.Security.AccessControl.GenericAcl" /> into the specified array.</summary>
		/// <param name="array">The array into which copies of the <see cref="T:System.Security.AccessControl.GenericAce" /> objects contained by the current <see cref="T:System.Security.AccessControl.GenericAcl" /> are placed.</param>
		/// <param name="index">The zero-based index of <paramref name="array" /> where the copying begins.</param>
		// Token: 0x06003482 RID: 13442 RVA: 0x000BF519 File Offset: 0x000BD719
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((GenericAce[])array, index);
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.GenericAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06003483 RID: 13443
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		/// <summary>Retrieves an object that you can use to iterate through the access control entries (ACEs) in an access control list (ACL).</summary>
		/// <returns>An enumerator object.</returns>
		// Token: 0x06003484 RID: 13444 RVA: 0x000BF528 File Offset: 0x000BD728
		public AceEnumerator GetEnumerator()
		{
			return new AceEnumerator(this);
		}

		/// <summary>Returns a new instance of the <see cref="T:System.Security.AccessControl.AceEnumerator" /> class cast as an instance of the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
		/// <returns>A new <see cref="T:System.Security.AccessControl.AceEnumerator" /> object, cast as an instance of the <see cref="T:System.Collections.IEnumerator" /> interface.</returns>
		// Token: 0x06003485 RID: 13445 RVA: 0x000BF530 File Offset: 0x000BD730
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003486 RID: 13446
		internal abstract string GetSddlForm(ControlFlags sdFlags, bool isDacl);

		/// <summary>The revision level of the current <see cref="T:System.Security.AccessControl.GenericAcl" />. This value is returned by the <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> property for Access Control Lists (ACLs) that are not associated with Directory Services objects.</summary>
		// Token: 0x040024A7 RID: 9383
		public static readonly byte AclRevision = 2;

		/// <summary>The revision level of the current <see cref="T:System.Security.AccessControl.GenericAcl" />. This value is returned by the <see cref="P:System.Security.AccessControl.GenericAcl.Revision" /> property for Access Control Lists (ACLs) that are associated with Directory Services objects.</summary>
		// Token: 0x040024A8 RID: 9384
		public static readonly byte AclRevisionDS = 4;

		/// <summary>The maximum allowed binary length of a <see cref="T:System.Security.AccessControl.GenericAcl" /> object.</summary>
		// Token: 0x040024A9 RID: 9385
		public static readonly int MaxBinaryLength = 65536;
	}
}
