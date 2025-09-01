using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x02000041 RID: 65
	public class EnvironmentData : IEnvironmentData
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000B384 File Offset: 0x00009584
		public static EnvironmentData Inferred
		{
			get
			{
				return new EnvironmentData
				{
					TimeZone = TimeZoneInfo.Local.StandardName,
					OSVersion = (RuntimeInformation.OSDescription ?? Environment.OSVersion.ToString()),
					Platform = (RuntimeInformation.FrameworkDescription ?? ".NET")
				};
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B3D3 File Offset: 0x000095D3
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000B3DB File Offset: 0x000095DB
		public string TimeZone
		{
			[CompilerGenerated]
			get
			{
				return this.<TimeZone>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TimeZone>k__BackingField = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B3E4 File Offset: 0x000095E4
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000B3EC File Offset: 0x000095EC
		public string OSVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<OSVersion>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OSVersion>k__BackingField = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B3F5 File Offset: 0x000095F5
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000B3FD File Offset: 0x000095FD
		public string Platform
		{
			[CompilerGenerated]
			get
			{
				return this.<Platform>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Platform>k__BackingField = value;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B406 File Offset: 0x00009606
		public EnvironmentData()
		{
		}

		// Token: 0x04000092 RID: 146
		[CompilerGenerated]
		private string <TimeZone>k__BackingField;

		// Token: 0x04000093 RID: 147
		[CompilerGenerated]
		private string <OSVersion>k__BackingField;

		// Token: 0x04000094 RID: 148
		[CompilerGenerated]
		private string <Platform>k__BackingField;
	}
}
