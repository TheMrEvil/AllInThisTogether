using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="P:System.ComponentModel.Design.IDesignerEventService.ActiveDesigner" /> event.</summary>
	// Token: 0x0200043C RID: 1084
	public class ActiveDesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignerEventArgs" /> class.</summary>
		/// <param name="oldDesigner">The document that is losing activation.</param>
		/// <param name="newDesigner">The document that is gaining activation.</param>
		// Token: 0x0600237A RID: 9082 RVA: 0x00080FBD File Offset: 0x0007F1BD
		public ActiveDesignerEventArgs(IDesignerHost oldDesigner, IDesignerHost newDesigner)
		{
			this.OldDesigner = oldDesigner;
			this.NewDesigner = newDesigner;
		}

		/// <summary>Gets the document that is losing activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document losing activation.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00080FD3 File Offset: 0x0007F1D3
		public IDesignerHost OldDesigner
		{
			[CompilerGenerated]
			get
			{
				return this.<OldDesigner>k__BackingField;
			}
		}

		/// <summary>Gets the document that is gaining activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document gaining activation.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x00080FDB File Offset: 0x0007F1DB
		public IDesignerHost NewDesigner
		{
			[CompilerGenerated]
			get
			{
				return this.<NewDesigner>k__BackingField;
			}
		}

		// Token: 0x040010AA RID: 4266
		[CompilerGenerated]
		private readonly IDesignerHost <OldDesigner>k__BackingField;

		// Token: 0x040010AB RID: 4267
		[CompilerGenerated]
		private readonly IDesignerHost <NewDesigner>k__BackingField;
	}
}
