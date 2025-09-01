using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a code checksum pragma code entity.</summary>
	// Token: 0x020002FE RID: 766
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class.</summary>
		// Token: 0x0600187F RID: 6271 RVA: 0x0005F624 File Offset: 0x0005D824
		public CodeChecksumPragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class using a file name, a GUID representing the checksum algorithm, and a byte stream representing the checksum data.</summary>
		/// <param name="fileName">The path to the checksum file.</param>
		/// <param name="checksumAlgorithmId">A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</param>
		/// <param name="checksumData">A byte array that contains the checksum data.</param>
		// Token: 0x06001880 RID: 6272 RVA: 0x0005F62C File Offset: 0x0005D82C
		public CodeChecksumPragma(string fileName, Guid checksumAlgorithmId, byte[] checksumData)
		{
			this._fileName = fileName;
			this.ChecksumAlgorithmId = checksumAlgorithmId;
			this.ChecksumData = checksumData;
		}

		/// <summary>Gets or sets the path to the checksum file.</summary>
		/// <returns>The path to the checksum file.</returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0005F649 File Offset: 0x0005D849
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0005F65A File Offset: 0x0005D85A
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets a GUID that identifies the checksum algorithm to use.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0005F663 File Offset: 0x0005D863
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0005F66B File Offset: 0x0005D86B
		public Guid ChecksumAlgorithmId
		{
			[CompilerGenerated]
			get
			{
				return this.<ChecksumAlgorithmId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChecksumAlgorithmId>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the value of the data for the checksum calculation.</summary>
		/// <returns>A byte array that contains the data for the checksum calculation.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0005F674 File Offset: 0x0005D874
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x0005F67C File Offset: 0x0005D87C
		public byte[] ChecksumData
		{
			[CompilerGenerated]
			get
			{
				return this.<ChecksumData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChecksumData>k__BackingField = value;
			}
		}

		// Token: 0x04000D70 RID: 3440
		private string _fileName;

		// Token: 0x04000D71 RID: 3441
		[CompilerGenerated]
		private Guid <ChecksumAlgorithmId>k__BackingField;

		// Token: 0x04000D72 RID: 3442
		[CompilerGenerated]
		private byte[] <ChecksumData>k__BackingField;
	}
}
