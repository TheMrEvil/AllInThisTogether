using System;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the padding mode and parameters to use with RSA signature creation or verification operations.</summary>
	// Token: 0x02000475 RID: 1141
	public sealed class RSASignaturePadding : IEquatable<RSASignaturePadding>
	{
		// Token: 0x06002E39 RID: 11833 RVA: 0x000A5FE5 File Offset: 0x000A41E5
		private RSASignaturePadding(RSASignaturePaddingMode mode)
		{
			this._mode = mode;
		}

		/// <summary>Gets an object that uses the PKCS #1 v1.5 padding mode.</summary>
		/// <returns>An object that uses the <see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pkcs1" /> padding mode.</returns>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x000A5FF4 File Offset: 0x000A41F4
		public static RSASignaturePadding Pkcs1
		{
			get
			{
				return RSASignaturePadding.s_pkcs1;
			}
		}

		/// <summary>Gets an object that uses PSS padding mode.</summary>
		/// <returns>An object that uses the <see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pss" /> padding mode with the number of salt bytes equal to the size of the hash.</returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000A5FFB File Offset: 0x000A41FB
		public static RSASignaturePadding Pss
		{
			get
			{
				return RSASignaturePadding.s_pss;
			}
		}

		/// <summary>Gets the padding mode of this <see cref="T:System.Security.Cryptography.RSASignaturePadding" /> instance.</summary>
		/// <returns>The padding mode (either <see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pkcs1" /> or <see cref="F:System.Security.Cryptography.RSASignaturePaddingMode.Pss" />) of this instance.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000A6002 File Offset: 0x000A4202
		public RSASignaturePaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Security.Cryptography.RSASignaturePadding" /> instance.</summary>
		/// <returns>The hash code for this <see cref="T:System.Security.Cryptography.RSASignaturePadding" /> instance.</returns>
		// Token: 0x06002E3D RID: 11837 RVA: 0x000A600A File Offset: 0x000A420A
		public override int GetHashCode()
		{
			return this._mode.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E3E RID: 11838 RVA: 0x000A601D File Offset: 0x000A421D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSASignaturePadding);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified <see cref="T:System.Security.Cryptography.RSASignaturePadding" /> object.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E3F RID: 11839 RVA: 0x000A602B File Offset: 0x000A422B
		public bool Equals(RSASignaturePadding other)
		{
			return other != null && this._mode == other._mode;
		}

		/// <summary>Indicates whether two specified <see cref="T:System.Security.Cryptography.RSASignaturePadding" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <see langword="left" /> and <see langword="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E40 RID: 11840 RVA: 0x000A6046 File Offset: 0x000A4246
		public static bool operator ==(RSASignaturePadding left, RSASignaturePadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Indicates whether two specified <see cref="T:System.Security.Cryptography.RSASignaturePadding" /> objects are unequal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <see langword="left" /> and <see langword="right" /> are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E41 RID: 11841 RVA: 0x000A6057 File Offset: 0x000A4257
		public static bool operator !=(RSASignaturePadding left, RSASignaturePadding right)
		{
			return !(left == right);
		}

		/// <summary>Returns the string representation of the current <see cref="T:System.Security.Cryptography.RSASignaturePadding" /> instance.</summary>
		/// <returns>The string representation of the current object.</returns>
		// Token: 0x06002E42 RID: 11842 RVA: 0x000A6063 File Offset: 0x000A4263
		public override string ToString()
		{
			return this._mode.ToString();
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000A6076 File Offset: 0x000A4276
		// Note: this type is marked as 'beforefieldinit'.
		static RSASignaturePadding()
		{
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000173AD File Offset: 0x000155AD
		internal RSASignaturePadding()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002123 RID: 8483
		private static readonly RSASignaturePadding s_pkcs1 = new RSASignaturePadding(RSASignaturePaddingMode.Pkcs1);

		// Token: 0x04002124 RID: 8484
		private static readonly RSASignaturePadding s_pss = new RSASignaturePadding(RSASignaturePaddingMode.Pss);

		// Token: 0x04002125 RID: 8485
		private readonly RSASignaturePaddingMode _mode;
	}
}
