using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Applies metadata to an assembly that indicates that a type is an unmanaged type.  This class cannot be inherited.</summary>
	// Token: 0x02000846 RID: 2118
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Struct, Inherited = true)]
	[Serializable]
	public sealed class NativeCppClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.NativeCppClassAttribute" /> class.</summary>
		// Token: 0x060046C4 RID: 18116 RVA: 0x00002050 File Offset: 0x00000250
		public NativeCppClassAttribute()
		{
		}
	}
}
