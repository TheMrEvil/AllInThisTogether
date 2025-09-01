using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the COM dispatch identifier (DISPID) of a method, field, or property.</summary>
	// Token: 0x020006E5 RID: 1765
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	[ComVisible(true)]
	public sealed class DispIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="DispIdAttribute" /> class with the specified DISPID.</summary>
		/// <param name="dispId">The DISPID for the member.</param>
		// Token: 0x06004062 RID: 16482 RVA: 0x000E0F3F File Offset: 0x000DF13F
		public DispIdAttribute(int dispId)
		{
			this._val = dispId;
		}

		/// <summary>Gets the DISPID for the member.</summary>
		/// <returns>The DISPID for the member.</returns>
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x000E0F4E File Offset: 0x000DF14E
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A2D RID: 10797
		internal int _val;
	}
}
