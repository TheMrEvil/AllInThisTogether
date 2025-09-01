using System;
using System.IO;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> interface.</summary>
	// Token: 0x02000342 RID: 834
	public abstract class CodeCompiler : CodeGenerator, ICodeCompiler
	{
		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDom(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit)" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06001A81 RID: 6785 RVA: 0x00061C40 File Offset: 0x0005FE40
		CompilerResults ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromDom(options, e);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFile(System.CodeDom.Compiler.CompilerParameters,System.String)" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileName">The file name to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06001A82 RID: 6786 RVA: 0x00061C84 File Offset: 0x0005FE84
		CompilerResults ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromFile(options, fileName);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSource(System.CodeDom.Compiler.CompilerParameters,System.String)" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="source">A string that indicates the source code to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06001A83 RID: 6787 RVA: 0x00061CC8 File Offset: 0x0005FEC8
		CompilerResults ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromSource(options, source);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSourceBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="sources">An array of strings that indicates the source code to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06001A84 RID: 6788 RVA: 0x00061D0C File Offset: 0x0005FF0C
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFileBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileNames">An array of strings that indicates the file names to compile.</param>
		/// <returns>The results of compilation.</returns>
		// Token: 0x06001A85 RID: 6789 RVA: 0x00061D50 File Offset: 0x0005FF50
		CompilerResults ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults result;
			try
			{
				for (int i = 0; i < fileNames.Length; i++)
				{
					File.OpenRead(fileNames[i]).Dispose();
				}
				result = this.FromFileBatch(options, fileNames);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDomBatch(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit[])" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		// Token: 0x06001A86 RID: 6790 RVA: 0x00061DC0 File Offset: 0x0005FFC0
		CompilerResults ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults result;
			try
			{
				result = this.FromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return result;
		}

		/// <summary>Gets the file name extension to use for source files.</summary>
		/// <returns>The file name extension to use for source files.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001A87 RID: 6791
		protected abstract string FileExtension { get; }

		/// <summary>Gets the name of the compiler executable.</summary>
		/// <returns>The name of the compiler executable.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001A88 RID: 6792
		protected abstract string CompilerName { get; }

		/// <summary>Compiles the specified compile unit using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> object that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06001A89 RID: 6793 RVA: 0x00061E04 File Offset: 0x00060004
		protected virtual CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromDomBatch(options, new CodeCompileUnit[]
			{
				e
			});
		}

		/// <summary>Compiles the specified file using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileName">The file name to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fileName" /> is <see langword="null" />.</exception>
		// Token: 0x06001A8A RID: 6794 RVA: 0x00061E25 File Offset: 0x00060025
		protected virtual CompilerResults FromFile(CompilerParameters options, string fileName)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			File.OpenRead(fileName).Dispose();
			return this.FromFileBatch(options, new string[]
			{
				fileName
			});
		}

		/// <summary>Compiles the specified source code string using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="source">The source code string to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.</exception>
		// Token: 0x06001A8B RID: 6795 RVA: 0x00061E5F File Offset: 0x0006005F
		protected virtual CompilerResults FromSource(CompilerParameters options, string source)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return this.FromSourceBatch(options, new string[]
			{
				source
			});
		}

		/// <summary>Compiles the specified compile units using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ea" /> is <see langword="null" />.</exception>
		// Token: 0x06001A8C RID: 6796 RVA: 0x00061E80 File Offset: 0x00060080
		protected virtual CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (ea == null)
			{
				throw new ArgumentNullException("ea");
			}
			string[] array = new string[ea.Length];
			for (int i = 0; i < ea.Length; i++)
			{
				if (ea[i] != null)
				{
					this.ResolveReferencedAssemblies(options, ea[i]);
					array[i] = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
					using (FileStream fileStream = new FileStream(array[i], FileMode.Create, FileAccess.Write, FileShare.Read))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
						{
							((ICodeGenerator)this).GenerateCodeFromCompileUnit(ea[i], streamWriter, base.Options);
							streamWriter.Flush();
						}
					}
				}
			}
			return this.FromFileBatch(options, array);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00061F58 File Offset: 0x00060158
		private void ResolveReferencedAssemblies(CompilerParameters options, CodeCompileUnit e)
		{
			if (e.ReferencedAssemblies.Count > 0)
			{
				foreach (string value in e.ReferencedAssemblies)
				{
					if (!options.ReferencedAssemblies.Contains(value))
					{
						options.ReferencedAssemblies.Add(value);
					}
				}
			}
		}

		/// <summary>Compiles the specified files using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="fileNames">An array of strings that indicates the file names of the files to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fileNames" /> is <see langword="null" />.</exception>
		// Token: 0x06001A8E RID: 6798 RVA: 0x00061FD0 File Offset: 0x000601D0
		protected virtual CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			throw new PlatformNotSupportedException();
		}

		/// <summary>Processes the specified line from the specified <see cref="T:System.CodeDom.Compiler.CompilerResults" />.</summary>
		/// <param name="results">A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> that indicates the results of compilation.</param>
		/// <param name="line">The line to process.</param>
		// Token: 0x06001A8F RID: 6799
		protected abstract void ProcessCompilerOutputLine(CompilerResults results, string line);

		/// <summary>Gets the command arguments to be passed to the compiler from the specified <see cref="T:System.CodeDom.Compiler.CompilerParameters" />.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> that indicates the compiler options.</param>
		/// <returns>The command arguments.</returns>
		// Token: 0x06001A90 RID: 6800
		protected abstract string CmdArgsFromParameters(CompilerParameters options);

		/// <summary>Gets the command arguments to use when invoking the compiler to generate a response file.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="cmdArgs">A command arguments string.</param>
		/// <returns>The command arguments to use to generate a response file, or <see langword="null" /> if there are no response file arguments.</returns>
		// Token: 0x06001A91 RID: 6801 RVA: 0x00061FF4 File Offset: 0x000601F4
		protected virtual string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
		{
			string text = options.TempFiles.AddExtension("cmdline");
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
				{
					streamWriter.Write(cmdArgs);
					streamWriter.Flush();
				}
			}
			return "@\"" + text + "\"";
		}

		/// <summary>Compiles the specified source code strings using the specified options, and returns the results from the compilation.</summary>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options.</param>
		/// <param name="sources">An array of strings containing the source code to compile.</param>
		/// <returns>The results of compilation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="sources" /> is <see langword="null" />.</exception>
		// Token: 0x06001A92 RID: 6802 RVA: 0x00062078 File Offset: 0x00060278
		protected virtual CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			string[] array = new string[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				string text = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
				using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
					{
						streamWriter.Write(sources[i]);
						streamWriter.Flush();
					}
				}
				array[i] = text;
			}
			return this.FromFileBatch(options, array);
		}

		/// <summary>Joins the specified string arrays.</summary>
		/// <param name="sa">The array of strings to join.</param>
		/// <param name="separator">The separator to use.</param>
		/// <returns>The concatenated string.</returns>
		// Token: 0x06001A93 RID: 6803 RVA: 0x00062140 File Offset: 0x00060340
		protected static string JoinStringArray(string[] sa, string separator)
		{
			if (sa == null || sa.Length == 0)
			{
				return string.Empty;
			}
			if (sa.Length == 1)
			{
				return "\"" + sa[0] + "\"";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < sa.Length - 1; i++)
			{
				stringBuilder.Append('"');
				stringBuilder.Append(sa[i]);
				stringBuilder.Append('"');
				stringBuilder.Append(separator);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(sa[sa.Length - 1]);
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CodeCompiler" /> class.</summary>
		// Token: 0x06001A94 RID: 6804 RVA: 0x000621D3 File Offset: 0x000603D3
		protected CodeCompiler()
		{
		}
	}
}
