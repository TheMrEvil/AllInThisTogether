using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Threading;

namespace System.Resources
{
	// Token: 0x02000865 RID: 2149
	internal class FileBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x0600476C RID: 18284 RVA: 0x000E895C File Offset: 0x000E6B5C
		public FileBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x000E896C File Offset: 0x000E6B6C
		[SecuritySafeCritical]
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
		{
			ResourceSet result = null;
			string resourceFileName = this._mediator.GetResourceFileName(culture);
			string text = this.FindResourceFile(culture, resourceFileName);
			if (text == null)
			{
				if (tryParents && culture.HasInvariantCultureName)
				{
					throw new MissingManifestResourceException(string.Concat(new string[]
					{
						Environment.GetResourceString("Could not find any resources appropriate for the specified culture (or the neutral culture) on disk."),
						Environment.NewLine,
						"baseName: ",
						this._mediator.BaseNameField,
						"  locationInfo: ",
						(this._mediator.LocationInfo == null) ? "<null>" : this._mediator.LocationInfo.FullName,
						"  fileName: ",
						this._mediator.GetResourceFileName(culture)
					}));
				}
			}
			else
			{
				result = this.CreateResourceSet(text);
			}
			return result;
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x000E8A3C File Offset: 0x000E6C3C
		public bool HasNeutralResources(CultureInfo culture, string defaultResName)
		{
			string text = this.FindResourceFile(culture, defaultResName);
			if (text == null || !File.Exists(text))
			{
				string moduleDir = this._mediator.ModuleDir;
				if (text != null)
				{
					Path.GetDirectoryName(text);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x000E8A78 File Offset: 0x000E6C78
		private string FindResourceFile(CultureInfo culture, string fileName)
		{
			if (this._mediator.ModuleDir != null)
			{
				string text = Path.Combine(this._mediator.ModuleDir, fileName);
				if (File.Exists(text))
				{
					return text;
				}
			}
			if (File.Exists(fileName))
			{
				return fileName;
			}
			return null;
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x000E8ABC File Offset: 0x000E6CBC
		[SecurityCritical]
		private ResourceSet CreateResourceSet(string file)
		{
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(file);
			}
			object[] args = new object[]
			{
				file
			};
			ResourceSet result;
			try
			{
				result = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, args);
			}
			catch (MissingMethodException innerException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("'{0}': ResourceSet derived classes must provide a constructor that takes a String file name and a constructor that takes a Stream.", new object[]
				{
					this._mediator.UserResourceSet.AssemblyQualifiedName
				}), innerException);
			}
			return result;
		}

		// Token: 0x04002DCC RID: 11724
		private ResourceManager.ResourceManagerMediator _mediator;
	}
}
