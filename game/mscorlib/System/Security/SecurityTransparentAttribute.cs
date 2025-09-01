using System;

namespace System.Security
{
	/// <summary>Specifies that an assembly cannot cause an elevation of privilege.</summary>
	// Token: 0x020003D6 RID: 982
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityTransparentAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityTransparentAttribute" /> class.</summary>
		// Token: 0x0600286B RID: 10347 RVA: 0x00002050 File Offset: 0x00000250
		public SecurityTransparentAttribute()
		{
		}
	}
}
