using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
	// Token: 0x0200006B RID: 107
	public sealed class CmsRecipientEnumerator : IEnumerator
	{
		// Token: 0x0600038F RID: 911 RVA: 0x000110CE File Offset: 0x0000F2CE
		internal CmsRecipientEnumerator(CmsRecipientCollection recipients)
		{
			this._recipients = recipients;
			this._current = -1;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public CmsRecipient Current
		{
			get
			{
				return this._recipients[this._current];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000110E4 File Offset: 0x0000F2E4
		object IEnumerator.Current
		{
			get
			{
				return this._recipients[this._current];
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.MoveNext" /> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object; <see langword="false" /> if the enumeration moved past the last item in the enumeration.</returns>
		// Token: 0x06000392 RID: 914 RVA: 0x000110F7 File Offset: 0x0000F2F7
		public bool MoveNext()
		{
			if (this._current >= this._recipients.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		// Token: 0x06000393 RID: 915 RVA: 0x0001111F File Offset: 0x0000F31F
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal CmsRecipientEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000267 RID: 615
		private readonly CmsRecipientCollection _recipients;

		// Token: 0x04000268 RID: 616
		private int _current;
	}
}
