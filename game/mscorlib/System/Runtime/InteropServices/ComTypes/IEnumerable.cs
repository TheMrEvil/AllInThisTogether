using System;
using System.Collections;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020007A3 RID: 1955
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		// Token: 0x0600450D RID: 17677
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
