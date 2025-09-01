using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x0200026D RID: 621
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.MonitoringDescriptionAttribute" /> class, using the specified description.</summary>
		/// <param name="description">The application-defined description text.</param>
		// Token: 0x060013A1 RID: 5025 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public MonitoringDescriptionAttribute(string description) : base(description)
		{
		}

		/// <summary>Gets description text associated with the item monitored.</summary>
		/// <returns>An application-defined description.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00052300 File Offset: 0x00050500
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
