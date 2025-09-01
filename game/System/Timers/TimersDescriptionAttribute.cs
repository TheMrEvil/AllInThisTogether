using System;
using System.ComponentModel;

namespace System.Timers
{
	/// <summary>Sets the description that visual designers can display when referencing an event, extender, or property.</summary>
	// Token: 0x02000194 RID: 404
	[AttributeUsage(AttributeTargets.All)]
	public class TimersDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.TimersDescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use.</param>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public TimersDescriptionAttribute(string description) : base(description)
		{
		}

		/// <summary>Gets the description that visual designers can display when referencing an event, extender, or property.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002DBEC File Offset: 0x0002BDEC
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x04000720 RID: 1824
		private bool replaced;
	}
}
