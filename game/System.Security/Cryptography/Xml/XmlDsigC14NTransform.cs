using System;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), without comments.</summary>
	// Token: 0x02000060 RID: 96
	public class XmlDsigC14NTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class.</summary>
		// Token: 0x0600032E RID: 814 RVA: 0x0000F678 File Offset: 0x0000D878
		public XmlDsigC14NTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> class with comments, if specified.</summary>
		/// <param name="includeComments">
		///   <see langword="true" /> to include comments; otherwise, <see langword="false" />.</param>
		// Token: 0x0600032F RID: 815 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public XmlDsigC14NTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = (includeComments ? "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments" : "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000F75F File Offset: 0x0000D95F
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object; the <see cref="M:System.Security.Cryptography.Xml.XmlDsigC14NTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object return only objects of one of these types.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000F767 File Offset: 0x0000D967
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element; this method is not supported because this element has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</param>
		// Token: 0x06000332 RID: 818 RVA: 0x0000F76F File Offset: 0x0000D96F
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		// Token: 0x06000333 RID: 819 RVA: 0x0000F43E File Offset: 0x0000D63E
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a <see cref="T:System.IO.Stream" /> object and it is <see langword="null" />.</exception>
		// Token: 0x06000334 RID: 820 RVA: 0x0000F788 File Offset: 0x0000D988
		public override void LoadInput(object obj)
		{
			XmlResolver resolver = base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			if (obj is Stream)
			{
				this._cXml = new CanonicalXml((Stream)obj, this._includeComments, resolver, base.BaseURI);
				return;
			}
			if (obj is XmlDocument)
			{
				this._cXml = new CanonicalXml((XmlDocument)obj, resolver, this._includeComments);
				return;
			}
			if (obj is XmlNodeList)
			{
				this._cXml = new CanonicalXml((XmlNodeList)obj, resolver, this._includeComments);
				return;
			}
			throw new ArgumentException("Type of input object is invalid.", "obj");
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		// Token: 0x06000335 RID: 821 RVA: 0x0000F82E File Offset: 0x0000DA2E
		public override object GetOutput()
		{
			return new MemoryStream(this._cXml.GetBytes());
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.IO.Stream" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object of type <see cref="T:System.IO.Stream" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a <see cref="T:System.IO.Stream" /> object.</exception>
		// Token: 0x06000336 RID: 822 RVA: 0x0000F840 File Offset: 0x0000DA40
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return new MemoryStream(this._cXml.GetBytes());
		}

		/// <summary>Returns the digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</summary>
		/// <param name="hash">The <see cref="T:System.Security.Cryptography.HashAlgorithm" /> object used to create a digest.</param>
		/// <returns>The digest associated with an <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NTransform" /> object.</returns>
		// Token: 0x06000337 RID: 823 RVA: 0x0000F891 File Offset: 0x0000DA91
		public override byte[] GetDigestedOutput(HashAlgorithm hash)
		{
			return this._cXml.GetDigestedBytes(hash);
		}

		// Token: 0x0400023F RID: 575
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x04000240 RID: 576
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x04000241 RID: 577
		private CanonicalXml _cXml;

		// Token: 0x04000242 RID: 578
		private bool _includeComments;
	}
}
