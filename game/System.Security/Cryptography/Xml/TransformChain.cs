using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Defines an ordered list of <see cref="T:System.Security.Cryptography.Xml.Transform" /> objects that is applied to unsigned content prior to digest calculation.</summary>
	// Token: 0x0200005C RID: 92
	public class TransformChain
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> class.</summary>
		// Token: 0x060002D5 RID: 725 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public TransformChain()
		{
			this._transforms = new ArrayList();
		}

		/// <summary>Adds a transform to the list of transforms to be applied to the unsigned content prior to digest calculation.</summary>
		/// <param name="transform">The transform to add to the list of transforms.</param>
		// Token: 0x060002D6 RID: 726 RVA: 0x0000D3BB File Offset: 0x0000B5BB
		public void Add(Transform transform)
		{
			if (transform != null)
			{
				this._transforms.Add(transform);
			}
		}

		/// <summary>Returns an enumerator of the transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <returns>An enumerator of the transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		// Token: 0x060002D7 RID: 727 RVA: 0x0000D3CD File Offset: 0x0000B5CD
		public IEnumerator GetEnumerator()
		{
			return this._transforms.GetEnumerator();
		}

		/// <summary>Gets the number of transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <returns>The number of transforms in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000D3DA File Offset: 0x0000B5DA
		public int Count
		{
			get
			{
				return this._transforms.Count;
			}
		}

		/// <summary>Gets the transform at the specified index in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</summary>
		/// <param name="index">The index into the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that specifies which transform to return.</param>
		/// <returns>The transform at the specified index in the <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> parameter is greater than the number of transforms.</exception>
		// Token: 0x170000A3 RID: 163
		public Transform this[int index]
		{
			get
			{
				if (index >= this._transforms.Count)
				{
					throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
				}
				return (Transform)this._transforms[index];
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000D418 File Offset: 0x0000B618
		internal Stream TransformToOctetStream(object inputObject, Type inputType, XmlResolver resolver, string baseUri)
		{
			object obj = inputObject;
			foreach (object obj2 in this._transforms)
			{
				Transform transform = (Transform)obj2;
				if (obj == null || transform.AcceptsType(obj.GetType()))
				{
					transform.Resolver = resolver;
					transform.BaseURI = baseUri;
					transform.LoadInput(obj);
					obj = transform.GetOutput();
				}
				else if (obj is Stream)
				{
					if (!transform.AcceptsType(typeof(XmlDocument)))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					Stream stream = obj as Stream;
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.PreserveWhitespace = true;
					XmlReader reader = Utils.PreProcessStreamInput(stream, resolver, baseUri);
					xmlDocument.Load(reader);
					transform.LoadInput(xmlDocument);
					stream.Close();
					obj = transform.GetOutput();
				}
				else if (obj is XmlNodeList)
				{
					if (!transform.AcceptsType(typeof(Stream)))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					MemoryStream memoryStream = new MemoryStream(new CanonicalXml((XmlNodeList)obj, resolver, false).GetBytes());
					transform.LoadInput(memoryStream);
					obj = transform.GetOutput();
					memoryStream.Close();
				}
				else
				{
					if (!(obj is XmlDocument))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					if (!transform.AcceptsType(typeof(Stream)))
					{
						throw new CryptographicException("The input type was invalid for this transform.");
					}
					MemoryStream memoryStream2 = new MemoryStream(new CanonicalXml((XmlDocument)obj, resolver).GetBytes());
					transform.LoadInput(memoryStream2);
					obj = transform.GetOutput();
					memoryStream2.Close();
				}
			}
			if (obj is Stream)
			{
				return obj as Stream;
			}
			if (obj is XmlNodeList)
			{
				return new MemoryStream(new CanonicalXml((XmlNodeList)obj, resolver, false).GetBytes());
			}
			if (obj is XmlDocument)
			{
				return new MemoryStream(new CanonicalXml((XmlDocument)obj, resolver).GetBytes());
			}
			throw new CryptographicException("The input type was invalid for this transform.");
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000D628 File Offset: 0x0000B828
		internal Stream TransformToOctetStream(Stream input, XmlResolver resolver, string baseUri)
		{
			return this.TransformToOctetStream(input, typeof(Stream), resolver, baseUri);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000D63D File Offset: 0x0000B83D
		internal Stream TransformToOctetStream(XmlDocument document, XmlResolver resolver, string baseUri)
		{
			return this.TransformToOctetStream(document, typeof(XmlDocument), resolver, baseUri);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000D654 File Offset: 0x0000B854
		internal XmlElement GetXml(XmlDocument document, string ns)
		{
			XmlElement xmlElement = document.CreateElement("Transforms", ns);
			foreach (object obj in this._transforms)
			{
				Transform transform = (Transform)obj;
				if (transform != null)
				{
					XmlElement xml = transform.GetXml(document);
					if (xml != null)
					{
						xmlElement.AppendChild(xml);
					}
				}
			}
			return xmlElement;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		internal void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNodeList xmlNodeList = value.SelectNodes("ds:Transform", xmlNamespaceManager);
			if (xmlNodeList.Count == 0)
			{
				throw new CryptographicException("Malformed element {0}.", "Transforms");
			}
			this._transforms.Clear();
			for (int i = 0; i < xmlNodeList.Count; i++)
			{
				XmlElement xmlElement = (XmlElement)xmlNodeList.Item(i);
				Transform transform = CryptoHelpers.CreateFromName<Transform>(Utils.GetAttribute(xmlElement, "Algorithm", "http://www.w3.org/2000/09/xmldsig#"));
				if (transform == null)
				{
					throw new CryptographicException("Unknown transform has been encountered.");
				}
				transform.LoadInnerXml(xmlElement.ChildNodes);
				this._transforms.Add(transform);
			}
		}

		// Token: 0x0400022D RID: 557
		private ArrayList _transforms;
	}
}
