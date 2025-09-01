using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the arguments for the <see cref="M:System.Diagnostics.Tracing.EventSource.OnEventCommand(System.Diagnostics.Tracing.EventCommandEventArgs)" /> callback.</summary>
	// Token: 0x020009EE RID: 2542
	public class EventCommandEventArgs : EventArgs
	{
		// Token: 0x06005AB4 RID: 23220 RVA: 0x0013443C File Offset: 0x0013263C
		private EventCommandEventArgs()
		{
		}

		/// <summary>Gets the array of arguments for the callback.</summary>
		/// <returns>An array of callback arguments.</returns>
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x000479F8 File Offset: 0x00045BF8
		public IDictionary<string, string> Arguments
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the command for the callback.</summary>
		/// <returns>The callback command.</returns>
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x000479F8 File Offset: 0x00045BF8
		public EventCommand Command
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Disables the event that have the specified identifier.</summary>
		/// <param name="eventId">The identifier of the event to disable.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="eventId" /> is in range; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005AB7 RID: 23223 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool DisableEvent(int eventId)
		{
			return true;
		}

		/// <summary>Enables the event that has the specified identifier.</summary>
		/// <param name="eventId">The identifier of the event to enable.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="eventId" /> is in range; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005AB8 RID: 23224 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool EnableEvent(int eventId)
		{
			return true;
		}
	}
}
