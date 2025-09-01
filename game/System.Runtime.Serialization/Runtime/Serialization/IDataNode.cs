using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000D6 RID: 214
	internal interface IDataNode
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C42 RID: 3138
		Type DataType { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C43 RID: 3139
		// (set) Token: 0x06000C44 RID: 3140
		object Value { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C45 RID: 3141
		// (set) Token: 0x06000C46 RID: 3142
		string DataContractName { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C47 RID: 3143
		// (set) Token: 0x06000C48 RID: 3144
		string DataContractNamespace { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000C49 RID: 3145
		// (set) Token: 0x06000C4A RID: 3146
		string ClrTypeName { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000C4B RID: 3147
		// (set) Token: 0x06000C4C RID: 3148
		string ClrAssemblyName { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000C4D RID: 3149
		// (set) Token: 0x06000C4E RID: 3150
		string Id { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000C4F RID: 3151
		bool PreservesReferences { get; }

		// Token: 0x06000C50 RID: 3152
		void GetData(ElementData element);

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000C51 RID: 3153
		// (set) Token: 0x06000C52 RID: 3154
		bool IsFinalValue { get; set; }

		// Token: 0x06000C53 RID: 3155
		void Clear();
	}
}
