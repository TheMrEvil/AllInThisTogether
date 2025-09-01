using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEditor.Experimental
{
	// Token: 0x02000006 RID: 6
	public class RenderSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002063 File Offset: 0x00000263
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000206A File Offset: 0x0000026A
		[Obsolete("Use UnityEngine.Experimental.GlobalIllumination.useRadianceAmbientProbe instead. (UnityUpgradable) -> UnityEngine.Experimental.GlobalIllumination.RenderSettings.useRadianceAmbientProbe", true)]
		public static bool useRadianceAmbientProbe
		{
			[CompilerGenerated]
			get
			{
				return RenderSettings.<useRadianceAmbientProbe>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				RenderSettings.<useRadianceAmbientProbe>k__BackingField = value;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002072 File Offset: 0x00000272
		public RenderSettings()
		{
		}

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static bool <useRadianceAmbientProbe>k__BackingField;
	}
}
