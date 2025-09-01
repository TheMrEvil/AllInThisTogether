using System;

namespace System.Runtime.InteropServices
{
	/// <summary>This attribute has been deprecated.</summary>
	// Token: 0x02000712 RID: 1810
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[Obsolete("This attribute has been deprecated.  Application Domains no longer respect Activation Context boundaries in IDispatch calls.", false)]
	[ComVisible(true)]
	public sealed class SetWin32ContextInIDispatchAttribute : Attribute
	{
		/// <summary>This attribute has been deprecated.</summary>
		// Token: 0x060040C5 RID: 16581 RVA: 0x00002050 File Offset: 0x00000250
		public SetWin32ContextInIDispatchAttribute()
		{
		}
	}
}
