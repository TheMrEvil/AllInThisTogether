using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event.</summary>
	// Token: 0x020003E7 RID: 999
	public class RefreshEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the component that has changed.</summary>
		/// <param name="componentChanged">The component that changed.</param>
		// Token: 0x060020C9 RID: 8393 RVA: 0x00071730 File Offset: 0x0006F930
		public RefreshEventArgs(object componentChanged)
		{
			this.ComponentChanged = componentChanged;
			this.TypeChanged = componentChanged.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the type of component that has changed.</summary>
		/// <param name="typeChanged">The <see cref="T:System.Type" /> that changed.</param>
		// Token: 0x060020CA RID: 8394 RVA: 0x0007174B File Offset: 0x0006F94B
		public RefreshEventArgs(Type typeChanged)
		{
			this.TypeChanged = typeChanged;
		}

		/// <summary>Gets the component that changed its properties, events, or extenders.</summary>
		/// <returns>The component that changed its properties, events, or extenders, or <see langword="null" /> if all components of the same type have changed.</returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0007175A File Offset: 0x0006F95A
		public object ComponentChanged
		{
			[CompilerGenerated]
			get
			{
				return this.<ComponentChanged>k__BackingField;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that changed its properties or events.</summary>
		/// <returns>The <see cref="T:System.Type" /> that changed its properties or events.</returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x00071762 File Offset: 0x0006F962
		public Type TypeChanged
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeChanged>k__BackingField;
			}
		}

		// Token: 0x04000FE0 RID: 4064
		[CompilerGenerated]
		private readonly object <ComponentChanged>k__BackingField;

		// Token: 0x04000FE1 RID: 4065
		[CompilerGenerated]
		private readonly Type <TypeChanged>k__BackingField;
	}
}
