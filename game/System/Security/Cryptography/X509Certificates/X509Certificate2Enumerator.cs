using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020002D2 RID: 722
	public sealed class X509Certificate2Enumerator : IEnumerator
	{
		// Token: 0x0600167A RID: 5754 RVA: 0x0005A48B File Offset: 0x0005868B
		internal X509Certificate2Enumerator(X509Certificate2Collection collection)
		{
			this.enumerator = ((IEnumerable)collection).GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0005A49F File Offset: 0x0005869F
		public X509Certificate2 Current
		{
			get
			{
				return (X509Certificate2)this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600167C RID: 5756 RVA: 0x0005A4B1 File Offset: 0x000586B1
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600167D RID: 5757 RVA: 0x0005A4BE File Offset: 0x000586BE
		public void Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0005A4CB File Offset: 0x000586CB
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600167F RID: 5759 RVA: 0x0005A4B1 File Offset: 0x000586B1
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06001680 RID: 5760 RVA: 0x0005A4BE File Offset: 0x000586BE
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal X509Certificate2Enumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000CF8 RID: 3320
		private IEnumerator enumerator;
	}
}
