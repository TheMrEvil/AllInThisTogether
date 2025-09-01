using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides methods that enable .NET Framework delegates that handle events to be added and removed from COM objects.</summary>
	// Token: 0x0200073A RID: 1850
	public static class ComEventsHelper
	{
		/// <summary>Adds a delegate to the invocation list of events originating from a COM object.</summary>
		/// <param name="rcw">The COM object that triggers the events the caller would like to respond to.</param>
		/// <param name="iid">The identifier of the source interface used by the COM object to trigger events.</param>
		/// <param name="dispid">The dispatch identifier of the method on the source interface.</param>
		/// <param name="d">The delegate to invoke when the COM event is fired.</param>
		// Token: 0x0600411C RID: 16668 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static void Combine(object rcw, Guid iid, int dispid, Delegate d)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a delegate from the invocation list of events originating from a COM object.</summary>
		/// <param name="rcw">The COM object the delegate is attached to.</param>
		/// <param name="iid">The identifier of the source interface used by the COM object to trigger events.</param>
		/// <param name="dispid">The dispatch identifier of the method on the source interface.</param>
		/// <param name="d">The delegate to remove from the invocation list.</param>
		/// <returns>The delegate that was removed from the invocation list.</returns>
		// Token: 0x0600411D RID: 16669 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static Delegate Remove(object rcw, Guid iid, int dispid, Delegate d)
		{
			throw new NotImplementedException();
		}
	}
}
