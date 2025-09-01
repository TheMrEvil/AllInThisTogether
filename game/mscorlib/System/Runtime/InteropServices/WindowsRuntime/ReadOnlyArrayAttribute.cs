using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>When applied to an array parameter in a Windows Runtime component, specifies that the contents of the array that is passed to that parameter are used only for input. The caller expects the array to be unchanged by the call.</summary>
	// Token: 0x02000785 RID: 1925
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ReadOnlyArrayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReadOnlyArrayAttribute" /> class.</summary>
		// Token: 0x06004484 RID: 17540 RVA: 0x00002050 File Offset: 0x00000250
		public ReadOnlyArrayAttribute()
		{
		}
	}
}
