using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000077 RID: 119
	[Preserve]
	[ES3Properties(new string[]
	{
		"name",
		"samples",
		"channels",
		"frequency",
		"sampleData"
	})]
	public class ES3Type_AudioClip : ES3UnityObjectType
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000F3D1 File Offset: 0x0000D5D1
		public ES3Type_AudioClip() : base(typeof(AudioClip))
		{
			ES3Type_AudioClip.Instance = this;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			AudioClip audioClip = (AudioClip)obj;
			float[] array = new float[audioClip.samples * audioClip.channels];
			audioClip.GetData(array, 0);
			writer.WriteProperty("name", audioClip.name);
			writer.WriteProperty("samples", audioClip.samples);
			writer.WriteProperty("channels", audioClip.channels);
			writer.WriteProperty("frequency", audioClip.frequency);
			writer.WriteProperty("sampleData", array);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F47C File Offset: 0x0000D67C
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			AudioClip audioClip = (AudioClip)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "sampleData")
					{
						audioClip.SetData(reader.Read<float[]>(ES3Type_floatArray.Instance), 0);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			string name = "";
			int lengthSamples = 0;
			int channels = 0;
			int frequency = 0;
			AudioClip audioClip = null;
			foreach (object obj in reader.Properties)
			{
				string a = (string)obj;
				if (!(a == "name"))
				{
					if (!(a == "samples"))
					{
						if (!(a == "channels"))
						{
							if (!(a == "frequency"))
							{
								if (!(a == "sampleData"))
								{
									reader.Skip();
								}
								else
								{
									audioClip = AudioClip.Create(name, lengthSamples, channels, frequency, false);
									audioClip.SetData(reader.Read<float[]>(ES3Type_floatArray.Instance), 0);
								}
							}
							else
							{
								frequency = reader.Read<int>(ES3Type_int.Instance);
							}
						}
						else
						{
							channels = reader.Read<int>(ES3Type_int.Instance);
						}
					}
					else
					{
						lengthSamples = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					name = reader.Read<string>(ES3Type_string.Instance);
				}
			}
			return audioClip;
		}

		// Token: 0x040000C7 RID: 199
		public static ES3Type Instance;
	}
}
