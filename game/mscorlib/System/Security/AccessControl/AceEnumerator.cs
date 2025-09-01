using System;
using System.Collections;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to iterate through the access control entries (ACEs) in an access control list (ACL).</summary>
	// Token: 0x02000505 RID: 1285
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x06003343 RID: 13123 RVA: 0x000BC872 File Offset: 0x000BAA72
		internal AceEnumerator(GenericAcl owner)
		{
			this.current = -1;
			base..ctor();
			this.owner = owner;
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection. This property gets the type-friendly version of the object.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</returns>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000BC888 File Offset: 0x000BAA88
		public GenericAce Current
		{
			get
			{
				if (this.current >= 0)
				{
					return this.owner[this.current];
				}
				return null;
			}
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000BC8A6 File Offset: 0x000BAAA6
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06003346 RID: 13126 RVA: 0x000BC8AE File Offset: 0x000BAAAE
		public bool MoveNext()
		{
			if (this.current + 1 == this.owner.Count)
			{
				return false;
			}
			this.current++;
			return true;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06003347 RID: 13127 RVA: 0x000BC8D6 File Offset: 0x000BAAD6
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000173AD File Offset: 0x000155AD
		internal AceEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400240F RID: 9231
		private GenericAcl owner;

		// Token: 0x04002410 RID: 9232
		private int current;
	}
}
