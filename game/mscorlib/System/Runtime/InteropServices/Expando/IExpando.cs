using System;
using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
	/// <summary>Enables modification of objects by adding and removing members, represented by <see cref="T:System.Reflection.MemberInfo" /> objects.</summary>
	// Token: 0x0200079B RID: 1947
	[ComVisible(true)]
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	public interface IExpando : IReflect
	{
		/// <summary>Adds the named field to the Reflection object.</summary>
		/// <param name="name">The name of the field.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the added field.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see langword="IExpando" /> object does not support this method.</exception>
		// Token: 0x060044F0 RID: 17648
		FieldInfo AddField(string name);

		/// <summary>Adds the named property to the Reflection object.</summary>
		/// <param name="name">The name of the property.</param>
		/// <returns>A <see langword="PropertyInfo" /> object representing the added property.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see langword="IExpando" /> object does not support this method.</exception>
		// Token: 0x060044F1 RID: 17649
		PropertyInfo AddProperty(string name);

		/// <summary>Adds the named method to the Reflection object.</summary>
		/// <param name="name">The name of the method.</param>
		/// <param name="method">The delegate to the method.</param>
		/// <returns>A <see langword="MethodInfo" /> object representing the added method.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see langword="IExpando" /> object does not support this method.</exception>
		// Token: 0x060044F2 RID: 17650
		MethodInfo AddMethod(string name, Delegate method);

		/// <summary>Removes the specified member.</summary>
		/// <param name="m">The member to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see langword="IExpando" /> object does not support this method.</exception>
		// Token: 0x060044F3 RID: 17651
		void RemoveMember(MemberInfo m);
	}
}
