using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the known, but unreferenced, object found in an encoded SOAP XML stream during deserialization.</summary>
	// Token: 0x02000313 RID: 787
	public class UnreferencedObjectEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.UnreferencedObjectEventArgs" /> class.</summary>
		/// <param name="o">The unreferenced object. </param>
		/// <param name="id">A unique string used to identify the unreferenced object. </param>
		// Token: 0x060020A9 RID: 8361 RVA: 0x000D16C8 File Offset: 0x000CF8C8
		public UnreferencedObjectEventArgs(object o, string id)
		{
			this.o = o;
			this.id = id;
		}

		/// <summary>Gets the deserialized, but unreferenced, object.</summary>
		/// <returns>The deserialized, but unreferenced, object.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x000D16DE File Offset: 0x000CF8DE
		public object UnreferencedObject
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the ID of the object.</summary>
		/// <returns>The ID of the object.</returns>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x000D16E6 File Offset: 0x000CF8E6
		public string UnreferencedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04001B3D RID: 6973
		private object o;

		// Token: 0x04001B3E RID: 6974
		private string id;
	}
}
