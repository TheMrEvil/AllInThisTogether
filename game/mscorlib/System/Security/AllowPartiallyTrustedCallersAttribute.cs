using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Allows an assembly to be called by partially trusted code. Without this declaration, only fully trusted callers are able to use the assembly. This class cannot be inherited.</summary>
	// Token: 0x020003D0 RID: 976
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> class.</summary>
		// Token: 0x06002863 RID: 10339 RVA: 0x00002050 File Offset: 0x00000250
		public AllowPartiallyTrustedCallersAttribute()
		{
		}

		/// <summary>Gets or sets the default partial trust visibility for code that is marked with the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> (APTCA) attribute.</summary>
		/// <returns>One of the enumeration values. The default is <see cref="F:System.Security.PartialTrustVisibilityLevel.VisibleToAllHosts" />.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x00092A39 File Offset: 0x00090C39
		// (set) Token: 0x06002865 RID: 10341 RVA: 0x00092A41 File Offset: 0x00090C41
		public PartialTrustVisibilityLevel PartialTrustVisibilityLevel
		{
			get
			{
				return this._visibilityLevel;
			}
			set
			{
				this._visibilityLevel = value;
			}
		}

		// Token: 0x04001E9A RID: 7834
		private PartialTrustVisibilityLevel _visibilityLevel;
	}
}
