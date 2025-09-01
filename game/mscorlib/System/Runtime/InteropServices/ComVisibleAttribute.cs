using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.</summary>
	// Token: 0x020006EB RID: 1771
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComVisibleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="ComVisibleAttribute" /> class.</summary>
		/// <param name="visibility">
		///   <see langword="true" /> to indicate that the type is visible to COM; otherwise, <see langword="false" />. The default is <see langword="true" />.</param>
		// Token: 0x0600406C RID: 16492 RVA: 0x000E0F9B File Offset: 0x000DF19B
		public ComVisibleAttribute(bool visibility)
		{
			this._val = visibility;
		}

		/// <summary>Gets a value that indicates whether the COM type is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is visible; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x000E0FAA File Offset: 0x000DF1AA
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A3A RID: 10810
		internal bool _val;
	}
}
