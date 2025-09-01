using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	// Token: 0x02000164 RID: 356
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbError
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x00003D93 File Offset: 0x00001F93
		internal OleDbError()
		{
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A short description of the error.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string Message
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the database-specific error information.</summary>
		/// <returns>The database-specific error information.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public int NativeError
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the provider that generated the error.</summary>
		/// <returns>The name of the provider that generated the error.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string Source
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the five-character error code following the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string SQLState
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		// Token: 0x0600135A RID: 4954 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string ToString()
		{
			throw ADP.OleDb();
		}
	}
}
