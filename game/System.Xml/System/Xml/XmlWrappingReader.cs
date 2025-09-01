using System;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000176 RID: 374
	internal class XmlWrappingReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x000578B2 File Offset: 0x00055AB2
		internal XmlWrappingReader(XmlReader baseReader)
		{
			this.reader = baseReader;
			this.readerAsIXmlLineInfo = (baseReader as IXmlLineInfo);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x000578CD File Offset: 0x00055ACD
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.reader.Settings;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x000578DA File Offset: 0x00055ADA
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000578E7 File Offset: 0x00055AE7
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x000578F4 File Offset: 0x00055AF4
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00057901 File Offset: 0x00055B01
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0005790E File Offset: 0x00055B0E
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0005791B File Offset: 0x00055B1B
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00057928 File Offset: 0x00055B28
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00057935 File Offset: 0x00055B35
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00022616 File Offset: 0x00020816
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00022623 File Offset: 0x00020823
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00057942 File Offset: 0x00055B42
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0005794F File Offset: 0x00055B4F
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0005795C File Offset: 0x00055B5C
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00057969 File Offset: 0x00055B69
		public override Type ValueType
		{
			get
			{
				return this.reader.ValueType;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00057976 File Offset: 0x00055B76
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00057983 File Offset: 0x00055B83
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00057990 File Offset: 0x00055B90
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0005799D File Offset: 0x00055B9D
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00022669 File Offset: 0x00020869
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000579AA File Offset: 0x00055BAA
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000579B7 File Offset: 0x00055BB7
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.reader.SchemaInfo;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000579C4 File Offset: 0x00055BC4
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000579D1 File Offset: 0x00055BD1
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000579DF File Offset: 0x00055BDF
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000579EE File Offset: 0x00055BEE
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000579FC File Offset: 0x00055BFC
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00057A0A File Offset: 0x00055C0A
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00057A19 File Offset: 0x00055C19
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00057A27 File Offset: 0x00055C27
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00057A34 File Offset: 0x00055C34
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00057A41 File Offset: 0x00055C41
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00057A4E File Offset: 0x00055C4E
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00057A5B File Offset: 0x00055C5B
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00057A68 File Offset: 0x00055C68
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00057A75 File Offset: 0x00055C75
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00057A83 File Offset: 0x00055C83
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00057A90 File Offset: 0x00055C90
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00057A9D File Offset: 0x00055C9D
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo != null && this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00057AB4 File Offset: 0x00055CB4
		public virtual int LineNumber
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00057ACB File Offset: 0x00055CCB
		public virtual int LinePosition
		{
			get
			{
				if (this.readerAsIXmlLineInfo != null)
				{
					return this.readerAsIXmlLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00057AE2 File Offset: 0x00055CE2
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.reader.DtdInfo;
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00057AEF File Offset: 0x00055CEF
		public override Task<string> GetValueAsync()
		{
			return this.reader.GetValueAsync();
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00057AFC File Offset: 0x00055CFC
		public override Task<bool> ReadAsync()
		{
			return this.reader.ReadAsync();
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00057B09 File Offset: 0x00055D09
		public override Task SkipAsync()
		{
			return this.reader.SkipAsync();
		}

		// Token: 0x04000EA1 RID: 3745
		protected XmlReader reader;

		// Token: 0x04000EA2 RID: 3746
		protected IXmlLineInfo readerAsIXmlLineInfo;
	}
}
