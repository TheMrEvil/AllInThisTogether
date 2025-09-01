using System;

namespace System.Drawing.Design
{
	/// <summary>Defines an interface for setting the currently selected toolbox item and indicating whether a designer supports a particular toolbox item.</summary>
	// Token: 0x02000124 RID: 292
	public interface IToolboxUser
	{
		/// <summary>Gets a value indicating whether the specified tool is supported by the current designer.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to be tested for toolbox support.</param>
		/// <returns>
		///   <see langword="true" /> if the tool is supported by the toolbox and can be enabled; <see langword="false" /> if the document designer does not know how to use the tool.</returns>
		// Token: 0x06000D97 RID: 3479
		bool GetToolSupported(ToolboxItem tool);

		/// <summary>Selects the specified tool.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
		// Token: 0x06000D98 RID: 3480
		void ToolPicked(ToolboxItem tool);
	}
}
