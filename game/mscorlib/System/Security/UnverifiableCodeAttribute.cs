using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Marks modules containing unverifiable code. This class cannot be inherited.</summary>
	// Token: 0x020003CF RID: 975
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnverifiableCodeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.UnverifiableCodeAttribute" /> class.</summary>
		// Token: 0x06002862 RID: 10338 RVA: 0x00002050 File Offset: 0x00000250
		public UnverifiableCodeAttribute()
		{
		}
	}
}
