using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using ES3Internal;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class ES3Settings : ICloneable
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000144 RID: 324 RVA: 0x000058F6 File Offset: 0x00003AF6
	public static ES3Defaults defaultSettingsScriptableObject
	{
		get
		{
			if (ES3Settings._defaultSettingsScriptableObject == null)
			{
				ES3Settings._defaultSettingsScriptableObject = Resources.Load<ES3Defaults>("ES3/ES3Defaults");
			}
			return ES3Settings._defaultSettingsScriptableObject;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000145 RID: 325 RVA: 0x00005919 File Offset: 0x00003B19
	public static ES3Settings defaultSettings
	{
		get
		{
			if (ES3Settings._defaults == null && ES3Settings.defaultSettingsScriptableObject != null)
			{
				ES3Settings._defaults = ES3Settings.defaultSettingsScriptableObject.settings;
			}
			return ES3Settings._defaults;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000146 RID: 326 RVA: 0x00005943 File Offset: 0x00003B43
	internal static ES3Settings unencryptedUncompressedSettings
	{
		get
		{
			if (ES3Settings._unencryptedUncompressedSettings == null)
			{
				ES3Settings._unencryptedUncompressedSettings = new ES3Settings(new Enum[]
				{
					ES3.EncryptionType.None,
					ES3.CompressionType.None
				});
			}
			return ES3Settings._unencryptedUncompressedSettings;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000147 RID: 327 RVA: 0x00005973 File Offset: 0x00003B73
	// (set) Token: 0x06000148 RID: 328 RVA: 0x00005997 File Offset: 0x00003B97
	public ES3.Location location
	{
		get
		{
			if (this._location == ES3.Location.File && (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.tvOS))
			{
				return ES3.Location.PlayerPrefs;
			}
			return this._location;
		}
		set
		{
			this._location = value;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000149 RID: 329 RVA: 0x000059A0 File Offset: 0x00003BA0
	public string FullPath
	{
		get
		{
			if (this.path == null)
			{
				throw new NullReferenceException("The 'path' field of this ES3Settings is null, indicating that it was not possible to load the default settings from Resources. Please check that the ES3 Default Settings.prefab exists in Assets/Plugins/Resources/ES3/");
			}
			if (ES3Settings.IsAbsolute(this.path))
			{
				return this.path;
			}
			if (this.location == ES3.Location.File)
			{
				if (this.directory == ES3.Directory.PersistentDataPath)
				{
					return ES3IO.persistentDataPath + "/" + this.path;
				}
				if (this.directory == ES3.Directory.DataPath)
				{
					return Application.dataPath + "/" + this.path;
				}
				throw new NotImplementedException("File directory \"" + this.directory.ToString() + "\" has not been implemented.");
			}
			else
			{
				if (this.location != ES3.Location.Resources)
				{
					return this.path;
				}
				string extension = Path.GetExtension(this.path);
				bool flag = false;
				foreach (string b in ES3Settings.resourcesExtensions)
				{
					if (extension == b)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new ArgumentException("Extension of file in Resources must be .json, .bytes, .txt, .csv, .htm, .html, .xml, .yaml or .fnt, but path given was \"" + this.path + "\"");
				}
				return this.path.Replace(extension, "");
			}
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00005AB3 File Offset: 0x00003CB3
	public ES3Settings(string path = null, ES3Settings settings = null) : this(true)
	{
		if (settings != null)
		{
			settings.CopyInto(this);
		}
		if (path != null)
		{
			this.path = path;
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00005AD0 File Offset: 0x00003CD0
	public ES3Settings(string path, params Enum[] enums) : this(enums)
	{
		if (path != null)
		{
			this.path = path;
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00005AE4 File Offset: 0x00003CE4
	public ES3Settings(params Enum[] enums) : this(true)
	{
		foreach (Enum @enum in enums)
		{
			if (@enum is ES3.EncryptionType)
			{
				this.encryptionType = (ES3.EncryptionType)@enum;
			}
			else if (@enum is ES3.Location)
			{
				this.location = (ES3.Location)@enum;
			}
			else if (@enum is ES3.CompressionType)
			{
				this.compressionType = (ES3.CompressionType)@enum;
			}
			else if (@enum is ES3.ReferenceMode)
			{
				this.referenceMode = (ES3.ReferenceMode)@enum;
			}
			else if (@enum is ES3.Format)
			{
				this.format = (ES3.Format)@enum;
			}
			else if (@enum is ES3.Directory)
			{
				this.directory = (ES3.Directory)@enum;
			}
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00005B94 File Offset: 0x00003D94
	public ES3Settings(ES3.EncryptionType encryptionType, string encryptionPassword) : this(true)
	{
		this.encryptionType = encryptionType;
		this.encryptionPassword = encryptionPassword;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00005BAB File Offset: 0x00003DAB
	public ES3Settings(string path, ES3.EncryptionType encryptionType, string encryptionPassword, ES3Settings settings = null) : this(path, settings)
	{
		this.encryptionType = encryptionType;
		this.encryptionPassword = encryptionPassword;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00005BC4 File Offset: 0x00003DC4
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ES3Settings(bool applyDefaults)
	{
		if (applyDefaults && ES3Settings.defaultSettings != null)
		{
			ES3Settings._defaults.CopyInto(this);
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00005C74 File Offset: 0x00003E74
	private static bool IsAbsolute(string path)
	{
		return (path.Length > 0 && (path[0] == '/' || path[0] == '\\')) || (path.Length > 1 && path[1] == ':');
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00005CB0 File Offset: 0x00003EB0
	[EditorBrowsable(EditorBrowsableState.Never)]
	public object Clone()
	{
		ES3Settings es3Settings = new ES3Settings(null, null);
		this.CopyInto(es3Settings);
		return es3Settings;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00005CD0 File Offset: 0x00003ED0
	private void CopyInto(ES3Settings newSettings)
	{
		newSettings._location = this._location;
		newSettings.directory = this.directory;
		newSettings.format = this.format;
		newSettings.prettyPrint = this.prettyPrint;
		newSettings.path = this.path;
		newSettings.encryptionType = this.encryptionType;
		newSettings.encryptionPassword = this.encryptionPassword;
		newSettings.compressionType = this.compressionType;
		newSettings.bufferSize = this.bufferSize;
		newSettings.encoding = this.encoding;
		newSettings.typeChecking = this.typeChecking;
		newSettings.safeReflection = this.safeReflection;
		newSettings.referenceMode = this.referenceMode;
		newSettings.memberReferenceMode = this.memberReferenceMode;
		newSettings.assemblyNames = this.assemblyNames;
		newSettings.saveChildren = this.saveChildren;
		newSettings.serializationDepthLimit = this.serializationDepthLimit;
		newSettings.postprocessRawCachedData = this.postprocessRawCachedData;
		newSettings.storeCacheAtEndOfEveryFrame = true;
		newSettings.storeCacheOnApplicationQuit = true;
		newSettings.storeCacheOnApplicationPause = true;
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00005DCC File Offset: 0x00003FCC
	// Note: this type is marked as 'beforefieldinit'.
	static ES3Settings()
	{
	}

	// Token: 0x04000036 RID: 54
	private static ES3Settings _defaults = null;

	// Token: 0x04000037 RID: 55
	private static ES3Defaults _defaultSettingsScriptableObject;

	// Token: 0x04000038 RID: 56
	private const string defaultSettingsPath = "ES3/ES3Defaults";

	// Token: 0x04000039 RID: 57
	private static ES3Settings _unencryptedUncompressedSettings = null;

	// Token: 0x0400003A RID: 58
	private static readonly string[] resourcesExtensions = new string[]
	{
		".txt",
		".htm",
		".html",
		".xml",
		".bytes",
		".json",
		".csv",
		".yaml",
		".fnt"
	};

	// Token: 0x0400003B RID: 59
	[SerializeField]
	private ES3.Location _location;

	// Token: 0x0400003C RID: 60
	public string path = "SaveFile.es3";

	// Token: 0x0400003D RID: 61
	public ES3.EncryptionType encryptionType;

	// Token: 0x0400003E RID: 62
	public ES3.CompressionType compressionType;

	// Token: 0x0400003F RID: 63
	public string encryptionPassword = "password";

	// Token: 0x04000040 RID: 64
	public ES3.Directory directory;

	// Token: 0x04000041 RID: 65
	public ES3.Format format;

	// Token: 0x04000042 RID: 66
	public bool prettyPrint = true;

	// Token: 0x04000043 RID: 67
	public int bufferSize = 2048;

	// Token: 0x04000044 RID: 68
	public Encoding encoding = Encoding.UTF8;

	// Token: 0x04000045 RID: 69
	public bool saveChildren = true;

	// Token: 0x04000046 RID: 70
	public bool postprocessRawCachedData;

	// Token: 0x04000047 RID: 71
	public bool storeCacheAtEndOfEveryFrame = true;

	// Token: 0x04000048 RID: 72
	public bool storeCacheOnApplicationQuit = true;

	// Token: 0x04000049 RID: 73
	public bool storeCacheOnApplicationPause = true;

	// Token: 0x0400004A RID: 74
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool typeChecking = true;

	// Token: 0x0400004B RID: 75
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool safeReflection = true;

	// Token: 0x0400004C RID: 76
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ES3.ReferenceMode memberReferenceMode;

	// Token: 0x0400004D RID: 77
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ES3.ReferenceMode referenceMode = ES3.ReferenceMode.ByRefAndValue;

	// Token: 0x0400004E RID: 78
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int serializationDepthLimit = 64;

	// Token: 0x0400004F RID: 79
	[EditorBrowsable(EditorBrowsableState.Never)]
	public string[] assemblyNames = new string[]
	{
		"Assembly-CSharp-firstpass",
		"Assembly-CSharp"
	};
}
