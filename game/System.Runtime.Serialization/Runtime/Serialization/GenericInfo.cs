using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000BF RID: 191
	internal class GenericInfo : IGenericNameProvider
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x0002FFEE File Offset: 0x0002E1EE
		internal GenericInfo(XmlQualifiedName stableName, string genericTypeName)
		{
			this.stableName = stableName;
			this.genericTypeName = genericTypeName;
			this.nestedParamCounts = new List<int>();
			this.nestedParamCounts.Add(0);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0003001B File Offset: 0x0002E21B
		internal void Add(GenericInfo actualParamInfo)
		{
			if (this.paramGenericInfos == null)
			{
				this.paramGenericInfos = new List<GenericInfo>();
			}
			this.paramGenericInfos.Add(actualParamInfo);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0003003C File Offset: 0x0002E23C
		internal void AddToLevel(int level, int count)
		{
			if (level >= this.nestedParamCounts.Count)
			{
				do
				{
					this.nestedParamCounts.Add((level == this.nestedParamCounts.Count) ? count : 0);
				}
				while (level >= this.nestedParamCounts.Count);
				return;
			}
			this.nestedParamCounts[level] = this.nestedParamCounts[level] + count;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0003009D File Offset: 0x0002E29D
		internal XmlQualifiedName GetExpandedStableName()
		{
			if (this.paramGenericInfos == null)
			{
				return this.stableName;
			}
			return new XmlQualifiedName(DataContract.EncodeLocalName(DataContract.ExpandGenericParameters(XmlConvert.DecodeName(this.stableName.Name), this)), this.stableName.Namespace);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000300D9 File Offset: 0x0002E2D9
		internal string GetStableNamespace()
		{
			return this.stableName.Namespace;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x000300E6 File Offset: 0x0002E2E6
		internal XmlQualifiedName StableName
		{
			get
			{
				return this.stableName;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x000300EE File Offset: 0x0002E2EE
		internal IList<GenericInfo> Parameters
		{
			get
			{
				return this.paramGenericInfos;
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000300F6 File Offset: 0x0002E2F6
		public int GetParameterCount()
		{
			return this.paramGenericInfos.Count;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00030103 File Offset: 0x0002E303
		public IList<int> GetNestedParameterCounts()
		{
			return this.nestedParamCounts;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0003010B File Offset: 0x0002E30B
		public string GetParameterName(int paramIndex)
		{
			return this.paramGenericInfos[paramIndex].GetExpandedStableName().Name;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00030124 File Offset: 0x0002E324
		public string GetNamespaces()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.paramGenericInfos.Count; i++)
			{
				stringBuilder.Append(" ").Append(this.paramGenericInfos[i].GetStableNamespace());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00030175 File Offset: 0x0002E375
		public string GetGenericTypeName()
		{
			return this.genericTypeName;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00030180 File Offset: 0x0002E380
		public bool ParametersFromBuiltInNamespaces
		{
			get
			{
				bool flag = true;
				int num = 0;
				while (num < this.paramGenericInfos.Count && flag)
				{
					flag = DataContract.IsBuiltInNamespace(this.paramGenericInfos[num].GetStableNamespace());
					num++;
				}
				return flag;
			}
		}

		// Token: 0x04000487 RID: 1159
		private string genericTypeName;

		// Token: 0x04000488 RID: 1160
		private XmlQualifiedName stableName;

		// Token: 0x04000489 RID: 1161
		private List<GenericInfo> paramGenericInfos;

		// Token: 0x0400048A RID: 1162
		private List<int> nestedParamCounts;
	}
}
