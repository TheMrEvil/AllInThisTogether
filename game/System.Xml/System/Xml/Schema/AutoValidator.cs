using System;

namespace System.Xml.Schema
{
	// Token: 0x020004E6 RID: 1254
	internal class AutoValidator : BaseValidator
	{
		// Token: 0x06003374 RID: 13172 RVA: 0x001256D1 File Offset: 0x001238D1
		public AutoValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling) : base(reader, schemaCollection, eventHandling)
		{
			this.schemaInfo = new SchemaInfo();
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x001256E8 File Offset: 0x001238E8
		public override void Validate()
		{
			switch (this.DetectValidationType())
			{
			case ValidationType.Auto:
			case ValidationType.DTD:
				break;
			case ValidationType.XDR:
				this.reader.Validator = new XdrValidator(this);
				this.reader.Validator.Validate();
				return;
			case ValidationType.Schema:
				this.reader.Validator = new XsdValidator(this);
				this.reader.Validator.Validate();
				break;
			default:
				return;
			}
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x0000B528 File Offset: 0x00009728
		public override void CompleteValidation()
		{
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override object FindId(string name)
		{
			return null;
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x00125758 File Offset: 0x00123958
		private ValidationType DetectValidationType()
		{
			if (this.reader.Schemas != null && this.reader.Schemas.Count > 0)
			{
				XmlSchemaCollectionEnumerator enumerator = this.reader.Schemas.GetEnumerator();
				while (enumerator.MoveNext())
				{
					SchemaInfo schemaInfo = enumerator.CurrentNode.SchemaInfo;
					if (schemaInfo.SchemaType == SchemaType.XSD)
					{
						return ValidationType.Schema;
					}
					if (schemaInfo.SchemaType == SchemaType.XDR)
					{
						return ValidationType.XDR;
					}
				}
			}
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				SchemaType schemaType = base.SchemaNames.SchemaTypeFromRoot(this.reader.LocalName, this.reader.NamespaceURI);
				if (schemaType == SchemaType.XSD)
				{
					return ValidationType.Schema;
				}
				if (schemaType == SchemaType.XDR)
				{
					return ValidationType.XDR;
				}
				int attributeCount = this.reader.AttributeCount;
				for (int i = 0; i < attributeCount; i++)
				{
					this.reader.MoveToAttribute(i);
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, base.SchemaNames.NsXmlNs))
					{
						if (XdrBuilder.IsXdrSchema(this.reader.Value))
						{
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
					else
					{
						if (Ref.Equal(namespaceURI, base.SchemaNames.NsXsi))
						{
							this.reader.MoveToElement();
							return ValidationType.Schema;
						}
						if (Ref.Equal(namespaceURI, base.SchemaNames.QnDtDt.Namespace) && Ref.Equal(localName, base.SchemaNames.QnDtDt.Name))
						{
							this.reader.SchemaTypeObject = XmlSchemaDatatype.FromXdrName(this.reader.Value);
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
				}
				if (attributeCount > 0)
				{
					this.reader.MoveToElement();
				}
			}
			return ValidationType.Auto;
		}

		// Token: 0x04002685 RID: 9861
		private const string x_schema = "x-schema";
	}
}
