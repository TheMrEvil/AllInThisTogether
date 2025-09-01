using System;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000CD RID: 205
	internal class AsnSerializerInvalidDefaultException : AsnSerializationConstraintException
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x00014D39 File Offset: 0x00012F39
		internal AsnSerializerInvalidDefaultException()
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00014D41 File Offset: 0x00012F41
		internal AsnSerializerInvalidDefaultException(Exception innerException) : base(string.Empty, innerException)
		{
		}
	}
}
