using System;

namespace System
{
	/// <summary>Represents the base class for classes that contain event data, and provides a value to use for events that do not include event data.</summary>
	// Token: 0x02000115 RID: 277
	[Serializable]
	public class EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EventArgs" /> class.</summary>
		// Token: 0x06000ACF RID: 2767 RVA: 0x0000259F File Offset: 0x0000079F
		public EventArgs()
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002880D File Offset: 0x00026A0D
		// Note: this type is marked as 'beforefieldinit'.
		static EventArgs()
		{
		}

		/// <summary>Provides a value to use with events that do not have event data.</summary>
		// Token: 0x040010E2 RID: 4322
		public static readonly EventArgs Empty = new EventArgs();
	}
}
