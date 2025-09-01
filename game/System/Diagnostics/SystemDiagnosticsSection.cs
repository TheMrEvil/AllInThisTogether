using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000229 RID: 553
	internal class SystemDiagnosticsSection : ConfigurationSection
	{
		// Token: 0x06001017 RID: 4119 RVA: 0x00046DF4 File Offset: 0x00044FF4
		static SystemDiagnosticsSection()
		{
			SystemDiagnosticsSection._properties = new ConfigurationPropertyCollection();
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propAssert);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propPerfCounters);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSources);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSharedListeners);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSwitches);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propTrace);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00046F1F File Offset: 0x0004511F
		[ConfigurationProperty("assert")]
		public AssertSection Assert
		{
			get
			{
				return (AssertSection)base[SystemDiagnosticsSection._propAssert];
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00046F31 File Offset: 0x00045131
		[ConfigurationProperty("performanceCounters")]
		public PerfCounterSection PerfCounters
		{
			get
			{
				return (PerfCounterSection)base[SystemDiagnosticsSection._propPerfCounters];
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00046F43 File Offset: 0x00045143
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SystemDiagnosticsSection._properties;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00046F4A File Offset: 0x0004514A
		[ConfigurationProperty("sources")]
		public SourceElementsCollection Sources
		{
			get
			{
				return (SourceElementsCollection)base[SystemDiagnosticsSection._propSources];
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00046F5C File Offset: 0x0004515C
		[ConfigurationProperty("sharedListeners")]
		public ListenerElementsCollection SharedListeners
		{
			get
			{
				return (ListenerElementsCollection)base[SystemDiagnosticsSection._propSharedListeners];
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00046F6E File Offset: 0x0004516E
		[ConfigurationProperty("switches")]
		public SwitchElementsCollection Switches
		{
			get
			{
				return (SwitchElementsCollection)base[SystemDiagnosticsSection._propSwitches];
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00046F80 File Offset: 0x00045180
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base[SystemDiagnosticsSection._propTrace];
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00046F92 File Offset: 0x00045192
		protected override void InitializeDefault()
		{
			this.Trace.Listeners.InitializeDefaultInternal();
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public SystemDiagnosticsSection()
		{
		}

		// Token: 0x040009C9 RID: 2505
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040009CA RID: 2506
		private static readonly ConfigurationProperty _propAssert = new ConfigurationProperty("assert", typeof(AssertSection), new AssertSection(), ConfigurationPropertyOptions.None);

		// Token: 0x040009CB RID: 2507
		private static readonly ConfigurationProperty _propPerfCounters = new ConfigurationProperty("performanceCounters", typeof(PerfCounterSection), new PerfCounterSection(), ConfigurationPropertyOptions.None);

		// Token: 0x040009CC RID: 2508
		private static readonly ConfigurationProperty _propSources = new ConfigurationProperty("sources", typeof(SourceElementsCollection), new SourceElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040009CD RID: 2509
		private static readonly ConfigurationProperty _propSharedListeners = new ConfigurationProperty("sharedListeners", typeof(SharedListenerElementsCollection), new SharedListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040009CE RID: 2510
		private static readonly ConfigurationProperty _propSwitches = new ConfigurationProperty("switches", typeof(SwitchElementsCollection), new SwitchElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040009CF RID: 2511
		private static readonly ConfigurationProperty _propTrace = new ConfigurationProperty("trace", typeof(TraceSection), new TraceSection(), ConfigurationPropertyOptions.None);
	}
}
