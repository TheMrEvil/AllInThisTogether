using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides access to the designer options located on the Tools menu under the Options command in the Visual Studio development environment.</summary>
	// Token: 0x0200045E RID: 1118
	public interface IDesignerOptionService
	{
		/// <summary>Gets the value of the specified Windows Forms Designer option.</summary>
		/// <param name="pageName">The name of the page that defines the option.</param>
		/// <param name="valueName">The name of the option property.</param>
		/// <returns>The value of the specified option.</returns>
		// Token: 0x06002440 RID: 9280
		object GetOptionValue(string pageName, string valueName);

		/// <summary>Sets the value of the specified Windows Forms Designer option.</summary>
		/// <param name="pageName">The name of the page that defines the option.</param>
		/// <param name="valueName">The name of the option property.</param>
		/// <param name="value">The new value.</param>
		// Token: 0x06002441 RID: 9281
		void SetOptionValue(string pageName, string valueName, object value);
	}
}
