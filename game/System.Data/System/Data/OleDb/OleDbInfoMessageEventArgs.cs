using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event. This class cannot be inherited.</summary>
	// Token: 0x02000168 RID: 360
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbInfoMessageEventArgs : EventArgs
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x0005ADEE File Offset: 0x00058FEE
		internal OleDbInfoMessageEventArgs()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the HRESULT following the ANSI SQL standard for the database.</summary>
		/// <returns>The HRESULT, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public int ErrorCode
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the collection of warnings sent from the data source.</summary>
		/// <returns>The collection of warnings sent from the data source.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbErrorCollection Errors
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the full text of the error sent from the data source.</summary>
		/// <returns>The full text of the error.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string Message
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string Source
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</returns>
		// Token: 0x06001374 RID: 4980 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string ToString()
		{
			throw ADP.OleDb();
		}
	}
}
