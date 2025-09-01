using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Represents a stack trace, which is an ordered collection of one or more stack frames.</summary>
	// Token: 0x020009C2 RID: 2498
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with .NET")]
	[Serializable]
	public class StackTrace
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame.</summary>
		// Token: 0x060059E5 RID: 23013 RVA: 0x00133613 File Offset: 0x00131813
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace()
		{
			this.init_frames(0, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		// Token: 0x060059E6 RID: 23014 RVA: 0x00133623 File Offset: 0x00131823
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(bool fNeedFileInfo)
		{
			this.init_frames(0, fNeedFileInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x060059E7 RID: 23015 RVA: 0x00133633 File Offset: 0x00131833
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(int skipFrames)
		{
			this.init_frames(skipFrames, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x060059E8 RID: 23016 RVA: 0x00133643 File Offset: 0x00131843
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			this.init_frames(skipFrames, fNeedFileInfo);
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x00133654 File Offset: 0x00131854
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void init_frames(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			List<StackFrame> list = new List<StackFrame>();
			skipFrames += 2;
			StackFrame stackFrame;
			while ((stackFrame = new StackFrame(skipFrames, fNeedFileInfo)) != null && stackFrame.GetMethod() != null)
			{
				list.Add(stackFrame);
				skipFrames++;
			}
			this.debug_info = fNeedFileInfo;
			this.frames = list.ToArray();
		}

		// Token: 0x060059EA RID: 23018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StackFrame[] get_trace(Exception e, int skipFrames, bool fNeedFileInfo);

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object.</summary>
		/// <param name="e">The exception object from which to construct the stack trace.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		// Token: 0x060059EB RID: 23019 RVA: 0x001336BA File Offset: 0x001318BA
		public StackTrace(Exception e) : this(e, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class, using the provided exception object and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		// Token: 0x060059EC RID: 23020 RVA: 0x001336C5 File Offset: 0x001318C5
		public StackTrace(Exception e, bool fNeedFileInfo) : this(e, 0, fNeedFileInfo)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object and skipping the specified number of frames.</summary>
		/// <param name="e">The exception object from which to construct the stack trace.</param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x060059ED RID: 23021 RVA: 0x001336D0 File Offset: 0x001318D0
		public StackTrace(Exception e, int skipFrames) : this(e, skipFrames, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace.</param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace.</param>
		/// <param name="fNeedFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative.</exception>
		// Token: 0x060059EE RID: 23022 RVA: 0x001336DC File Offset: 0x001318DC
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			this.frames = StackTrace.get_trace(e, skipFrames, fNeedFileInfo);
			this.captured_traces = e.captured_traces;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class that contains a single frame.</summary>
		/// <param name="frame">The frame that the <see cref="T:System.Diagnostics.StackTrace" /> object should contain.</param>
		// Token: 0x060059EF RID: 23023 RVA: 0x0013372B File Offset: 0x0013192B
		public StackTrace(StackFrame frame)
		{
			this.frames = new StackFrame[1];
			this.frames[0] = frame;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class for a specific thread, optionally capturing source information.  
		///  Do not use this constructor overload.</summary>
		/// <param name="targetThread">The thread whose stack trace is requested.</param>
		/// <param name="needFileInfo">
		///   <see langword="true" /> to capture the file name, line number, and column number; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread <paramref name="targetThread" /> is not suspended.</exception>
		// Token: 0x060059F0 RID: 23024 RVA: 0x00133748 File Offset: 0x00131948
		[MonoLimitation("Not possible to create StackTraces from other threads")]
		[Obsolete]
		public StackTrace(Thread targetThread, bool needFileInfo)
		{
			if (targetThread == Thread.CurrentThread)
			{
				this.init_frames(0, needFileInfo);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x00133766 File Offset: 0x00131966
		internal StackTrace(StackFrame[] frames)
		{
			this.frames = frames;
		}

		/// <summary>Gets the number of frames in the stack trace.</summary>
		/// <returns>The number of frames in the stack trace.</returns>
		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060059F2 RID: 23026 RVA: 0x00133775 File Offset: 0x00131975
		public virtual int FrameCount
		{
			get
			{
				if (this.frames != null)
				{
					return this.frames.Length;
				}
				return 0;
			}
		}

		/// <summary>Gets the specified stack frame.</summary>
		/// <param name="index">The index of the stack frame requested.</param>
		/// <returns>The specified stack frame.</returns>
		// Token: 0x060059F3 RID: 23027 RVA: 0x00133789 File Offset: 0x00131989
		public virtual StackFrame GetFrame(int index)
		{
			if (index < 0 || index >= this.FrameCount)
			{
				return null;
			}
			return this.frames[index];
		}

		/// <summary>Returns a copy of all stack frames in the current stack trace.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.StackFrame" /> representing the function calls in the stack trace.</returns>
		// Token: 0x060059F4 RID: 23028 RVA: 0x001337A4 File Offset: 0x001319A4
		[ComVisible(false)]
		public virtual StackFrame[] GetFrames()
		{
			if (this.captured_traces == null)
			{
				return this.frames;
			}
			List<StackFrame> list = new List<StackFrame>();
			foreach (StackTrace stackTrace in this.captured_traces)
			{
				for (int j = 0; j < stackTrace.FrameCount; j++)
				{
					list.Add(stackTrace.GetFrame(j));
				}
			}
			list.AddRange(this.frames);
			return list.ToArray();
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00133814 File Offset: 0x00131A14
		private static string GetAotId()
		{
			if (!StackTrace.isAotidSet)
			{
				byte[] aotId = RuntimeAssembly.GetAotId();
				if (aotId != null)
				{
					StackTrace.aotid = new Guid(aotId).ToString("N");
				}
				StackTrace.isAotidSet = true;
			}
			return StackTrace.aotid;
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x00133854 File Offset: 0x00131A54
		private bool AddFrames(StringBuilder sb, bool separator, out bool isAsync)
		{
			isAsync = false;
			bool flag = false;
			int i = 0;
			while (i < this.FrameCount)
			{
				StackFrame frame = this.GetFrame(i);
				if (frame.GetMethod() == null)
				{
					if (flag || separator)
					{
						sb.Append(Environment.NewLine);
					}
					sb.Append("  at ");
					string internalMethodName = frame.GetInternalMethodName();
					if (internalMethodName != null)
					{
						sb.Append(internalMethodName);
						goto IL_180;
					}
					sb.AppendFormat("<0x{0:x5} + 0x{1:x5}> <unknown method>", frame.GetMethodAddress(), frame.GetNativeOffset());
					goto IL_180;
				}
				else
				{
					bool flag2;
					this.GetFullNameForStackTrace(sb, frame.GetMethod(), flag || separator, out flag2, out isAsync);
					if (!flag2)
					{
						if (frame.GetILOffset() == -1)
						{
							sb.AppendFormat(" <0x{0:x5} + 0x{1:x5}>", frame.GetMethodAddress(), frame.GetNativeOffset());
							if (frame.GetMethodIndex() != 16777215U)
							{
								sb.AppendFormat(" {0}", frame.GetMethodIndex());
							}
						}
						else
						{
							sb.AppendFormat(" [0x{0:x5}]", frame.GetILOffset());
						}
						string text = frame.GetSecureFileName();
						if (text[0] == '<')
						{
							string arg = frame.GetMethod().Module.ModuleVersionId.ToString("N");
							string aotId = StackTrace.GetAotId();
							if (frame.GetILOffset() != -1 || aotId == null)
							{
								text = string.Format("<{0}>", arg);
							}
							else
							{
								text = string.Format("<{0}#{1}>", arg, aotId);
							}
						}
						sb.AppendFormat(" in {0}:{1} ", text, frame.GetFileLineNumber());
						goto IL_180;
					}
				}
				IL_182:
				i++;
				continue;
				IL_180:
				flag = true;
				goto IL_182;
			}
			return flag;
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x001339F4 File Offset: 0x00131BF4
		private void GetFullNameForStackTrace(StringBuilder sb, MethodBase mi, bool needsNewLine, out bool skipped, out bool isAsync)
		{
			Type type = mi.DeclaringType;
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				type = type.GetGenericTypeDefinition();
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.MetadataToken == mi.MetadataToken)
					{
						mi = methodInfo;
						break;
					}
				}
			}
			isAsync = typeof(IAsyncStateMachine).IsAssignableFrom(type);
			skipped = (mi.IsDefined(typeof(StackTraceHiddenAttribute)) || type.IsDefined(typeof(StackTraceHiddenAttribute)));
			if (skipped)
			{
				return;
			}
			if (isAsync)
			{
				StackTrace.ConvertAsyncStateMachineMethod(ref mi, ref type);
			}
			if (needsNewLine)
			{
				sb.Append(Environment.NewLine);
			}
			sb.Append("  at ");
			sb.Append(type.ToString());
			sb.Append(".");
			sb.Append(mi.Name);
			if (mi.IsGenericMethod)
			{
				mi = ((MethodInfo)mi).GetGenericMethodDefinition();
				Type[] genericArguments = mi.GetGenericArguments();
				sb.Append("[");
				for (int j = 0; j < genericArguments.Length; j++)
				{
					if (j > 0)
					{
						sb.Append(",");
					}
					sb.Append(genericArguments[j].Name);
				}
				sb.Append("]");
			}
			ParameterInfo[] parameters = mi.GetParameters();
			sb.Append(" (");
			for (int k = 0; k < parameters.Length; k++)
			{
				if (k > 0)
				{
					sb.Append(", ");
				}
				Type type2 = parameters[k].ParameterType;
				if (type2.IsGenericType && !type2.IsGenericTypeDefinition)
				{
					type2 = type2.GetGenericTypeDefinition();
				}
				sb.Append(type2.ToString());
				if (parameters[k].Name != null)
				{
					sb.Append(" ");
					sb.Append(parameters[k].Name);
				}
			}
			sb.Append(")");
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x00133BE8 File Offset: 0x00131DE8
		private static void ConvertAsyncStateMachineMethod(ref MethodBase method, ref Type declaringType)
		{
			Type declaringType2 = declaringType.DeclaringType;
			if (declaringType2 == null)
			{
				return;
			}
			MethodInfo[] methods = declaringType2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methods == null)
			{
				return;
			}
			foreach (MethodInfo methodInfo in methods)
			{
				IEnumerable<AsyncStateMachineAttribute> customAttributes = methodInfo.GetCustomAttributes<AsyncStateMachineAttribute>();
				if (customAttributes != null)
				{
					using (IEnumerator<AsyncStateMachineAttribute> enumerator = customAttributes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.StateMachineType == declaringType)
							{
								method = methodInfo;
								declaringType = methodInfo.DeclaringType;
								return;
							}
						}
					}
				}
			}
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		// Token: 0x060059F9 RID: 23033 RVA: 0x00133C8C File Offset: 0x00131E8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			if (this.captured_traces != null)
			{
				StackTrace[] array = this.captured_traces;
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2;
					flag = array[i].AddFrames(stringBuilder, flag, out flag2);
					if (flag && !flag2)
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append("--- End of stack trace from previous location where exception was thrown ---");
						stringBuilder.Append(Environment.NewLine);
					}
				}
			}
			bool flag3;
			this.AddFrames(stringBuilder, flag, out flag3);
			return stringBuilder.ToString();
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x000559F4 File Offset: 0x00053BF4
		internal string ToString(StackTrace.TraceFormat traceFormat)
		{
			return this.ToString();
		}

		/// <summary>Defines the default for the number of methods to omit from the stack trace. This field is constant.</summary>
		// Token: 0x04003798 RID: 14232
		public const int METHODS_TO_SKIP = 0;

		// Token: 0x04003799 RID: 14233
		private const string prefix = "  at ";

		// Token: 0x0400379A RID: 14234
		private StackFrame[] frames;

		// Token: 0x0400379B RID: 14235
		private readonly StackTrace[] captured_traces;

		// Token: 0x0400379C RID: 14236
		private bool debug_info;

		// Token: 0x0400379D RID: 14237
		private static bool isAotidSet;

		// Token: 0x0400379E RID: 14238
		private static string aotid;

		// Token: 0x020009C3 RID: 2499
		internal enum TraceFormat
		{
			// Token: 0x040037A0 RID: 14240
			Normal,
			// Token: 0x040037A1 RID: 14241
			TrailingNewLine,
			// Token: 0x040037A2 RID: 14242
			NoResourceLookup
		}
	}
}
