using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.Common.DataAdapter.FillError" /> event of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
	// Token: 0x020000E1 RID: 225
	public class FillErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.FillErrorEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> being updated.</param>
		/// <param name="values">The values for the row being updated.</param>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x00037E23 File Offset: 0x00036023
		public FillErrorEventArgs(DataTable dataTable, object[] values)
		{
			this._dataTable = dataTable;
			this._values = values;
			if (this._values == null)
			{
				this._values = Array.Empty<object>();
			}
		}

		/// <summary>Gets or sets a value indicating whether to continue the fill operation despite the error.</summary>
		/// <returns>
		///   <see langword="true" /> if the fill operation should continue; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00037E4C File Offset: 0x0003604C
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x00037E54 File Offset: 0x00036054
		public bool Continue
		{
			get
			{
				return this._continueFlag;
			}
			set
			{
				this._continueFlag = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> being updated when the error occurred.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> being updated.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00037E5D File Offset: 0x0003605D
		public DataTable DataTable
		{
			get
			{
				return this._dataTable;
			}
		}

		/// <summary>Gets the errors being handled.</summary>
		/// <returns>The errors being handled.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00037E65 File Offset: 0x00036065
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x00037E6D File Offset: 0x0003606D
		public Exception Errors
		{
			get
			{
				return this._errors;
			}
			set
			{
				this._errors = value;
			}
		}

		/// <summary>Gets the values for the row being updated when the error occurred.</summary>
		/// <returns>The values for the row being updated.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00037E78 File Offset: 0x00036078
		public object[] Values
		{
			get
			{
				object[] array = new object[this._values.Length];
				for (int i = 0; i < this._values.Length; i++)
				{
					array[i] = this._values[i];
				}
				return array;
			}
		}

		// Token: 0x0400087D RID: 2173
		private bool _continueFlag;

		// Token: 0x0400087E RID: 2174
		private DataTable _dataTable;

		// Token: 0x0400087F RID: 2175
		private Exception _errors;

		// Token: 0x04000880 RID: 2176
		private object[] _values;
	}
}
