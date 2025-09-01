using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a set of options used by a code generator.</summary>
	// Token: 0x02000346 RID: 838
	public class CodeGeneratorOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> class.</summary>
		// Token: 0x06001B44 RID: 6980 RVA: 0x0006481E File Offset: 0x00062A1E
		public CodeGeneratorOptions()
		{
		}

		/// <summary>Gets or sets the object at the specified index.</summary>
		/// <param name="index">The name associated with the object to retrieve.</param>
		/// <returns>The object associated with the specified name. If no object associated with the specified name exists in the collection, <see langword="null" />.</returns>
		// Token: 0x17000574 RID: 1396
		public object this[string index]
		{
			get
			{
				return this._options[index];
			}
			set
			{
				this._options[index] = value;
			}
		}

		/// <summary>Gets or sets the string to use for indentations.</summary>
		/// <returns>A string containing the characters to use for indentations.</returns>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00064850 File Offset: 0x00062A50
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x0006487D File Offset: 0x00062A7D
		public string IndentString
		{
			get
			{
				object obj = this._options["IndentString"];
				if (obj == null)
				{
					return "    ";
				}
				return (string)obj;
			}
			set
			{
				this._options["IndentString"] = value;
			}
		}

		/// <summary>Gets or sets the style to use for bracing.</summary>
		/// <returns>A string containing the bracing style to use.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00064890 File Offset: 0x00062A90
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x000648BD File Offset: 0x00062ABD
		public string BracingStyle
		{
			get
			{
				object obj = this._options["BracingStyle"];
				if (obj == null)
				{
					return "Block";
				}
				return (string)obj;
			}
			set
			{
				this._options["BracingStyle"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to append an <see langword="else" />, <see langword="catch" />, or <see langword="finally" /> block, including brackets, at the closing line of each previous <see langword="if" /> or <see langword="try" /> block.</summary>
		/// <returns>
		///   <see langword="true" /> if an else should be appended; otherwise, <see langword="false" />. The default value of this property is <see langword="false" />.</returns>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x000648D0 File Offset: 0x00062AD0
		// (set) Token: 0x06001B4C RID: 6988 RVA: 0x000648F9 File Offset: 0x00062AF9
		public bool ElseOnClosing
		{
			get
			{
				object obj = this._options["ElseOnClosing"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._options["ElseOnClosing"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to insert blank lines between members.</summary>
		/// <returns>
		///   <see langword="true" /> if blank lines should be inserted; otherwise, <see langword="false" />. By default, the value of this property is <see langword="true" />.</returns>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x00064914 File Offset: 0x00062B14
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0006493D File Offset: 0x00062B3D
		public bool BlankLinesBetweenMembers
		{
			get
			{
				object obj = this._options["BlankLinesBetweenMembers"];
				return obj == null || (bool)obj;
			}
			set
			{
				this._options["BlankLinesBetweenMembers"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate members in the order in which they occur in member collections.</summary>
		/// <returns>
		///   <see langword="true" /> to generate the members in the order in which they occur in the member collection; otherwise, <see langword="false" />. The default value of this property is <see langword="false" />.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00064958 File Offset: 0x00062B58
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x00064981 File Offset: 0x00062B81
		public bool VerbatimOrder
		{
			get
			{
				object obj = this._options["VerbatimOrder"];
				return obj != null && (bool)obj;
			}
			set
			{
				this._options["VerbatimOrder"] = value;
			}
		}

		// Token: 0x04000E26 RID: 3622
		private readonly IDictionary _options = new ListDictionary();
	}
}
