using System;
using System.Xml.Linq;

namespace System.Xml.Schema
{
	/// <summary>This class contains the LINQ to XML extension methods for XSD validation.</summary>
	// Token: 0x0200000E RID: 14
	public static class Extensions
	{
		/// <summary>Gets the post-schema-validation infoset (PSVI) of a validated element.</summary>
		/// <param name="source">An <see cref="T:System.Xml.Linq.XElement" /> that has been previously validated.</param>
		/// <returns>A <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> that contains the post-schema-validation infoset (PSVI) for an <see cref="T:System.Xml.Linq.XElement" />.</returns>
		// Token: 0x0600006B RID: 107 RVA: 0x00003E3E File Offset: 0x0000203E
		public static IXmlSchemaInfo GetSchemaInfo(this XElement source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Annotation<IXmlSchemaInfo>();
		}

		/// <summary>Gets the post-schema-validation infoset (PSVI) of a validated attribute.</summary>
		/// <param name="source">An <see cref="T:System.Xml.Linq.XAttribute" /> that has been previously validated.</param>
		/// <returns>A <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> that contains the post-schema-validation infoset for an <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		// Token: 0x0600006C RID: 108 RVA: 0x00003E3E File Offset: 0x0000203E
		public static IXmlSchemaInfo GetSchemaInfo(this XAttribute source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Annotation<IXmlSchemaInfo>();
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XDocument" /> conforms to an XSD in an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XDocument" /> to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		// Token: 0x0600006D RID: 109 RVA: 0x00003E54 File Offset: 0x00002054
		public static void Validate(this XDocument source, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(schemas, validationEventHandler, false);
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XDocument" /> conforms to an XSD in an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XDocument" /> to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		// Token: 0x0600006E RID: 110 RVA: 0x00003E5F File Offset: 0x0000205F
		public static void Validate(this XDocument source, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, null, addSchemaInfo);
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XElement" /> sub-tree conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XElement" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		// Token: 0x0600006F RID: 111 RVA: 0x00003E8C File Offset: 0x0000208C
		public static void Validate(this XElement source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(partialValidationType, schemas, validationEventHandler, false);
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XElement" /> sub-tree conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XElement" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		// Token: 0x06000070 RID: 112 RVA: 0x00003E98 File Offset: 0x00002098
		public static void Validate(this XElement source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, partialValidationType, addSchemaInfo);
		}

		/// <summary>This method validates that an <see cref="T:System.Xml.Linq.XAttribute" /> conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XAttribute" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		// Token: 0x06000071 RID: 113 RVA: 0x00003ED4 File Offset: 0x000020D4
		public static void Validate(this XAttribute source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			source.Validate(partialValidationType, schemas, validationEventHandler, false);
		}

		/// <summary>Validates that an <see cref="T:System.Xml.Linq.XAttribute" /> conforms to a specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> and an <see cref="T:System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
		/// <param name="source">The <see cref="T:System.Xml.Linq.XAttribute" /> to validate.</param>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaObject" /> that specifies the sub-tree to validate.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to validate against.</param>
		/// <param name="validationEventHandler">A <see cref="T:System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If <see langword="null" />, throws an exception upon validation errors.</param>
		/// <param name="addSchemaInfo">A <see cref="T:System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
		// Token: 0x06000072 RID: 114 RVA: 0x00003E98 File Offset: 0x00002098
		public static void Validate(this XAttribute source, XmlSchemaObject partialValidationType, XmlSchemaSet schemas, ValidationEventHandler validationEventHandler, bool addSchemaInfo)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			new XNodeValidator(schemas, validationEventHandler).Validate(source, partialValidationType, addSchemaInfo);
		}
	}
}
