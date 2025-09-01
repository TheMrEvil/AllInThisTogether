using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class CryptoSignedXmlRecursionException : XmlException
	{
		// Token: 0x060000EC RID: 236 RVA: 0x000050A0 File Offset: 0x000032A0
		public CryptoSignedXmlRecursionException()
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000050A8 File Offset: 0x000032A8
		public CryptoSignedXmlRecursionException(string message) : base(message)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000050B1 File Offset: 0x000032B1
		public CryptoSignedXmlRecursionException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000050BB File Offset: 0x000032BB
		protected CryptoSignedXmlRecursionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
