using System;
using System.Globalization;

// Token: 0x02000006 RID: 6
internal static class SR
{
	// Token: 0x0600000C RID: 12 RVA: 0x00002050 File Offset: 0x00000250
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000205E File Offset: 0x0000025E
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002068 File Offset: 0x00000268
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000206B File Offset: 0x0000026B
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000206E File Offset: 0x0000026E
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002081 File Offset: 0x00000281
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000208F File Offset: 0x0000028F
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000209E File Offset: 0x0000029E
	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000020A9 File Offset: 0x000002A9
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002068 File Offset: 0x00000268
	internal static string GetResourceString(string str)
	{
		return str;
	}

	// Token: 0x0400002B RID: 43
	public const string ArgumentOutOfRange_Enum = "Enum value was out of legal range.";

	// Token: 0x0400002C RID: 44
	public const string ArgumentOutOfRange_NeedPosNum = "Positive number required.";

	// Token: 0x0400002D RID: 45
	public const string CannotReadFromDeflateStream = "Reading from the compression stream is not supported.";

	// Token: 0x0400002E RID: 46
	public const string CannotWriteToDeflateStream = "Writing to the compression stream is not supported.";

	// Token: 0x0400002F RID: 47
	public const string GenericInvalidData = "Found invalid data while decoding.";

	// Token: 0x04000030 RID: 48
	public const string InvalidArgumentOffsetCount = "Offset plus count is larger than the length of target array.";

	// Token: 0x04000031 RID: 49
	public const string InvalidBeginCall = "Only one asynchronous reader or writer is allowed time at one time.";

	// Token: 0x04000032 RID: 50
	public const string InvalidBlockLength = "Block length does not match with its complement.";

	// Token: 0x04000033 RID: 51
	public const string InvalidHuffmanData = "Failed to construct a huffman tree using the length array. The stream might be corrupted.";

	// Token: 0x04000034 RID: 52
	public const string NotSupported = "This operation is not supported.";

	// Token: 0x04000035 RID: 53
	public const string NotSupported_UnreadableStream = "Stream does not support reading.";

	// Token: 0x04000036 RID: 54
	public const string NotSupported_UnwritableStream = "Stream does not support writing.";

	// Token: 0x04000037 RID: 55
	public const string ObjectDisposed_StreamClosed = "Can not access a closed Stream.";

	// Token: 0x04000038 RID: 56
	public const string UnknownBlockType = "Unknown block type. Stream might be corrupted.";

	// Token: 0x04000039 RID: 57
	public const string UnknownState = "Decoder is in some unknown state. This might be caused by corrupted data.";

	// Token: 0x0400003A RID: 58
	public const string ZLibErrorDLLLoadError = "The underlying compression routine could not be loaded correctly.";

	// Token: 0x0400003B RID: 59
	public const string ZLibErrorInconsistentStream = "The stream state of the underlying compression routine is inconsistent.";

	// Token: 0x0400003C RID: 60
	public const string ZLibErrorIncorrectInitParameters = "The underlying compression routine received incorrect initialization parameters.";

	// Token: 0x0400003D RID: 61
	public const string ZLibErrorNotEnoughMemory = "The underlying compression routine could not reserve sufficient memory.";

	// Token: 0x0400003E RID: 62
	public const string ZLibErrorVersionMismatch = "The version of the underlying compression routine does not match expected version.";

	// Token: 0x0400003F RID: 63
	public const string ZLibErrorUnexpected = "The underlying compression routine returned an unexpected error code.";

	// Token: 0x04000040 RID: 64
	public const string ArgumentNeedNonNegative = "The argument must be non-negative.";

	// Token: 0x04000041 RID: 65
	public const string CannotBeEmpty = "String cannot be empty.";

	// Token: 0x04000042 RID: 66
	public const string CDCorrupt = "Central Directory corrupt.";

	// Token: 0x04000043 RID: 67
	public const string CentralDirectoryInvalid = "Central Directory is invalid.";

