using System;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000FD RID: 253
	internal class PrintableStringEncoding : RestrictedAsciiStringEncoding
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00017936 File Offset: 0x00015B36
		internal PrintableStringEncoding() : base("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 '()+,-./:=?")
		{
		}
	}
}
