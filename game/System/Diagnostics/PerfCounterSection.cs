using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200021D RID: 541
	internal class PerfCounterSection : ConfigurationElement
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x00046064 File Offset: 0x00044264
		static PerfCounterSection()
		{
			PerfCounterSection._properties = new ConfigurationPropertyCollection();
			PerfCounterSection._properties.Add(PerfCounterSection._propFileMappingSize);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x000460A3 File Offset: 0x000442A3
		[ConfigurationProperty("filemappingsize", DefaultValue = 524288)]
		public int FileMappingSize
		{
			get
			{
				return (int)base[PerfCounterSection._propFileMappingSize];
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000460B5 File Offset: 0x000442B5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerfCounterSection._properties;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00031238 File Offset: 0x0002F438
		public PerfCounterSection()
		{
		}

		// Token: 0x040009A1 RID: 2465
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x040009A2 RID: 2466
		private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);
	}
}
