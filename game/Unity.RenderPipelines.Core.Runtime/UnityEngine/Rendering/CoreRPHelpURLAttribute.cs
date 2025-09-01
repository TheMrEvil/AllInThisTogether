using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000074 RID: 116
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
	public class CoreRPHelpURLAttribute : HelpURLAttribute
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x000115F1 File Offset: 0x0000F7F1
		public CoreRPHelpURLAttribute(string pageName, string packageName = "com.unity.render-pipelines.core") : base(DocumentationInfo.GetPageLink(packageName, pageName))
		{
		}
	}
}
