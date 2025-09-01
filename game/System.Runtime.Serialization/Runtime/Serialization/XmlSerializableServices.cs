using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	/// <summary>Contains methods for reading and writing XML.</summary>
	// Token: 0x02000150 RID: 336
	public static class XmlSerializableServices
	{
		/// <summary>Reads a set of XML nodes from the specified reader and returns the result.</summary>
		/// <param name="xmlReader">An <see cref="T:System.Xml.XmlReader" /> used for reading.</param>
		/// <returns>An array of type <see cref="T:System.Xml.XmlNode" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlReader" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">While reading, a <see langword="null" /> node was encountered.</exception>
		// Token: 0x0600118B RID: 4491 RVA: 0x00044DD0 File Offset: 0x00042FD0
		public static XmlNode[] ReadNodes(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlReader");
			}
			XmlDocument xmlDocument = new XmlDocument();
			List<XmlNode> list = new List<XmlNode>();
			if (xmlReader.MoveToFirstAttribute())
			{
				for (;;)
				{
					if (XmlSerializableServices.IsValidAttribute(xmlReader))
					{
						XmlNode xmlNode = xmlDocument.ReadNode(xmlReader);
						if (xmlNode == null)
						{
							break;
						}
						list.Add(xmlNode);
					}
					if (!xmlReader.MoveToNextAttribute())
					{
						goto IL_59;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unexpected end of file.")));
			}
			IL_59:
			xmlReader.MoveToElement();
			if (!xmlReader.IsEmptyElement)
			{
				int depth = xmlReader.Depth;
				xmlReader.Read();
				while (xmlReader.Depth > depth && xmlReader.NodeType != XmlNodeType.EndElement)
				{
					XmlNode xmlNode2 = xmlDocument.ReadNode(xmlReader);
					if (xmlNode2 == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unexpected end of file.")));
					}
					list.Add(xmlNode2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00044E98 File Offset: 0x00043098
		private static bool IsValidAttribute(XmlReader xmlReader)
		{
			return xmlReader.NamespaceURI != "http://schemas.microsoft.com/2003/10/Serialization/" && xmlReader.NamespaceURI != "http://www.w3.org/2001/XMLSchema-instance" && xmlReader.Prefix != "xmlns" && xmlReader.LocalName != "xmlns";
		}

		/// <summary>Writes the supplied nodes using the specified writer.</summary>
		/// <param name="xmlWriter">An <see cref="T:System.Xml.XmlWriter" /> used for writing.</param>
		/// <param name="nodes">An array of type <see cref="T:System.Xml.XmlNode" /> to write.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlWriter" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600118D RID: 4493 RVA: 0x00044EF0 File Offset: 0x000430F0
		public static void WriteNodes(XmlWriter xmlWriter, XmlNode[] nodes)
		{
			if (xmlWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlWriter");
			}
			if (nodes != null)
			{
				for (int i = 0; i < nodes.Length; i++)
				{
					if (nodes[i] != null)
					{
						nodes[i].WriteTo(xmlWriter);
					}
				}
			}
		}

		/// <summary>Generates a default schema type given the specified type name and adds it to the specified schema set.</summary>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to add the generated schema type to.</param>
		/// <param name="typeQName">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type name to assign the schema to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeQName" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600118E RID: 4494 RVA: 0x00044F2A File Offset: 0x0004312A
		public static void AddDefaultSchema(XmlSchemaSet schemas, XmlQualifiedName typeQName)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("schemas");
			}
			if (typeQName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeQName");
			}
			SchemaExporter.AddDefaultXmlType(schemas, typeQName.Name, typeQName.Namespace);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00044F60 File Offset: 0x00043160
		// Note: this type is marked as 'beforefieldinit'.
		static XmlSerializableServices()
		{
		}

		// Token: 0x0400072C RID: 1836
		internal static readonly string ReadNodesMethodName = "ReadNodes";

		// Token: 0x0400072D RID: 1837
		internal static string WriteNodesMethodName = "WriteNodes";

		// Token: 0x0400072E RID: 1838
		internal static string AddDefaultSchemaMethodName = "AddDefaultSchema";
	}
}
