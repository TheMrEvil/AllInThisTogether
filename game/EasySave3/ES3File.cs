using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using ES3Internal;
using ES3Types;

// Token: 0x02000008 RID: 8
public class ES3File
{
	// Token: 0x060000B6 RID: 182 RVA: 0x00003EF6 File Offset: 0x000020F6
	public ES3File() : this(new ES3Settings(null, null), true)
	{
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00003F06 File Offset: 0x00002106
	public ES3File(string filePath) : this(new ES3Settings(filePath, null), true)
	{
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00003F16 File Offset: 0x00002116
	public ES3File(string filePath, ES3Settings settings) : this(new ES3Settings(filePath, settings), true)
	{
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00003F26 File Offset: 0x00002126
	public ES3File(ES3Settings settings) : this(settings, true)
	{
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00003F30 File Offset: 0x00002130
	public ES3File(bool syncWithFile) : this(new ES3Settings(null, null), syncWithFile)
	{
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00003F40 File Offset: 0x00002140
	public ES3File(string filePath, bool syncWithFile) : this(new ES3Settings(filePath, null), syncWithFile)
	{
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00003F50 File Offset: 0x00002150
	public ES3File(string filePath, ES3Settings settings, bool syncWithFile) : this(new ES3Settings(filePath, settings), syncWithFile)
	{
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00003F60 File Offset: 0x00002160
	public ES3File(ES3Settings settings, bool syncWithFile)
	{
		this.cache = new Dictionary<string, ES3Data>();
		this.timestamp = DateTime.UtcNow;
		base..ctor();
		this.settings = settings;
		this.syncWithFile = syncWithFile;
		if (syncWithFile)
		{
			ES3Settings es3Settings = (ES3Settings)settings.Clone();
			es3Settings.typeChecking = true;
			using (ES3Reader es3Reader = ES3Reader.Create(es3Settings))
			{
				if (es3Reader == null)
				{
					return;
				}
				foreach (object obj in es3Reader.RawEnumerator)
				{
					KeyValuePair<string, ES3Data> keyValuePair = (KeyValuePair<string, ES3Data>)obj;
					this.cache[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			this.timestamp = ES3.GetTimestamp(es3Settings);
		}
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00004040 File Offset: 0x00002240
	public ES3File(byte[] bytes, ES3Settings settings = null)
	{
		this.cache = new Dictionary<string, ES3Data>();
		this.timestamp = DateTime.UtcNow;
		base..ctor();
		if (settings == null)
		{
			this.settings = new ES3Settings(null, null);
		}
		else
		{
			this.settings = settings;
		}
		this.syncWithFile = true;
		this.SaveRaw(bytes, settings);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00004091 File Offset: 0x00002291
	public void Sync()
	{
		this.Sync(this.settings);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000409F File Offset: 0x0000229F
	public void Sync(string filePath, ES3Settings settings = null)
	{
		this.Sync(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x000040B0 File Offset: 0x000022B0
	public void Sync(ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		if (this.cache.Count == 0)
		{
			ES3.DeleteFile(settings);
			return;
		}
		if (!this.dirty)
		{
			return;
		}
		using (ES3Writer es3Writer = ES3Writer.Create(settings, true, !this.syncWithFile, false))
		{
			foreach (KeyValuePair<string, ES3Data> keyValuePair in this.cache)
			{
				Type type;
				if (keyValuePair.Value.type == null)
				{
					type = typeof(object);
				}
				else
				{
					type = keyValuePair.Value.type.type;
				}
				es3Writer.Write(keyValuePair.Key, type, keyValuePair.Value.bytes);
			}
			es3Writer.Save(!this.syncWithFile);
		}
		this.dirty = false;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x000041AC File Offset: 0x000023AC
	public void Clear()
	{
		this.cache.Clear();
		this.dirty = true;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x000041C0 File Offset: 0x000023C0
	public string[] GetKeys()
	{
		Dictionary<string, ES3Data>.KeyCollection keys = this.cache.Keys;
		string[] array = new string[keys.Count];
		keys.CopyTo(array, 0);
		return array;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000041EC File Offset: 0x000023EC
	public void Save<T>(string key, T value)
	{
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		Type type;
		if (value == null)
		{
			type = typeof(T);
		}
		else
		{
			type = value.GetType();
		}
		ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(type, true);
		this.cache[key] = new ES3Data(orCreateES3Type, ES3.Serialize(value, orCreateES3Type, es3Settings));
		this.dirty = true;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000426C File Offset: 0x0000246C
	public void SaveRaw(byte[] bytes, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		ES3Settings es3Settings = (ES3Settings)settings.Clone();
		es3Settings.typeChecking = true;
		using (ES3Reader es3Reader = ES3Reader.Create(bytes, es3Settings))
		{
			if (es3Reader == null)
			{
				return;
			}
			foreach (object obj in es3Reader.RawEnumerator)
			{
				KeyValuePair<string, ES3Data> keyValuePair = (KeyValuePair<string, ES3Data>)obj;
				this.cache[keyValuePair.Key] = keyValuePair.Value;
			}
		}
		this.dirty = true;
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00004324 File Offset: 0x00002524
	public void AppendRaw(byte[] bytes, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		this.SaveRaw(bytes, settings);
		this.dirty = true;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00004341 File Offset: 0x00002541
	public object Load(string key)
	{
		return this.Load<object>(key);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000434A File Offset: 0x0000254A
	public object Load(string key, object defaultValue)
	{
		return this.Load<object>(key, defaultValue);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00004354 File Offset: 0x00002554
	public T Load<T>(string key)
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			throw new KeyNotFoundException("Key \"" + key + "\" was not found in this ES3File. Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist.");
		}
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (typeof(T) != es3Data.type.type && ES3Reflection.IsAssignableFrom(typeof(T), es3Data.type.type))
		{
			return (T)((object)ES3.Deserialize(es3Data.type, es3Data.bytes, es3Settings));
		}
		return ES3.Deserialize<T>(es3Data.bytes, es3Settings);
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00004404 File Offset: 0x00002604
	public T Load<T>(string key, T defaultValue)
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			return defaultValue;
		}
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (typeof(T) != es3Data.type.type && ES3Reflection.IsAssignableFrom(typeof(T), es3Data.type.type))
		{
			return (T)((object)ES3.Deserialize(es3Data.type, es3Data.bytes, es3Settings));
		}
		return ES3.Deserialize<T>(es3Data.bytes, es3Settings);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000044A0 File Offset: 0x000026A0
	public void LoadInto<T>(string key, T obj) where T : class
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			throw new KeyNotFoundException("Key \"" + key + "\" was not found in this ES3File. Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist.");
		}
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (typeof(T) != es3Data.type.type && ES3Reflection.IsAssignableFrom(typeof(T), es3Data.type.type))
		{
			ES3.DeserializeInto<T>(es3Data.type, es3Data.bytes, obj, es3Settings);
			return;
		}
		ES3.DeserializeInto<T>(es3Data.bytes, obj, es3Settings);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000454C File Offset: 0x0000274C
	public byte[] LoadRawBytes()
	{
		ES3Settings es3Settings = (ES3Settings)this.settings.Clone();
		if (!es3Settings.postprocessRawCachedData)
		{
			es3Settings.encryptionType = ES3.EncryptionType.None;
			es3Settings.compressionType = ES3.CompressionType.None;
		}
		return this.GetBytes(es3Settings);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00004587 File Offset: 0x00002787
	public string LoadRawString()
	{
		if (this.cache.Count == 0)
		{
			return "";
		}
		return this.settings.encoding.GetString(this.LoadRawBytes());
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000045B4 File Offset: 0x000027B4
	internal byte[] GetBytes(ES3Settings settings = null)
	{
		if (this.cache.Count == 0)
		{
			return new byte[0];
		}
		if (settings == null)
		{
			settings = this.settings;
		}
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			ES3Settings es3Settings = (ES3Settings)settings.Clone();
			es3Settings.location = ES3.Location.InternalMS;
			if (!es3Settings.postprocessRawCachedData)
			{
				es3Settings.encryptionType = ES3.EncryptionType.None;
				es3Settings.compressionType = ES3.CompressionType.None;
			}
			using (ES3Writer es3Writer = ES3Writer.Create(ES3Stream.CreateStream(memoryStream, es3Settings, ES3FileMode.Write), es3Settings, true, false))
			{
				foreach (KeyValuePair<string, ES3Data> keyValuePair in this.cache)
				{
					es3Writer.Write(keyValuePair.Key, keyValuePair.Value.type.type, keyValuePair.Value.bytes);
				}
				es3Writer.Save(false);
			}
			result = memoryStream.ToArray();
		}
		return result;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x000046CC File Offset: 0x000028CC
	public void DeleteKey(string key)
	{
		this.cache.Remove(key);
		this.dirty = true;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000046E2 File Offset: 0x000028E2
	public bool KeyExists(string key)
	{
		return this.cache.ContainsKey(key);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000046F0 File Offset: 0x000028F0
	public int Size()
	{
		int num = 0;
		foreach (KeyValuePair<string, ES3Data> keyValuePair in this.cache)
		{
			num += keyValuePair.Value.bytes.Length;
		}
		return num;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00004750 File Offset: 0x00002950
	public Type GetKeyType(string key)
	{
		ES3Data es3Data;
		if (!this.cache.TryGetValue(key, out es3Data))
		{
			throw new KeyNotFoundException("Key \"" + key + "\" was not found in this ES3File. Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist.");
		}
		return es3Data.type.type;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00004790 File Offset: 0x00002990
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static ES3File GetOrCreateCachedFile(ES3Settings settings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			es3File = new ES3File(settings, false);
			ES3File.cachedFiles.Add(settings.path, es3File);
			es3File.syncWithFile = true;
		}
		es3File.settings = settings;
		return es3File;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000047DC File Offset: 0x000029DC
	internal static void CacheFile(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			settings = (ES3Settings)settings.Clone();
			settings.location = ((ES3Settings.defaultSettings.location == ES3.Location.Cache) ? ES3.Location.File : ES3Settings.defaultSettings.location);
		}
		if (!ES3.FileExists(settings))
		{
			return;
		}
		ES3Settings es3Settings = (ES3Settings)settings.Clone();
		es3Settings.compressionType = ES3.CompressionType.None;
		es3Settings.encryptionType = ES3.EncryptionType.None;
		ES3File.cachedFiles[settings.path] = new ES3File(ES3.LoadRawBytes(es3Settings), settings);
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00004860 File Offset: 0x00002A60
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void Store(ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(new Enum[]
			{
				ES3.Location.File
			});
		}
		else if (settings.location == ES3.Location.Cache)
		{
			settings = (ES3Settings)settings.Clone();
			settings.location = ((ES3Settings.defaultSettings.location == ES3.Location.Cache) ? ES3.Location.File : ES3Settings.defaultSettings.location);
		}
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			throw new FileNotFoundException("The file '" + settings.path + "' could not be stored because it could not be found in the cache.");
		}
		es3File.Sync(settings);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000048F4 File Offset: 0x00002AF4
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void StoreAll()
	{
		foreach (KeyValuePair<string, ES3File> keyValuePair in ES3File.cachedFiles)
		{
			if (keyValuePair.Key != null && keyValuePair.Value != null)
			{
				ES3File.Store(keyValuePair.Value.settings);
			}
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00004964 File Offset: 0x00002B64
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void RemoveCachedFile(ES3Settings settings)
	{
		ES3File.cachedFiles.Remove(settings.path);
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00004978 File Offset: 0x00002B78
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void CopyCachedFile(ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(oldSettings.path, out es3File))
		{
			throw new FileNotFoundException("The file '" + oldSettings.path + "' could not be copied because it could not be found in the cache.");
		}
		if (ES3File.cachedFiles.ContainsKey(newSettings.path))
		{
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Cannot copy file '",
				oldSettings.path,
				"' to '",
				newSettings.path,
				"' because '",
				newSettings.path,
				"' already exists"
			}));
		}
		ES3File.cachedFiles.Add(newSettings.path, (ES3File)es3File.MemberwiseClone());
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00004A30 File Offset: 0x00002C30
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static void DeleteKey(string key, ES3Settings settings)
	{
		ES3File es3File;
		if (ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			es3File.DeleteKey(key);
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00004A58 File Offset: 0x00002C58
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static bool KeyExists(string key, ES3Settings settings)
	{
		ES3File es3File;
		return ES3File.cachedFiles.TryGetValue(settings.path, out es3File) && es3File.KeyExists(key);
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00004A82 File Offset: 0x00002C82
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static bool FileExists(ES3Settings settings)
	{
		return ES3File.cachedFiles.ContainsKey(settings.path);
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00004A94 File Offset: 0x00002C94
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static string[] GetKeys(ES3Settings settings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			throw new FileNotFoundException("Could not get keys from the file '" + settings.path + "' because it could not be found in the cache.");
		}
		return es3File.cache.Keys.ToArray<string>();
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00004AE0 File Offset: 0x00002CE0
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static string[] GetFiles()
	{
		return ES3File.cachedFiles.Keys.ToArray<string>();
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00004AF4 File Offset: 0x00002CF4
	internal static DateTime GetTimestamp(ES3Settings settings)
	{
		ES3File es3File;
		if (!ES3File.cachedFiles.TryGetValue(settings.path, out es3File))
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		}
		return es3File.timestamp;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00004B2D File Offset: 0x00002D2D
	// Note: this type is marked as 'beforefieldinit'.
	static ES3File()
	{
	}

	// Token: 0x04000015 RID: 21
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static Dictionary<string, ES3File> cachedFiles = new Dictionary<string, ES3File>();

	// Token: 0x04000016 RID: 22
	public ES3Settings settings;

	// Token: 0x04000017 RID: 23
	private Dictionary<string, ES3Data> cache;

	// Token: 0x04000018 RID: 24
	private bool syncWithFile;

	// Token: 0x04000019 RID: 25
	private DateTime timestamp;

	// Token: 0x0400001A RID: 26
	private bool dirty;
}
