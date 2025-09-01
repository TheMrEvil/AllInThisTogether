using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides command execution functions for invoking compilers. This class cannot be inherited.</summary>
	// Token: 0x0200035C RID: 860
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public static class Executor
	{
		/// <summary>Executes the command using the specified temporary files and waits for the call to return.</summary>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		// Token: 0x06001C8C RID: 7308 RVA: 0x00067518 File Offset: 0x00065718
		public static void ExecWait(string cmd, TempFileCollection tempFiles)
		{
			string text = null;
			string text2 = null;
			Executor.ExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref text, ref text2);
		}

		/// <summary>Executes the specified command using the specified user token, current directory, and temporary files; then waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="userToken">The token to start the compiler process with.</param>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="currentDir">The directory to start the process in.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06001C8D RID: 7309 RVA: 0x0006753C File Offset: 0x0006573C
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			int result;
			using (WindowsIdentity.Impersonate(userToken))
			{
				result = Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
			}
			return result;
		}

		/// <summary>Executes the specified command using the specified user token and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="userToken">The token to start the compiler process with.</param>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06001C8E RID: 7310 RVA: 0x0006757C File Offset: 0x0006577C
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(userToken, cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified current directory and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="currentDir">The current directory.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06001C8F RID: 7311 RVA: 0x0006758E File Offset: 0x0006578E
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified temporary files and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06001C90 RID: 7312 RVA: 0x0006759B File Offset: 0x0006579B
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static int ExecWaitWithCapture(string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000675AC File Offset: 0x000657AC
		private static int InternalExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			if (cmd == null || cmd.Length == 0)
			{
				throw new ExternalException(Locale.GetText("No command provided for execution."));
			}
			if (outputName == null)
			{
				outputName = tempFiles.AddExtension("out");
			}
			if (errorName == null)
			{
				errorName = tempFiles.AddExtension("err");
			}
			int result = -1;
			Process process = new Process();
			process.StartInfo.FileName = cmd;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.WorkingDirectory = currentDir;
			try
			{
				process.Start();
				Executor.ProcessResultReader processResultReader = new Executor.ProcessResultReader(process.StandardOutput, outputName);
				Thread thread = new Thread(new ThreadStart(new Executor.ProcessResultReader(process.StandardError, errorName).Read));
				thread.Start();
				processResultReader.Read();
				thread.Join();
				process.WaitForExit();
			}
			finally
			{
				result = process.ExitCode;
				process.Close();
			}
			return result;
		}

		// Token: 0x0200035D RID: 861
		private class ProcessResultReader
		{
			// Token: 0x06001C92 RID: 7314 RVA: 0x000676B4 File Offset: 0x000658B4
			public ProcessResultReader(StreamReader reader, string file)
			{
				this.reader = reader;
				this.file = file;
			}

			// Token: 0x06001C93 RID: 7315 RVA: 0x000676CC File Offset: 0x000658CC
			public void Read()
			{
				StreamWriter streamWriter = new StreamWriter(this.file);
				try
				{
					string value;
					while ((value = this.reader.ReadLine()) != null)
					{
						streamWriter.WriteLine(value);
					}
				}
				finally
				{
					streamWriter.Close();
				}
			}

			// Token: 0x04000E88 RID: 3720
			private StreamReader reader;

			// Token: 0x04000E89 RID: 3721
			private string file;
		}
	}
}
