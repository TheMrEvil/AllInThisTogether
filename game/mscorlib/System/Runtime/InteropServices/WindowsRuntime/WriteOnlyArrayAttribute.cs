using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>When applied to an array parameter in a Windows Runtime component, specifies that the contents of an array that is passed to that parameter are used only for output. The caller does not guarantee that the contents are initialized, and the called method should not read the contents.</summary>
	// Token: 0x02000786 RID: 1926
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class WriteOnlyArrayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.WriteOnlyArrayAttribute" /> class.</summary>
		// Token: 0x06004485 RID: 17541 RVA: 0x00002050 File Offset: 0x00000250
		public WriteOnlyArrayAttribute()
		{
		}
	}
}
