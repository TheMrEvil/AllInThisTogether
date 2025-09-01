using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a parameter. This class cannot be inherited.</summary>
	// Token: 0x020005DC RID: 1500
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class SoapParameterAttribute : SoapAttribute
	{
		/// <summary>Creates an instance of <see cref="T:System.Runtime.Remoting.Metadata.SoapParameterAttribute" />.</summary>
		// Token: 0x06003911 RID: 14609 RVA: 0x000CAFB2 File Offset: 0x000C91B2
		public SoapParameterAttribute()
		{
		}
	}
}
