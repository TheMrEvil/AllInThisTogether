using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyDataErrorInfo.ErrorsChanged" /> event.</summary>
	// Token: 0x020003FF RID: 1023
	public class DataErrorsChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataErrorsChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that has an error.  <see langword="null" /> or <see cref="F:System.String.Empty" /> if the error is object-level.</param>
		// Token: 0x0600213F RID: 8511 RVA: 0x00072171 File Offset: 0x00070371
		public DataErrorsChangedEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that has an error.</summary>
		/// <returns>The name of the property that has an error. <see langword="null" /> or <see cref="F:System.String.Empty" /> if the error is object-level.</returns>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x00072180 File Offset: 0x00070380
		public virtual string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x04000FFB RID: 4091
		private readonly string _propertyName;
	}
}
