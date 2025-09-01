using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerCreated" /> and <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerDisposed" /> events.</summary>
	// Token: 0x02000451 RID: 1105
	public class DesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerEventArgs" /> class.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</param>
		// Token: 0x060023EE RID: 9198 RVA: 0x0008180E File Offset: 0x0007FA0E
		public DesignerEventArgs(IDesignerHost host)
		{
			this.Designer = host;
		}

		/// <summary>Gets the host of the document.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x0008181D File Offset: 0x0007FA1D
		public IDesignerHost Designer
		{
			[CompilerGenerated]
			get
			{
				return this.<Designer>k__BackingField;
			}
		}

		// Token: 0x040010C5 RID: 4293
		[CompilerGenerated]
		private readonly IDesignerHost <Designer>k__BackingField;
	}
}
