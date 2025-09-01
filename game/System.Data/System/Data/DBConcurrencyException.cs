using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	/// <summary>The exception that is thrown by the <see cref="T:System.Data.Common.DataAdapter" /> during an insert, update, or delete operation if the number of rows affected equals zero.</summary>
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public sealed class DBConcurrencyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		// Token: 0x06000AAD RID: 2733 RVA: 0x0002CA0D File Offset: 0x0002AC0D
		public DBConcurrencyException() : this("DB concurrency violation.", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception.</param>
		// Token: 0x06000AAE RID: 2734 RVA: 0x0002CA1B File Offset: 0x0002AC1B
		public DBConcurrencyException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception.</param>
		/// <param name="inner">A reference to an inner exception.</param>
		// Token: 0x06000AAF RID: 2735 RVA: 0x0002CA25 File Offset: 0x0002AC25
		public DBConcurrencyException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232011;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		/// <param name="dataRows">An array containing the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception.</param>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002CA3A File Offset: 0x0002AC3A
		public DBConcurrencyException(string message, Exception inner, DataRow[] dataRows) : base(message, inner)
		{
			base.HResult = -2146232011;
			this._dataRows = dataRows;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00010C10 File Offset: 0x0000EE10
		private DBConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Populates the aprcified serialization information object with the data needed to serialize the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00010C1A File Offset: 0x0000EE1A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Data.DataRow" /> that generated the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <returns>The value of the <see cref="T:System.Data.DataRow" />.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002CA58 File Offset: 0x0002AC58
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0002CA78 File Offset: 0x0002AC78
		public DataRow Row
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null || dataRows.Length == 0)
				{
					return null;
				}
				return dataRows[0];
			}
			set
			{
				this._dataRows = new DataRow[]
				{
					value
				};
			}
		}

		/// <summary>Gets the number of rows whose update failed, generating this exception.</summary>
		/// <returns>An integer containing a count of the number of rows whose update failed.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002CA8C File Offset: 0x0002AC8C
		public int RowCount
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null)
				{
					return 0;
				}
				return dataRows.Length;
			}
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
		public void CopyToRows(DataRow[] array)
		{
			this.CopyToRows(array, 0);
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects, starting at the specified destination array index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		/// <param name="arrayIndex">The destination array index to start copying into.</param>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002CAB4 File Offset: 0x0002ACB4
		public void CopyToRows(DataRow[] array, int arrayIndex)
		{
			DataRow[] dataRows = this._dataRows;
			if (dataRows != null)
			{
				dataRows.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x0400078D RID: 1933
		private DataRow[] _dataRows;
	}
}
