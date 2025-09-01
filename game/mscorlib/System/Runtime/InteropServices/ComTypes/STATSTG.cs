using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains statistical information about an open storage, stream, or byte-array object.</summary>
	// Token: 0x020007AE RID: 1966
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Represents a pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x04002C4B RID: 11339
		public string pwcsName;

		/// <summary>Indicates the type of storage object, which is one of the values from the <see langword="STGTY" /> enumeration.</summary>
		// Token: 0x04002C4C RID: 11340
		public int type;

		/// <summary>Specifies the size, in bytes, of the stream or byte array.</summary>
		// Token: 0x04002C4D RID: 11341
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x04002C4E RID: 11342
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x04002C4F RID: 11343
		public FILETIME ctime;

		/// <summary>Specifies the last access time for this storage, stream, or byte array.</summary>
		// Token: 0x04002C50 RID: 11344
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x04002C51 RID: 11345
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x04002C52 RID: 11346
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x04002C53 RID: 11347
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the <see langword="IStorage::SetStateBits" /> method).</summary>
		// Token: 0x04002C54 RID: 11348
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002C55 RID: 11349
		public int reserved;
	}
}
