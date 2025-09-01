using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000234 RID: 564
	internal class TraceSection : ConfigurationElement
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x000495DC File Offset: 0x000477DC
		static TraceSection()
		{
			TraceSection._properties = new ConfigurationPropertyCollection();
			TraceSection._properties.Add(TraceSection._propListeners);
			TraceSection._properties.Add(TraceSection._propAutoFlush);
			TraceSection._properties.Add(TraceSection._propIndentSize);
			TraceSection._properties.Add(TraceSection._propUseGlobalLock);
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000496AE File Offset: 0x000478AE
		[ConfigurationProperty("autoflush", DefaultValue = false)]
		public bool AutoFlush
		{
			get
			{
				return (bool)base[TraceSection._propAutoFlush];
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x000496C0 File Offset: 0x000478C0
		[ConfigurationProperty("indentsize", DefaultValue = 4)]
		public int IndentSize
		{
			get
			{
				return (int)base[TraceSection._propIndentSize];
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000496D2 File Offset: 0x000478D2
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[TraceSection._propListeners];
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x000496E4 File Offset: 0x000478E4
		[ConfigurationProperty("useGlobalLock", DefaultValue = true)]
		public bool UseGlobalLock
		{
			get
			{
				return (bool)base[TraceSection._propUseGlobalLock];
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000496F6 File Offset: 0x000478F6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection._properties;
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00031238 File Offset: 0x0002F438
		public TraceSection()
		{
		}

		// Token: 0x04000A04 RID: 2564
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x04000A05 RID: 2565
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x04000A06 RID: 2566
		private static readonly ConfigurationProperty _propAutoFlush = new ConfigurationProperty("autoflush", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04000A07 RID: 2567
		private static readonly ConfigurationProperty _propIndentSize = new ConfigurationProperty("indentsize", typeof(int), 4, ConfigurationPropertyOptions.None);

		// Token: 0x04000A08 RID: 2568
		private static readonly ConfigurationProperty _propUseGlobalLock = new ConfigurationProperty("useGlobalLock", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
