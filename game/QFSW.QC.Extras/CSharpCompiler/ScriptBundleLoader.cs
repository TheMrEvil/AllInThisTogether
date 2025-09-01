using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace CSharpCompiler
{
	// Token: 0x02000006 RID: 6
	public class ScriptBundleLoader
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public ScriptBundleLoader(ISynchronizeInvoke synchronizedInvoke)
		{
			this.synchronizedInvoke = synchronizedInvoke;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C6C File Offset: 0x00000E6C
		public ScriptBundleLoader.ScriptBundle LoadAndWatchScriptsBundle(IEnumerable<string> fileSources)
		{
			ScriptBundleLoader.ScriptBundle scriptBundle = new ScriptBundleLoader.ScriptBundle(this, fileSources);
			this.allFilesBundle.Add(scriptBundle);
			return scriptBundle;
		}

		// Token: 0x04000008 RID: 8
		public Func<Type, object> createInstance = (Type type) => Activator.CreateInstance(type);

		// Token: 0x04000009 RID: 9
		public Action<object> destroyInstance = delegate(object <p0>)
		{
		};

		// Token: 0x0400000A RID: 10
		public TextWriter logWriter = Console.Out;

		// Token: 0x0400000B RID: 11
		private ISynchronizeInvoke synchronizedInvoke;

		// Token: 0x0400000C RID: 12
		private List<ScriptBundleLoader.ScriptBundle> allFilesBundle = new List<ScriptBundleLoader.ScriptBundle>();

		// Token: 0x02000015 RID: 21
		public class ScriptBundle
		{
			// Token: 0x0600005B RID: 91 RVA: 0x0000379C File Offset: 0x0000199C
			public ScriptBundle(ScriptBundleLoader manager, IEnumerable<string> filePaths)
			{
				this.filePaths = from x in filePaths
				select Path.GetFullPath(x);
				this.manager = manager;
				AppDomain currentDomain = AppDomain.CurrentDomain;
				this.assemblyReferences = (from a in currentDomain.GetAssemblies()
				where !(a is AssemblyBuilder) && !string.IsNullOrEmpty(a.Location)
				select a.Location).ToArray<string>();
				manager.logWriter.WriteLine("loading " + string.Join(", ", filePaths.ToArray<string>()));
				this.CompileFiles();
				this.CreateFileWatchers();
				this.CreateNewInstances();
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00003890 File Offset: 0x00001A90
			private void CompileFiles()
			{
				this.filePaths = (from x in this.filePaths
				where File.Exists(x)
				select x).ToArray<string>();
				CompilerParameters compilerParameters = new CompilerParameters();
				compilerParameters.GenerateExecutable = false;
				compilerParameters.GenerateInMemory = true;
				compilerParameters.ReferencedAssemblies.AddRange(this.assemblyReferences);
				CompilerResults compilerResults = new CodeCompiler().CompileAssemblyFromFileBatch(compilerParameters, this.filePaths.ToArray<string>());
				foreach (object value in compilerResults.Errors)
				{
					this.manager.logWriter.WriteLine(value);
				}
				this.assembly = compilerResults.CompiledAssembly;
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00003970 File Offset: 0x00001B70
			private void CreateFileWatchers()
			{
				foreach (string path in this.filePaths)
				{
					FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
					this.fileSystemWatchers.Add(fileSystemWatcher);
					fileSystemWatcher.Path = Path.GetDirectoryName(path);
					fileSystemWatcher.NotifyFilter = (NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite);
					fileSystemWatcher.Filter = Path.GetFileName(path);
					fileSystemWatcher.Changed += delegate(object o, FileSystemEventArgs a)
					{
						this.Reload(false);
					};
					fileSystemWatcher.Deleted += delegate(object o, FileSystemEventArgs a)
					{
						this.Reload(false);
					};
					fileSystemWatcher.Renamed += delegate(object o, RenamedEventArgs a)
					{
						this.filePaths = this.filePaths.Select(delegate(string x)
						{
							if (x == a.OldFullPath)
							{
								return a.FullPath;
							}
							return x;
						});
						this.Reload(true);
					};
					fileSystemWatcher.SynchronizingObject = this.manager.synchronizedInvoke;
					fileSystemWatcher.EnableRaisingEvents = true;
				}
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00003A3C File Offset: 0x00001C3C
			private void StopFileWatchers()
			{
				foreach (FileSystemWatcher fileSystemWatcher in this.fileSystemWatchers)
				{
					fileSystemWatcher.EnableRaisingEvents = false;
					fileSystemWatcher.Dispose();
				}
				this.fileSystemWatchers.Clear();
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00003AA0 File Offset: 0x00001CA0
			private void Reload(bool recreateWatchers = false)
			{
				this.manager.logWriter.WriteLine("reloading " + string.Join(", ", this.filePaths.ToArray<string>()));
				this.StopInstances();
				this.CompileFiles();
				this.CreateNewInstances();
				if (recreateWatchers)
				{
					this.StopFileWatchers();
					this.CreateFileWatchers();
				}
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00003B00 File Offset: 0x00001D00
			private void CreateNewInstances()
			{
				if (this.assembly == null)
				{
					return;
				}
				Type[] types = this.assembly.GetTypes();
				for (int i = 0; i < types.Length; i++)
				{
					Type type = types[i];
					this.manager.synchronizedInvoke.Invoke(new Action(delegate
					{
						this.instances.Add(this.manager.createInstance(type));
					}), null);
				}
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00003B6C File Offset: 0x00001D6C
			private void StopInstances()
			{
				using (List<object>.Enumerator enumerator = this.instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object instance = enumerator.Current;
						this.manager.synchronizedInvoke.Invoke(new Action(delegate
						{
							this.manager.destroyInstance(instance);
						}), null);
					}
				}
				this.instances.Clear();
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00003BF4 File Offset: 0x00001DF4
			[CompilerGenerated]
			private void <CreateFileWatchers>b__8_0(object o, FileSystemEventArgs a)
			{
				this.Reload(false);
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00003BFD File Offset: 0x00001DFD
			[CompilerGenerated]
			private void <CreateFileWatchers>b__8_1(object o, FileSystemEventArgs a)
			{
				this.Reload(false);
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00003C08 File Offset: 0x00001E08
			[CompilerGenerated]
			private void <CreateFileWatchers>b__8_2(object o, RenamedEventArgs a)
			{
				this.filePaths = this.filePaths.Select(delegate(string x)
				{
					if (x == a.OldFullPath)
					{
						return a.FullPath;
					}
					return x;
				});
				this.Reload(true);
			}

			// Token: 0x0400001A RID: 26
			private Assembly assembly;

			// Token: 0x0400001B RID: 27
			private IEnumerable<string> filePaths;

			// Token: 0x0400001C RID: 28
			private List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();

			// Token: 0x0400001D RID: 29
			private List<object> instances = new List<object>();

			// Token: 0x0400001E RID: 30
			private ScriptBundleLoader manager;

			// Token: 0x0400001F RID: 31
			private string[] assemblyReferences;

			// Token: 0x02000023 RID: 35
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000099 RID: 153 RVA: 0x0000446B File Offset: 0x0000266B
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600009A RID: 154 RVA: 0x00004477 File Offset: 0x00002677
				public <>c()
				{
				}

				// Token: 0x0600009B RID: 155 RVA: 0x0000447F File Offset: 0x0000267F
				internal string <.ctor>b__6_0(string x)
				{
					return Path.GetFullPath(x);
				}

				// Token: 0x0600009C RID: 156 RVA: 0x00004487 File Offset: 0x00002687
				internal bool <.ctor>b__6_1(Assembly a)
				{
					return !(a is AssemblyBuilder) && !string.IsNullOrEmpty(a.Location);
				}

				// Token: 0x0600009D RID: 157 RVA: 0x000044A1 File Offset: 0x000026A1
				internal string <.ctor>b__6_2(Assembly a)
				{
					return a.Location;
				}

				// Token: 0x0600009E RID: 158 RVA: 0x000044A9 File Offset: 0x000026A9
				internal bool <CompileFiles>b__7_0(string x)
				{
					return File.Exists(x);
				}

				// Token: 0x04000053 RID: 83
				public static readonly ScriptBundleLoader.ScriptBundle.<>c <>9 = new ScriptBundleLoader.ScriptBundle.<>c();

				// Token: 0x04000054 RID: 84
				public static Func<string, string> <>9__6_0;

				// Token: 0x04000055 RID: 85
				public static Func<Assembly, bool> <>9__6_1;

				// Token: 0x04000056 RID: 86
				public static Func<Assembly, string> <>9__6_2;

				// Token: 0x04000057 RID: 87
				public static Func<string, bool> <>9__7_0;
			}

			// Token: 0x02000024 RID: 36
			[CompilerGenerated]
			private sealed class <>c__DisplayClass8_0
			{
				// Token: 0x0600009F RID: 159 RVA: 0x000044B1 File Offset: 0x000026B1
				public <>c__DisplayClass8_0()
				{
				}

				// Token: 0x060000A0 RID: 160 RVA: 0x000044B9 File Offset: 0x000026B9
				internal string <CreateFileWatchers>b__3(string x)
				{
					if (x == this.a.OldFullPath)
					{
						return this.a.FullPath;
					}
					return x;
				}

				// Token: 0x04000058 RID: 88
				public RenamedEventArgs a;
			}

			// Token: 0x02000025 RID: 37
			[CompilerGenerated]
			private sealed class <>c__DisplayClass11_0
			{
				// Token: 0x060000A1 RID: 161 RVA: 0x000044DB File Offset: 0x000026DB
				public <>c__DisplayClass11_0()
				{
				}

				// Token: 0x060000A2 RID: 162 RVA: 0x000044E3 File Offset: 0x000026E3
				internal void <CreateNewInstances>b__0()
				{
					this.<>4__this.instances.Add(this.<>4__this.manager.createInstance(this.type));
				}

				// Token: 0x04000059 RID: 89
				public Type type;

				// Token: 0x0400005A RID: 90
				public ScriptBundleLoader.ScriptBundle <>4__this;
			}

			// Token: 0x02000026 RID: 38
			[CompilerGenerated]
			private sealed class <>c__DisplayClass12_0
			{
				// Token: 0x060000A3 RID: 163 RVA: 0x00004510 File Offset: 0x00002710
				public <>c__DisplayClass12_0()
				{
				}

				// Token: 0x060000A4 RID: 164 RVA: 0x00004518 File Offset: 0x00002718
				internal void <StopInstances>b__0()
				{
					this.<>4__this.manager.destroyInstance(this.instance);
				}

				// Token: 0x0400005B RID: 91
				public object instance;

				// Token: 0x0400005C RID: 92
				public ScriptBundleLoader.ScriptBundle <>4__this;
			}
		}

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000065 RID: 101 RVA: 0x00003C46 File Offset: 0x00001E46
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000066 RID: 102 RVA: 0x00003C52 File Offset: 0x00001E52
			public <>c()
			{
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00003C5A File Offset: 0x00001E5A
			internal object <.ctor>b__5_0(Type type)
			{
				return Activator.CreateInstance(type);
			}

			// Token: 0x06000068 RID: 104 RVA: 0x00003C62 File Offset: 0x00001E62
			internal void <.ctor>b__5_1(object <p0>)
			{
			}

			// Token: 0x04000020 RID: 32
			public static readonly ScriptBundleLoader.<>c <>9 = new ScriptBundleLoader.<>c();

			// Token: 0x04000021 RID: 33
			public static Func<Type, object> <>9__5_0;

			// Token: 0x04000022 RID: 34
			public static Action<object> <>9__5_1;
		}
	}
}
