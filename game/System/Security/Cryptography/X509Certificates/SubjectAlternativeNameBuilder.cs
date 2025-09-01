using System;
using System.Collections.Generic;
using System.Net;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C0 RID: 704
	public sealed class SubjectAlternativeNameBuilder
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x00058009 File Offset: 0x00056209
		public void AddEmailAddress(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentOutOfRangeException("emailAddress", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeEmailAddress(emailAddress));
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0005803A File Offset: 0x0005623A
		public void AddDnsName(string dnsName)
		{
			if (string.IsNullOrEmpty(dnsName))
			{
				throw new ArgumentOutOfRangeException("dnsName", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeDnsName(dnsName));
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0005806B File Offset: 0x0005626B
		public void AddUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUri(uri));
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00058098 File Offset: 0x00056298
		public void AddIpAddress(IPAddress ipAddress)
		{
			if (ipAddress == null)
			{
				throw new ArgumentNullException("ipAddress");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeIpAddress(ipAddress));
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000580BF File Offset: 0x000562BF
		public void AddUserPrincipalName(string upn)
		{
			if (string.IsNullOrEmpty(upn))
			{
				throw new ArgumentOutOfRangeException("upn", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUserPrincipalName(upn));
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000580F0 File Offset: 0x000562F0
		public X509Extension Build(bool critical = false)
		{
			return new X509Extension("2.5.29.17", DerEncoder.ConstructSequence(this._encodedTlvs), critical);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00058108 File Offset: 0x00056308
		public SubjectAlternativeNameBuilder()
		{
		}

		// Token: 0x04000C6A RID: 3178
		private readonly List<byte[][]> _encodedTlvs = new List<byte[][]>();

		// Token: 0x04000C6B RID: 3179
		private readonly GeneralNameEncoder _generalNameEncoder = new GeneralNameEncoder();
	}
}
