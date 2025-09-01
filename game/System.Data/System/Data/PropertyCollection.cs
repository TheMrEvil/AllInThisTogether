using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents a collection of properties that can be added to <see cref="T:System.Data.DataColumn" />, <see cref="T:System.Data.DataSet" />, or <see cref="T:System.Data.DataTable" />.</summary>
	// Token: 0x02000115 RID: 277
	[Serializable]
	public class PropertyCollection : Hashtable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		// Token: 0x06000F9A RID: 3994 RVA: 0x0003FD90 File Offset: 0x0003DF90
		public PropertyCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x06000F9B RID: 3995 RVA: 0x0003FD98 File Offset: 0x0003DF98
		protected PropertyCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Data.PropertyCollection" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Object" />, a shallow copy of the <see cref="T:System.Data.PropertyCollection" /> object.</returns>
		// Token: 0x06000F9C RID: 3996 RVA: 0x0003FDA4 File Offset: 0x0003DFA4
		public override object Clone()
		{
			PropertyCollection propertyCollection = new PropertyCollection();
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				propertyCollection.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			return propertyCollection;
		}
	}
}
