using System;
using System.ComponentModel.Design;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event that occurs when components are added to the toolbox.</summary>
	// Token: 0x0200012B RID: 299
	public class ToolboxComponentsCreatingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> class.</summary>
		/// <param name="host">The designer host that is making the request.</param>
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		public ToolboxComponentsCreatingEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		/// <summary>Gets or sets an instance of the <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that made the request to create toolbox components, or <see langword="null" /> if no designer host was provided to the toolbox item.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0001F0B3 File Offset: 0x0001D2B3
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x04000AA3 RID: 2723
		private readonly IDesignerHost host;
	}
}
