using System;

namespace System.Data
{
	/// <summary>Provides data for the state change event of a .NET Framework data provider.</summary>
	// Token: 0x02000131 RID: 305
	public sealed class StateChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StateChangeEventArgs" /> class, when given the original state and the current state of the object.</summary>
		/// <param name="originalState">One of the <see cref="T:System.Data.ConnectionState" /> values.</param>
		/// <param name="currentState">One of the <see cref="T:System.Data.ConnectionState" /> values.</param>
		// Token: 0x060010A2 RID: 4258 RVA: 0x00045BDA File Offset: 0x00043DDA
		public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
		{
			this._originalState = originalState;
			this._currentState = currentState;
		}

		/// <summary>Gets the new state of the connection. The connection object will be in the new state already when the event is fired.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00045BF0 File Offset: 0x00043DF0
		public ConnectionState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		/// <summary>Gets the original state of the connection.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00045BF8 File Offset: 0x00043DF8
		public ConnectionState OriginalState
		{
			get
			{
				return this._originalState;
			}
		}

		// Token: 0x04000A42 RID: 2626
		private ConnectionState _originalState;

		// Token: 0x04000A43 RID: 2627
		private ConnectionState _currentState;
	}
}
