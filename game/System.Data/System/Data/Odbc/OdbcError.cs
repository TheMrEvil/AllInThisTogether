using System;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	// Token: 0x020002E7 RID: 743
	[Serializable]
	public sealed class OdbcError
	{
		// Token: 0x060020FA RID: 8442 RVA: 0x0009A4D8 File Offset: 0x000986D8
		internal OdbcError(string source, string message, string state, int nativeerror)
		{
			this._source = source;
			this._message = message;
			this._state = state;
			this._nativeerror = nativeerror;
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A description of the error.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x0009A4FD File Offset: 0x000986FD
		public string Message
		{
			get
			{
				if (this._message == null)
				{
					return string.Empty;
				}
				return this._message;
			}
		}

		/// <summary>Gets the five-character error code that follows the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error if the error can be issued from more than one place.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x0009A513 File Offset: 0x00098713
		public string SQLState
		{
			get
			{
				return this._state;
			}
		}

		/// <summary>Gets the data source-specific error information.</summary>
		/// <returns>The data source-specific error information.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x0009A51B File Offset: 0x0009871B
		public int NativeError
		{
			get
			{
				return this._nativeerror;
			}
		}

		/// <summary>Gets the name of the driver that generated the error.</summary>
		/// <returns>The name of the driver that generated the error.</returns>
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0009A523 File Offset: 0x00098723
		public string Source
		{
			get
			{
				if (this._source == null)
				{
					return string.Empty;
				}
				return this._source;
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0009A539 File Offset: 0x00098739
		internal void SetSource(string Source)
		{
			this._source = Source;
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		// Token: 0x06002100 RID: 8448 RVA: 0x0009A542 File Offset: 0x00098742
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal OdbcError()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040017B6 RID: 6070
		internal string _message;

		// Token: 0x040017B7 RID: 6071
		internal string _state;

		// Token: 0x040017B8 RID: 6072
		internal int _nativeerror;

		// Token: 0x040017B9 RID: 6073
		internal string _source;
	}
}
