using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A3 RID: 1187
	internal class StaticDataManager
	{
		// Token: 0x06002E72 RID: 11890 RVA: 0x0010FEA3 File Offset: 0x0010E0A3
		public int DeclareName(string name)
		{
			if (this.uniqueNames == null)
			{
				this.uniqueNames = new UniqueList<string>();
			}
			return this.uniqueNames.Add(name);
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x0010FEC4 File Offset: 0x0010E0C4
		public string[] Names
		{
			get
			{
				if (this.uniqueNames == null)
				{
					return null;
				}
				return this.uniqueNames.ToArray();
			}
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0010FEDB File Offset: 0x0010E0DB
		public int DeclareNameFilter(string locName, string nsUri)
		{
			if (this.uniqueFilters == null)
			{
				this.uniqueFilters = new UniqueList<Int32Pair>();
			}
			return this.uniqueFilters.Add(new Int32Pair(this.DeclareName(locName), this.DeclareName(nsUri)));
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x0010FF0E File Offset: 0x0010E10E
		public Int32Pair[] NameFilters
		{
			get
			{
				if (this.uniqueFilters == null)
				{
					return null;
				}
				return this.uniqueFilters.ToArray();
			}
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0010FF28 File Offset: 0x0010E128
		public int DeclarePrefixMappings(IList<QilNode> list)
		{
			StringPair[] array = new StringPair[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				QilBinary qilBinary = (QilBinary)list[i];
				array[i] = new StringPair((QilLiteral)qilBinary.Left, (QilLiteral)qilBinary.Right);
			}
			if (this.prefixMappingsList == null)
			{
				this.prefixMappingsList = new List<StringPair[]>();
			}
			this.prefixMappingsList.Add(array);
			return this.prefixMappingsList.Count - 1;
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x0010FFB7 File Offset: 0x0010E1B7
		public StringPair[][] PrefixMappingsList
		{
			get
			{
				if (this.prefixMappingsList == null)
				{
					return null;
				}
				return this.prefixMappingsList.ToArray();
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0010FFCE File Offset: 0x0010E1CE
		public int DeclareGlobalValue(string name)
		{
			if (this.globalNames == null)
			{
				this.globalNames = new List<string>();
			}
			int count = this.globalNames.Count;
			this.globalNames.Add(name);
			return count;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x0010FFFA File Offset: 0x0010E1FA
		public string[] GlobalNames
		{
			get
			{
				if (this.globalNames == null)
				{
					return null;
				}
				return this.globalNames.ToArray();
			}
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x00110011 File Offset: 0x0010E211
		public int DeclareEarlyBound(string namespaceUri, Type ebType)
		{
			if (this.earlyInfo == null)
			{
				this.earlyInfo = new UniqueList<EarlyBoundInfo>();
			}
			return this.earlyInfo.Add(new EarlyBoundInfo(namespaceUri, ebType));
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x00110038 File Offset: 0x0010E238
		public EarlyBoundInfo[] EarlyBound
		{
			get
			{
				if (this.earlyInfo != null)
				{
					return this.earlyInfo.ToArray();
				}
				return null;
			}
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x0011004F File Offset: 0x0010E24F
		public int DeclareXmlType(XmlQueryType type)
		{
			if (this.uniqueXmlTypes == null)
			{
				this.uniqueXmlTypes = new UniqueList<XmlQueryType>();
			}
			return this.uniqueXmlTypes.Add(type);
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x00110070 File Offset: 0x0010E270
		public XmlQueryType[] XmlTypes
		{
			get
			{
				if (this.uniqueXmlTypes == null)
				{
					return null;
				}
				return this.uniqueXmlTypes.ToArray();
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x00110087 File Offset: 0x0010E287
		public int DeclareCollation(string collation)
		{
			if (this.uniqueCollations == null)
			{
				this.uniqueCollations = new UniqueList<XmlCollation>();
			}
			return this.uniqueCollations.Add(XmlCollation.Create(collation));
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x001100AD File Offset: 0x0010E2AD
		public XmlCollation[] Collations
		{
			get
			{
				if (this.uniqueCollations == null)
				{
					return null;
				}
				return this.uniqueCollations.ToArray();
			}
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0000216B File Offset: 0x0000036B
		public StaticDataManager()
		{
		}

		// Token: 0x040024CF RID: 9423
		private UniqueList<string> uniqueNames;

		// Token: 0x040024D0 RID: 9424
		private UniqueList<Int32Pair> uniqueFilters;

		// Token: 0x040024D1 RID: 9425
		private List<StringPair[]> prefixMappingsList;

		// Token: 0x040024D2 RID: 9426
		private List<string> globalNames;

		// Token: 0x040024D3 RID: 9427
		private UniqueList<EarlyBoundInfo> earlyInfo;

		// Token: 0x040024D4 RID: 9428
		private UniqueList<XmlQueryType> uniqueXmlTypes;

		// Token: 0x040024D5 RID: 9429
		private UniqueList<XmlCollation> uniqueCollations;
	}
}
