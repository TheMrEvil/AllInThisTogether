using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000211 RID: 529
	internal class AssertSection : ConfigurationElement
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x00044A40 File Offset: 0x00042C40
		static AssertSection()
		{
			AssertSection._properties = new ConfigurationPropertyCollection();
			AssertSection._properties.Add(AssertSection._propAssertUIEnabled);
			AssertSection._properties.Add(AssertSection._propLogFile);
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00044AB4 File Offset: 0x00042CB4
		[ConfigurationProperty("assertuienabled", DefaultValue = true)]
		public bool AssertUIEnabled
		{
			get
			{
				return (bool)base[AssertSection._propAssertUIEnabled];
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00044AC6 File Offset: 0x00042CC6
		[ConfigurationProperty("logfilename", DefaultValue = "")]
		public string LogFileName
		{
			get
			{
				return (string)base[AssertSection._propLogFile];
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00044AD8 File Offset: 0x00042CD8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssertSection._properties;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00031238 File Offset: 0x0002F438
		public AssertSection()
		{
		}

		// Token: 0x0400098C RID: 2444
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x0400098D RID: 2445
		private static readonly ConfigurationProperty _propAssertUIEnabled = new ConfigurationProperty("assertuienabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x0400098E RID: 2446
		private static readonly ConfigurationProperty _propLogFile = new ConfigurationProperty("logfilename", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
