using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a compiler error or warning.</summary>
	// Token: 0x02000349 RID: 841
	[Serializable]
	public class CompilerError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class.</summary>
		// Token: 0x06001BAA RID: 7082 RVA: 0x00065FFC File Offset: 0x000641FC
		public CompilerError() : this(string.Empty, 0, 0, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class using the specified file name, line, column, error number, and error text.</summary>
		/// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error.</param>
		/// <param name="line">The line of the source of the error.</param>
		/// <param name="column">The column of the source of the error.</param>
		/// <param name="errorNumber">The error number of the error.</param>
		/// <param name="errorText">The error message text.</param>
		// Token: 0x06001BAB RID: 7083 RVA: 0x00066015 File Offset: 0x00064215
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.Line = line;
			this.Column = column;
			this.ErrorNumber = errorNumber;
			this.ErrorText = errorText;
			this.FileName = fileName;
		}

		/// <summary>Gets or sets the line number where the source of the error occurs.</summary>
		/// <returns>The line number of the source file where the compiler encountered the error.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00066042 File Offset: 0x00064242
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x0006604A File Offset: 0x0006424A
		public int Line
		{
			[CompilerGenerated]
			get
			{
				return this.<Line>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Line>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the column number where the source of the error occurs.</summary>
		/// <returns>The column number of the source file where the compiler encountered the error.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00066053 File Offset: 0x00064253
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x0006605B File Offset: 0x0006425B
		public int Column
		{
			[CompilerGenerated]
			get
			{
				return this.<Column>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Column>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the error number.</summary>
		/// <returns>The error number as a string.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00066064 File Offset: 0x00064264
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x0006606C File Offset: 0x0006426C
		public string ErrorNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorNumber>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ErrorNumber>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the text of the error message.</summary>
		/// <returns>The text of the error message.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00066075 File Offset: 0x00064275
		// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x0006607D File Offset: 0x0006427D
		public string ErrorText
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorText>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ErrorText>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error is a warning.</summary>
		/// <returns>
		///   <see langword="true" /> if the error is a warning; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00066086 File Offset: 0x00064286
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x0006608E File Offset: 0x0006428E
		public bool IsWarning
		{
			[CompilerGenerated]
			get
			{
				return this.<IsWarning>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsWarning>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the file name of the source file that contains the code which caused the error.</summary>
		/// <returns>The file name of the source file that contains the code which caused the error.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00066097 File Offset: 0x00064297
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x0006609F File Offset: 0x0006429F
		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FileName>k__BackingField = value;
			}
		}

		/// <summary>Provides an implementation of Object's <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string representation of the compiler error.</returns>
		// Token: 0x06001BB8 RID: 7096 RVA: 0x000660A8 File Offset: 0x000642A8
		public override string ToString()
		{
			if (this.FileName.Length <= 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", this.WarningString, this.ErrorNumber, this.ErrorText);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[]
			{
				this.FileName,
				this.Line,
				this.Column,
				this.WarningString,
				this.ErrorNumber,
				this.ErrorText
			});
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0006613A File Offset: 0x0006433A
		private string WarningString
		{
			get
			{
				if (!this.IsWarning)
				{
					return "error";
				}
				return "warning";
			}
		}

		// Token: 0x04000E29 RID: 3625
		[CompilerGenerated]
		private int <Line>k__BackingField;

		// Token: 0x04000E2A RID: 3626
		[CompilerGenerated]
		private int <Column>k__BackingField;

		// Token: 0x04000E2B RID: 3627
		[CompilerGenerated]
		private string <ErrorNumber>k__BackingField;

		// Token: 0x04000E2C RID: 3628
		[CompilerGenerated]
		private string <ErrorText>k__BackingField;

		// Token: 0x04000E2D RID: 3629
		[CompilerGenerated]
		private bool <IsWarning>k__BackingField;

		// Token: 0x04000E2E RID: 3630
		[CompilerGenerated]
		private string <FileName>k__BackingField;
	}
}
