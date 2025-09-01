using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.CodeDom
{
	/// <summary>Provides a common base class for most Code Document Object Model (CodeDOM) objects.</summary>
	// Token: 0x020002EB RID: 747
	[Serializable]
	public class CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObject" /> class.</summary>
		// Token: 0x060017DE RID: 6110 RVA: 0x0000219B File Offset: 0x0000039B
		public CodeObject()
		{
		}

		/// <summary>Gets the user-definable data for the current object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing user data for the current object.</returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x0005E720 File Offset: 0x0005C920
		public IDictionary UserData
		{
			get
			{
				IDictionary result;
				if ((result = this._userData) == null)
				{
					result = (this._userData = new ListDictionary());
				}
				return result;
			}
		}

		// Token: 0x04000D3B RID: 3387
		private IDictionary _userData;
	}
}
