using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;

namespace System.Data
{
	/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	// Token: 0x02000157 RID: 343
	public class DataSetSchemaImporterExtension : SchemaImporterExtension
	{
		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">
		///   <paramref name="name" />
		/// </param>
		/// <param name="schemaNamespace">
		///   <paramref name="schemaNamespace" />
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
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06001270 RID: 4720 RVA: 0x0005A188 File Offset: 0x00058388
		public override string ImportSchemaType(string name, string schemaNamespace, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			IList schemas2 = schemas.GetSchemas(schemaNamespace);
			if (schemas2.Count != 1)
			{
				return null;
			}
			XmlSchema xmlSchema = schemas2[0] as XmlSchema;
			if (xmlSchema == null)
			{
				return null;
			}
			XmlSchemaType type = (XmlSchemaType)xmlSchema.SchemaTypes[new XmlQualifiedName(name, schemaNamespace)];
			return this.ImportSchemaType(type, context, schemas, importer, compileUnit, mainNamespace, options, codeProvider);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
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
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06001271 RID: 4721 RVA: 0x0005A1E8 File Offset: 0x000583E8
		public override string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			if (type == null)
			{
				return null;
			}
			if (this.importedTypes[type] != null)
			{
				mainNamespace.Imports.Add(new CodeNamespaceImport(typeof(DataSet).Namespace));
				compileUnit.ReferencedAssemblies.Add("System.Data.dll");
				return (string)this.importedTypes[type];
			}
			if (!(context is XmlSchemaElement))
			{
				return null;
			}
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)context;
			if (type is XmlSchemaComplexType)
			{
				XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)type;
				if (xmlSchemaComplexType.Particle is XmlSchemaSequence)
				{
					XmlSchemaObjectCollection items = ((XmlSchemaSequence)xmlSchemaComplexType.Particle).Items;
					if (2 == items.Count && items[0] is XmlSchemaAny && items[1] is XmlSchemaAny)
					{
						XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)items[0];
						XmlSchemaAny xmlSchemaAny2 = (XmlSchemaAny)items[1];
						if (xmlSchemaAny.Namespace == "http://www.w3.org/2001/XMLSchema" && xmlSchemaAny2.Namespace == "urn:schemas-microsoft-com:xml-diffgram-v1")
						{
							string text = null;
							foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaComplexType.Attributes)
							{
								XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)xmlSchemaObject;
								if (xmlSchemaAttribute.Name == "namespace")
								{
									text = xmlSchemaAttribute.FixedValue.Trim();
									break;
								}
							}
							bool flag;
							if (((XmlSchemaSequence)xmlSchemaComplexType.Particle).MaxOccurs == 79228162514264337593543950335m)
							{
								flag = true;
							}
							else
							{
								if (!(xmlSchemaAny.MaxOccurs == 79228162514264337593543950335m))
								{
									return null;
								}
								flag = false;
							}
							if (text == null)
							{
								string text2 = flag ? typeof(DataSet).FullName : typeof(DataTable).FullName;
								this.importedTypes.Add(type, text2);
								mainNamespace.Imports.Add(new CodeNamespaceImport(typeof(DataSet).Namespace));
								compileUnit.ReferencedAssemblies.Add("System.Data.dll");
								return text2;
							}
							foreach (object obj in schemas.GetSchemas(text))
							{
								XmlSchema xmlSchema = (XmlSchema)obj;
								if (xmlSchema != null && xmlSchema.Id != null)
								{
									XmlSchemaElement xmlSchemaElement2 = this.FindDataSetElement(xmlSchema);
									if (xmlSchemaElement2 != null)
									{
										return this.ImportSchemaType(xmlSchemaElement2.SchemaType, xmlSchemaElement2, schemas, importer, compileUnit, mainNamespace, options, codeProvider);
									}
								}
							}
							return null;
						}
					}
				}
				if (!(xmlSchemaComplexType.Particle is XmlSchemaSequence) && !(xmlSchemaComplexType.Particle is XmlSchemaAll))
				{
					goto IL_45D;
				}
				XmlSchemaObjectCollection items2 = ((XmlSchemaGroupBase)xmlSchemaComplexType.Particle).Items;
				if (items2.Count == 2)
				{
					if (!(items2[0] is XmlSchemaElement) || !(items2[1] is XmlSchemaAny))
					{
						return null;
					}
					XmlSchemaElement xmlSchemaElement3 = (XmlSchemaElement)items2[0];
					if (!(xmlSchemaElement3.RefName.Name == "schema") || !(xmlSchemaElement3.RefName.Namespace == "http://www.w3.org/2001/XMLSchema"))
					{
						return null;
					}
					string fullName = typeof(DataSet).FullName;
					this.importedTypes.Add(type, fullName);
					mainNamespace.Imports.Add(new CodeNamespaceImport(typeof(DataSet).Namespace));
					compileUnit.ReferencedAssemblies.Add("System.Data.dll");
					return fullName;
				}
				else
				{
					if (1 != items2.Count)
					{
						goto IL_45D;
					}
					XmlSchemaAny xmlSchemaAny3 = items2[0] as XmlSchemaAny;
					if (xmlSchemaAny3 != null && xmlSchemaAny3.Namespace != null && xmlSchemaAny3.Namespace.IndexOfAny(new char[]
					{
						'#',
						' '
					}) < 0)
					{
						foreach (object obj2 in schemas.GetSchemas(xmlSchemaAny3.Namespace))
						{
							XmlSchema xmlSchema2 = (XmlSchema)obj2;
							if (xmlSchema2 != null && xmlSchema2.Id != null)
							{
								XmlSchemaElement xmlSchemaElement4 = this.FindDataSetElement(xmlSchema2);
								if (xmlSchemaElement4 != null)
								{
									return this.ImportSchemaType(xmlSchemaElement4.SchemaType, xmlSchemaElement4, schemas, importer, compileUnit, mainNamespace, options, codeProvider);
								}
							}
						}
						goto IL_45D;
					}
					goto IL_45D;
				}
				string result;
				return result;
			}
			IL_45D:
			return null;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0005A680 File Offset: 0x00058880
		internal XmlSchemaElement FindDataSetElement(XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in schema.Items)
			{
				if (xmlSchemaObject is XmlSchemaElement && DataSetSchemaImporterExtension.IsDataSet((XmlSchemaElement)xmlSchemaObject))
				{
					return (XmlSchemaElement)xmlSchemaObject;
				}
			}
			return null;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0005A6F0 File Offset: 0x000588F0
		internal string GenerateTypedDataSet(XmlSchemaElement element, XmlSchemas schemas, CodeNamespace codeNamespace, StringCollection references, CodeDomProvider codeProvider)
		{
			if (element == null)
			{
				return null;
			}
			if (this.importedTypes[element.SchemaType] != null)
			{
				return (string)this.importedTypes[element.SchemaType];
			}
			IList schemas2 = schemas.GetSchemas(element.QualifiedName.Namespace);
			if (schemas2.Count != 1)
			{
				return null;
			}
			XmlSchema xmlSchema = schemas2[0] as XmlSchema;
			if (xmlSchema == null)
			{
				return null;
			}
			DataSet dataSet = new DataSet();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				xmlSchema.Write(memoryStream);
				memoryStream.Position = 0L;
				dataSet.ReadXmlSchema(memoryStream);
			}
			string name = new TypedDataSetGenerator().GenerateCode(dataSet, codeNamespace, codeProvider.CreateGenerator()).Name;
			this.importedTypes.Add(element.SchemaType, name);
			references.Add("System.Data.dll");
			return name;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0005A7D8 File Offset: 0x000589D8
		internal static bool IsDataSet(XmlSchemaElement e)
		{
			if (e.UnhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in e.UnhandledAttributes)
				{
					if (xmlAttribute.LocalName == "IsDataSet" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && (xmlAttribute.Value == "True" || xmlAttribute.Value == "true" || xmlAttribute.Value == "1"))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x06001275 RID: 4725 RVA: 0x0005A863 File Offset: 0x00058A63
		public DataSetSchemaImporterExtension()
		{
		}

		// Token: 0x04000BA1 RID: 2977
		private Hashtable importedTypes = new Hashtable();
	}
}
