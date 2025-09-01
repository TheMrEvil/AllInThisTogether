using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;DataReference&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000032 RID: 50
	public sealed class DataReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class.</summary>
		// Token: 0x06000109 RID: 265 RVA: 0x000058A0 File Offset: 0x00003AA0
		public DataReference()
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		// Token: 0x0600010A RID: 266 RVA: 0x000058B3 File Offset: 0x00003AB3
		public DataReference(string uri) : base(uri)
		{
			base.ReferenceType = "DataReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataReference" /> class using the specified Uniform Resource Identifier (URI) and a <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted data.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted data.</param>
		// Token: 0x0600010B RID: 267 RVA: 0x000058C7 File Offset: 0x00003AC7
		public DataReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "DataReference";
		}
	}
}
