using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event.</summary>
	// Token: 0x02000405 RID: 1029
	public class PropertyChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property whose value is changing.</param>
		// Token: 0x0600214F RID: 8527 RVA: 0x0007219F File Offset: 0x0007039F
		public PropertyChangingEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		/// <summary>Gets the name of the property whose value is changing.</summary>
		/// <returns>The name of the property whose value is changing.</returns>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000721AE File Offset: 0x000703AE
		public virtual string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x04000FFD RID: 4093
		private readonly string _propertyName;
	}
}
