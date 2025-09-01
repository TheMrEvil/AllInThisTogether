using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides a base class from which all objects to be used as evidence must derive.</summary>
	// Token: 0x0200040D RID: 1037
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class EvidenceBase
	{
		/// <summary>Creates a new object that is a complete copy of the current instance.</summary>
		/// <returns>A duplicate copy of this evidence object.</returns>
		// Token: 0x06002A74 RID: 10868 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		public virtual EvidenceBase Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.EvidenceBase" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">An object to be used as evidence is not serializable.</exception>
		// Token: 0x06002A75 RID: 10869 RVA: 0x0000259F File Offset: 0x0000079F
		protected EvidenceBase()
		{
		}
	}
}
