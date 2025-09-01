using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>This type is used as a base class for typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool, and is not intended to be used directly from your code.</summary>
	/// <typeparam name="T">The type of objects in the source sequence represented by the table, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	// Token: 0x02000011 RID: 17
	[Serializable]
	public abstract class TypedTableBase<T> : DataTable, IEnumerable<T>, IEnumerable where T : DataRow
	{
		/// <summary>Initializes a new <see cref="T:System.Data.TypedTableBase`1" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This type is not intended to be used directly from your code.</summary>
		// Token: 0x06000058 RID: 88 RVA: 0x000031A2 File Offset: 0x000013A2
		protected TypedTableBase()
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Data.TypedTableBase`1" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains data to construct the object.</param>
		/// <param name="context">The streaming context for the object being deserializad.</param>
		// Token: 0x06000059 RID: 89 RVA: 0x000031AA File Offset: 0x000013AA
		protected TypedTableBase(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Returns an enumerator for the typed-<see cref="T:System.Data.DataRow" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.Generic.IEnumerator`1" /> interface.</returns>
		// Token: 0x0600005A RID: 90 RVA: 0x000031B4 File Offset: 0x000013B4
		public IEnumerator<T> GetEnumerator()
		{
			return base.Rows.Cast<T>().GetEnumerator();
		}

		/// <summary>Returns an enumerator for the typed-<see cref="T:System.Data.DataRow" />. This method supports typed-<see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.Generic.IEnumerator`1" /> interface.</returns>
		// Token: 0x0600005B RID: 91 RVA: 0x000031C6 File Offset: 0x000013C6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Converts the elements of an <see cref="T:System.Data.TypedTableBase`1" /> to the specified type. This method supports typed <see cref="T:System.Data.DataTable" /> object generation by Visual Studio and the XSD.exe .NET Framework tool. This method is not intended to be used directly from your code.</summary>
		/// <typeparam name="TResult" />
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection" /> that contains each element of the source sequence converted to the specified type.</returns>
		// Token: 0x0600005C RID: 92 RVA: 0x000031CE File Offset: 0x000013CE
		public EnumerableRowCollection<TResult> Cast<TResult>()
		{
			return new EnumerableRowCollection<T>(this).Cast<TResult>();
		}
	}
}