	// Token: 0x04000044 RID: 68
	public const string CreateInReadMode = "Cannot create entries on an archive opened in read mode.";

	// Token: 0x04000045 RID: 69
	public const string CreateModeCapabilities = "Cannot use create mode on a non-writable stream.";

	// Token: 0x04000046 RID: 70
	public const string CreateModeCreateEntryWhileOpen = "Entries cannot be created while previously created entries are still open.";

	// Token: 0x04000047 RID: 71
	public const string CreateModeWriteOnceAndOneEntryAtATime = "Entries in create mode may only be written to once, and only one entry may be held open at a time.";

	// Token: 0x04000048 RID: 72
	public const string DateTimeOutOfRange = "The DateTimeOffset specified cannot be converted into a Zip file timestamp.";

	// Token: 0x04000049 RID: 73
	public const string DeletedEntry = "Cannot modify deleted entry.";

	// Token: 0x0400004A RID: 74
	public const string DeleteOnlyInUpdate = "Delete can only be used when the archive is in Update mode.";

	// Token: 0x0400004B RID: 75
	public const string DeleteOpenEntry = "Cannot delete an entry currently open for writing.";

	// Token: 0x0400004C RID: 76
	public const string EntriesInCreateMode = "Cannot access entries in Create mode.";

	// Token: 0x0400004D RID: 77
	public const string EntryNameEncodingNotSupported = "The specified entry name encoding is not supported.";

	// Token: 0x0400004E RID: 78
	public const string EntryNamesTooLong = "Entry names cannot require more than 2^16 bits.";

	// Token: 0x0400004F RID: 79
	public const string EntryTooLarge = "Entries larger than 4GB are not supported in Update mode.";

	// Token: 0x04000050 RID: 80
	public const string EOCDNotFound = "End of Central Directory record could not be found.";

	// Token: 0x04000051 RID: 81
	public const string FieldTooBigCompressedSize = "Compressed Size cannot be held in an Int64.";

	// Token: 0x04000052 RID: 82
	public const string FieldTooBigLocalHeaderOffset = "Local Header Offset cannot be held in an Int64.";

	// Token: 0x04000053 RID: 83
	public const string FieldTooBigNumEntries = "Number of Entries cannot be held in an Int64.";

	// Token: 0x04000054 RID: 84
	public const string FieldTooBigOffsetToCD = "Offset to Central Directory cannot be held in an Int64.";

	// Token: 0x04000055 RID: 85
	public const string FieldTooBigOffsetToZip64EOCD = "Offset to Zip64 End Of Central Directory record cannot be held in an Int64.";

	// Token: 0x04000056 RID: 86
	public const string FieldTooBigStartDiskNumber = "Start Disk Number cannot be held in an Int64.";

	// Token: 0x04000057 RID: 87
	public const string FieldTooBigUncompressedSize = "Uncompressed Size cannot be held in an Int64.";

	// Token: 0x04000058 RID: 88
	public const string FrozenAfterWrite = "Cannot modify entry in Create mode after entry has been opened for writing.";

	// Token: 0x04000059 RID: 89
	public const string HiddenStreamName = "A stream from ZipArchiveEntry has been disposed.";

	// Token: 0x0400005A RID: 90
	public const string LengthAfterWrite = "Length properties are unavailable once an entry has been opened for writing.";

	// Token: 0x0400005B RID: 91
	public const string LocalFileHeaderCorrupt = "A local file header is corrupt.";

	// Token: 0x0400005C RID: 92
	public const string NumEntriesWrong = "Number of entries expected in End Of Central Directory does not correspond to number of entries in Central Directory.";

	// Token: 0x0400005D RID: 93
	public const string OffsetLengthInvalid = "The offset and length parameters are not valid for the array that was given.";

	// Token: 0x0400005E RID: 94
	public const string ReadingNotSupported = "This stream from ZipArchiveEntry does not support reading.";

