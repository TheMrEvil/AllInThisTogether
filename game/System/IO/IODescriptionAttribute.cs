using System;
using System.ComponentModel;

namespace System.IO
{
	/// <summary>Sets the description visual designers can display when referencing an event, extender, or property.</summary>
	// Token: 0x02000517 RID: 1303
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IODescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use.</param>
		// Token: 0x06002A4A RID: 10826 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public IODescriptionAttribute(string description) : base(description)
		{
		}

		/// <summary>Gets the description.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x0006801F File Offset: 0x0006621F
		public override string Description
		{
			get
			{
				return base.DescriptionValue;
			}
		}
	}
}
