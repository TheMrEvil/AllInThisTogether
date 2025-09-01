using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	// Token: 0x02000446 RID: 1094
	public class ComponentRenameEventArgs : EventArgs
	{
		/// <summary>Gets the component that is being renamed.</summary>
		/// <returns>The component that is being renamed.</returns>
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00081180 File Offset: 0x0007F380
		public object Component
		{
			[CompilerGenerated]
			get
			{
				return this.<Component>k__BackingField;
			}
		}

		/// <summary>Gets the name of the component before the rename event.</summary>
		/// <returns>The previous name of the component.</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00081188 File Offset: 0x0007F388
		public virtual string OldName
		{
			[CompilerGenerated]
			get
			{
				return this.<OldName>k__BackingField;
			}
		}

		/// <summary>Gets the name of the component after the rename event.</summary>
		/// <returns>The name of the component after the rename event.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x00081190 File Offset: 0x0007F390
		public virtual string NewName
		{
			[CompilerGenerated]
			get
			{
				return this.<NewName>k__BackingField;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> class.</summary>
		/// <param name="component">The component to be renamed.</param>
		/// <param name="oldName">The old name of the component.</param>
		/// <param name="newName">The new name of the component.</param>
		// Token: 0x060023A6 RID: 9126 RVA: 0x00081198 File Offset: 0x0007F398
		public ComponentRenameEventArgs(object component, string oldName, string newName)
		{
			this.OldName = oldName;
			this.NewName = newName;
			this.Component = component;
		}

		// Token: 0x040010B7 RID: 4279
		[CompilerGenerated]
		private readonly object <Component>k__BackingField;

		// Token: 0x040010B8 RID: 4280
		[CompilerGenerated]
		private readonly string <OldName>k__BackingField;

		// Token: 0x040010B9 RID: 4281
		[CompilerGenerated]
		private readonly string <NewName>k__BackingField;
	}
}
