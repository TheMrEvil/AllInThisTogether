using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Sets the default value of a parameter when called from a language that supports default parameters. This class cannot be inherited.</summary>
	// Token: 0x02000184 RID: 388
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class DefaultParameterValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultParameterValueAttribute" /> class with the default value of a parameter.</summary>
		/// <param name="value">An object that represents the default value of a parameter.</param>
		// Token: 0x06000A65 RID: 2661 RVA: 0x0002D4D9 File Offset: 0x0002B6D9
		public DefaultParameterValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value of a parameter.</summary>
		/// <returns>An object that represents the default value of a parameter.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040006E7 RID: 1767
		private object value;
	}
}
