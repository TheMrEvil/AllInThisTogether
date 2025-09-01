using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
	/// <summary>Stores all the resources localized for one particular culture, ignoring all other cultures, including any fallback rules.</summary>
	// Token: 0x0200086E RID: 2158
	[ComVisible(true)]
	[Serializable]
	public class ResourceSet : IDisposable, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceSet" /> class with default properties.</summary>
		// Token: 0x060047E4 RID: 18404 RVA: 0x000EC051 File Offset: 0x000EA251
		protected ResourceSet()
		{
			this.CommonInit();
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x0000259F File Offset: 0x0000079F
		internal ResourceSet(bool junk)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the system default <see cref="T:System.Resources.ResourceReader" /> that opens and reads resources from the given file.</summary>
		/// <param name="fileName">Resource file to read.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060047E6 RID: 18406 RVA: 0x000EC05F File Offset: 0x000EA25F
		public ResourceSet(string fileName)
		{
			this.Reader = new ResourceReader(fileName);
			this.CommonInit();
			this.ReadResources();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the system default <see cref="T:System.Resources.ResourceReader" /> that reads resources from the given stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> of resources to be read. The stream should refer to an existing resources file.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is not readable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060047E7 RID: 18407 RVA: 0x000EC07F File Offset: 0x000EA27F
		[SecurityCritical]
		public ResourceSet(Stream stream)
		{
			this.Reader = new ResourceReader(stream);
			this.CommonInit();
			this.ReadResources();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Resources.ResourceSet" /> class using the specified resource reader.</summary>
		/// <param name="reader">The reader that will be used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="reader" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060047E8 RID: 18408 RVA: 0x000EC09F File Offset: 0x000EA29F
		public ResourceSet(IResourceReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x000EC0C8 File Offset: 0x000EA2C8
		private void CommonInit()
		{
			this.Table = new Hashtable();
		}

		/// <summary>Closes and releases any resources used by this <see cref="T:System.Resources.ResourceSet" />.</summary>
		// Token: 0x060047EA RID: 18410 RVA: 0x000EC0D5 File Offset: 0x000EA2D5
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases resources (other than memory) associated with the current instance, closing internal managed objects if requested.</summary>
		/// <param name="disposing">Indicates whether the objects contained in the current instance should be explicitly closed.</param>
		// Token: 0x060047EB RID: 18411 RVA: 0x000EC0E0 File Offset: 0x000EA2E0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		/// <summary>Disposes of the resources (other than memory) used by the current instance of <see cref="T:System.Resources.ResourceSet" />.</summary>
		// Token: 0x060047EC RID: 18412 RVA: 0x000EC0D5 File Offset: 0x000EA2D5
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Returns the preferred resource reader class for this kind of <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> for the preferred resource reader for this kind of <see cref="T:System.Resources.ResourceSet" />.</returns>
		// Token: 0x060047ED RID: 18413 RVA: 0x000EC11C File Offset: 0x000EA31C
		public virtual Type GetDefaultReader()
		{
			return typeof(ResourceReader);
		}

		/// <summary>Returns the preferred resource writer class for this kind of <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> for the preferred resource writer for this kind of <see cref="T:System.Resources.ResourceSet" />.</returns>
		// Token: 0x060047EE RID: 18414 RVA: 0x000EC128 File Offset: 0x000EA328
		public virtual Type GetDefaultWriter()
		{
			return typeof(ResourceWriter);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through the <see cref="T:System.Resources.ResourceSet" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for this <see cref="T:System.Resources.ResourceSet" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The resource set has been closed or disposed.</exception>
		// Token: 0x060047EF RID: 18415 RVA: 0x000EC134 File Offset: 0x000EA334
		[ComVisible(false)]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> object to avoid a race condition with <see langword="Dispose" />. This member is not intended to be used directly from your code.</summary>
		/// <returns>An enumerator for the current <see cref="T:System.Resources.ResourceSet" /> object.</returns>
		// Token: 0x060047F0 RID: 18416 RVA: 0x000EC134 File Offset: 0x000EA334
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x000EC13C File Offset: 0x000EA33C
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table.GetEnumerator();
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name.</summary>
		/// <param name="name">Name of the resource to search for.</param>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x060047F2 RID: 18418 RVA: 0x000EC160 File Offset: 0x000EA360
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string result;
			try
			{
				result = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[]
				{
					name
				}));
			}
			return result;
		}

		/// <summary>Searches for a <see cref="T:System.String" /> resource with the specified name in a case-insensitive manner, if requested.</summary>
		/// <param name="name">Name of the resource to search for.</param>
		/// <param name="ignoreCase">Indicates whether the case of the case of the specified name should be ignored.</param>
		/// <returns>The value of a resource, if the value is a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by <paramref name="name" /> is not a <see cref="T:System.String" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x060047F3 RID: 18419 RVA: 0x000EC1AC File Offset: 0x000EA3AC
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[]
				{
					name
				}));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string result;
			try
			{
				result = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Resource '{0}' was not a String - call GetObject instead.", new object[]
				{
					name
				}));
			}
			return result;
		}

		/// <summary>Searches for a resource object with the specified name.</summary>
		/// <param name="name">Case-sensitive name of the resource to search for.</param>
		/// <returns>The requested resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x060047F4 RID: 18420 RVA: 0x000EC238 File Offset: 0x000EA438
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		/// <summary>Searches for a resource object with the specified name in a case-insensitive manner, if requested.</summary>
		/// <param name="name">Name of the resource to search for.</param>
		/// <param name="ignoreCase">Indicates whether the case of the specified name should be ignored.</param>
		/// <returns>The requested resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been closed or disposed.</exception>
		// Token: 0x060047F5 RID: 18421 RVA: 0x000EC244 File Offset: 0x000EA444
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		/// <summary>Reads all the resources and stores them in a <see cref="T:System.Collections.Hashtable" /> indicated in the <see cref="F:System.Resources.ResourceSet.Table" /> property.</summary>
		// Token: 0x060047F6 RID: 18422 RVA: 0x000EC268 File Offset: 0x000EA468
		protected virtual void ReadResources()
		{
			IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				this.Table.Add(enumerator.Key, value);
			}
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x000EC2A4 File Offset: 0x000EA4A4
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			return table[name];
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x000EC2D4 File Offset: 0x000EA4D4
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed resource set."));
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		/// <summary>Indicates the <see cref="T:System.Resources.IResourceReader" /> used to read the resources.</summary>
		// Token: 0x04002E03 RID: 11779
		[NonSerialized]
		protected IResourceReader Reader;

		/// <summary>The <see cref="T:System.Collections.Hashtable" /> in which the resources are stored.</summary>
		// Token: 0x04002E04 RID: 11780
		protected Hashtable Table;

		// Token: 0x04002E05 RID: 11781
		private Hashtable _caseInsensitiveTable;
	}
}
