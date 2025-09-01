using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	/// <summary>Allows the event tracing for Windows (ETW) name to be defined independently of the name of the event source class.</summary>
	// Token: 0x020009F9 RID: 2553
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventSourceAttribute : Attribute
	{
		/// <summary>Gets or sets the event source identifier.</summary>
		/// <returns>The event source identifier.</returns>
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x001347DE File Offset: 0x001329DE
		// (set) Token: 0x06005B16 RID: 23318 RVA: 0x001347E6 File Offset: 0x001329E6
		public string Guid
		{
			[CompilerGenerated]
			get
			{
				return this.<Guid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Guid>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the name of the localization resource file.</summary>
		/// <returns>The name of the localization resource file, or <see langword="null" /> if the localization resource file does not exist.</returns>
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06005B17 RID: 23319 RVA: 0x001347EF File Offset: 0x001329EF
		// (set) Token: 0x06005B18 RID: 23320 RVA: 0x001347F7 File Offset: 0x001329F7
		public string LocalizationResources
		{
			[CompilerGenerated]
			get
			{
				return this.<LocalizationResources>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LocalizationResources>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the name of the event source.</summary>
		/// <returns>The name of the event source.</returns>
		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06005B19 RID: 23321 RVA: 0x00134800 File Offset: 0x00132A00
		// (set) Token: 0x06005B1A RID: 23322 RVA: 0x00134808 File Offset: 0x00132A08
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" /> class.</summary>
		// Token: 0x06005B1B RID: 23323 RVA: 0x00002050 File Offset: 0x00000250
		public EventSourceAttribute()
		{
		}

		// Token: 0x04003834 RID: 14388
		[CompilerGenerated]
		private string <Guid>k__BackingField;

		// Token: 0x04003835 RID: 14389
		[CompilerGenerated]
		private string <LocalizationResources>k__BackingField;

		// Token: 0x04003836 RID: 14390
		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
