using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies the direction of the data flow in the <paramref name="dwDirection" /> parameter of the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.EnumFormatEtc(System.Runtime.InteropServices.ComTypes.DATADIR)" /> method. This determines the formats that the resulting enumerator can enumerate.</summary>
	// Token: 0x02000188 RID: 392
	public enum DATADIR
	{
		/// <summary>Requests that <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.EnumFormatEtc(System.Runtime.InteropServices.ComTypes.DATADIR)" /> supply an enumerator for the formats that can be specified in <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />.</summary>
		// Token: 0x040006F9 RID: 1785
		DATADIR_GET = 1,
		/// <summary>Requests that <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.EnumFormatEtc(System.Runtime.InteropServices.ComTypes.DATADIR)" /> supply an enumerator for the formats that can be specified in <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />.</summary>
		// Token: 0x040006FA RID: 1786
		DATADIR_SET
	}
}
