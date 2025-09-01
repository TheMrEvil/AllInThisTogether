using System;

namespace System.ComponentModel
{
	/// <summary>Provides the base class for a custom component editor.</summary>
	// Token: 0x02000380 RID: 896
	public abstract class ComponentEditor
	{
		/// <summary>Edits the component and returns a value indicating whether the component was modified.</summary>
		/// <param name="component">The component to be edited.</param>
		/// <returns>
		///   <see langword="true" /> if the component was modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D74 RID: 7540 RVA: 0x00068F9F File Offset: 0x0006719F
		public bool EditComponent(object component)
		{
			return this.EditComponent(null, component);
		}

		/// <summary>Edits the component and returns a value indicating whether the component was modified based upon a given context.</summary>
		/// <param name="context">An optional context object that can be used to obtain further information about the edit.</param>
		/// <param name="component">The component to be edited.</param>
		/// <returns>
		///   <see langword="true" /> if the component was modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D75 RID: 7541
		public abstract bool EditComponent(ITypeDescriptorContext context, object component);

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComponentEditor" /> class.</summary>
		// Token: 0x06001D76 RID: 7542 RVA: 0x0000219B File Offset: 0x0000039B
		protected ComponentEditor()
		{
		}
	}
}
