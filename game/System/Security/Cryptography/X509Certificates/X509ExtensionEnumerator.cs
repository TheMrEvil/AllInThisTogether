using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />. This class cannot be inherited.</summary>
	// Token: 0x020002E4 RID: 740
	public sealed class X509ExtensionEnumerator : IEnumerator
	{
		// Token: 0x0600178B RID: 6027 RVA: 0x0005D527 File Offset: 0x0005B727
		internal X509ExtensionEnumerator(ArrayList list)
		{
			this.enumerator = list.GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0005D53B File Offset: 0x0005B73B
		public X509Extension Current
		{
			get
			{
				return (X509Extension)this.enumerator.Current;
			}
		}

		/// <summary>Gets an object from a collection.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0005D54D File Offset: 0x0005B74D
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600178E RID: 6030 RVA: 0x0005D55A File Offset: 0x0005B75A
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600178F RID: 6031 RVA: 0x0005D567 File Offset: 0x0005B767
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal X509ExtensionEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000D2B RID: 3371
		private IEnumerator enumerator;
	}
}
