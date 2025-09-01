using System;
using System.Collections;
using System.Runtime.InteropServices;
using Unity;

namespace System.Security.Permissions
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />.</summary>
	// Token: 0x02000448 RID: 1096
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x06002C78 RID: 11384 RVA: 0x0009FBB1 File Offset: 0x0009DDB1
		internal KeyContainerPermissionAccessEntryEnumerator(ArrayList list)
		{
			this.e = list.GetEnumerator();
		}

		/// <summary>Gets the current entry in the collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed before first calling the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method. The cursor is located before the first object in the collection.  
		///  -or-  
		///  The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed after a call to the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method returns <see langword="false" />, which indicates that the cursor is located after the last object in the collection.</exception>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x0009FBC5 File Offset: 0x0009DDC5
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return (KeyContainerPermissionAccessEntry)this.e.Current;
			}
		}

		/// <summary>Gets the current object in the collection.</summary>
		/// <returns>The current object in the collection.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x0009FBD7 File Offset: 0x0009DDD7
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		/// <summary>Moves to the next element in the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		// Token: 0x06002C7B RID: 11387 RVA: 0x0009FBE4 File Offset: 0x0009DDE4
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		/// <summary>Resets the enumerator to the beginning of the collection.</summary>
		// Token: 0x06002C7C RID: 11388 RVA: 0x0009FBF1 File Offset: 0x0009DDF1
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000173AD File Offset: 0x000155AD
		internal KeyContainerPermissionAccessEntryEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002054 RID: 8276
		private IEnumerator e;
	}
}
