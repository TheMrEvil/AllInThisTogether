using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	/// <summary>Allows the transformation of a set of XML schema files (.xsd) into common language runtime (CLR) types.</summary>
	// Token: 0x02000154 RID: 340
	public class XsdDataContractImporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.XsdDataContractImporter" /> class.</summary>
		// Token: 0x0600121F RID: 4639 RVA: 0x0000222F File Offset: 0x0000042F
		public XsdDataContractImporter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.XsdDataContractImporter" /> class with the <see cref="T:System.CodeDom.CodeCompileUnit" /> that will be used to generate CLR code.</summary>
		/// <param name="codeCompileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> that will be used to store the code.</param>
		// Token: 0x06001220 RID: 4640 RVA: 0x00046790 File Offset: 0x00044990
		public XsdDataContractImporter(CodeCompileUnit codeCompileUnit)
		{
			this.codeCompileUnit = codeCompileUnit;
		}

		/// <summary>Gets or sets an <see cref="T:System.Runtime.Serialization.ImportOptions" /> that contains settable options for the import operation.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.ImportOptions" /> that contains settable options.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0004679F File Offset: 0x0004499F
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x000467A7 File Offset: 0x000449A7
		public ImportOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeCompileUnit" /> used for storing the CLR types generated.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> used to store the CLR types generated.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000467B0 File Offset: 0x000449B0
		public CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this.GetCodeCompileUnit();
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000467B8 File Offset: 0x000449B8
		private CodeCompileUnit GetCodeCompileUnit()
		{
			if (this.codeCompileUnit == null)
			{
				this.codeCompileUnit = new CodeCompileUnit();
			}
			return this.codeCompileUnit;
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x000467D4 File Offset: 0x000449D4
		private DataContractSet DataContractSet
		{
			get
			{
				if (this.dataContractSet == null)
				{
					this.dataContractSet = ((this.Options == null) ? new DataContractSet(null, null, null) : new DataContractSet(this.Options.DataContractSurrogate, this.Options.ReferencedTypes, this.Options.ReferencedCollectionTypes));
				}
				return this.dataContractSet;
			}
		}

		/// <summary>Transforms the specified set of XML schemas contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations to generate CLR types for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001226 RID: 4646 RVA: 0x0004682D File Offset: 0x00044A2D
		public void Import(XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			this.InternalImport(schemas, null, null, null);
		}

		/// <summary>Transforms the specified set of schema types contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into CLR types generated into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations.</param>
		/// <param name="typeNames">A <see cref="T:System.Collections.Generic.ICollection`1" /> (of <see cref="T:System.Xml.XmlQualifiedName" />) that represents the set of schema types to import.</param>
		// Token: 0x06001227 RID: 4647 RVA: 0x0004684C File Offset: 0x00044A4C
		public void Import(XmlSchemaSet schemas, ICollection<XmlQualifiedName> typeNames)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			if (typeNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typeNames"));
			}
			this.InternalImport(schemas, typeNames, XsdDataContractImporter.emptyElementArray, XsdDataContractImporter.emptyTypeNameArray);
		}

		/// <summary>Transforms the specified XML schema type contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations.</param>
		/// <param name="typeName">A <see cref="T:System.Xml.XmlQualifiedName" /> that represents a specific schema type to import.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001228 RID: 4648 RVA: 0x00046888 File Offset: 0x00044A88
		public void Import(XmlSchemaSet schemas, XmlQualifiedName typeName)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			if (typeName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typeName"));
			}
			this.SingleTypeNameArray[0] = typeName;
			this.InternalImport(schemas, this.SingleTypeNameArray, XsdDataContractImporter.emptyElementArray, XsdDataContractImporter.emptyTypeNameArray);
		}

		/// <summary>Transforms the specified schema element in the set of specified XML schemas into a <see cref="T:System.CodeDom.CodeCompileUnit" /> and returns an <see cref="T:System.Xml.XmlQualifiedName" /> that represents the data contract name for the specified element.</summary>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas to transform.</param>
		/// <param name="element">An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that represents the specific schema element to transform.</param>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the specified element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="element" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001229 RID: 4649 RVA: 0x000468E4 File Offset: 0x00044AE4
		public XmlQualifiedName Import(XmlSchemaSet schemas, XmlSchemaElement element)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("element"));
			}
			this.SingleTypeNameArray[0] = null;
			this.SingleElementArray[0] = element;
			this.InternalImport(schemas, XsdDataContractImporter.emptyTypeNameArray, this.SingleElementArray, this.SingleTypeNameArray);
			return this.SingleTypeNameArray[0];
		}

		/// <summary>Gets a value that indicates whether the schemas contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be transformed into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas to transform.</param>
		/// <returns>
		///   <see langword="true" /> if the schemas can be transformed to data contract types; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		// Token: 0x0600122A RID: 4650 RVA: 0x00046949 File Offset: 0x00044B49
		public bool CanImport(XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			return this.InternalCanImport(schemas, null, null, null);
		}

		/// <summary>Gets a value that indicates whether the specified set of types contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be transformed into CLR types generated into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schemas to transform.</param>
		/// <param name="typeNames">An <see cref="T:System.Collections.Generic.ICollection`1" /> of <see cref="T:System.Xml.XmlQualifiedName" /> that represents the set of schema types to import.</param>
		/// <returns>
		///   <see langword="true" /> if the schemas can be transformed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeNames" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		// Token: 0x0600122B RID: 4651 RVA: 0x00046968 File Offset: 0x00044B68
		public bool CanImport(XmlSchemaSet schemas, ICollection<XmlQualifiedName> typeNames)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			if (typeNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typeNames"));
			}
			return this.InternalCanImport(schemas, typeNames, XsdDataContractImporter.emptyElementArray, XsdDataContractImporter.emptyTypeNameArray);
		}

		/// <summary>Gets a value that indicates whether the schemas contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be transformed into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <param name="schemas">A <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that contains the schema representations.</param>
		/// <param name="typeName">An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the names of the schema types that need to be imported from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <returns>
		///   <see langword="true" /> if the schemas can be transformed to data contract types; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		// Token: 0x0600122C RID: 4652 RVA: 0x000469A4 File Offset: 0x00044BA4
		public bool CanImport(XmlSchemaSet schemas, XmlQualifiedName typeName)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			if (typeName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typeName"));
			}
			return this.InternalCanImport(schemas, new XmlQualifiedName[]
			{
				typeName
			}, XsdDataContractImporter.emptyElementArray, XsdDataContractImporter.emptyTypeNameArray);
		}

		/// <summary>Gets a value that indicates whether a specific schema element contained in an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> can be imported.</summary>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to import.</param>
		/// <param name="element">A specific <see cref="T:System.Xml.Schema.XmlSchemaElement" /> to check in the set of schemas.</param>
		/// <returns>
		///   <see langword="true" /> if the element can be imported; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="element" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">A data contract involved in the import is invalid.</exception>
		// Token: 0x0600122D RID: 4653 RVA: 0x000469F8 File Offset: 0x00044BF8
		public bool CanImport(XmlSchemaSet schemas, XmlSchemaElement element)
		{
			if (schemas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("schemas"));
			}
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("element"));
			}
			this.SingleTypeNameArray[0] = null;
			this.SingleElementArray[0] = element;
			return this.InternalCanImport(schemas, XsdDataContractImporter.emptyTypeNameArray, this.SingleElementArray, this.SingleTypeNameArray);
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.CodeTypeReference" /> to the CLR type generated for the schema type with the specified <see cref="T:System.Xml.XmlQualifiedName" />.</summary>
		/// <param name="typeName">The <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the schema type to look up.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> reference to the CLR type generated for the schema type with the <paramref name="typeName" /> specified.</returns>
		// Token: 0x0600122E RID: 4654 RVA: 0x00046A58 File Offset: 0x00044C58
		public CodeTypeReference GetCodeTypeReference(XmlQualifiedName typeName)
		{
			DataContract dataContract = this.FindDataContract(typeName);
			return new CodeExporter(this.DataContractSet, this.Options, this.GetCodeCompileUnit()).GetCodeTypeReference(dataContract);
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.CodeTypeReference" /> for the specified XML qualified element and schema element.</summary>
		/// <param name="typeName">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the XML qualified name of the schema type to look up.</param>
		/// <param name="element">An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that specifies an element in an XML schema.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that represents the type that was generated for the specified schema type.</returns>
		// Token: 0x0600122F RID: 4655 RVA: 0x00046A8C File Offset: 0x00044C8C
		public CodeTypeReference GetCodeTypeReference(XmlQualifiedName typeName, XmlSchemaElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("element"));
			}
			if (typeName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typeName"));
			}
			DataContract dataContract = this.FindDataContract(typeName);
			return new CodeExporter(this.DataContractSet, this.Options, this.GetCodeCompileUnit()).GetElementTypeReference(dataContract, element.IsNillable);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00046AF0 File Offset: 0x00044CF0
		internal DataContract FindDataContract(XmlQualifiedName typeName)
		{
			if (typeName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typeName"));
			}
			DataContract dataContract = DataContract.GetBuiltInDataContract(typeName.Name, typeName.Namespace);
			if (dataContract == null)
			{
				dataContract = this.DataContractSet[typeName];
				if (dataContract == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Type '{0}' in '{1}' namespace has not been imported.", new object[]
					{
						typeName.Name,
						typeName.Namespace
					})));
				}
			}
			return dataContract;
		}

		/// <summary>Returns a list of <see cref="T:System.CodeDom.CodeTypeReference" /> objects that represents the known types generated when generating code for the specified schema type.</summary>
		/// <param name="typeName">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the schema type to look up known types for.</param>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> of type <see cref="T:System.CodeDom.CodeTypeReference" />.</returns>
		// Token: 0x06001231 RID: 4657 RVA: 0x00046B6C File Offset: 0x00044D6C
		public ICollection<CodeTypeReference> GetKnownTypeReferences(XmlQualifiedName typeName)
		{
			if (typeName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typeName"));
			}
			DataContract dataContract = DataContract.GetBuiltInDataContract(typeName.Name, typeName.Namespace);
			if (dataContract == null)
			{
				dataContract = this.DataContractSet[typeName];
				if (dataContract == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Type '{0}' in '{1}' namespace has not been imported.", new object[]
					{
						typeName.Name,
						typeName.Namespace
					})));
				}
			}
			return new CodeExporter(this.DataContractSet, this.Options, this.GetCodeCompileUnit()).GetKnownTypeReferences(dataContract);
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00046C01 File Offset: 0x00044E01
		private XmlQualifiedName[] SingleTypeNameArray
		{
			get
			{
				if (this.singleTypeNameArray == null)
				{
					this.singleTypeNameArray = new XmlQualifiedName[1];
				}
				return this.singleTypeNameArray;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00046C1D File Offset: 0x00044E1D
		private XmlSchemaElement[] SingleElementArray
		{
			get
			{
				if (this.singleElementArray == null)
				{
					this.singleElementArray = new XmlSchemaElement[1];
				}
				return this.singleElementArray;
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00046C3C File Offset: 0x00044E3C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void InternalImport(XmlSchemaSet schemas, ICollection<XmlQualifiedName> typeNames, ICollection<XmlSchemaElement> elements, XmlQualifiedName[] elementTypeNames)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.Trace(TraceEventType.Information, 196618, SR.GetString("XSD import begins"));
			}
			DataContractSet dataContractSet = (this.dataContractSet == null) ? null : new DataContractSet(this.dataContractSet);
			try
			{
				new SchemaImporter(schemas, typeNames, elements, elementTypeNames, this.DataContractSet, this.ImportXmlDataType).Import();
				new CodeExporter(this.DataContractSet, this.Options, this.GetCodeCompileUnit()).Export();
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.dataContractSet = dataContractSet;
				this.TraceImportError(exception);
				throw;
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.Trace(TraceEventType.Information, 196619, SR.GetString("XSD import ends"));
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00046CFC File Offset: 0x00044EFC
		private bool ImportXmlDataType
		{
			get
			{
				return this.Options != null && this.Options.ImportXmlType;
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00046D13 File Offset: 0x00044F13
		private void TraceImportError(Exception exception)
		{
			if (DiagnosticUtility.ShouldTraceError)
			{
				TraceUtility.Trace(TraceEventType.Error, 196621, SR.GetString("XSD import error"), null, exception);
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00046D34 File Offset: 0x00044F34
		private bool InternalCanImport(XmlSchemaSet schemas, ICollection<XmlQualifiedName> typeNames, ICollection<XmlSchemaElement> elements, XmlQualifiedName[] elementTypeNames)
		{
			DataContractSet dataContractSet = (this.dataContractSet == null) ? null : new DataContractSet(this.dataContractSet);
			bool result;
			try
			{
				new SchemaImporter(schemas, typeNames, elements, elementTypeNames, this.DataContractSet, this.ImportXmlDataType).Import();
				result = true;
			}
			catch (InvalidDataContractException)
			{
				this.dataContractSet = dataContractSet;
				result = false;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.dataContractSet = dataContractSet;
				this.TraceImportError(exception);
				throw;
			}
			return result;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00046DBC File Offset: 0x00044FBC
		// Note: this type is marked as 'beforefieldinit'.
		static XsdDataContractImporter()
		{
		}

		// Token: 0x0400073B RID: 1851
		private ImportOptions options;

		// Token: 0x0400073C RID: 1852
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x0400073D RID: 1853
		private DataContractSet dataContractSet;

		// Token: 0x0400073E RID: 1854
		private static readonly XmlQualifiedName[] emptyTypeNameArray = new XmlQualifiedName[0];

		// Token: 0x0400073F RID: 1855
		private static readonly XmlSchemaElement[] emptyElementArray = new XmlSchemaElement[0];

		// Token: 0x04000740 RID: 1856
		private XmlQualifiedName[] singleTypeNameArray;

		// Token: 0x04000741 RID: 1857
		private XmlSchemaElement[] singleElementArray;
	}
}
