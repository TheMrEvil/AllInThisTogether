using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides the basic framework for building a custom designer.</summary>
	// Token: 0x02000459 RID: 1113
	public interface IDesigner : IDisposable
	{
		/// <summary>Gets the base component that this designer is designing.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> indicating the base component that this designer is designing.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600240F RID: 9231
		IComponent Component { get; }

		/// <summary>Gets a collection of the design-time verbs supported by the designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the verbs supported by the designer, or <see langword="null" /> if the component has no verbs.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002410 RID: 9232
		DesignerVerbCollection Verbs { get; }

		/// <summary>Performs the default action for this designer.</summary>
		// Token: 0x06002411 RID: 9233
		void DoDefaultAction();

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The component to associate with this designer.</param>
		// Token: 0x06002412 RID: 9234
		void Initialize(IComponent component);
	}
}
