using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200032B RID: 811
	public class SqlTypesSchemaImporterExtensionHelper : SchemaImporterExtension
	{
		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="name">The name as a string.</param>
		/// <param name="targetNamespace">The target namespace.</param>
		/// <param name="references">String array of references.</param>
		/// <param name="namespaceImports">Array of CodeNamespaceImport objects.</param>
		/// <param name="destinationType">The destination type as a string.</param>
		/// <param name="direct">A Boolean for direct.</param>
		// Token: 0x06002629 RID: 9769 RVA: 0x000AA1D5 File Offset: 0x000A83D5
		public SqlTypesSchemaImporterExtensionHelper(string name, string targetNamespace, string[] references, CodeNamespaceImport[] namespaceImports, string destinationType, bool direct)
		{
			this.Init(name, targetNamespace, references, namespaceImports, destinationType, direct);
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="name">The name as a string.</param>
		/// <param name="destinationType">The destination type as a string.</param>
		// Token: 0x0600262A RID: 9770 RVA: 0x000AA1EC File Offset: 0x000A83EC
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, true);
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="name">The name as a string.</param>
		/// <param name="destinationType">The destination type as a string.</param>
		/// <param name="direct">A Boolean.</param>
		// Token: 0x0600262B RID: 9771 RVA: 0x000AA204 File Offset: 0x000A8404
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType, bool direct)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, direct);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000AA21C File Offset: 0x000A841C
		private void Init(string name, string targetNamespace, string[] references, CodeNamespaceImport[] namespaceImports, string destinationType, bool direct)
		{
			this.m_name = name;
			this.m_targetNamespace = targetNamespace;
			if (references == null)
			{
				this.m_references = new string[1];
				this.m_references[0] = "System.Data.dll";
			}
			else
			{
				this.m_references = references;
			}
			if (namespaceImports == null)
			{
				this.m_namespaceImports = new CodeNamespaceImport[2];
				this.m_namespaceImports[0] = new CodeNamespaceImport("System.Data");
				this.m_namespaceImports[1] = new CodeNamespaceImport("System.Data.SqlTypes");
			}
			else
			{
				this.m_namespaceImports = namespaceImports;
			}
			this.m_destinationType = destinationType;
			this.m_direct = direct;
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="name">
		///   <paramref name="name" />
		/// </param>
		/// <param name="xmlNamespace">
		///   <paramref name="xmlNamespace" />
		/// </param>
		/// <param name="context">
		///   <paramref name="context" />
		/// </param>
		/// <param name="schemas">
		///   <paramref name="schemas" />
		/// </param>
		/// <param name="importer">
		///   <paramref name="importer" />
		/// </param>
		/// <param name="compileUnit">
		///   <paramref name="compileUnit" />
		/// </param>
		/// <param name="mainNamespace">
		///   <paramref name="mainNamespace" />
		/// </param>
		/// <param name="options">
		///   <paramref name="options" />
		/// </param>
		/// <param name="codeProvider">
		///   <paramref name="codeProvider" />
		/// </param>
		/// <returns>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</returns>
		// Token: 0x0600262D RID: 9773 RVA: 0x000AA2AC File Offset: 0x000A84AC
		public override string ImportSchemaType(string name, string xmlNamespace, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			if (this.m_direct && context is XmlSchemaElement && string.CompareOrdinal(this.m_name, name) == 0 && string.CompareOrdinal(this.m_targetNamespace, xmlNamespace) == 0)
			{
				compileUnit.ReferencedAssemblies.AddRange(this.m_references);
				mainNamespace.Imports.AddRange(this.m_namespaceImports);
				return this.m_destinationType;
			}
			return null;
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="type">
		///   <paramref name="type" />
		/// </param>
		/// <param name="context">
		///   <paramref name="context" />
		/// </param>
		/// <param name="schemas">
		///   <paramref name="schemas" />
		/// </param>
		/// <param name="importer">
		///   <paramref name="importer" />
		/// </param>
		/// <param name="compileUnit">
		///   <paramref name="compileUnit" />
		/// </param>
		/// <param name="mainNamespace">
		///   <paramref name="mainNamespace" />
		/// </param>
		/// <param name="options">
		///   <paramref name="options" />
		/// </param>
		/// <param name="codeProvider">
		///   <paramref name="codeProvider" />
		/// </param>
		/// <returns>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</returns>
		// Token: 0x0600262E RID: 9774 RVA: 0x000AA314 File Offset: 0x000A8514
		public override string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			if (!this.m_direct && type is XmlSchemaSimpleType && context is XmlSchemaElement)
			{
				XmlQualifiedName qualifiedName = ((XmlSchemaSimpleType)type).BaseXmlSchemaType.QualifiedName;
				if (string.CompareOrdinal(this.m_name, qualifiedName.Name) == 0 && string.CompareOrdinal(this.m_targetNamespace, qualifiedName.Namespace) == 0)
				{
					compileUnit.ReferencedAssemblies.AddRange(this.m_references);
					mainNamespace.Imports.AddRange(this.m_namespaceImports);
					return this.m_destinationType;
				}
			}
			return null;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000AA39C File Offset: 0x000A859C
		// Note: this type is marked as 'beforefieldinit'.
		static SqlTypesSchemaImporterExtensionHelper()
		{
		}

		// Token: 0x04001922 RID: 6434
		private string m_name;

		// Token: 0x04001923 RID: 6435
		private string m_targetNamespace;

		// Token: 0x04001924 RID: 6436
		private string[] m_references;

		// Token: 0x04001925 RID: 6437
		private CodeNamespaceImport[] m_namespaceImports;

		// Token: 0x04001926 RID: 6438
		private string m_destinationType;

		// Token: 0x04001927 RID: 6439
		private bool m_direct;

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		// Token: 0x04001928 RID: 6440
		protected static readonly string SqlTypesNamespace = "http://schemas.microsoft.com/sqlserver/2004/sqltypes";
	}
}
