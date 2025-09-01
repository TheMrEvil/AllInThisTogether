using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Discovers attributed parts in the dynamic link library (DLL) and EXE files in an application's directory and path.</summary>
	// Token: 0x020000A0 RID: 160
	public class ApplicationCatalog : ComposablePartCatalog, ICompositionElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class.</summary>
		// Token: 0x06000431 RID: 1073 RVA: 0x0000BD36 File Offset: 0x00009F36
		public ApplicationCatalog()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class by using the specified source for parts.</summary>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		// Token: 0x06000432 RID: 1074 RVA: 0x0000BD49 File Offset: 0x00009F49
		public ApplicationCatalog(ICompositionElement definitionOrigin)
		{
			Requires.NotNull<ICompositionElement>(definitionOrigin, "definitionOrigin");
			this._definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class by using the specified reflection context.</summary>
		/// <param name="reflectionContext">The reflection context.</param>
		// Token: 0x06000433 RID: 1075 RVA: 0x0000BD6E File Offset: 0x00009F6E
		public ApplicationCatalog(ReflectionContext reflectionContext)
		{
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			this._reflectionContext = reflectionContext;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ApplicationCatalog" /> class by using the specified reflection context and source for parts.</summary>
		/// <param name="reflectionContext">The reflection context.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the source for parts.</param>
		// Token: 0x06000434 RID: 1076 RVA: 0x0000BD93 File Offset: 0x00009F93
		public ApplicationCatalog(ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			Requires.NotNull<ICompositionElement>(definitionOrigin, "definitionOrigin");
			this._reflectionContext = reflectionContext;
			this._definitionOrigin = definitionOrigin;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000BDCC File Offset: 0x00009FCC
		internal ComposablePartCatalog CreateCatalog(string location, string pattern)
		{
			if (this._reflectionContext != null)
			{
				if (this._definitionOrigin == null)
				{
					return new DirectoryCatalog(location, pattern, this._reflectionContext);
				}
				return new DirectoryCatalog(location, pattern, this._reflectionContext, this._definitionOrigin);
			}
			else
			{
				if (this._definitionOrigin == null)
				{
					return new DirectoryCatalog(location, pattern);
				}
				return new DirectoryCatalog(location, pattern, this._definitionOrigin);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000BE28 File Offset: 0x0000A028
		private AggregateCatalog InnerCatalog
		{
			get
			{
				if (this._innerCatalog == null)
				{
					object thisLock = this._thisLock;
					lock (thisLock)
					{
						if (this._innerCatalog == null)
						{
							string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
							Assumes.NotNull<string>(baseDirectory);
							List<ComposablePartCatalog> list = new List<ComposablePartCatalog>();
							list.Add(this.CreateCatalog(baseDirectory, "*.exe"));
							list.Add(this.CreateCatalog(baseDirectory, "*.dll"));
							string relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
							if (!string.IsNullOrEmpty(relativeSearchPath))
							{
								foreach (string path in relativeSearchPath.Split(new char[]
								{
									';'
								}, StringSplitOptions.RemoveEmptyEntries))
								{
									string text = Path.Combine(baseDirectory, path);
									if (Directory.Exists(text))
									{
										list.Add(this.CreateCatalog(text, "*.dll"));
									}
								}
							}
							AggregateCatalog innerCatalog = new AggregateCatalog(list);
							this._innerCatalog = innerCatalog;
						}
					}
				}
				return this._innerCatalog;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000437 RID: 1079 RVA: 0x0000BF3C File Offset: 0x0000A13C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._isDisposed)
				{
					IDisposable disposable = null;
					object thisLock = this._thisLock;
					lock (thisLock)
					{
						disposable = this._innerCatalog;
						this._innerCatalog = null;
						this._isDisposed = true;
					}
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000438 RID: 1080 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			this.ThrowIfDisposed();
			return this.InnerCatalog.GetEnumerator();
		}

		/// <summary>Gets the export definitions that match the constraint expressed by the specified import definition.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects to be returned.</param>
		/// <returns>A collection of objects that contain the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects that match the specified constraint.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		// Token: 0x06000439 RID: 1081 RVA: 0x0000BFCB File Offset: 0x0000A1CB
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			this.ThrowIfDisposed();
			Requires.NotNull<ImportDefinition>(definition, "definition");
			return this.InnerCatalog.GetExports(definition);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000BFEA File Offset: 0x0000A1EA
		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000BFFB File Offset: 0x0000A1FB
		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (Path=\"{1}\") (PrivateProbingPath=\"{2}\")", base.GetType().Name, AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath);
		}

		/// <summary>Retrieves a string representation of the application catalog.</summary>
		/// <returns>A string representation of the catalog.</returns>
		// Token: 0x0600043C RID: 1084 RVA: 0x0000C02B File Offset: 0x0000A22B
		public override string ToString()
		{
			return this.GetDisplayName();
		}

		/// <summary>Gets the display name of the application catalog.</summary>
		/// <returns>A string that contains a human-readable display name of the <see cref="T:System.ComponentModel.Composition.Hosting.DirectoryCatalog" /> object.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000C02B File Offset: 0x0000A22B
		string ICompositionElement.DisplayName
		{
			get
			{
				return this.GetDisplayName();
			}
		}

		/// <summary>Gets the composition element from which the application catalog originated.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000AD5C File Offset: 0x00008F5C
		ICompositionElement ICompositionElement.Origin
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040001A6 RID: 422
		private bool _isDisposed;

		// Token: 0x040001A7 RID: 423
		private volatile AggregateCatalog _innerCatalog;

		// Token: 0x040001A8 RID: 424
		private readonly object _thisLock = new object();

		// Token: 0x040001A9 RID: 425
		private ICompositionElement _definitionOrigin;

		// Token: 0x040001AA RID: 426
		private ReflectionContext _reflectionContext;
	}
}
