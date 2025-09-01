using System;

namespace System.Xml.Schema
{
	// Token: 0x020004E7 RID: 1255
	internal class BaseProcessor
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x0012590F File Offset: 0x00123B0F
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler) : this(nameTable, schemaNames, eventHandler, new XmlSchemaCompilationSettings())
		{
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x0012591F File Offset: 0x00123B1F
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.eventHandler = eventHandler;
			this.compilationSettings = compilationSettings;
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x00125955 File Offset: 0x00123B55
		protected XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x0012595D File Offset: 0x00123B5D
		protected SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames == null)
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x0012597E File Offset: 0x00123B7E
		protected ValidationEventHandler EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x00125986 File Offset: 0x00123B86
		protected XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x0012598E File Offset: 0x00123B8E
		protected bool HasErrors
		{
			get
			{
				return this.errorCount != 0;
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x0012599C File Offset: 0x00123B9C
		protected void AddToTable(XmlSchemaObjectTable table, XmlQualifiedName qname, XmlSchemaObject item)
		{
			if (qname.Name.Length == 0)
			{
				return;
			}
			XmlSchemaObject xmlSchemaObject = table[qname];
			if (xmlSchemaObject == null)
			{
				table.Add(qname, item);
				return;
			}
			if (xmlSchemaObject == item)
			{
				return;
			}
			string code = "The global element '{0}' has already been declared.";
			if (item is XmlSchemaAttributeGroup)
			{
				if (Ref.Equal(this.nameTable.Add(qname.Namespace), this.NsXml))
				{
					XmlSchemaObject xmlSchemaObject2 = Preprocessor.GetBuildInSchema().AttributeGroups[qname];
					if (xmlSchemaObject == xmlSchemaObject2)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject2)
					{
						return;
					}
				}
				else if (this.IsValidAttributeGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "The attributeGroup '{0}' has already been declared.";
			}
			else if (item is XmlSchemaAttribute)
			{
				if (Ref.Equal(this.nameTable.Add(qname.Namespace), this.NsXml))
				{
					XmlSchemaObject xmlSchemaObject3 = Preprocessor.GetBuildInSchema().Attributes[qname];
					if (xmlSchemaObject == xmlSchemaObject3)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject3)
					{
						return;
					}
				}
				code = "The global attribute '{0}' has already been declared.";
			}
			else if (item is XmlSchemaSimpleType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "The simpleType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaComplexType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "The complexType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaGroup)
			{
				if (this.IsValidGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "The group '{0}' has already been declared.";
			}
			else if (item is XmlSchemaNotation)
			{
				code = "The notation '{0}' has already been declared.";
			}
			else if (item is XmlSchemaIdentityConstraint)
			{
				code = "The identity constraint '{0}' has already been declared.";
			}
			this.SendValidationEvent(code, qname.ToString(), item);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x00125B0C File Offset: 0x00123D0C
		private bool IsValidAttributeGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = item as XmlSchemaAttributeGroup;
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup2 = existingObject as XmlSchemaAttributeGroup;
			if (xmlSchemaAttributeGroup2 == xmlSchemaAttributeGroup.Redefined)
			{
				if (xmlSchemaAttributeGroup2.AttributeUses.Count == 0)
				{
					table.Insert(xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
					return true;
				}
			}
			else if (xmlSchemaAttributeGroup2.Redefined == xmlSchemaAttributeGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x00125B58 File Offset: 0x00123D58
		private bool IsValidGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaGroup xmlSchemaGroup = item as XmlSchemaGroup;
			XmlSchemaGroup xmlSchemaGroup2 = existingObject as XmlSchemaGroup;
			if (xmlSchemaGroup2 == xmlSchemaGroup.Redefined)
			{
				if (xmlSchemaGroup2.CanonicalParticle == null)
				{
					table.Insert(xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
					return true;
				}
			}
			else if (xmlSchemaGroup2.Redefined == xmlSchemaGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x00125BA0 File Offset: 0x00123DA0
		private bool IsValidTypeRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaType xmlSchemaType = item as XmlSchemaType;
			XmlSchemaType xmlSchemaType2 = existingObject as XmlSchemaType;
			if (xmlSchemaType2 == xmlSchemaType.Redefined)
			{
				if (xmlSchemaType2.ElementDecl == null)
				{
					table.Insert(xmlSchemaType.QualifiedName, xmlSchemaType);
					return true;
				}
			}
			else if (xmlSchemaType2.Redefined == xmlSchemaType)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x00125BE7 File Offset: 0x00123DE7
		protected void SendValidationEvent(string code, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), XmlSeverityType.Error);
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x00125BF7 File Offset: 0x00123DF7
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), XmlSeverityType.Error);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x00125C08 File Offset: 0x00123E08
		protected void SendValidationEvent(string code, string msg1, string msg2, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				msg1,
				msg2
			}, source), XmlSeverityType.Error);
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x00125C27 File Offset: 0x00123E27
		protected void SendValidationEvent(string code, string[] args, Exception innerException, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, innerException, source.SourceUri, source.LineNumber, source.LinePosition, source), XmlSeverityType.Error);
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x00125C4F File Offset: 0x00123E4F
		protected void SendValidationEvent(string code, string msg1, string msg2, string sourceUri, int lineNumber, int linePosition)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				msg1,
				msg2
			}, sourceUri, lineNumber, linePosition), XmlSeverityType.Error);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x00125C72 File Offset: 0x00123E72
		protected void SendValidationEvent(string code, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), severity);
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x00125C82 File Offset: 0x00123E82
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x00125C8C File Offset: 0x00123E8C
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), severity);
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x00125C9E File Offset: 0x00123E9E
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x00125CD2 File Offset: 0x00123ED2
		protected void SendValidationEventNoThrow(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
			}
		}

		// Token: 0x04002686 RID: 9862
		private XmlNameTable nameTable;

		// Token: 0x04002687 RID: 9863
		private SchemaNames schemaNames;

		// Token: 0x04002688 RID: 9864
		private ValidationEventHandler eventHandler;

		// Token: 0x04002689 RID: 9865
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x0400268A RID: 9866
		private int errorCount;

		// Token: 0x0400268B RID: 9867
		private string NsXml;
	}
}
