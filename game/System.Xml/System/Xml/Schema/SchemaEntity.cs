using System;

namespace System.Xml.Schema
{
	// Token: 0x02000574 RID: 1396
	internal sealed class SchemaEntity : IDtdEntityInfo
	{
		// Token: 0x060037F9 RID: 14329 RVA: 0x00140128 File Offset: 0x0013E328
		internal SchemaEntity(XmlQualifiedName qname, bool isParameter)
		{
			this.qname = qname;
			this.isParameter = isParameter;
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x00140149 File Offset: 0x0013E349
		string IDtdEntityInfo.Name
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x00140156 File Offset: 0x0013E356
		bool IDtdEntityInfo.IsExternal
		{
			get
			{
				return this.IsExternal;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x0014015E File Offset: 0x0013E35E
		bool IDtdEntityInfo.IsDeclaredInExternal
		{
			get
			{
				return this.DeclaredInExternal;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x00140166 File Offset: 0x0013E366
		bool IDtdEntityInfo.IsUnparsedEntity
		{
			get
			{
				return !this.NData.IsEmpty;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x00140176 File Offset: 0x0013E376
		bool IDtdEntityInfo.IsParameterEntity
		{
			get
			{
				return this.isParameter;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x0014017E File Offset: 0x0013E37E
		string IDtdEntityInfo.BaseUriString
		{
			get
			{
				return this.BaseURI;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x00140186 File Offset: 0x0013E386
		string IDtdEntityInfo.DeclaredUriString
		{
			get
			{
				return this.DeclaredURI;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x0014018E File Offset: 0x0013E38E
		string IDtdEntityInfo.SystemId
		{
			get
			{
				return this.Url;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x00140196 File Offset: 0x0013E396
		string IDtdEntityInfo.PublicId
		{
			get
			{
				return this.Pubid;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x0014019E File Offset: 0x0013E39E
		string IDtdEntityInfo.Text
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06003804 RID: 14340 RVA: 0x001401A6 File Offset: 0x0013E3A6
		int IDtdEntityInfo.LineNumber
		{
			get
			{
				return this.Line;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x001401AE File Offset: 0x0013E3AE
		int IDtdEntityInfo.LinePosition
		{
			get
			{
				return this.Pos;
			}
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x001401B8 File Offset: 0x0013E3B8
		internal static bool IsPredefinedEntity(string n)
		{
			return n == "lt" || n == "gt" || n == "amp" || n == "apos" || n == "quot";
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x00140206 File Offset: 0x0013E406
		internal XmlQualifiedName Name
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x0014020E File Offset: 0x0013E40E
		// (set) Token: 0x06003809 RID: 14345 RVA: 0x00140216 File Offset: 0x0013E416
		internal string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
				this.isExternal = true;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x00140226 File Offset: 0x0013E426
		// (set) Token: 0x0600380B RID: 14347 RVA: 0x0014022E File Offset: 0x0013E42E
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x00140237 File Offset: 0x0013E437
		// (set) Token: 0x0600380D RID: 14349 RVA: 0x0014023F File Offset: 0x0013E43F
		internal bool IsExternal
		{
			get
			{
				return this.isExternal;
			}
			set
			{
				this.isExternal = value;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x00140248 File Offset: 0x0013E448
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x00140250 File Offset: 0x0013E450
		internal bool DeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x00140259 File Offset: 0x0013E459
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x00140261 File Offset: 0x0013E461
		internal XmlQualifiedName NData
		{
			get
			{
				return this.ndata;
			}
			set
			{
				this.ndata = value;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x0014026A File Offset: 0x0013E46A
		// (set) Token: 0x06003813 RID: 14355 RVA: 0x00140272 File Offset: 0x0013E472
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.isExternal = false;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x00140282 File Offset: 0x0013E482
		// (set) Token: 0x06003815 RID: 14357 RVA: 0x0014028A File Offset: 0x0013E48A
		internal int Line
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x00140293 File Offset: 0x0013E493
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x0014029B File Offset: 0x0013E49B
		internal int Pos
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x001402A4 File Offset: 0x0013E4A4
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x001402BA File Offset: 0x0013E4BA
		internal string BaseURI
		{
			get
			{
				if (this.baseURI != null)
				{
					return this.baseURI;
				}
				return string.Empty;
			}
			set
			{
				this.baseURI = value;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x001402C3 File Offset: 0x0013E4C3
		// (set) Token: 0x0600381B RID: 14363 RVA: 0x001402CB File Offset: 0x0013E4CB
		internal bool ParsingInProgress
		{
			get
			{
				return this.parsingInProgress;
			}
			set
			{
				this.parsingInProgress = value;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x001402D4 File Offset: 0x0013E4D4
		// (set) Token: 0x0600381D RID: 14365 RVA: 0x001402EA File Offset: 0x0013E4EA
		internal string DeclaredURI
		{
			get
			{
				if (this.declaredURI != null)
				{
					return this.declaredURI;
				}
				return string.Empty;
			}
			set
			{
				this.declaredURI = value;
			}
		}

		// Token: 0x040028BB RID: 10427
		private XmlQualifiedName qname;

		// Token: 0x040028BC RID: 10428
		private string url;

		// Token: 0x040028BD RID: 10429
		private string pubid;

		// Token: 0x040028BE RID: 10430
		private string text;

		// Token: 0x040028BF RID: 10431
		private XmlQualifiedName ndata = XmlQualifiedName.Empty;

		// Token: 0x040028C0 RID: 10432
		private int lineNumber;

		// Token: 0x040028C1 RID: 10433
		private int linePosition;

		// Token: 0x040028C2 RID: 10434
		private bool isParameter;

		// Token: 0x040028C3 RID: 10435
		private bool isExternal;

		// Token: 0x040028C4 RID: 10436
		private bool parsingInProgress;

		// Token: 0x040028C5 RID: 10437
		private bool isDeclaredInExternal;

		// Token: 0x040028C6 RID: 10438
		private string baseURI;

		// Token: 0x040028C7 RID: 10439
		private string declaredURI;
	}
}
