using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides enumeration functionality for the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection. This class cannot be inherited.</summary>
	// Token: 0x0200000F RID: 15
	public sealed class CryptographicAttributeObjectEnumerator : IEnumerator
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000294A File Offset: 0x00000B4A
		internal CryptographicAttributeObjectEnumerator(CryptographicAttributeObjectCollection attributes)
		{
			this._attributes = attributes;
			this._current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002960 File Offset: 0x00000B60
		public CryptographicAttributeObject Current
		{
			get
			{
				return this._attributes[this._current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002960 File Offset: 0x00000B60
		object IEnumerator.Current
		{
			get
			{
				return this._attributes[this._current];
			}
		}

		/// <summary>Advances the enumeration to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object; <see langword="false" /> if the enumerator is at the end of the enumeration.</returns>
		// Token: 0x0600003E RID: 62 RVA: 0x00002973 File Offset: 0x00000B73
		public bool MoveNext()
		{
			if (this._current >= this._attributes.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>Resets the enumeration to the first <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		// Token: 0x0600003F RID: 63 RVA: 0x0000299B File Offset: 0x00000B9B
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal CryptographicAttributeObjectEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000083 RID: 131
		private readonly CryptographicAttributeObjectCollection _attributes;

		// Token: 0x04000084 RID: 132
		private int _current;
	}
}
