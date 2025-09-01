using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;KeyReference&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000047 RID: 71
	public sealed class KeyReference : EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption.</summary>
		// Token: 0x060001E3 RID: 483 RVA: 0x00008D60 File Offset: 0x00006F60
		public KeyReference()
		{
			base.ReferenceType = "KeyReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption using the supplied Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted key.</param>
		// Token: 0x060001E4 RID: 484 RVA: 0x00008D73 File Offset: 0x00006F73
		public KeyReference(string uri) : base(uri)
		{
			base.ReferenceType = "KeyReference";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyReference" /> class for XML encryption using the specified Uniform Resource Identifier (URI) and a <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) that points to the encrypted key.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to do on the encrypted key.</param>
		// Token: 0x060001E5 RID: 485 RVA: 0x00008D87 File Offset: 0x00006F87
		public KeyReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "KeyReference";
		}
	}
}
