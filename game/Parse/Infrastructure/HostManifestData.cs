using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x02000042 RID: 66
	public class HostManifestData : IHostManifestData
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000B410 File Offset: 0x00009610
		public static HostManifestData Inferred
		{
			get
			{
				HostManifestData hostManifestData = new HostManifestData();
				hostManifestData.Version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
				AssemblyTitleAttribute customAttribute = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>();
				string name;
				if ((name = ((customAttribute != null) ? customAttribute.Title : null)) == null)
				{
					AssemblyProductAttribute customAttribute2 = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>();
					name = (((customAttribute2 != null) ? customAttribute2.Product : null) ?? Assembly.GetEntryAssembly().GetName().Name);
				}
				hostManifestData.Name = name;
				hostManifestData.ShortVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
				hostManifestData.Identifier = AppDomain.CurrentDomain.FriendlyName;
				return hostManifestData;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000B4AA File Offset: 0x000096AA
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000B4B2 File Offset: 0x000096B2
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000B4BB File Offset: 0x000096BB
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000B4C3 File Offset: 0x000096C3
		public string ShortVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<ShortVersion>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShortVersion>k__BackingField = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000B4CC File Offset: 0x000096CC
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public string Identifier
		{
			[CompilerGenerated]
			get
			{
				return this.<Identifier>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Identifier>k__BackingField = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000B4DD File Offset: 0x000096DD
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000B4E5 File Offset: 0x000096E5
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000B4EE File Offset: 0x000096EE
		public bool IsDefault
		{
			get
			{
				return this.Version == null && this.ShortVersion == null && this.Identifier == null && this.Name == null;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B513 File Offset: 0x00009713
		public bool CanBeUsedForInference
		{
			get
			{
				return !this.IsDefault && !string.IsNullOrWhiteSpace(this.ShortVersion);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B52D File Offset: 0x0000972D
		public HostManifestData()
		{
		}

		// Token: 0x04000095 RID: 149
		[CompilerGenerated]
		private string <Version>k__BackingField;

		// Token: 0x04000096 RID: 150
		[CompilerGenerated]
		private string <ShortVersion>k__BackingField;

		// Token: 0x04000097 RID: 151
		[CompilerGenerated]
		private string <Identifier>k__BackingField;

		// Token: 0x04000098 RID: 152
		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
