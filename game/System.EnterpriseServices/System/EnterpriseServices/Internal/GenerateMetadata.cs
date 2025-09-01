using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Generates common language runtime (CLR) metadata for a COM+ component.</summary>
	// Token: 0x0200005D RID: 93
	[Guid("d8013ff1-730b-45e2-ba24-874b7242c425")]
	public class GenerateMetadata : IComSoapMetadata
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.GenerateMetadata" /> class.</summary>
		// Token: 0x06000164 RID: 356 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public GenerateMetadata()
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates, or locates, an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library.</summary>
		/// <param name="strSrcTypeLib">The name of the type library for which to generate an assembly.</param>
		/// <param name="outPath">The folder in which to generate an assembly or to locate an already existing assembly.</param>
		/// <returns>The generated assembly name; otherwise, an empty string if the inputs are invalid.</returns>
		// Token: 0x06000165 RID: 357 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string Generate(string strSrcTypeLib, string outPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates, or locates, an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library, signs the assembly with a strong-named key pair, and installs it in the global assembly cache.</summary>
		/// <param name="strSrcTypeLib">The name of the type library for which to generate an assembly.</param>
		/// <param name="outPath">The folder in which to generate an assembly or to locate an already existing assembly.</param>
		/// <param name="PublicKey">A public key used to import type library information into an assembly.</param>
		/// <param name="KeyPair">A strong-named key pair used to sign the generated assembly.</param>
		/// <returns>The generated assembly name; otherwise, an empty string if the inputs are invalid.</returns>
		// Token: 0x06000166 RID: 358 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string GenerateMetaData(string strSrcTypeLib, string outPath, byte[] PublicKey, StrongNameKeyPair KeyPair)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates, or locates, an assembly that contains common language runtime (CLR) metadata for a COM+ component represented by the specified type library, signs the assembly with a strong-named key pair, and installs it in the global assembly cache.</summary>
		/// <param name="strSrcTypeLib">The name of the type library for which to generate an assembly.</param>
		/// <param name="outPath">The folder in which to generate an assembly or to locate an already existing assembly.</param>
		/// <param name="InstallGac">Ignored.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		/// <returns>The generated assembly name; otherwise, an empty string if the inputs are invalid.</returns>
		// Token: 0x06000167 RID: 359 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public string GenerateSigned(string strSrcTypeLib, string outPath, bool InstallGac, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches for a specified file in a specified path.</summary>
		/// <param name="path">The path to be searched for the file.</param>
		/// <param name="fileName">The name of the file for which to search.</param>
		/// <param name="extension">An extension to be added to the file name when searching for the file.</param>
		/// <param name="numBufferChars">The size of the buffer that receives the valid path and file name.</param>
		/// <param name="buffer">The buffer that receives the path and file name of the file found.</param>
		/// <param name="filePart">The variable that receives the address of the last component of the valid path and file name.</param>
		/// <returns>If the search succeeds, the return value is the length of the string copied to <paramref name="buffer" />. If the search fails, the return value is 0.</returns>
		// Token: 0x06000168 RID: 360 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static int SearchPath(string path, string fileName, string extension, int numBufferChars, string buffer, int[] filePart)
		{
			throw new NotImplementedException();
		}
	}
}
