using System;

namespace System
{
	/// <summary>Specifies the parts of a <see cref="T:System.Uri" />.</summary>
	// Token: 0x02000157 RID: 343
	[Flags]
	public enum UriComponents
	{
		/// <summary>The <see cref="P:System.Uri.Scheme" /> data.</summary>
		// Token: 0x04000612 RID: 1554
		Scheme = 1,
		/// <summary>The <see cref="P:System.Uri.UserInfo" /> data.</summary>
		// Token: 0x04000613 RID: 1555
		UserInfo = 2,
		/// <summary>The <see cref="P:System.Uri.Host" /> data.</summary>
		// Token: 0x04000614 RID: 1556
		Host = 4,
		/// <summary>The <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x04000615 RID: 1557
		Port = 8,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> data.</summary>
		// Token: 0x04000616 RID: 1558
		Path = 16,
		/// <summary>The <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x04000617 RID: 1559
		Query = 32,
		/// <summary>The <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x04000618 RID: 1560
		Fragment = 64,
		/// <summary>The <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000619 RID: 1561
		StrongPort = 128,
		/// <summary>The normalized form of the <see cref="P:System.Uri.Host" />.</summary>
		// Token: 0x0400061A RID: 1562
		NormalizedHost = 256,
		/// <summary>Specifies that the delimiter should be included.</summary>
		// Token: 0x0400061B RID: 1563
		KeepDelimiter = 1073741824,
		/// <summary>The complete <see cref="T:System.Uri" /> context that is needed for Uri Serializers. The context includes the IPv6 scope.</summary>
		// Token: 0x0400061C RID: 1564
		SerializationInfoString = -2147483648,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, <see cref="P:System.Uri.Query" />, and <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x0400061D RID: 1565
		AbsoluteUri = 127,
		/// <summary>The <see cref="P:System.Uri.Host" /> and <see cref="P:System.Uri.Port" /> data. If no port data is in the Uri and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x0400061E RID: 1566
		HostAndPort = 132,
		/// <summary>The <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x0400061F RID: 1567
		StrongAuthority = 134,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x04000620 RID: 1568
		SchemeAndServer = 13,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, and <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x04000621 RID: 1569
		HttpRequestUrl = 61,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> and <see cref="P:System.Uri.Query" /> data. Also see <see cref="P:System.Uri.PathAndQuery" />.</summary>
		// Token: 0x04000622 RID: 1570
		PathAndQuery = 48
	}
}
