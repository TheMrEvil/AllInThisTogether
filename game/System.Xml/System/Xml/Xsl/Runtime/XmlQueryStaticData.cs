using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl.IlGen;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200047F RID: 1151
	internal class XmlQueryStaticData
	{
		// Token: 0x06002D16 RID: 11542 RVA: 0x00108BFC File Offset: 0x00106DFC
		public XmlQueryStaticData(XmlWriterSettings defaultWriterSettings, IList<WhitespaceRule> whitespaceRules, StaticDataManager staticData)
		{
			this.defaultWriterSettings = defaultWriterSettings;
			this.whitespaceRules = whitespaceRules;
			this.names = staticData.Names;
			this.prefixMappingsList = staticData.PrefixMappingsList;
			this.filters = staticData.NameFilters;
			this.types = staticData.XmlTypes;
			this.collations = staticData.Collations;
			this.globalNames = staticData.GlobalNames;
			this.earlyBound = staticData.EarlyBound;
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x00108C74 File Offset: 0x00106E74
		public XmlQueryStaticData(byte[] data, Type[] ebTypes)
		{
			XmlQueryDataReader xmlQueryDataReader = new XmlQueryDataReader(new MemoryStream(data, false));
			if ((xmlQueryDataReader.ReadInt32Encoded() & -256) > 0)
			{
				throw new NotSupportedException();
			}
			this.defaultWriterSettings = new XmlWriterSettings(xmlQueryDataReader);
			int num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.whitespaceRules = new WhitespaceRule[num];
				for (int i = 0; i < num; i++)
				{
					this.whitespaceRules[i] = new WhitespaceRule(xmlQueryDataReader);
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.names = new string[num];
				for (int j = 0; j < num; j++)
				{
					this.names[j] = xmlQueryDataReader.ReadString();
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.prefixMappingsList = new StringPair[num][];
				for (int k = 0; k < num; k++)
				{
					int num2 = xmlQueryDataReader.ReadInt32();
					this.prefixMappingsList[k] = new StringPair[num2];
					for (int l = 0; l < num2; l++)
					{
						this.prefixMappingsList[k][l] = new StringPair(xmlQueryDataReader.ReadString(), xmlQueryDataReader.ReadString());
					}
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.filters = new Int32Pair[num];
				for (int m = 0; m < num; m++)
				{
					this.filters[m] = new Int32Pair(xmlQueryDataReader.ReadInt32Encoded(), xmlQueryDataReader.ReadInt32Encoded());
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.types = new XmlQueryType[num];
				for (int n = 0; n < num; n++)
				{
					this.types[n] = XmlQueryTypeFactory.Deserialize(xmlQueryDataReader);
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.collations = new XmlCollation[num];
				for (int num3 = 0; num3 < num; num3++)
				{
					this.collations[num3] = new XmlCollation(xmlQueryDataReader);
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.globalNames = new string[num];
				for (int num4 = 0; num4 < num; num4++)
				{
					this.globalNames[num4] = xmlQueryDataReader.ReadString();
				}
			}
			num = xmlQueryDataReader.ReadInt32();
			if (num != 0)
			{
				this.earlyBound = new EarlyBoundInfo[num];
				for (int num5 = 0; num5 < num; num5++)
				{
					this.earlyBound[num5] = new EarlyBoundInfo(xmlQueryDataReader.ReadString(), ebTypes[num5]);
				}
			}
			xmlQueryDataReader.Close();
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x00108EB0 File Offset: 0x001070B0
		public void GetObjectData(out byte[] data, out Type[] ebTypes)
		{
			MemoryStream memoryStream = new MemoryStream(4096);
			XmlQueryDataWriter xmlQueryDataWriter = new XmlQueryDataWriter(memoryStream);
			xmlQueryDataWriter.WriteInt32Encoded(0);
			this.defaultWriterSettings.GetObjectData(xmlQueryDataWriter);
			if (this.whitespaceRules == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.whitespaceRules.Count);
				foreach (WhitespaceRule whitespaceRule in this.whitespaceRules)
				{
					whitespaceRule.GetObjectData(xmlQueryDataWriter);
				}
			}
			if (this.names == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.names.Length);
				foreach (string value in this.names)
				{
					xmlQueryDataWriter.Write(value);
				}
			}
			if (this.prefixMappingsList == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.prefixMappingsList.Length);
				foreach (StringPair[] array3 in this.prefixMappingsList)
				{
					xmlQueryDataWriter.Write(array3.Length);
					foreach (StringPair stringPair in array3)
					{
						xmlQueryDataWriter.Write(stringPair.Left);
						xmlQueryDataWriter.Write(stringPair.Right);
					}
				}
			}
			if (this.filters == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.filters.Length);
				foreach (Int32Pair int32Pair in this.filters)
				{
					xmlQueryDataWriter.WriteInt32Encoded(int32Pair.Left);
					xmlQueryDataWriter.WriteInt32Encoded(int32Pair.Right);
				}
			}
			if (this.types == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.types.Length);
				foreach (XmlQueryType type in this.types)
				{
					XmlQueryTypeFactory.Serialize(xmlQueryDataWriter, type);
				}
			}
			if (this.collations == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.collations.Length);
				XmlCollation[] array7 = this.collations;
				for (int i = 0; i < array7.Length; i++)
				{
					array7[i].GetObjectData(xmlQueryDataWriter);
				}
			}
			if (this.globalNames == null)
			{
				xmlQueryDataWriter.Write(0);
			}
			else
			{
				xmlQueryDataWriter.Write(this.globalNames.Length);
				foreach (string value2 in this.globalNames)
				{
					xmlQueryDataWriter.Write(value2);
				}
			}
			if (this.earlyBound == null)
			{
				xmlQueryDataWriter.Write(0);
				ebTypes = null;
			}
			else
			{
				xmlQueryDataWriter.Write(this.earlyBound.Length);
				ebTypes = new Type[this.earlyBound.Length];
				int num = 0;
				foreach (EarlyBoundInfo earlyBoundInfo in this.earlyBound)
				{
					xmlQueryDataWriter.Write(earlyBoundInfo.NamespaceUri);
					ebTypes[num++] = earlyBoundInfo.EarlyBoundType;
				}
			}
			xmlQueryDataWriter.Close();
			data = memoryStream.ToArray();
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x001091C4 File Offset: 0x001073C4
		public XmlWriterSettings DefaultWriterSettings
		{
			get
			{
				return this.defaultWriterSettings;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x001091CC File Offset: 0x001073CC
		public IList<WhitespaceRule> WhitespaceRules
		{
			get
			{
				return this.whitespaceRules;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x001091D4 File Offset: 0x001073D4
		public string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x001091DC File Offset: 0x001073DC
		public StringPair[][] PrefixMappingsList
		{
			get
			{
				return this.prefixMappingsList;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x001091E4 File Offset: 0x001073E4
		public Int32Pair[] Filters
		{
			get
			{
				return this.filters;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x001091EC File Offset: 0x001073EC
		public XmlQueryType[] Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x001091F4 File Offset: 0x001073F4
		public XmlCollation[] Collations
		{
			get
			{
				return this.collations;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x001091FC File Offset: 0x001073FC
		public string[] GlobalNames
		{
			get
			{
				return this.globalNames;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x00109204 File Offset: 0x00107404
		public EarlyBoundInfo[] EarlyBound
		{
			get
			{
				return this.earlyBound;
			}
		}

		// Token: 0x04002314 RID: 8980
		public const string DataFieldName = "staticData";

		// Token: 0x04002315 RID: 8981
		public const string TypesFieldName = "ebTypes";

		// Token: 0x04002316 RID: 8982
		private const int CurrentFormatVersion = 0;

		// Token: 0x04002317 RID: 8983
		private XmlWriterSettings defaultWriterSettings;

		// Token: 0x04002318 RID: 8984
		private IList<WhitespaceRule> whitespaceRules;

		// Token: 0x04002319 RID: 8985
		private string[] names;

		// Token: 0x0400231A RID: 8986
		private StringPair[][] prefixMappingsList;

		// Token: 0x0400231B RID: 8987
		private Int32Pair[] filters;

		// Token: 0x0400231C RID: 8988
		private XmlQueryType[] types;

		// Token: 0x0400231D RID: 8989
		private XmlCollation[] collations;

		// Token: 0x0400231E RID: 8990
		private string[] globalNames;

		// Token: 0x0400231F RID: 8991
		private EarlyBoundInfo[] earlyBound;
	}
}
