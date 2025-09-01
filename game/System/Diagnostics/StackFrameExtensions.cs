using System;

namespace System.Diagnostics
{
	/// <summary>Provides extension methods for the <see cref="T:System.Diagnostics.StackFrame" /> class, which represents a function call on the call stack for the current thread.</summary>
	// Token: 0x02000210 RID: 528
	public static class StackFrameExtensions
	{
		/// <summary>Indicates whether the native image is available for the specified stack frame.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if a native image is available for this stack frame; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F3C RID: 3900 RVA: 0x00044A07 File Offset: 0x00042C07
		public static bool HasNativeImage(this StackFrame stackFrame)
		{
			return stackFrame.GetNativeImageBase() != IntPtr.Zero;
		}

		/// <summary>Indicates whether information about the method in which the specified frame is executing is available.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if information about the method in which the current frame is executing is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F3D RID: 3901 RVA: 0x00044A19 File Offset: 0x00042C19
		public static bool HasMethod(this StackFrame stackFrame)
		{
			return stackFrame.GetMethod() != null;
		}

		/// <summary>Indicates whether an offset from the start of the IL code for the method that is executing is available.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if the offset is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F3E RID: 3902 RVA: 0x00044A27 File Offset: 0x00042C27
		public static bool HasILOffset(this StackFrame stackFrame)
		{
			return stackFrame.GetILOffset() != -1;
		}

		/// <summary>Indicates whether the file that contains the code that the specified stack frame is executing is available.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>
		///   <see langword="true" /> if the code that the specified stack frame is executing is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F3F RID: 3903 RVA: 0x00044A35 File Offset: 0x00042C35
		public static bool HasSource(this StackFrame stackFrame)
		{
			return stackFrame.GetFileName() != null;
		}

		/// <summary>Gets an interface pointer to the start of the native code for the method that is being executed.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>An interface pointer to the start of the native code for the method that is being executed or <see cref="F:System.IntPtr.Zero" /> if you're targeting the .NET Framework.</returns>
		// Token: 0x06000F40 RID: 3904 RVA: 0x00011E31 File Offset: 0x00010031
		public static IntPtr GetNativeIP(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}

		/// <summary>Returns a pointer to the base address of the native image that this stack frame is executing.</summary>
		/// <param name="stackFrame">A stack frame.</param>
		/// <returns>A pointer to the base address of the native image or <see cref="F:System.IntPtr.Zero" /> if you're targeting the .NET Framework.</returns>
		// Token: 0x06000F41 RID: 3905 RVA: 0x00011E31 File Offset: 0x00010031
		public static IntPtr GetNativeImageBase(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}
	}
}
