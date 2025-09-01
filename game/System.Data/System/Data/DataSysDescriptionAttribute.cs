using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Marks a property, event, or extender with a description. Visual designers can display this description when referencing the member.</summary>
	// Token: 0x020000CB RID: 203
	[Obsolete("DataSysDescriptionAttribute has been deprecated.  https://go.microsoft.com/fwlink/?linkid=14202", false)]
	[AttributeUsage(AttributeTargets.All)]
	public class DataSysDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSysDescriptionAttribute" /> class using the specified description string.</summary>
		/// <param name="description">The description string.</param>
		// Token: 0x06000C53 RID: 3155 RVA: 0x000327EA File Offset: 0x000309EA
		[Obsolete("DataSysDescriptionAttribute has been deprecated.  https://go.microsoft.com/fwlink/?linkid=14202", false)]
		public DataSysDescriptionAttribute(string description) : base(description)
		{
		}

		/// <summary>Gets the text for the description.</summary>
		/// <returns>The description string.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x000327F3 File Offset: 0x000309F3
		public override string Description
		{
			get
			{
				if (!this._replaced)
				{
					this._replaced = true;
					base.DescriptionValue = base.Description;
				}
				return base.Description;
			}
		}

		// Token: 0x04000800 RID: 2048
		private bool _replaced;
	}
}
