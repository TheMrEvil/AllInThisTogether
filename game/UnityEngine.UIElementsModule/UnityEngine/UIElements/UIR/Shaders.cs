using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000321 RID: 801
	internal static class Shaders
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x0006ECB4 File Offset: 0x0006CEB4
		static Shaders()
		{
			bool isUIEPackageLoaded = UIElementsPackageUtility.IsUIEPackageLoaded;
			if (isUIEPackageLoaded)
			{
				Shaders.k_AtlasBlit = "Hidden/UIE-AtlasBlit";
				Shaders.k_Editor = "Hidden/UIE-Editor";
				Shaders.k_Runtime = "Hidden/UIE-Runtime";
				Shaders.k_RuntimeWorld = "Hidden/UIE-RuntimeWorld";
				Shaders.k_GraphView = "Hidden/UIE-GraphView";
			}
			else
			{
				Shaders.k_AtlasBlit = "Hidden/Internal-UIRAtlasBlitCopy";
				Shaders.k_Editor = "Hidden/UIElements/EditorUIE";
				Shaders.k_Runtime = "Hidden/Internal-UIRDefault";
				Shaders.k_RuntimeWorld = "Hidden/Internal-UIRDefaultWorld";
				Shaders.k_GraphView = "Hidden/GraphView/GraphViewUIE";
			}
		}

		// Token: 0x04000BE7 RID: 3047
		public static readonly string k_AtlasBlit;

		// Token: 0x04000BE8 RID: 3048
		public static readonly string k_Editor;

		// Token: 0x04000BE9 RID: 3049
		public static readonly string k_Runtime;

		// Token: 0x04000BEA RID: 3050
		public static readonly string k_RuntimeWorld;

		// Token: 0x04000BEB RID: 3051
		public static readonly string k_GraphView;
	}
}
