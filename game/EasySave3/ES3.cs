using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using ES3Internal;
using ES3Types;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000007 RID: 7
public class ES3
{
	// Token: 0x0600001F RID: 31 RVA: 0x000027A2 File Offset: 0x000009A2
	public static void Save(string key, object value)
	{
		ES3.Save<object>(key, value, new ES3Settings(null, null));
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000027B2 File Offset: 0x000009B2
	public static void Save(string key, object value, string filePath)
	{
		ES3.Save<object>(key, value, new ES3Settings(filePath, null));
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000027C2 File Offset: 0x000009C2
	public static void Save(string key, object value, string filePath, ES3Settings settings)
	{
		ES3.Save<object>(key, value, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000027D2 File Offset: 0x000009D2
	public static void Save(string key, object value, ES3Settings settings)
	{
		ES3.Save<object>(key, value, settings);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000027DC File Offset: 0x000009DC
	public static void Save<T>(string key, T value)
	{
		ES3.Save<T>(key, value, new ES3Settings(null, null));
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000027EC File Offset: 0x000009EC
	public static void Save<T>(string key, T value, string filePath)
	{
		ES3.Save<T>(key, value, new ES3Settings(filePath, null));
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000027FC File Offset: 0x000009FC
	public static void Save<T>(string key, T value, string filePath, ES3Settings settings)
	{
		ES3.Save<T>(key, value, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000280C File Offset: 0x00000A0C
	public static void Save<T>(string key, T value, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).Save<T>(key, value);
			return;
		}
		using (ES3Writer es3Writer = ES3Writer.Create(settings))
		{
			es3Writer.Write<T>(key, value);
			es3Writer.Save();
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002868 File Offset: 0x00000A68
	public static void SaveRaw(byte[] bytes)
	{
		ES3.SaveRaw(bytes, new ES3Settings(null, null));
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002877 File Offset: 0x00000A77
	public static void SaveRaw(byte[] bytes, string filePath)
	{
		ES3.SaveRaw(bytes, new ES3Settings(filePath, null));
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002886 File Offset: 0x00000A86
	public static void SaveRaw(byte[] bytes, string filePath, ES3Settings settings)
	{
		ES3.SaveRaw(bytes, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002898 File Offset: 0x00000A98
	public static void SaveRaw(byte[] bytes, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).SaveRaw(bytes, settings);
			return;
		}
		using (Stream stream = ES3Stream.CreateStream(settings, ES3FileMode.Write))
		{
			stream.Write(bytes, 0, bytes.Length);
		}
		ES3IO.CommitBackup(settings);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000028F4 File Offset: 0x00000AF4
	public static void SaveRaw(string str)
	{
		ES3.SaveRaw(str, new ES3Settings(null, null));
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002903 File Offset: 0x00000B03
	public static void SaveRaw(string str, string filePath)
	{
		ES3.SaveRaw(str, new ES3Settings(filePath, null));
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002912 File Offset: 0x00000B12
	public static void SaveRaw(string str, string filePath, ES3Settings settings)
	{
		ES3.SaveRaw(str, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002921 File Offset: 0x00000B21
	public static void SaveRaw(string str, ES3Settings settings)
	{
		ES3.SaveRaw(settings.encoding.GetBytes(str), settings);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002935 File Offset: 0x00000B35
	public static void AppendRaw(byte[] bytes)
	{
		ES3.AppendRaw(bytes, new ES3Settings(null, null));
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002944 File Offset: 0x00000B44
	public static void AppendRaw(byte[] bytes, string filePath, ES3Settings settings)
	{
		ES3.AppendRaw(bytes, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002954 File Offset: 0x00000B54
	public static void AppendRaw(byte[] bytes, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).AppendRaw(bytes, null);
			return;
		}
		using (Stream stream = ES3Stream.CreateStream(new ES3Settings(settings.path, settings)
		{
			encryptionType = ES3.EncryptionType.None,
			compressionType = ES3.CompressionType.None
		}, ES3FileMode.Append))
		{
			stream.Write(bytes, 0, bytes.Length);
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000029C0 File Offset: 0x00000BC0
	public static void AppendRaw(string str)
	{
		ES3.AppendRaw(str, new ES3Settings(null, null));
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000029CF File Offset: 0x00000BCF
	public static void AppendRaw(string str, string filePath)
	{
		ES3.AppendRaw(str, new ES3Settings(filePath, null));
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000029DE File Offset: 0x00000BDE
	public static void AppendRaw(string str, string filePath, ES3Settings settings)
	{
		ES3.AppendRaw(str, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000029F0 File Offset: 0x00000BF0
	public static void AppendRaw(string str, ES3Settings settings)
	{
		byte[] bytes = settings.encoding.GetBytes(str);
		ES3Settings es3Settings = new ES3Settings(settings.path, settings);
		es3Settings.encryptionType = ES3.EncryptionType.None;
		es3Settings.compressionType = ES3.CompressionType.None;
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).SaveRaw(bytes, null);
			return;
		}
		using (Stream stream = ES3Stream.CreateStream(es3Settings, ES3FileMode.Append))
		{
			stream.Write(bytes, 0, bytes.Length);
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002A6C File Offset: 0x00000C6C
	public static void SaveImage(Texture2D texture, string imagePath)
	{
		ES3.SaveImage(texture, new ES3Settings(imagePath, null));
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002A7B File Offset: 0x00000C7B
	public static void SaveImage(Texture2D texture, string imagePath, ES3Settings settings)
	{
		ES3.SaveImage(texture, new ES3Settings(imagePath, settings));
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002A8A File Offset: 0x00000C8A
	public static void SaveImage(Texture2D texture, ES3Settings settings)
	{
		ES3.SaveImage(texture, 75, settings);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002A95 File Offset: 0x00000C95
	public static void SaveImage(Texture2D texture, int quality, string imagePath)
	{
		ES3.SaveImage(texture, quality, new ES3Settings(imagePath, null));
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002AA5 File Offset: 0x00000CA5
	public static void SaveImage(Texture2D texture, int quality, string imagePath, ES3Settings settings)
	{
		ES3.SaveImage(texture, quality, new ES3Settings(imagePath, settings));
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002AB8 File Offset: 0x00000CB8
	public static void SaveImage(Texture2D texture, int quality, ES3Settings settings)
	{
		string text = ES3IO.GetExtension(settings.path).ToLower();
		if (string.IsNullOrEmpty(text))
		{
			throw new ArgumentException("File path must have a file extension when using ES3.SaveImage.");
		}
		byte[] bytes;
		if (text == ".jpg" || text == ".jpeg")
		{
			bytes = texture.EncodeToJPG(quality);
		}
		else
		{
			if (!(text == ".png"))
			{
				throw new ArgumentException("File path must have extension of .png, .jpg or .jpeg when using ES3.SaveImage.");
			}
			bytes = texture.EncodeToPNG();
		}
		ES3.SaveRaw(bytes, settings);
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002B35 File Offset: 0x00000D35
	public static byte[] SaveImageToBytes(Texture2D texture, int quality, ES3.ImageType imageType)
	{
		if (imageType == ES3.ImageType.JPEG)
		{
			return texture.EncodeToJPG(quality);
		}
		return texture.EncodeToPNG();
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002B48 File Offset: 0x00000D48
	public static object Load(string key)
	{
		return ES3.Load<object>(key, new ES3Settings(null, null));
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002B57 File Offset: 0x00000D57
	public static object Load(string key, string filePath)
	{
		return ES3.Load<object>(key, new ES3Settings(filePath, null));
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002B66 File Offset: 0x00000D66
	public static object Load(string key, string filePath, ES3Settings settings)
	{
		return ES3.Load<object>(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002B75 File Offset: 0x00000D75
	public static object Load(string key, ES3Settings settings)
	{
		return ES3.Load<object>(key, settings);
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002B7E File Offset: 0x00000D7E
	public static object Load(string key, object defaultValue)
	{
		return ES3.Load<object>(key, defaultValue, new ES3Settings(null, null));
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002B8E File Offset: 0x00000D8E
	public static object Load(string key, string filePath, object defaultValue)
	{
		return ES3.Load<object>(key, defaultValue, new ES3Settings(filePath, null));
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002B9E File Offset: 0x00000D9E
	public static object Load(string key, string filePath, object defaultValue, ES3Settings settings)
	{
		return ES3.Load<object>(key, defaultValue, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002BAE File Offset: 0x00000DAE
	public static object Load(string key, object defaultValue, ES3Settings settings)
	{
		return ES3.Load<object>(key, defaultValue, settings);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002BB8 File Offset: 0x00000DB8
	public static T Load<T>(string key)
	{
		return ES3.Load<T>(key, new ES3Settings(null, null));
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002BC7 File Offset: 0x00000DC7
	public static T Load<T>(string key, string filePath)
	{
		return ES3.Load<T>(key, new ES3Settings(filePath, null));
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00002BD6 File Offset: 0x00000DD6
	public static T Load<T>(string key, string filePath, ES3Settings settings)
	{
		if (typeof(T) == typeof(string))
		{
			ES3Debug.LogWarning("Using ES3.Load<string>(string, string) to load a string, but the second parameter is ambiguous between defaultValue and filePath. By default C# will assume that the second parameter is the filePath. If you want the second parameter to be the defaultValue, use a named parameter. E.g. ES3.Load<string>(\"key\", defaultValue: \"myDefaultValue\")", null, 0);
		}
		return ES3.Load<T>(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002C0C File Offset: 0x00000E0C
	public static T Load<T>(string key, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetOrCreateCachedFile(settings).Load<T>(key);
		}
		T result;
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				throw new FileNotFoundException("File \"" + settings.FullPath + "\" could not be found.");
			}
			result = es3Reader.Read<T>(key);
		}
		return result;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002C7C File Offset: 0x00000E7C
	public static T Load<T>(string key, T defaultValue)
	{
		return ES3.Load<T>(key, defaultValue, new ES3Settings(null, null));
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002C8C File Offset: 0x00000E8C
	public static T Load<T>(string key, string filePath, T defaultValue)
	{
		return ES3.Load<T>(key, defaultValue, new ES3Settings(filePath, null));
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002C9C File Offset: 0x00000E9C
	public static T Load<T>(string key, string filePath, T defaultValue, ES3Settings settings)
	{
		return ES3.Load<T>(key, defaultValue, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00002CAC File Offset: 0x00000EAC
	public static T Load<T>(string key, T defaultValue, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetOrCreateCachedFile(settings).Load<T>(key, defaultValue);
		}
		T result;
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				result = defaultValue;
			}
			else
			{
				result = es3Reader.Read<T>(key, defaultValue);
			}
		}
		return result;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002D04 File Offset: 0x00000F04
	public static void LoadInto<T>(string key, object obj) where T : class
	{
		ES3.LoadInto<object>(key, obj, new ES3Settings(null, null));
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00002D14 File Offset: 0x00000F14
	public static void LoadInto(string key, string filePath, object obj)
	{
		ES3.LoadInto<object>(key, obj, new ES3Settings(filePath, null));
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002D24 File Offset: 0x00000F24
	public static void LoadInto(string key, string filePath, object obj, ES3Settings settings)
	{
		ES3.LoadInto<object>(key, obj, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00002D34 File Offset: 0x00000F34
	public static void LoadInto(string key, object obj, ES3Settings settings)
	{
		ES3.LoadInto<object>(key, obj, settings);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00002D3E File Offset: 0x00000F3E
	public static void LoadInto<T>(string key, T obj) where T : class
	{
		ES3.LoadInto<T>(key, obj, new ES3Settings(null, null));
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00002D4E File Offset: 0x00000F4E
	public static void LoadInto<T>(string key, string filePath, T obj) where T : class
	{
		ES3.LoadInto<T>(key, obj, new ES3Settings(filePath, null));
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00002D5E File Offset: 0x00000F5E
	public static void LoadInto<T>(string key, string filePath, T obj, ES3Settings settings) where T : class
	{
		ES3.LoadInto<T>(key, obj, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00002D70 File Offset: 0x00000F70
	public static void LoadInto<T>(string key, T obj, ES3Settings settings) where T : class
	{
		if (ES3Reflection.IsValueType(obj.GetType()))
		{
			throw new InvalidOperationException("ES3.LoadInto can only be used with reference types, but the data you're loading is a value type. Use ES3.Load instead.");
		}
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.GetOrCreateCachedFile(settings).LoadInto<T>(key, obj);
			return;
		}
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				throw new FileNotFoundException("File \"" + settings.FullPath + "\" could not be found.");
			}
			es3Reader.ReadInto<T>(key, obj);
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00002E08 File Offset: 0x00001008
	public static string LoadString(string key, string defaultValue, ES3Settings settings)
	{
		return ES3.Load<string>(key, null, defaultValue, settings);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00002E13 File Offset: 0x00001013
	public static string LoadString(string key, string defaultValue, string filePath = null, ES3Settings settings = null)
	{
		return ES3.Load<string>(key, filePath, defaultValue, settings);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00002E1E File Offset: 0x0000101E
	public static byte[] LoadRawBytes()
	{
		return ES3.LoadRawBytes(new ES3Settings(null, null));
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00002E2C File Offset: 0x0000102C
	public static byte[] LoadRawBytes(string filePath)
	{
		return ES3.LoadRawBytes(new ES3Settings(filePath, null));
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00002E3A File Offset: 0x0000103A
	public static byte[] LoadRawBytes(string filePath, ES3Settings settings)
	{
		return ES3.LoadRawBytes(new ES3Settings(filePath, settings));
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002E48 File Offset: 0x00001048
	public static byte[] LoadRawBytes(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetOrCreateCachedFile(settings).LoadRawBytes();
		}
		byte[] result;
		using (Stream stream = ES3Stream.CreateStream(settings, ES3FileMode.Read))
		{
			if (stream == null)
			{
				throw new FileNotFoundException("File " + settings.path + " could not be found");
			}
			if (stream.GetType() == typeof(GZipStream))
			{
				GZipStream source = (GZipStream)stream;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					ES3Stream.CopyTo(source, memoryStream);
					return memoryStream.ToArray();
				}
			}
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			result = array;
		}
		return result;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00002F18 File Offset: 0x00001118
	public static string LoadRawString()
	{
		return ES3.LoadRawString(new ES3Settings(null, null));
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002F26 File Offset: 0x00001126
	public static string LoadRawString(string filePath)
	{
		return ES3.LoadRawString(new ES3Settings(filePath, null));
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00002F34 File Offset: 0x00001134
	public static string LoadRawString(string filePath, ES3Settings settings)
	{
		return ES3.LoadRawString(new ES3Settings(filePath, settings));
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00002F44 File Offset: 0x00001144
	public static string LoadRawString(ES3Settings settings)
	{
		byte[] array = ES3.LoadRawBytes(settings);
		return settings.encoding.GetString(array, 0, array.Length);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00002F68 File Offset: 0x00001168
	public static Texture2D LoadImage(string imagePath)
	{
		return ES3.LoadImage(new ES3Settings(imagePath, null));
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00002F76 File Offset: 0x00001176
	public static Texture2D LoadImage(string imagePath, ES3Settings settings)
	{
		return ES3.LoadImage(new ES3Settings(imagePath, settings));
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00002F84 File Offset: 0x00001184
	public static Texture2D LoadImage(ES3Settings settings)
	{
		return ES3.LoadImage(ES3.LoadRawBytes(settings));
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00002F91 File Offset: 0x00001191
	public static Texture2D LoadImage(byte[] bytes)
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.LoadImage(bytes);
		return texture2D;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00002FA2 File Offset: 0x000011A2
	public static AudioClip LoadAudio(string audioFilePath, AudioType audioType)
	{
		return ES3.LoadAudio(audioFilePath, audioType, new ES3Settings(null, null));
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00002FB4 File Offset: 0x000011B4
	public static AudioClip LoadAudio(string audioFilePath, AudioType audioType, ES3Settings settings)
	{
		if (settings.location != ES3.Location.File)
		{
			throw new InvalidOperationException("ES3.LoadAudio can only be used with the File save location");
		}
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			throw new InvalidOperationException("You cannot use ES3.LoadAudio with WebGL");
		}
		string a = ES3IO.GetExtension(audioFilePath).ToLower();
		if (a == ".mp3" && (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer))
		{
			throw new InvalidOperationException("You can only load Ogg, WAV, XM, IT, MOD or S3M on Unity Standalone");
		}
		if (a == ".ogg" && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.MetroPlayerARM))
		{
			throw new InvalidOperationException("You can only load MP3, WAV, XM, IT, MOD or S3M on Unity Standalone");
		}
		ES3Settings es3Settings = new ES3Settings(audioFilePath, settings);
		AudioClip content;
		using (UnityWebRequest audioClip = UnityWebRequestMultimedia.GetAudioClip("file://" + es3Settings.FullPath, audioType))
		{
			audioClip.SendWebRequest();
			while (!audioClip.isDone)
			{
			}
			if (ES3WebClass.IsNetworkError(audioClip))
			{
				throw new Exception(audioClip.error);
			}
			content = DownloadHandlerAudioClip.GetContent(audioClip);
		}
		return content;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000030B4 File Offset: 0x000012B4
	public static byte[] Serialize<T>(T value, ES3Settings settings = null)
	{
		return ES3.Serialize(value, ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), settings);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000030D4 File Offset: 0x000012D4
	internal static byte[] Serialize(object value, ES3Type type, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (Stream stream = ES3Stream.CreateStream(memoryStream, settings, ES3FileMode.Write))
			{
				using (ES3Writer es3Writer = ES3Writer.Create(stream, settings, false, false))
				{
					es3Writer.Write(value, type, settings.referenceMode);
				}
				result = memoryStream.ToArray();
			}
		}
		return result;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00003168 File Offset: 0x00001368
	public static T Deserialize<T>(byte[] bytes, ES3Settings settings = null)
	{
		return (T)((object)ES3.Deserialize(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), bytes, settings));
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00003188 File Offset: 0x00001388
	internal static object Deserialize(ES3Type type, byte[] bytes, ES3Settings settings = null)
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		object result;
		using (MemoryStream memoryStream = new MemoryStream(bytes, false))
		{
			using (Stream stream = ES3Stream.CreateStream(memoryStream, settings, ES3FileMode.Read))
			{
				using (ES3Reader es3Reader = ES3Reader.Create(stream, settings, false))
				{
					result = es3Reader.Read<object>(type);
				}
			}
		}
		return result;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0000320C File Offset: 0x0000140C
	public static void DeserializeInto<T>(byte[] bytes, T obj, ES3Settings settings = null) where T : class
	{
		ES3.DeserializeInto<T>(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), bytes, obj, settings);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003228 File Offset: 0x00001428
	public static void DeserializeInto<T>(ES3Type type, byte[] bytes, T obj, ES3Settings settings = null) where T : class
	{
		if (settings == null)
		{
			settings = new ES3Settings(null, null);
		}
		using (MemoryStream memoryStream = new MemoryStream(bytes, false))
		{
			using (ES3Reader es3Reader = ES3Reader.Create(memoryStream, settings, false))
			{
				es3Reader.ReadInto<T>(obj, type);
			}
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003294 File Offset: 0x00001494
	public static byte[] EncryptBytes(byte[] bytes, string password = null)
	{
		if (string.IsNullOrEmpty(password))
		{
			password = ES3Settings.defaultSettings.encryptionPassword;
		}
		return new AESEncryptionAlgorithm().Encrypt(bytes, password, ES3Settings.defaultSettings.bufferSize);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000032C0 File Offset: 0x000014C0
	public static byte[] DecryptBytes(byte[] bytes, string password = null)
	{
		if (string.IsNullOrEmpty(password))
		{
			password = ES3Settings.defaultSettings.encryptionPassword;
		}
		return new AESEncryptionAlgorithm().Decrypt(bytes, password, ES3Settings.defaultSettings.bufferSize);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000032EC File Offset: 0x000014EC
	public static string EncryptString(string str, string password = null)
	{
		return Convert.ToBase64String(ES3.EncryptBytes(ES3Settings.defaultSettings.encoding.GetBytes(str), password));
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00003309 File Offset: 0x00001509
	public static string DecryptString(string str, string password = null)
	{
		return ES3Settings.defaultSettings.encoding.GetString(ES3.DecryptBytes(Convert.FromBase64String(str), password));
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00003328 File Offset: 0x00001528
	public static byte[] CompressBytes(byte[] bytes)
	{
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (Stream stream = ES3Stream.CreateStream(memoryStream, new ES3Settings(null, null)
			{
				location = ES3.Location.InternalMS,
				compressionType = ES3.CompressionType.Gzip,
				encryptionType = ES3.EncryptionType.None
			}, ES3FileMode.Write))
			{
				stream.Write(bytes, 0, bytes.Length);
			}
			result = memoryStream.ToArray();
		}
		return result;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000033A8 File Offset: 0x000015A8
	public static byte[] DecompressBytes(byte[] bytes)
	{
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream(bytes))
		{
			ES3Settings es3Settings = new ES3Settings(null, null);
			es3Settings.location = ES3.Location.InternalMS;
			es3Settings.compressionType = ES3.CompressionType.Gzip;
			es3Settings.encryptionType = ES3.EncryptionType.None;
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (Stream stream = ES3Stream.CreateStream(memoryStream, es3Settings, ES3FileMode.Read))
				{
					ES3Stream.CopyTo(stream, memoryStream2);
				}
				result = memoryStream2.ToArray();
			}
		}
		return result;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003444 File Offset: 0x00001644
	public static string CompressString(string str)
	{
		return Convert.ToBase64String(ES3.CompressBytes(ES3Settings.defaultSettings.encoding.GetBytes(str)));
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003460 File Offset: 0x00001660
	public static string DecompressString(string str)
	{
		return ES3Settings.defaultSettings.encoding.GetString(ES3.DecompressBytes(Convert.FromBase64String(str)));
	}

	// Token: 0x06000073 RID: 115 RVA: 0x0000347C File Offset: 0x0000167C
	public static void DeleteFile()
	{
		ES3.DeleteFile(new ES3Settings(null, null));
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000348A File Offset: 0x0000168A
	public static void DeleteFile(string filePath)
	{
		ES3.DeleteFile(new ES3Settings(filePath, null));
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003498 File Offset: 0x00001698
	public static void DeleteFile(string filePath, ES3Settings settings)
	{
		ES3.DeleteFile(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000034A8 File Offset: 0x000016A8
	public static void DeleteFile(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			ES3IO.DeleteFile(settings.FullPath);
			return;
		}
		if (settings.location == ES3.Location.PlayerPrefs)
		{
			PlayerPrefs.DeleteKey(settings.FullPath);
			return;
		}
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.RemoveCachedFile(settings);
			return;
		}
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Deleting files from Resources is not possible.");
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003502 File Offset: 0x00001702
	public static void CopyFile(string oldFilePath, string newFilePath)
	{
		ES3.CopyFile(new ES3Settings(oldFilePath, null), new ES3Settings(newFilePath, null));
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003517 File Offset: 0x00001717
	public static void CopyFile(string oldFilePath, string newFilePath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.CopyFile(new ES3Settings(oldFilePath, oldSettings), new ES3Settings(newFilePath, newSettings));
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000352C File Offset: 0x0000172C
	public static void CopyFile(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location != newSettings.location)
		{
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Cannot copy file from ",
				oldSettings.location.ToString(),
				" to ",
				newSettings.location.ToString(),
				". Location must be the same for both source and destination."
			}));
		}
		if (oldSettings.location == ES3.Location.File)
		{
			if (ES3IO.FileExists(oldSettings.FullPath))
			{
				string directoryPath = ES3IO.GetDirectoryPath(newSettings.FullPath, '/');
				if (!ES3IO.DirectoryExists(directoryPath))
				{
					ES3IO.CreateDirectory(directoryPath);
				}
				else
				{
					ES3IO.DeleteFile(newSettings.FullPath);
				}
				ES3IO.CopyFile(oldSettings.FullPath, newSettings.FullPath);
				return;
			}
		}
		else
		{
			if (oldSettings.location == ES3.Location.PlayerPrefs)
			{
				PlayerPrefs.SetString(newSettings.FullPath, PlayerPrefs.GetString(oldSettings.FullPath));
				return;
			}
			if (oldSettings.location == ES3.Location.Cache)
			{
				ES3File.CopyCachedFile(oldSettings, newSettings);
				return;
			}
			if (oldSettings.location == ES3.Location.Resources)
			{
				throw new NotSupportedException("Modifying files from Resources is not allowed.");
			}
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003636 File Offset: 0x00001836
	public static void RenameFile(string oldFilePath, string newFilePath)
	{
		ES3.RenameFile(new ES3Settings(oldFilePath, null), new ES3Settings(newFilePath, null));
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000364B File Offset: 0x0000184B
	public static void RenameFile(string oldFilePath, string newFilePath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.RenameFile(new ES3Settings(oldFilePath, oldSettings), new ES3Settings(newFilePath, newSettings));
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003660 File Offset: 0x00001860
	public static void RenameFile(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location != newSettings.location)
		{
			throw new InvalidOperationException(string.Concat(new string[]
			{
				"Cannot rename file in ",
				oldSettings.location.ToString(),
				" to ",
				newSettings.location.ToString(),
				". Location must be the same for both source and destination."
			}));
		}
		if (oldSettings.location == ES3.Location.File)
		{
			if (ES3IO.FileExists(oldSettings.FullPath))
			{
				ES3IO.DeleteFile(newSettings.FullPath);
				ES3IO.MoveFile(oldSettings.FullPath, newSettings.FullPath);
				return;
			}
		}
		else
		{
			if (oldSettings.location == ES3.Location.PlayerPrefs)
			{
				PlayerPrefs.SetString(newSettings.FullPath, PlayerPrefs.GetString(oldSettings.FullPath));
				PlayerPrefs.DeleteKey(oldSettings.FullPath);
				return;
			}
			if (oldSettings.location == ES3.Location.Cache)
			{
				ES3File.CopyCachedFile(oldSettings, newSettings);
				ES3File.RemoveCachedFile(oldSettings);
				return;
			}
			if (oldSettings.location == ES3.Location.Resources)
			{
				throw new NotSupportedException("Modifying files from Resources is not allowed.");
			}
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x0000375A File Offset: 0x0000195A
	public static void CopyDirectory(string oldDirectoryPath, string newDirectoryPath)
	{
		ES3.CopyDirectory(new ES3Settings(oldDirectoryPath, null), new ES3Settings(newDirectoryPath, null));
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000376F File Offset: 0x0000196F
	public static void CopyDirectory(string oldDirectoryPath, string newDirectoryPath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.CopyDirectory(new ES3Settings(oldDirectoryPath, oldSettings), new ES3Settings(newDirectoryPath, newSettings));
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003784 File Offset: 0x00001984
	public static void CopyDirectory(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location != ES3.Location.File)
		{
			throw new InvalidOperationException("ES3.CopyDirectory can only be used when the save location is 'File' or 'Cache', and can't be used with WebGL.");
		}
		if (!ES3.DirectoryExists(oldSettings))
		{
			throw new DirectoryNotFoundException("Directory " + oldSettings.FullPath + " not found");
		}
		if (!ES3.DirectoryExists(newSettings))
		{
			ES3IO.CreateDirectory(newSettings.FullPath);
		}
		foreach (string fileOrDirectoryName in ES3.GetFiles(oldSettings))
		{
			ES3.CopyFile(ES3IO.CombinePathAndFilename(oldSettings.path, fileOrDirectoryName), ES3IO.CombinePathAndFilename(newSettings.path, fileOrDirectoryName), oldSettings, newSettings);
		}
		foreach (string fileOrDirectoryName2 in ES3.GetDirectories(oldSettings))
		{
			ES3.CopyDirectory(ES3IO.CombinePathAndFilename(oldSettings.path, fileOrDirectoryName2), ES3IO.CombinePathAndFilename(newSettings.path, fileOrDirectoryName2), oldSettings, newSettings);
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000384A File Offset: 0x00001A4A
	public static void RenameDirectory(string oldDirectoryPath, string newDirectoryPath)
	{
		ES3.RenameDirectory(new ES3Settings(oldDirectoryPath, null), new ES3Settings(newDirectoryPath, null));
	}

	// Token: 0x06000081 RID: 129 RVA: 0x0000385F File Offset: 0x00001A5F
	public static void RenameDirectory(string oldDirectoryPath, string newDirectoryPath, ES3Settings oldSettings, ES3Settings newSettings)
	{
		ES3.RenameDirectory(new ES3Settings(oldDirectoryPath, oldSettings), new ES3Settings(newDirectoryPath, newSettings));
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003874 File Offset: 0x00001A74
	public static void RenameDirectory(ES3Settings oldSettings, ES3Settings newSettings)
	{
		if (oldSettings.location == ES3.Location.File)
		{
			if (ES3IO.DirectoryExists(oldSettings.FullPath))
			{
				ES3IO.DeleteDirectory(newSettings.FullPath);
				ES3IO.MoveDirectory(oldSettings.FullPath, newSettings.FullPath);
				return;
			}
		}
		else
		{
			if (oldSettings.location == ES3.Location.PlayerPrefs || oldSettings.location == ES3.Location.Cache)
			{
				throw new NotSupportedException("Directories cannot be renamed when saving to Cache, PlayerPrefs, tvOS or using WebGL.");
			}
			if (oldSettings.location == ES3.Location.Resources)
			{
				throw new NotSupportedException("Modifying files from Resources is not allowed.");
			}
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x000038E4 File Offset: 0x00001AE4
	public static void DeleteDirectory(string directoryPath)
	{
		ES3.DeleteDirectory(new ES3Settings(directoryPath, null));
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000038F2 File Offset: 0x00001AF2
	public static void DeleteDirectory(string directoryPath, ES3Settings settings)
	{
		ES3.DeleteDirectory(new ES3Settings(directoryPath, settings));
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00003900 File Offset: 0x00001B00
	public static void DeleteDirectory(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			ES3IO.DeleteDirectory(settings.FullPath);
			return;
		}
		if (settings.location == ES3.Location.PlayerPrefs || settings.location == ES3.Location.Cache)
		{
			throw new NotSupportedException("Deleting Directories using Cache or PlayerPrefs is not supported.");
		}
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Deleting directories from Resources is not allowed.");
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00003952 File Offset: 0x00001B52
	public static void DeleteKey(string key)
	{
		ES3.DeleteKey(key, new ES3Settings(null, null));
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00003961 File Offset: 0x00001B61
	public static void DeleteKey(string key, string filePath)
	{
		ES3.DeleteKey(key, new ES3Settings(filePath, null));
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00003970 File Offset: 0x00001B70
	public static void DeleteKey(string key, string filePath, ES3Settings settings)
	{
		ES3.DeleteKey(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00003980 File Offset: 0x00001B80
	public static void DeleteKey(string key, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Modifying files in Resources is not allowed.");
		}
		if (settings.location == ES3.Location.Cache)
		{
			ES3File.DeleteKey(key, settings);
			return;
		}
		if (ES3.FileExists(settings))
		{
			using (ES3Writer es3Writer = ES3Writer.Create(settings))
			{
				es3Writer.MarkKeyForDeletion(key);
				es3Writer.Save();
			}
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000039EC File Offset: 0x00001BEC
	public static bool KeyExists(string key)
	{
		return ES3.KeyExists(key, new ES3Settings(null, null));
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000039FB File Offset: 0x00001BFB
	public static bool KeyExists(string key, string filePath)
	{
		return ES3.KeyExists(key, new ES3Settings(filePath, null));
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00003A0A File Offset: 0x00001C0A
	public static bool KeyExists(string key, string filePath, ES3Settings settings)
	{
		return ES3.KeyExists(key, new ES3Settings(filePath, settings));
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00003A1C File Offset: 0x00001C1C
	public static bool KeyExists(string key, ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.KeyExists(key, settings);
		}
		bool result;
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				result = false;
			}
			else
			{
				result = es3Reader.Goto(key);
			}
		}
		return result;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00003A70 File Offset: 0x00001C70
	public static bool FileExists()
	{
		return ES3.FileExists(new ES3Settings(null, null));
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00003A7E File Offset: 0x00001C7E
	public static bool FileExists(string filePath)
	{
		return ES3.FileExists(new ES3Settings(filePath, null));
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00003A8C File Offset: 0x00001C8C
	public static bool FileExists(string filePath, ES3Settings settings)
	{
		return ES3.FileExists(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00003A9C File Offset: 0x00001C9C
	public static bool FileExists(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			return ES3IO.FileExists(settings.FullPath);
		}
		if (settings.location == ES3.Location.PlayerPrefs)
		{
			return PlayerPrefs.HasKey(settings.FullPath);
		}
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.FileExists(settings);
		}
		return settings.location == ES3.Location.Resources && Resources.Load(settings.FullPath) != null;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00003AFE File Offset: 0x00001CFE
	public static bool DirectoryExists(string folderPath)
	{
		return ES3.DirectoryExists(new ES3Settings(folderPath, null));
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00003B0C File Offset: 0x00001D0C
	public static bool DirectoryExists(string folderPath, ES3Settings settings)
	{
		return ES3.DirectoryExists(new ES3Settings(folderPath, settings));
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00003B1C File Offset: 0x00001D1C
	public static bool DirectoryExists(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			return ES3IO.DirectoryExists(settings.FullPath);
		}
		if (settings.location == ES3.Location.PlayerPrefs || Application.platform == RuntimePlatform.WebGLPlayer)
		{
			throw new NotSupportedException("Directories are not supported for PlayerPrefs or WebGL.");
		}
		if (settings.location == ES3.Location.Resources)
		{
			throw new NotSupportedException("Checking existence of folder in Resources not supported.");
		}
		return false;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00003B6F File Offset: 0x00001D6F
	public static string[] GetKeys()
	{
		return ES3.GetKeys(new ES3Settings(null, null));
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00003B7D File Offset: 0x00001D7D
	public static string[] GetKeys(string filePath)
	{
		return ES3.GetKeys(new ES3Settings(filePath, null));
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00003B8B File Offset: 0x00001D8B
	public static string[] GetKeys(string filePath, ES3Settings settings)
	{
		return ES3.GetKeys(new ES3Settings(filePath, settings));
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00003B9C File Offset: 0x00001D9C
	public static string[] GetKeys(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetKeys(settings);
		}
		List<string> list = new List<string>();
		using (ES3Reader es3Reader = ES3Reader.Create(settings))
		{
			if (es3Reader == null)
			{
				throw new FileNotFoundException("Could not get keys from file " + settings.FullPath + " as file does not exist");
			}
			foreach (object obj in es3Reader.Properties)
			{
				string item = (string)obj;
				list.Add(item);
				es3Reader.Skip();
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003C58 File Offset: 0x00001E58
	public static string[] GetFiles()
	{
		ES3Settings es3Settings = new ES3Settings(null, null);
		if (es3Settings.location == ES3.Location.File)
		{
			if (es3Settings.directory == ES3.Directory.PersistentDataPath)
			{
				es3Settings.path = ES3IO.persistentDataPath;
			}
			else
			{
				es3Settings.path = ES3IO.dataPath;
			}
		}
		return ES3.GetFiles(new ES3Settings(null, null));
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00003CA1 File Offset: 0x00001EA1
	public static string[] GetFiles(string directoryPath)
	{
		return ES3.GetFiles(new ES3Settings(directoryPath, null));
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00003CAF File Offset: 0x00001EAF
	public static string[] GetFiles(string directoryPath, ES3Settings settings)
	{
		return ES3.GetFiles(new ES3Settings(directoryPath, settings));
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00003CBD File Offset: 0x00001EBD
	public static string[] GetFiles(ES3Settings settings)
	{
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetFiles();
		}
		if (settings.location != ES3.Location.File)
		{
			throw new NotSupportedException("ES3.GetFiles can only be used when the location is set to File or Cache.");
		}
		return ES3IO.GetFiles(settings.FullPath, false);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00003CED File Offset: 0x00001EED
	public static string[] GetDirectories()
	{
		return ES3.GetDirectories(new ES3Settings(null, null));
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00003CFB File Offset: 0x00001EFB
	public static string[] GetDirectories(string directoryPath)
	{
		return ES3.GetDirectories(new ES3Settings(directoryPath, null));
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00003D09 File Offset: 0x00001F09
	public static string[] GetDirectories(string directoryPath, ES3Settings settings)
	{
		return ES3.GetDirectories(new ES3Settings(directoryPath, settings));
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00003D17 File Offset: 0x00001F17
	public static string[] GetDirectories(ES3Settings settings)
	{
		if (settings.location != ES3.Location.File)
		{
			throw new NotSupportedException("ES3.GetDirectories can only be used when the location is set to File.");
		}
		return ES3IO.GetDirectories(settings.FullPath, false);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00003D38 File Offset: 0x00001F38
	public static void CreateBackup()
	{
		ES3.CreateBackup(new ES3Settings(null, null));
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00003D46 File Offset: 0x00001F46
	public static void CreateBackup(string filePath)
	{
		ES3.CreateBackup(new ES3Settings(filePath, null));
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00003D54 File Offset: 0x00001F54
	public static void CreateBackup(string filePath, ES3Settings settings)
	{
		ES3.CreateBackup(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00003D64 File Offset: 0x00001F64
	public static void CreateBackup(ES3Settings settings)
	{
		ES3Settings newSettings = new ES3Settings(settings.path + ".bac", settings);
		ES3.CopyFile(settings, newSettings);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00003D8F File Offset: 0x00001F8F
	public static bool RestoreBackup(string filePath)
	{
		return ES3.RestoreBackup(new ES3Settings(filePath, null));
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00003D9D File Offset: 0x00001F9D
	public static bool RestoreBackup(string filePath, ES3Settings settings)
	{
		return ES3.RestoreBackup(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00003DAC File Offset: 0x00001FAC
	public static bool RestoreBackup(ES3Settings settings)
	{
		ES3Settings es3Settings = new ES3Settings(settings.path + ".bac", settings);
		if (!ES3.FileExists(es3Settings))
		{
			return false;
		}
		ES3.RenameFile(es3Settings, settings);
		return true;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00003DE2 File Offset: 0x00001FE2
	public static DateTime GetTimestamp()
	{
		return ES3.GetTimestamp(new ES3Settings(null, null));
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00003DF0 File Offset: 0x00001FF0
	public static DateTime GetTimestamp(string filePath)
	{
		return ES3.GetTimestamp(new ES3Settings(filePath, null));
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00003DFE File Offset: 0x00001FFE
	public static DateTime GetTimestamp(string filePath, ES3Settings settings)
	{
		return ES3.GetTimestamp(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00003E0C File Offset: 0x0000200C
	public static DateTime GetTimestamp(ES3Settings settings)
	{
		if (settings.location == ES3.Location.File)
		{
			return ES3IO.GetTimestamp(settings.FullPath);
		}
		if (settings.location == ES3.Location.PlayerPrefs)
		{
			return new DateTime(long.Parse(PlayerPrefs.GetString("timestamp_" + settings.FullPath, "0")), DateTimeKind.Utc);
		}
		if (settings.location == ES3.Location.Cache)
		{
			return ES3File.GetTimestamp(settings);
		}
		return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00003E7D File Offset: 0x0000207D
	public static void StoreCachedFile()
	{
		ES3File.Store(null);
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00003E85 File Offset: 0x00002085
	public static void StoreCachedFile(string filePath)
	{
		ES3.StoreCachedFile(new ES3Settings(filePath, null));
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00003E93 File Offset: 0x00002093
	public static void StoreCachedFile(string filePath, ES3Settings settings)
	{
		ES3.StoreCachedFile(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00003EA1 File Offset: 0x000020A1
	public static void StoreCachedFile(ES3Settings settings)
	{
		ES3File.Store(settings);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00003EA9 File Offset: 0x000020A9
	public static void CacheFile()
	{
		ES3.CacheFile(new ES3Settings(null, null));
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00003EB7 File Offset: 0x000020B7
	public static void CacheFile(string filePath)
	{
		ES3.CacheFile(new ES3Settings(filePath, null));
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00003EC5 File Offset: 0x000020C5
	public static void CacheFile(string filePath, ES3Settings settings)
	{
		ES3.CacheFile(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x00003ED3 File Offset: 0x000020D3
	public static void CacheFile(ES3Settings settings)
	{
		ES3File.CacheFile(settings);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00003EDB File Offset: 0x000020DB
	public static void Init()
	{
		ES3Settings defaultSettings = ES3Settings.defaultSettings;
		string persistentDataPath = ES3IO.persistentDataPath;
		ES3TypeMgr.Init();
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00003EEE File Offset: 0x000020EE
	public ES3()
	{
	}

	// Token: 0x020000F0 RID: 240
	public enum Location
	{
		// Token: 0x04000195 RID: 405
		File,
		// Token: 0x04000196 RID: 406
		PlayerPrefs,
		// Token: 0x04000197 RID: 407
		InternalMS,
		// Token: 0x04000198 RID: 408
		Resources,
		// Token: 0x04000199 RID: 409
		Cache
	}

	// Token: 0x020000F1 RID: 241
	public enum Directory
	{
		// Token: 0x0400019B RID: 411
		PersistentDataPath,
		// Token: 0x0400019C RID: 412
		DataPath
	}

	// Token: 0x020000F2 RID: 242
	public enum EncryptionType
	{
		// Token: 0x0400019E RID: 414
		None,
		// Token: 0x0400019F RID: 415
		AES
	}

	// Token: 0x020000F3 RID: 243
	public enum CompressionType
	{
		// Token: 0x040001A1 RID: 417
		None,
		// Token: 0x040001A2 RID: 418
		Gzip
	}

	// Token: 0x020000F4 RID: 244
	public enum Format
	{
		// Token: 0x040001A4 RID: 420
		JSON
	}

	// Token: 0x020000F5 RID: 245
	public enum ReferenceMode
	{
		// Token: 0x040001A6 RID: 422
		ByRef,
		// Token: 0x040001A7 RID: 423
		ByValue,
		// Token: 0x040001A8 RID: 424
		ByRefAndValue
	}

	// Token: 0x020000F6 RID: 246
	public enum ImageType
	{
		// Token: 0x040001AA RID: 426
		JPEG,
		// Token: 0x040001AB RID: 427
		PNG
	}
}
