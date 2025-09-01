using System;

namespace System.ComponentModel
{
	/// <summary>Creates an instance of a particular type of property from a drop-down box within the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	// Token: 0x020003BF RID: 959
	public abstract class InstanceCreationEditor
	{
		/// <summary>Gets the specified text.</summary>
		/// <returns>The specified text.</returns>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0006CD0C File Offset: 0x0006AF0C
		public virtual string Text
		{
			get
			{
				return "(New...)";
			}
		}

		/// <summary>When overridden in a derived class, returns an instance of the specified type.</summary>
		/// <param name="context">The context information.</param>
		/// <param name="instanceType">The specified type.</param>
		/// <returns>An instance of the specified type or <see langword="null" />.</returns>
		// Token: 0x06001F2A RID: 7978
		public abstract object CreateInstance(ITypeDescriptorContext context, Type instanceType);

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstanceCreationEditor" /> class.</summary>
		// Token: 0x06001F2B RID: 7979 RVA: 0x0000219B File Offset: 0x0000039B
		protected InstanceCreationEditor()
		{
		}
	}
}
