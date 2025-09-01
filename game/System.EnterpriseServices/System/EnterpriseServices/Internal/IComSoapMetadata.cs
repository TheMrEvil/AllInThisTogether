using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Specifies methods for generating common language runtime (CLR) metadata for a COM+ component.</summary>
	// Token: 0x02000061 RID: 97
	[Guid("d8013ff0-730b-45e2-ba24-874b7242c425")]
	public interface IComSoapMetadata
	{
		/// <summary>Generates an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library.</summary>
		/// <param name="SrcTypeLibFileName">The name of the type library for which to generate an assembly.</param>
		/// <param name="OutPath">The folder in which to generate an assembly.</param>
		/// <returns>The generated assembly name.</returns>
		// Token: 0x06000171 RID: 369
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string Generate([MarshalAs(UnmanagedType.BStr)] string SrcTypeLibFileName, [MarshalAs(UnmanagedType.BStr)] string OutPath);

		/// <summary>Generates an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library, signs the assembly with a strong-named key pair, and installs it in the global assembly cache.</summary>
		/// <param name="SrcTypeLibFileName">The name of the type library for which to generate an assembly.</param>
		/// <param name="OutPath">The folder in which to generate an assembly.</param>
		/// <param name="InstallGac">A flag that indicates whether to install the assembly in the global assembly cache.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		/// <returns>The generated assembly name.</returns>
		// Token: 0x06000172 RID: 370
		[DispId(2)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GenerateSigned([MarshalAs(UnmanagedType.BStr)] string SrcTypeLibFileName, [MarshalAs(UnmanagedType.BStr)] string OutPath, [MarshalAs(UnmanagedType.Bool)] bool InstallGac, [MarshalAs(UnmanagedType.BStr)] out string Error);
	}
}
