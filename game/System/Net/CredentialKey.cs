using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005C9 RID: 1481
	internal class CredentialKey
	{
		// Token: 0x06002FF8 RID: 12280 RVA: 0x000A591D File Offset: 0x000A3B1D
		internal CredentialKey(Uri uriPrefix, string authenticationType)
		{
			this.UriPrefix = uriPrefix;
			this.UriPrefixLength = this.UriPrefix.ToString().Length;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000A5950 File Offset: 0x000A3B50
		internal bool Match(Uri uri, string authenticationType)
		{
			return !(uri == null) && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.IsPrefix(uri, this.UriPrefix);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000A5980 File Offset: 0x000A3B80
		internal bool IsPrefix(Uri uri, Uri prefixUri)
		{
			if (prefixUri.Scheme != uri.Scheme || prefixUri.Host != uri.Host || prefixUri.Port != uri.Port)
			{
				return false;
			}
			int num = prefixUri.AbsolutePath.LastIndexOf('/');
			return num <= uri.AbsolutePath.LastIndexOf('/') && string.Compare(uri.AbsolutePath, 0, prefixUri.AbsolutePath, 0, num, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000A59FB File Offset: 0x000A3BFB
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.UriPrefixLength + this.UriPrefix.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000A5A3C File Offset: 0x000A3C3C
		public override bool Equals(object comparand)
		{
			CredentialKey credentialKey = comparand as CredentialKey;
			return comparand != null && string.Compare(this.AuthenticationType, credentialKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.UriPrefix.Equals(credentialKey.UriPrefix);
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000A5A7C File Offset: 0x000A3C7C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.UriPrefixLength.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				ValidationHelper.ToString(this.UriPrefix),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04001A6F RID: 6767
		internal Uri UriPrefix;

		// Token: 0x04001A70 RID: 6768
		internal int UriPrefixLength = -1;

		// Token: 0x04001A71 RID: 6769
		internal string AuthenticationType;

		// Token: 0x04001A72 RID: 6770
		private int m_HashCode;

		// Token: 0x04001A73 RID: 6771
		private bool m_ComputedHashCode;
	}
}
