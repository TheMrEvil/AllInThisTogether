using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Enables classes to be activated by the Windows Runtime.</summary>
	// Token: 0x0200078A RID: 1930
	[Guid("00000035-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IActivationFactory
	{
		/// <summary>Returns a new instance of the Windows Runtime class that is created by the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.IActivationFactory" /> interface.</summary>
		/// <returns>The new instance of the Windows Runtime class.</returns>
		// Token: 0x06004499 RID: 17561
		object ActivateInstance();
	}
}