	// Token: 0x0400005F RID: 95
	public const string ReadModeCapabilities = "Cannot use read mode on a non-readable stream.";

	// Token: 0x04000060 RID: 96
	public const string ReadOnlyArchive = "Cannot modify read-only archive.";

	// Token: 0x04000061 RID: 97
	public const string SeekingNotSupported = "This stream from ZipArchiveEntry does not support seeking.";

	// Token: 0x04000062 RID: 98
	public const string SetLengthRequiresSeekingAndWriting = "SetLength requires a stream that supports seeking and writing.";

	// Token: 0x04000063 RID: 99
	public const string SplitSpanned = "Split or spanned archives are not supported.";

	// Token: 0x04000064 RID: 100
	public const string UnexpectedEndOfStream = "Zip file corrupt: unexpected end of stream reached.";

	// Token: 0x04000065 RID: 101
	public const string UnsupportedCompression = "The archive entry was compressed using an unsupported compression method.";

	// Token: 0x04000066 RID: 102
	public const string UnsupportedCompressionMethod = "The archive entry was compressed using {0} and is not supported.";

	// Token: 0x04000067 RID: 103
	public const string UpdateModeCapabilities = "Update mode requires a stream with read, write, and seek capabilities.";

	// Token: 0x04000068 RID: 104
	public const string UpdateModeOneStream = "Entries cannot be opened multiple times in Update mode.";

	// Token: 0x04000069 RID: 105
	public const string WritingNotSupported = "This stream from ZipArchiveEntry does not support writing.";

	// Token: 0x0400006A RID: 106
	public const string Zip64EOCDNotWhereExpected = "Zip 64 End of Central Directory Record not where indicated.";

	// Token: 0x0400006B RID: 107
	public const string Argument_InvalidPathChars = "Illegal characters in path '{0}'.";

	// Token: 0x0400006C RID: 108
	public const string Stream_FalseCanRead = "Stream does not support reading.";

	// Token: 0x0400006D RID: 109
	public const string Stream_FalseCanWrite = "Stream does not support writing.";

	// Token: 0x0400006E RID: 110
	public const string BrotliEncoder_Create = "Failed to create BrotliEncoder instance";

	// Token: 0x0400006F RID: 111
	public const string BrotliEncoder_Disposed = "Can not access a closed Encoder.";

	// Token: 0x04000070 RID: 112
	public const string BrotliEncoder_Quality = "Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}";

	// Token: 0x04000071 RID: 113
	public const string BrotliEncoder_Window = "Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}";

	// Token: 0x04000072 RID: 114
	public const string BrotliEncoder_InvalidSetParameter = "The BrotliEncoder {0} can not be changed at current encoder state.";

	// Token: 0x04000073 RID: 115
	public const string BrotliDecoder_Create = "Failed to create BrotliDecoder instance";

	// Token: 0x04000074 RID: 116
	public const string BrotliDecoder_Error = "Decoder threw unexpected error: {0}";

	// Token: 0x04000075 RID: 117
	public const string BrotliDecoder_Disposed = "Can not access a closed Decoder.";

	// Token: 0x04000076 RID: 118
	public const string BrotliStream_Compress_UnsupportedOperation = "Can not perform Read operations on a BrotliStream constructed with CompressionMode.Compress.";

	// Token: 0x04000077 RID: 119
	public const string BrotliStream_Compress_InvalidData = "Encoder ran into invalid data.";

	// Token: 0x04000078 RID: 120
	public const string BrotliStream_Decompress_UnsupportedOperation = "Can not perform Write operations on a BrotliStream constructed with CompressionMode.Decompress.";

	// Token: 0x04000079 RID: 121
	public const string BrotliStream_Decompress_InvalidData = "Decoder ran into invalid data.";

	// Token: 0x0400007A RID: 122
	public const string BrotliStream_Decompress_InvalidStream = "BrotliStream.BaseStream returned more bytes than requested in Read.";
}
