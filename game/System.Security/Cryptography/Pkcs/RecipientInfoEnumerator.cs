using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
	// Token: 0x02000082 RID: 130
	public sealed class RecipientInfoEnumerator : IEnumerator
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x00012E66 File Offset: 0x00011066
		internal RecipientInfoEnumerator(RecipientInfoCollection RecipientInfos)
		{
			this._recipientInfos = RecipientInfos;
			this._current = -1;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the current recipient information structure in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00012E7C File Offset: 0x0001107C
		public RecipientInfo Current
		{
			get
			{
				return this._recipientInfos[this._current];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the current recipient information structure in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00012E7C File Offset: 0x0001107C
		object IEnumerator.Current
		{
			get
			{
				return this._recipientInfos[this._current];
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.MoveNext" /> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>This method returns a bool that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object, the method returns <see langword="true" />. If the enumeration moved past the last item in the enumeration, it returns <see langword="false" />.</returns>
		// Token: 0x06000439 RID: 1081 RVA: 0x00012E8F File Offset: 0x0001108F
		public bool MoveNext()
		{
			if (this._current >= this._recipientInfos.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		// Token: 0x0600043A RID: 1082 RVA: 0x00012EB7 File Offset: 0x000110B7
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal RecipientInfoEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400029C RID: 668
		private readonly RecipientInfoCollection _recipientInfos;

		// Token: 0x0400029D RID: 669
		private int _current;
	}
}
