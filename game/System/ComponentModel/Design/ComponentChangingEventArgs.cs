using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000442 RID: 1090
	public sealed class ComponentChangingEventArgs : EventArgs
	{
		/// <summary>Gets the component that is about to be changed or the component that is the parent container of the member that is about to be changed.</summary>
		/// <returns>The component that is about to have a member changed.</returns>
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x00081143 File Offset: 0x0007F343
		public object Component
		{
			[CompilerGenerated]
			get
			{
				return this.<Component>k__BackingField;
			}
		}

		/// <summary>Gets the member that is about to be changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member that is about to be changed, if known, or <see langword="null" /> otherwise.</returns>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0008114B File Offset: 0x0007F34B
		public MemberDescriptor Member
		{
			[CompilerGenerated]
			get
			{
				return this.<Member>k__BackingField;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> class.</summary>
		/// <param name="component">The component that is about to be changed.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member of the component that is about to be changed.</param>
		// Token: 0x06002398 RID: 9112 RVA: 0x00081153 File Offset: 0x0007F353
		public ComponentChangingEventArgs(object component, MemberDescriptor member)
		{
			this.Component = component;
			this.Member = member;
		}

		// Token: 0x040010B4 RID: 4276
		[CompilerGenerated]
		private readonly object <Component>k__BackingField;

		// Token: 0x040010B5 RID: 4277
		[CompilerGenerated]
		private readonly MemberDescriptor <Member>k__BackingField;
	}
}
