using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for root-level designer view technologies.</summary>
	// Token: 0x02000468 RID: 1128
	public interface IRootDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets the set of technologies that this designer can support for its display.</summary>
		/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002468 RID: 9320
		ViewTechnology[] SupportedTechnologies { get; }

		/// <summary>Gets a view object for the specified view technology.</summary>
		/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
		/// <returns>An object that represents the view for this designer.</returns>
		/// <exception cref="T:System.ArgumentException">The specified view technology is not supported or does not exist.</exception>
		// Token: 0x06002469 RID: 9321
		object GetView(ViewTechnology technology);
	}
}
