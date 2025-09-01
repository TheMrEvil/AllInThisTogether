using System;

namespace System.ComponentModel
{
	/// <summary>Identifies the type of data operation performed by a method, as specified by the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> applied to the method.</summary>
	// Token: 0x02000397 RID: 919
	public enum DataObjectMethodType
	{
		/// <summary>Indicates that a method is used for a data operation that fills a <see cref="T:System.Data.DataSet" /> object.</summary>
		// Token: 0x04000F11 RID: 3857
		Fill,
		/// <summary>Indicates that a method is used for a data operation that retrieves data.</summary>
		// Token: 0x04000F12 RID: 3858
		Select,
		/// <summary>Indicates that a method is used for a data operation that updates data.</summary>
		// Token: 0x04000F13 RID: 3859
		Update,
		/// <summary>Indicates that a method is used for a data operation that inserts data.</summary>
		// Token: 0x04000F14 RID: 3860
		Insert,
		/// <summary>Indicates that a method is used for a data operation that deletes data.</summary>
		// Token: 0x04000F15 RID: 3861
		Delete
	}
}
