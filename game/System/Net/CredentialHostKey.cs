using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005C8 RID: 1480
	internal class CredentialHostKey
	{
		// Token: 0x06002FF3 RID: 12275 RVA: 0x000A579C File Offset: 0x000A399C
		internal CredentialHostKey(string host, int port, string authenticationType)
		{
			this.Host = host;
			this.Port = port;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000A57B9 File Offset: 0x000A39B9
		internal bool Match(string host, int port, string authenticationType)
		{
			return host != null && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, host, StringComparison.OrdinalIgnoreCase) == 0 && port == this.Port;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000A57F4 File Offset: 0x000A39F4
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.Host.ToUpperInvariant().GetHashCode() + this.Port.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000A584C File Offset: 0x000A3A4C
		public override bool Equals(object comparand)
		{
			CredentialHostKey credentialHostKey = comparand as CredentialHostKey;
			return comparand != null && (string.Compare(this.AuthenticationType, credentialHostKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, credentialHostKey.Host, StringComparison.OrdinalIgnoreCase) == 0) && this.Port == credentialHostKey.Port;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000A58A0 File Offset: 0x000A3AA0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.Host.Length.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				this.Host,
				":",
				this.Port.ToString(NumberFormatInfo.InvariantInfo),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04001A6A RID: 6762
		internal string Host;

		// Token: 0x04001A6B RID: 6763
		internal string AuthenticationType;

		// Token: 0x04001A6C RID: 6764
		internal int Port;

		// Token: 0x04001A6D RID: 6765
		private int m_HashCode;

		// Token: 0x04001A6E RID: 6766
		private bool m_ComputedHashCode;
	}
}
