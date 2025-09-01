using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides information about a <see cref="T:System.Diagnostics.StackFrame" />, which represents a function call on the call stack for the current thread.</summary>
	// Token: 0x020009C1 RID: 2497
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with MS.NET")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class StackFrame
	{
		// Token: 0x060059D3 RID: 22995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_frame_info(int skip, bool needFileInfo, out MethodBase method, out int iloffset, out int native_offset, out string file, out int line, out int column);

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class.</summary>
		// Token: 0x060059D4 RID: 22996 RVA: 0x001332C0 File Offset: 0x001314C0
		public StackFrame()
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number of the stack frame; otherwise, <see langword="false" />.</param>
		// Token: 0x060059D5 RID: 22997 RVA: 0x00133310 File Offset: 0x00131510
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip.</param>
		// Token: 0x060059D6 RID: 22998 RVA: 0x00133360 File Offset: 0x00131560
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame, optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip.</param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number of the stack frame; otherwise, <see langword="false" />.</param>
		// Token: 0x060059D7 RID: 22999 RVA: 0x001333B0 File Offset: 0x001315B0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name and line number.</summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="lineNumber">The line number in the specified file.</param>
		// Token: 0x060059D8 RID: 23000 RVA: 0x00133400 File Offset: 0x00131600
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that contains only the given file name, line number, and column number.</summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="lineNumber">The line number in the specified file.</param>
		/// <param name="colNumber">The column number in the specified file.</param>
		// Token: 0x060059D9 RID: 23001 RVA: 0x0013345C File Offset: 0x0013165C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber, int colNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = colNumber;
		}

		/// <summary>Gets the line number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file line number, or 0 (zero) if the file line number cannot be determined.</returns>
		// Token: 0x060059DA RID: 23002 RVA: 0x001334B6 File Offset: 0x001316B6
		public virtual int GetFileLineNumber()
		{
			return this.lineNumber;
		}

		/// <summary>Gets the column number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file column number, or 0 (zero) if the file column number cannot be determined.</returns>
		// Token: 0x060059DB RID: 23003 RVA: 0x001334BE File Offset: 0x001316BE
		public virtual int GetFileColumnNumber()
		{
			return this.columnNumber;
		}

		/// <summary>Gets the file name that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file name, or <see langword="null" /> if the file name cannot be determined.</returns>
		// Token: 0x060059DC RID: 23004 RVA: 0x001334C6 File Offset: 0x001316C6
		public virtual string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x001334D0 File Offset: 0x001316D0
		internal string GetSecureFileName()
		{
			string result = "<filename unknown>";
			if (this.fileName == null)
			{
				return result;
			}
			try
			{
				result = this.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return result;
		}

		/// <summary>Gets the offset from the start of the Microsoft intermediate language (MSIL) code for the method that is executing. This offset might be an approximation depending on whether or not the just-in-time (JIT) compiler is generating debugging code. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" />.</summary>
		/// <returns>The offset from the start of the MSIL code for the method that is executing.</returns>
		// Token: 0x060059DE RID: 23006 RVA: 0x0013350C File Offset: 0x0013170C
		public virtual int GetILOffset()
		{
			return this.ilOffset;
		}

		/// <summary>Gets the method in which the frame is executing.</summary>
		/// <returns>The method in which the frame is executing.</returns>
		// Token: 0x060059DF RID: 23007 RVA: 0x00133514 File Offset: 0x00131714
		public virtual MethodBase GetMethod()
		{
			return this.methodBase;
		}

		/// <summary>Gets the offset from the start of the native just-in-time (JIT)-compiled code for the method that is being executed. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class.</summary>
		/// <returns>The offset from the start of the JIT-compiled code for the method that is being executed.</returns>
		// Token: 0x060059E0 RID: 23008 RVA: 0x0013351C File Offset: 0x0013171C
		public virtual int GetNativeOffset()
		{
			return this.nativeOffset;
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00133524 File Offset: 0x00131724
		internal long GetMethodAddress()
		{
			return this.methodAddress;
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0013352C File Offset: 0x0013172C
		internal uint GetMethodIndex()
		{
			return this.methodIndex;
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x00133534 File Offset: 0x00131734
		internal string GetInternalMethodName()
		{
			return this.internalMethodName;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		// Token: 0x060059E4 RID: 23012 RVA: 0x0013353C File Offset: 0x0013173C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.methodBase == null)
			{
				stringBuilder.Append(Locale.GetText("<unknown method>"));
			}
			else
			{
				stringBuilder.Append(this.methodBase.Name);
			}
			stringBuilder.Append(Locale.GetText(" at "));
			if (this.ilOffset == -1)
			{
				stringBuilder.Append(Locale.GetText("<unknown offset>"));
			}
			else
			{
				stringBuilder.Append(Locale.GetText("offset "));
				stringBuilder.Append(this.ilOffset);
			}
			stringBuilder.Append(Locale.GetText(" in file:line:column "));
			stringBuilder.Append(this.GetSecureFileName());
			stringBuilder.AppendFormat(":{0}:{1}", this.lineNumber, this.columnNumber);
			return stringBuilder.ToString();
		}

		/// <summary>Defines the value that is returned from the <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" /> or <see cref="M:System.Diagnostics.StackFrame.GetILOffset" /> method when the native or Microsoft intermediate language (MSIL) offset is unknown. This field is constant.</summary>
		// Token: 0x0400378E RID: 14222
		public const int OFFSET_UNKNOWN = -1;

		// Token: 0x0400378F RID: 14223
		private int ilOffset = -1;

		// Token: 0x04003790 RID: 14224
		private int nativeOffset = -1;

		// Token: 0x04003791 RID: 14225
		private long methodAddress;

		// Token: 0x04003792 RID: 14226
		private uint methodIndex;

		// Token: 0x04003793 RID: 14227
		private MethodBase methodBase;

		// Token: 0x04003794 RID: 14228
		private string fileName;

		// Token: 0x04003795 RID: 14229
		private int lineNumber;

		// Token: 0x04003796 RID: 14230
		private int columnNumber;

		// Token: 0x04003797 RID: 14231
		private string internalMethodName;
	}
}
