using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />. This class cannot be inherited.</summary>
	// Token: 0x020002DC RID: 732
	public sealed class X509ChainElementEnumerator : IEnumerator
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x0005B7A4 File Offset: 0x000599A4
		internal X509ChainElementEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0005B7B8 File Offset: 0x000599B8
		public X509ChainElement Current
		{
			get
			{
				return (X509ChainElement)this.enumerator.Current;
			}
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0005B7CA File Offset: 0x000599CA
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600171C RID: 5916 RVA: 0x0005B7D7 File Offset: 0x000599D7
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600171D RID: 5917 RVA: 0x0005B7E4 File Offset: 0x000599E4
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal X509ChainElementEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000D08 RID: 3336
		private IEnumerator enumerator;
	}
}
