using System;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Gets an object's <see cref="T:System.Security.Policy.Evidence" />.</summary>
	// Token: 0x020003C7 RID: 967
	public interface IEvidenceFactory
	{
		/// <summary>Gets <see cref="T:System.Security.Policy.Evidence" /> that verifies the current object's identity.</summary>
		/// <returns>
		///   <see cref="T:System.Security.Policy.Evidence" /> of the current object's identity.</returns>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002853 RID: 10323
		Evidence Evidence { get; }
	}
}
