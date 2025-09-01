using System;
using System.Runtime.CompilerServices;

namespace Parse.Platform.Files
{
	// Token: 0x02000036 RID: 54
	public class FileState
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000A8DA File Offset: 0x00008ADA
		private static string SecureHyperTextTransferScheme
		{
			[CompilerGenerated]
			get
			{
				return FileState.<SecureHyperTextTransferScheme>k__BackingField;
			}
		} = "https";

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000A8E9 File Offset: 0x00008AE9
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000A8F2 File Offset: 0x00008AF2
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000A8FA File Offset: 0x00008AFA
		public string MediaType
		{
			[CompilerGenerated]
			get
			{
				return this.<MediaType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MediaType>k__BackingField = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000A903 File Offset: 0x00008B03
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000A90B File Offset: 0x00008B0B
		public Uri Location
		{
			[CompilerGenerated]
			get
			{
				return this.<Location>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Location>k__BackingField = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A914 File Offset: 0x00008B14
		public Uri SecureLocation
		{
			get
			{
				Uri location = this.Location;
				if (location != null)
				{
					string host = location.Host;
					if (host != null && host == "files.parsetfss.com")
					{
						return new UriBuilder(location)
						{
							Scheme = FileState.SecureHyperTextTransferScheme,
							Port = -1
						}.Uri;
					}
				}
				return this.Location;
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A96A File Offset: 0x00008B6A
		public FileState()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A972 File Offset: 0x00008B72
		// Note: this type is marked as 'beforefieldinit'.
		static FileState()
		{
		}

		// Token: 0x04000077 RID: 119
		[CompilerGenerated]
		private static readonly string <SecureHyperTextTransferScheme>k__BackingField;

		// Token: 0x04000078 RID: 120
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000079 RID: 121
		[CompilerGenerated]
		private string <MediaType>k__BackingField;

		// Token: 0x0400007A RID: 122
		[CompilerGenerated]
		private Uri <Location>k__BackingField;
	}
}
