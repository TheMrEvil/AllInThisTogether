using System;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.X509Certificates;
using Internal.Cryptography;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000CA RID: 202
	internal struct X509ExtensionAsn
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x00014C9C File Offset: 0x00012E9C
		public X509ExtensionAsn(X509Extension extension, bool copyValue = true)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			this.ExtnId = extension.Oid.Value;
			this.Critical = extension.Critical;
			this.ExtnValue = (copyValue ? extension.RawData.CloneByteArray() : extension.RawData);
		}

		// Token: 0x04000385 RID: 901
		[ObjectIdentifier]
		internal string ExtnId;

		// Token: 0x04000386 RID: 902
		[DefaultValue(new byte[]
		{
			1,
			1,
			0
		})]
		internal bool Critical;

		// Token: 0x04000387 RID: 903
		[OctetString]
		internal ReadOnlyMemory<byte> ExtnValue;
	}
}
