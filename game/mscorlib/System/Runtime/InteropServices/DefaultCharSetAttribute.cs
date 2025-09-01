using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the value of the <see cref="T:System.Runtime.InteropServices.CharSet" /> enumeration. This class cannot be inherited.</summary>
	// Token: 0x02000711 RID: 1809
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	[ComVisible(true)]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultCharSetAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.CharSet" /> value.</summary>
		/// <param name="charSet">One of the <see cref="T:System.Runtime.InteropServices.CharSet" /> values.</param>
		// Token: 0x060040C3 RID: 16579 RVA: 0x000E161F File Offset: 0x000DF81F
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._CharSet = charSet;
		}

		/// <summary>Gets the default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</summary>
		/// <returns>The default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x000E162E File Offset: 0x000DF82E
		public CharSet CharSet
		{
			get
			{
				return this._CharSet;
			}
		}

		// Token: 0x04002AEF RID: 10991
		internal CharSet _CharSet;
	}
}
