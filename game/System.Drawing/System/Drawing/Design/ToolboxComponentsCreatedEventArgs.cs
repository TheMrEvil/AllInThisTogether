using System;
using System.ComponentModel;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event that occurs when components are added to the toolbox.</summary>
	// Token: 0x02000129 RID: 297
	public class ToolboxComponentsCreatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> class.</summary>
		/// <param name="components">The components to include in the toolbox.</param>
		// Token: 0x06000DAB RID: 3499 RVA: 0x0001F083 File Offset: 0x0001D283
		public ToolboxComponentsCreatedEventArgs(IComponent[] components)
		{
			this.comps = components;
		}

		/// <summary>Gets or sets an array containing the components to add to the toolbox.</summary>
		/// <returns>An array of type <see cref="T:System.ComponentModel.IComponent" /> indicating the components to add to the toolbox.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0001F092 File Offset: 0x0001D292
		public IComponent[] Components
		{
			get
			{
				return (IComponent[])this.comps.Clone();
			}
		}

		// Token: 0x04000AA2 RID: 2722
		private readonly IComponent[] comps;
	}
}
