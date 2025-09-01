using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000440 RID: 1088
	public sealed class ComponentChangedEventArgs : EventArgs
	{
		/// <summary>Gets the component that was modified.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the component that was modified.</returns>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000810FE File Offset: 0x0007F2FE
		public object Component
		{
			[CompilerGenerated]
			get
			{
				return this.<Component>k__BackingField;
			}
		}

		/// <summary>Gets the member that has been changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed.</returns>
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x00081106 File Offset: 0x0007F306
		public MemberDescriptor Member
		{
			[CompilerGenerated]
			get
			{
				return this.<Member>k__BackingField;
			}
		}

		/// <summary>Gets the new value of the changed member.</summary>
		/// <returns>The new value of the changed member. This property can be <see langword="null" />.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x0008110E File Offset: 0x0007F30E
		public object NewValue
		{
			[CompilerGenerated]
			get
			{
				return this.<NewValue>k__BackingField;
			}
		}

		/// <summary>Gets the old value of the changed member.</summary>
		/// <returns>The old value of the changed member. This property can be <see langword="null" />.</returns>
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x00081116 File Offset: 0x0007F316
		public object OldValue
		{
			[CompilerGenerated]
			get
			{
				return this.<OldValue>k__BackingField;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> class.</summary>
		/// <param name="component">The component that was changed.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that represents the member that was changed.</param>
		/// <param name="oldValue">The old value of the changed member.</param>
		/// <param name="newValue">The new value of the changed member.</param>
		// Token: 0x06002391 RID: 9105 RVA: 0x0008111E File Offset: 0x0007F31E
		public ComponentChangedEventArgs(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			this.Component = component;
			this.Member = member;
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}

		// Token: 0x040010B0 RID: 4272
		[CompilerGenerated]
		private readonly object <Component>k__BackingField;

		// Token: 0x040010B1 RID: 4273
		[CompilerGenerated]
		private readonly MemberDescriptor <Member>k__BackingField;

		// Token: 0x040010B2 RID: 4274
		[CompilerGenerated]
		private readonly object <NewValue>k__BackingField;

		// Token: 0x040010B3 RID: 4275
		[CompilerGenerated]
		private readonly object <OldValue>k__BackingField;
	}
}
