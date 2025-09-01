using System;

namespace System.ComponentModel
{
	/// <summary>Defines the interface for extending properties to other components in a container.</summary>
	// Token: 0x020003B5 RID: 949
	public interface IExtenderProvider
	{
		/// <summary>Specifies whether this object can provide its extender properties to the specified object.</summary>
		/// <param name="extendee">The <see cref="T:System.Object" /> to receive the extender properties.</param>
		/// <returns>
		///   <see langword="true" /> if this object can provide extender properties to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F12 RID: 7954
		bool CanExtend(object extendee);
	}
}
