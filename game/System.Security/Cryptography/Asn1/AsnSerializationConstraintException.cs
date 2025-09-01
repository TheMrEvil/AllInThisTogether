using System;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000CB RID: 203
	internal class AsnSerializationConstraintException : CryptographicException
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x00014CF5 File Offset: 0x00012EF5
		public AsnSerializationConstraintException()
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00014CFD File Offset: 0x00012EFD
		public AsnSerializationConstraintException(string message) : base(message)
		{
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00014D06 File Offset: 0x00012F06
		public AsnSerializationConstraintException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
