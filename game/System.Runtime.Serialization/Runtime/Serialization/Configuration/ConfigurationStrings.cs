using System;

namespace System.Runtime.Serialization.Configuration
{
	// Token: 0x0200019F RID: 415
	internal static class ConfigurationStrings
	{
		// Token: 0x06001518 RID: 5400 RVA: 0x00053D78 File Offset: 0x00051F78
		private static string GetSectionPath(string sectionName)
		{
			return "system.runtime.serialization" + "/" + sectionName;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00053D8A File Offset: 0x00051F8A
		internal static string DataContractSerializerSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("dataContractSerializer");
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00053D96 File Offset: 0x00051F96
		internal static string NetDataContractSerializerSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("netDataContractSerializer");
			}
		}

		// Token: 0x04000A7A RID: 2682
		internal const string SectionGroupName = "system.runtime.serialization";

		// Token: 0x04000A7B RID: 2683
		internal const string DefaultCollectionName = "";

		// Token: 0x04000A7C RID: 2684
		internal const string DeclaredTypes = "declaredTypes";

		// Token: 0x04000A7D RID: 2685
		internal const string Index = "index";

		// Token: 0x04000A7E RID: 2686
		internal const string Parameter = "parameter";

		// Token: 0x04000A7F RID: 2687
		internal const string Type = "type";

		// Token: 0x04000A80 RID: 2688
		internal const string EnableUnsafeTypeForwarding = "enableUnsafeTypeForwarding";

		// Token: 0x04000A81 RID: 2689
		internal const string DataContractSerializerSectionName = "dataContractSerializer";

		// Token: 0x04000A82 RID: 2690
		internal const string NetDataContractSerializerSectionName = "netDataContractSerializer";
	}
}
