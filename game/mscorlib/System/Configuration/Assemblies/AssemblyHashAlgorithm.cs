using System;

namespace System.Configuration.Assemblies
{
	/// <summary>Specifies all the hash algorithms used for hashing files and for generating the strong name.</summary>
	// Token: 0x02000A0C RID: 2572
	public enum AssemblyHashAlgorithm
	{
		/// <summary>A mask indicating that there is no hash algorithm. If you specify <see langword="None" /> for a multi-module assembly, the common language runtime defaults to the SHA1 algorithm, since multi-module assemblies need to generate a hash.</summary>
		// Token: 0x04003857 RID: 14423
		None,
		/// <summary>Retrieves the MD5 message-digest algorithm. MD5 was developed by Rivest in 1991. It is basically MD4 with safety-belts and while it is slightly slower than MD4, it helps provide more security. The algorithm consists of four distinct rounds, which has a slightly different design from that of MD4. Message-digest size, as well as padding requirements, remain the same.</summary>
		// Token: 0x04003858 RID: 14424
		MD5 = 32771,
		/// <summary>A mask used to retrieve a revision of the Secure Hash Algorithm that corrects an unpublished flaw in SHA.</summary>
		// Token: 0x04003859 RID: 14425
		SHA1,
		/// <summary>A mask used to retrieve a version of the Secure Hash Algorithm with a hash size of 256 bits.</summary>
		// Token: 0x0400385A RID: 14426
		SHA256 = 32780,
		/// <summary>A mask used to retrieve a version of the Secure Hash Algorithm with a hash size of 384 bits.</summary>
		// Token: 0x0400385B RID: 14427
		SHA384,
		/// <summary>A mask used to retrieve a version of the Secure Hash Algorithm with a hash size of 512 bits.</summary>
		// Token: 0x0400385C RID: 14428
		SHA512
	}
}
