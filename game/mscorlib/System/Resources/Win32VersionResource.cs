using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000878 RID: 2168
	internal class Win32VersionResource : Win32Resource
	{
		// Token: 0x06004823 RID: 18467 RVA: 0x000ED27C File Offset: 0x000EB47C
		public Win32VersionResource(int id, int language, bool compilercontext) : base(Win32ResourceType.RT_VERSION, id, language)
		{
			this.signature = (long)((ulong)-17890115);
			this.struct_version = 65536;
			this.file_flags_mask = 63;
			this.file_flags = 0;
			this.file_os = 4;
			this.file_type = 2;
			this.file_subtype = 0;
			this.file_date = 0L;
			this.file_lang = (compilercontext ? 0 : 127);
			this.file_codepage = 1200;
			this.properties = new Hashtable();
			string value = compilercontext ? string.Empty : " ";
			foreach (string key in this.WellKnownProperties)
			{
				this.properties[key] = value;
			}
			this.LegalCopyright = " ";
			this.FileDescription = " ";
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x000ED394 File Offset: 0x000EB594
		// (set) Token: 0x06004825 RID: 18469 RVA: 0x000ED428 File Offset: 0x000EB628
		public string Version
		{
			get
			{
				return string.Concat(new string[]
				{
					(this.file_version >> 48).ToString(),
					".",
					(this.file_version >> 32 & 65535L).ToString(),
					".",
					(this.file_version >> 16 & 65535L).ToString(),
					".",
					(this.file_version & 65535L).ToString()
				});
			}
			set
			{
				long[] array = new long[4];
				if (value != null)
				{
					string[] array2 = value.Split('.', StringSplitOptions.None);
					try
					{
						for (int i = 0; i < array2.Length; i++)
						{
							if (i < array.Length)
							{
								array[i] = (long)int.Parse(array2[i]);
							}
						}
					}
					catch (FormatException)
					{
					}
				}
				this.file_version = (array[0] << 48 | array[1] << 32 | (array[2] << 16) + array[3]);
				this.properties["FileVersion"] = this.Version;
			}
		}

		// Token: 0x17000B14 RID: 2836
		public virtual string this[string key]
		{
			set
			{
				this.properties[key] = value;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x000ED4C3 File Offset: 0x000EB6C3
		// (set) Token: 0x06004828 RID: 18472 RVA: 0x000ED4DA File Offset: 0x000EB6DA
		public virtual string Comments
		{
			get
			{
				return (string)this.properties["Comments"];
			}
			set
			{
				this.properties["Comments"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x000ED501 File Offset: 0x000EB701
		// (set) Token: 0x0600482A RID: 18474 RVA: 0x000ED518 File Offset: 0x000EB718
		public virtual string CompanyName
		{
			get
			{
				return (string)this.properties["CompanyName"];
			}
			set
			{
				this.properties["CompanyName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x000ED53F File Offset: 0x000EB73F
		// (set) Token: 0x0600482C RID: 18476 RVA: 0x000ED556 File Offset: 0x000EB756
		public virtual string LegalCopyright
		{
			get
			{
				return (string)this.properties["LegalCopyright"];
			}
			set
			{
				this.properties["LegalCopyright"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x000ED57D File Offset: 0x000EB77D
		// (set) Token: 0x0600482E RID: 18478 RVA: 0x000ED594 File Offset: 0x000EB794
		public virtual string LegalTrademarks
		{
			get
			{
				return (string)this.properties["LegalTrademarks"];
			}
			set
			{
				this.properties["LegalTrademarks"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x000ED5BB File Offset: 0x000EB7BB
		// (set) Token: 0x06004830 RID: 18480 RVA: 0x000ED5D2 File Offset: 0x000EB7D2
		public virtual string OriginalFilename
		{
			get
			{
				return (string)this.properties["OriginalFilename"];
			}
			set
			{
				this.properties["OriginalFilename"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004831 RID: 18481 RVA: 0x000ED5F9 File Offset: 0x000EB7F9
		// (set) Token: 0x06004832 RID: 18482 RVA: 0x000ED610 File Offset: 0x000EB810
		public virtual string ProductName
		{
			get
			{
				return (string)this.properties["ProductName"];
			}
			set
			{
				this.properties["ProductName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x000ED637 File Offset: 0x000EB837
		// (set) Token: 0x06004834 RID: 18484 RVA: 0x000ED650 File Offset: 0x000EB850
		public virtual string ProductVersion
		{
			get
			{
				return (string)this.properties["ProductVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split('.', StringSplitOptions.None);
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["ProductVersion"] = value;
				this.product_version = (array[0] << 48 | array[1] << 32 | (array[2] << 16) + array[3]);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x000ED6E4 File Offset: 0x000EB8E4
		// (set) Token: 0x06004836 RID: 18486 RVA: 0x000ED6FB File Offset: 0x000EB8FB
		public virtual string InternalName
		{
			get
			{
				return (string)this.properties["InternalName"];
			}
			set
			{
				this.properties["InternalName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004837 RID: 18487 RVA: 0x000ED722 File Offset: 0x000EB922
		// (set) Token: 0x06004838 RID: 18488 RVA: 0x000ED739 File Offset: 0x000EB939
		public virtual string FileDescription
		{
			get
			{
				return (string)this.properties["FileDescription"];
			}
			set
			{
				this.properties["FileDescription"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x000ED760 File Offset: 0x000EB960
		// (set) Token: 0x0600483A RID: 18490 RVA: 0x000ED768 File Offset: 0x000EB968
		public virtual int FileLanguage
		{
			get
			{
				return this.file_lang;
			}
			set
			{
				this.file_lang = value;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600483B RID: 18491 RVA: 0x000ED771 File Offset: 0x000EB971
		// (set) Token: 0x0600483C RID: 18492 RVA: 0x000ED788 File Offset: 0x000EB988
		public virtual string FileVersion
		{
			get
			{
				return (string)this.properties["FileVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split('.', StringSplitOptions.None);
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["FileVersion"] = value;
				this.file_version = (array[0] << 48 | array[1] << 32 | (array[2] << 16) + array[3]);
			}
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x000ED81C File Offset: 0x000EBA1C
		private void emit_padding(BinaryWriter w)
		{
			if (w.BaseStream.Position % 4L != 0L)
			{
				w.Write(0);
			}
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x000ED838 File Offset: 0x000EBA38
		private void patch_length(BinaryWriter w, long len_pos)
		{
			Stream baseStream = w.BaseStream;
			long position = baseStream.Position;
			baseStream.Position = len_pos;
			w.Write((short)(position - len_pos));
			baseStream.Position = position;
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x000ED86C File Offset: 0x000EBA6C
		public override void WriteTo(Stream ms)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(ms, Encoding.Unicode))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(52);
				binaryWriter.Write(0);
				binaryWriter.Write("VS_VERSION_INFO".ToCharArray());
				binaryWriter.Write(0);
				this.emit_padding(binaryWriter);
				binaryWriter.Write((uint)this.signature);
				binaryWriter.Write(this.struct_version);
				binaryWriter.Write((int)(this.file_version >> 32));
				binaryWriter.Write((int)(this.file_version & (long)((ulong)-1)));
				binaryWriter.Write((int)(this.product_version >> 32));
				binaryWriter.Write((int)(this.product_version & (long)((ulong)-1)));
				binaryWriter.Write(this.file_flags_mask);
				binaryWriter.Write(this.file_flags);
				binaryWriter.Write(this.file_os);
				binaryWriter.Write(this.file_type);
				binaryWriter.Write(this.file_subtype);
				binaryWriter.Write((int)(this.file_date >> 32));
				binaryWriter.Write((int)(this.file_date & (long)((ulong)-1)));
				this.emit_padding(binaryWriter);
				long position = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("VarFileInfo".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				long position2 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(4);
				binaryWriter.Write(0);
				binaryWriter.Write("Translation".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				binaryWriter.Write((short)this.file_lang);
				binaryWriter.Write((short)this.file_codepage);
				this.patch_length(binaryWriter, position2);
				this.patch_length(binaryWriter, position);
				long position3 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("StringFileInfo".ToCharArray());
				this.emit_padding(binaryWriter);
				long position4 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write(string.Format("{0:x4}{1:x4}", this.file_lang, this.file_codepage).ToCharArray());
				this.emit_padding(binaryWriter);
				foreach (object obj in this.properties.Keys)
				{
					string text = (string)obj;
					string text2 = (string)this.properties[text];
					long position5 = ms.Position;
					binaryWriter.Write(0);
					binaryWriter.Write((short)(text2.ToCharArray().Length + 1));
					binaryWriter.Write(1);
					binaryWriter.Write(text.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					binaryWriter.Write(text2.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					this.patch_length(binaryWriter, position5);
				}
				this.patch_length(binaryWriter, position4);
				this.patch_length(binaryWriter, position3);
				this.patch_length(binaryWriter, 0L);
			}
		}

		// Token: 0x04002E30 RID: 11824
		public string[] WellKnownProperties = new string[]
		{
			"Comments",
			"CompanyName",
			"FileVersion",
			"InternalName",
			"LegalTrademarks",
			"OriginalFilename",
			"ProductName",
			"ProductVersion"
		};

		// Token: 0x04002E31 RID: 11825
		private long signature;

		// Token: 0x04002E32 RID: 11826
		private int struct_version;

		// Token: 0x04002E33 RID: 11827
		private long file_version;

		// Token: 0x04002E34 RID: 11828
		private long product_version;

		// Token: 0x04002E35 RID: 11829
		private int file_flags_mask;

		// Token: 0x04002E36 RID: 11830
		private int file_flags;

		// Token: 0x04002E37 RID: 11831
		private int file_os;

		// Token: 0x04002E38 RID: 11832
		private int file_type;

		// Token: 0x04002E39 RID: 11833
		private int file_subtype;

		// Token: 0x04002E3A RID: 11834
		private long file_date;

		// Token: 0x04002E3B RID: 11835
		private int file_lang;

		// Token: 0x04002E3C RID: 11836
		private int file_codepage;

		// Token: 0x04002E3D RID: 11837
		private Hashtable properties;
	}
}
