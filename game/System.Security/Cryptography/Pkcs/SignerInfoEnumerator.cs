using System;
using System.Collections;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
	// Token: 0x02000088 RID: 136
	public sealed class SignerInfoEnumerator : IEnumerator
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x00002145 File Offset: 0x00000345
		private SignerInfoEnumerator()
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000147E3 File Offset: 0x000129E3
		internal SignerInfoEnumerator(SignerInfoCollection signerInfos)
		{
			this._signerInfos = signerInfos;
			this._position = -1;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000147F9 File Offset: 0x000129F9
		public SignerInfo Current
		{
			get
			{
				return this._signerInfos[this._position];
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000147F9 File Offset: 0x000129F9
		object IEnumerator.Current
		{
			get
			{
				return this._signerInfos[this._position];
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.MoveNext" /> method advances the enumeration to the next   <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>This method returns a bool value that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object, the method returns <see langword="true" />. If the enumeration moved past the last item in the enumeration, it returns <see langword="false" />.</returns>
		// Token: 0x06000490 RID: 1168 RVA: 0x0001480C File Offset: 0x00012A0C
		public bool MoveNext()
		{
			int num = this._position + 1;
			if (num >= this._signerInfos.Count)
			{
				return false;
			}
			this._position = num;
			return true;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		// Token: 0x06000491 RID: 1169 RVA: 0x0001483A File Offset: 0x00012A3A
		public void Reset()
		{
			this._position = -1;
		}

		// Token: 0x040002BD RID: 701
		private readonly SignerInfoCollection _signerInfos;

		// Token: 0x040002BE RID: 702
		private int _position;
	}
}
