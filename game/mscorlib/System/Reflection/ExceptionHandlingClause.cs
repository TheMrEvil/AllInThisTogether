using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a clause in a structured exception-handling block.</summary>
	// Token: 0x020008EE RID: 2286
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class ExceptionHandlingClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ExceptionHandlingClause" /> class.</summary>
		// Token: 0x06004CA2 RID: 19618 RVA: 0x0000259F File Offset: 0x0000079F
		protected ExceptionHandlingClause()
		{
		}

		/// <summary>Gets the type of exception handled by this clause.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents that type of exception handled by this clause, or <see langword="null" /> if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property is <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" /> or <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Finally" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Invalid use of property for the object's current state.</exception>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x000F300F File Offset: 0x000F120F
		public virtual Type CatchType
		{
			get
			{
				return this.catch_type;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of the user-supplied filter code.</summary>
		/// <returns>The offset within the method body, in bytes, of the user-supplied filter code. The value of this property has no meaning if the <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> property has any value other than <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot get the offset because the exception handling clause is not a filter.</exception>
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004CA4 RID: 19620 RVA: 0x000F3017 File Offset: 0x000F1217
		public virtual int FilterOffset
		{
			get
			{
				return this.filter_offset;
			}
		}

		/// <summary>Gets a value indicating whether this exception-handling clause is a finally clause, a type-filtered clause, or a user-filtered clause.</summary>
		/// <returns>An <see cref="T:System.Reflection.ExceptionHandlingClauseOptions" /> value that indicates what kind of action this clause performs.</returns>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x000F301F File Offset: 0x000F121F
		public virtual ExceptionHandlingClauseOptions Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets the length, in bytes, of the body of this exception-handling clause.</summary>
		/// <returns>An integer that represents the length, in bytes, of the MSIL that forms the body of this exception-handling clause.</returns>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x000F3027 File Offset: 0x000F1227
		public virtual int HandlerLength
		{
			get
			{
				return this.handler_length;
			}
		}

		/// <summary>Gets the offset within the method body, in bytes, of this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method body, in bytes, of this exception-handling clause.</returns>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x000F302F File Offset: 0x000F122F
		public virtual int HandlerOffset
		{
			get
			{
				return this.handler_offset;
			}
		}

		/// <summary>The total length, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>The total length, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004CA8 RID: 19624 RVA: 0x000F3037 File Offset: 0x000F1237
		public virtual int TryLength
		{
			get
			{
				return this.try_length;
			}
		}

		/// <summary>The offset within the method, in bytes, of the try block that includes this exception-handling clause.</summary>
		/// <returns>An integer that represents the offset within the method, in bytes, of the try block that includes this exception-handling clause.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004CA9 RID: 19625 RVA: 0x000F303F File Offset: 0x000F123F
		public virtual int TryOffset
		{
			get
			{
				return this.try_offset;
			}
		}

		/// <summary>A string representation of the exception-handling clause.</summary>
		/// <returns>A string that lists appropriate property values for the filter clause type.</returns>
		// Token: 0x06004CAA RID: 19626 RVA: 0x000F3048 File Offset: 0x000F1248
		public override string ToString()
		{
			string text = string.Format("Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[]
			{
				this.flags,
				this.try_offset,
				this.try_length,
				this.handler_offset,
				this.handler_length
			});
			if (this.catch_type != null)
			{
				text = string.Format("{0}, CatchType={1}", text, this.catch_type);
			}
			if (this.flags == ExceptionHandlingClauseOptions.Filter)
			{
				text = string.Format("{0}, FilterOffset={1}", text, this.filter_offset);
			}
			return text;
		}

		// Token: 0x0400302B RID: 12331
		internal Type catch_type;

		// Token: 0x0400302C RID: 12332
		internal int filter_offset;

		// Token: 0x0400302D RID: 12333
		internal ExceptionHandlingClauseOptions flags;

		// Token: 0x0400302E RID: 12334
		internal int try_offset;

		// Token: 0x0400302F RID: 12335
		internal int try_length;

		// Token: 0x04003030 RID: 12336
		internal int handler_offset;

		// Token: 0x04003031 RID: 12337
		internal int handler_length;
	}
}
