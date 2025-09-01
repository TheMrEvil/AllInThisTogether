using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000218 RID: 536
	internal static class DiagnosticsConfiguration
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x000453C4 File Offset: 0x000435C4
		internal static SwitchElementsCollection SwitchSettings
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.Switches;
				}
				return null;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000453EC File Offset: 0x000435EC
		internal static bool AssertUIEnabled
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Assert == null || systemDiagnosticsSection.Assert.AssertUIEnabled;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00045420 File Offset: 0x00043620
		internal static string ConfigFilePath
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.ElementInformation.Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00045450 File Offset: 0x00043650
		internal static string LogFileName
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Assert != null)
				{
					return systemDiagnosticsSection.Assert.LogFileName;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00045488 File Offset: 0x00043688
		internal static bool AutoFlush
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null && systemDiagnosticsSection.Trace.AutoFlush;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x000454BC File Offset: 0x000436BC
		internal static bool UseGlobalLock
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Trace == null || systemDiagnosticsSection.Trace.UseGlobalLock;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000454F0 File Offset: 0x000436F0
		internal static int IndentSize
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null)
				{
					return systemDiagnosticsSection.Trace.IndentSize;
				}
				return 4;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00045524 File Offset: 0x00043724
		internal static ListenerElementsCollection SharedListeners
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.SharedListeners;
				}
				return null;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0004554C File Offset: 0x0004374C
		internal static SourceElementsCollection Sources
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Sources != null)
				{
					return systemDiagnosticsSection.Sources;
				}
				return null;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00045579 File Offset: 0x00043779
		internal static SystemDiagnosticsSection SystemDiagnosticsSection
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				return DiagnosticsConfiguration.configSection;
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00045588 File Offset: 0x00043788
		private static SystemDiagnosticsSection GetConfigSection()
		{
			object section = PrivilegedConfigurationManager.GetSection("system.diagnostics");
			if (section is SystemDiagnosticsSection)
			{
				return (SystemDiagnosticsSection)section;
			}
			return null;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000455B0 File Offset: 0x000437B0
		internal static bool IsInitializing()
		{
			return DiagnosticsConfiguration.initState == InitState.Initializing;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000455BC File Offset: 0x000437BC
		internal static bool IsInitialized()
		{
			return DiagnosticsConfiguration.initState == InitState.Initialized;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000455C8 File Offset: 0x000437C8
		internal static bool CanInitialize()
		{
			return DiagnosticsConfiguration.initState != InitState.Initializing && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000455E0 File Offset: 0x000437E0
		internal static void Initialize()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				if (DiagnosticsConfiguration.initState == InitState.NotInitialized && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress)
				{
					DiagnosticsConfiguration.initState = InitState.Initializing;
					try
					{
						DiagnosticsConfiguration.configSection = DiagnosticsConfiguration.GetConfigSection();
					}
					finally
					{
						DiagnosticsConfiguration.initState = InitState.Initialized;
					}
				}
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00045654 File Offset: 0x00043854
		internal static void Refresh()
		{
			ConfigurationManager.RefreshSection("system.diagnostics");
			SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
			if (systemDiagnosticsSection != null)
			{
				if (systemDiagnosticsSection.Switches != null)
				{
					foreach (object obj in systemDiagnosticsSection.Switches)
					{
						((SwitchElement)obj).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.SharedListeners != null)
				{
					foreach (object obj2 in systemDiagnosticsSection.SharedListeners)
					{
						((ListenerElement)obj2).ResetProperties();
					}
				}
				if (systemDiagnosticsSection.Sources != null)
				{
					foreach (object obj3 in systemDiagnosticsSection.Sources)
					{
						((SourceElement)obj3).ResetProperties();
					}
				}
			}
			DiagnosticsConfiguration.configSection = null;
			DiagnosticsConfiguration.initState = InitState.NotInitialized;
			DiagnosticsConfiguration.Initialize();
		}

		// Token: 0x04000998 RID: 2456
		private static volatile SystemDiagnosticsSection configSection;

		// Token: 0x04000999 RID: 2457
		private static volatile InitState initState;
	}
}
