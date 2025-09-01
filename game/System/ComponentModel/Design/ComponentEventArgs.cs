using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> events.</summary>
	// Token: 0x02000444 RID: 1092
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>Gets the component associated with the event.</summary>
		/// <returns>The component associated with the event.</returns>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x00081169 File Offset: 0x0007F369
		public virtual IComponent Component
		{
			[CompilerGenerated]
			get
			{
				return this.<Component>k__BackingField;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> class.</summary>
		/// <param name="component">The component that is the source of the event.</param>
		// Token: 0x0600239E RID: 9118 RVA: 0x00081171 File Offset: 0x0007F371
		public ComponentEventArgs(IComponent component)
		{
			this.Component = component;
		}

		// Token: 0x040010B6 RID: 4278
		[CompilerGenerated]
		private readonly IComponent <Component>k__BackingField;
	}
}
