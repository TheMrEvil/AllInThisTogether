using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000469 RID: 1129
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlCollation
	{
		// Token: 0x06002BBD RID: 11197 RVA: 0x00104DB2 File Offset: 0x00102FB2
		private XmlCollation(CultureInfo cultureInfo, XmlCollation.Options options)
		{
			this.cultInfo = cultureInfo;
			this.options = options;
			this.compops = options.CompareOptions;
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x00104DD5 File Offset: 0x00102FD5
		internal static XmlCollation CodePointCollation
		{
			get
			{
				return XmlCollation.cp;
			}
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00104DDC File Offset: 0x00102FDC
		internal static XmlCollation Create(string collationLiteral)
		{
			return XmlCollation.Create(collationLiteral, true);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00104DE8 File Offset: 0x00102FE8
		internal static XmlCollation Create(string collationLiteral, bool throwOnError)
		{
			if (collationLiteral == "http://www.w3.org/2004/10/xpath-functions/collation/codepoint")
			{
				return XmlCollation.CodePointCollation;
			}
			CultureInfo cultureInfo = null;
			XmlCollation.Options options = default(XmlCollation.Options);
			Uri uri;
			if (throwOnError)
			{
				uri = new Uri(collationLiteral);
			}
			else if (!Uri.TryCreate(collationLiteral, UriKind.Absolute, out uri))
			{
				return null;
			}
			if (uri.GetLeftPart(UriPartial.Authority) == "http://collations.microsoft.com")
			{
				string text = uri.LocalPath.Substring(1);
				if (text.Length == 0)
				{
					goto IL_C7;
				}
				try
				{
					cultureInfo = new CultureInfo(text);
					goto IL_C7;
				}
				catch (ArgumentException)
				{
					if (!throwOnError)
					{
						return null;
					}
					throw new XslTransformException("Collation language '{0}' is not supported.", new string[]
					{
						text
					});
				}
			}
			if (uri.IsBaseOf(new Uri("http://www.w3.org/2004/10/xpath-functions/collation/codepoint")))
			{
				options.CompareOptions = CompareOptions.Ordinal;
			}
			else
			{
				if (!throwOnError)
				{
					return null;
				}
				throw new XslTransformException("The collation '{0}' is not supported.", new string[]
				{
					collationLiteral
				});
			}
			IL_C7:
			string query = uri.Query;
			string text2 = null;
			if (query.Length != 0)
			{
				string[] array = query.Substring(1).Split('&', StringSplitOptions.None);
				int i = 0;
				while (i < array.Length)
				{
					string text3 = array[i];
					string[] array2 = text3.Split('=', StringSplitOptions.None);
					if (array2.Length != 2)
					{
						if (!throwOnError)
						{
							return null;
						}
						throw new XslTransformException("Collation option '{0}' is invalid. Options must have the following format: <option-name>=<option-value>.", new string[]
						{
							text3
						});
					}
					else
					{
						string text4 = array2[0].ToUpper(CultureInfo.InvariantCulture);
						string text5 = array2[1].ToUpper(CultureInfo.InvariantCulture);
						if (text4 == "SORT")
						{
							text2 = text5;
						}
						else
						{
							uint num = <PrivateImplementationDetails>.ComputeStringHash(text4);
							int flag;
							if (num <= 1153929311U)
							{
								if (num <= 399689514U)
								{
									if (num != 346004547U)
									{
										if (num != 399689514U)
										{
											goto IL_2AB;
										}
										if (!(text4 == "IGNOREKANATYPE"))
										{
											goto IL_2AB;
										}
										flag = 8;
									}
									else
									{
										if (!(text4 == "UPPERFIRST"))
										{
											goto IL_2AB;
										}
										flag = 4096;
									}
								}
								else if (num != 542255445U)
								{
									if (num != 1153929311U)
									{
										goto IL_2AB;
									}
									if (!(text4 == "IGNORECASE"))
									{
										goto IL_2AB;
									}
									flag = 1;
								}
								else
								{
									if (!(text4 == "IGNOREWIDTH"))
									{
										goto IL_2AB;
									}
									flag = 16;
								}
							}
							else if (num <= 1618186332U)
							{
								if (num != 1537080989U)
								{
									if (num != 1618186332U)
									{
										goto IL_2AB;
									}
									if (!(text4 == "IGNORENONSPACE"))
									{
										goto IL_2AB;
									}
									flag = 2;
								}
								else
								{
									if (!(text4 == "DESCENDINGORDER"))
									{
										goto IL_2AB;
									}
									flag = 16384;
								}
							}
							else if (num != 1721049792U)
							{
								if (num != 3407466425U)
								{
									goto IL_2AB;
								}
								if (!(text4 == "EMPTYGREATEST"))
								{
									goto IL_2AB;
								}
								flag = 8192;
							}
							else
							{
								if (!(text4 == "IGNORESYMBOLS"))
								{
									goto IL_2AB;
								}
								flag = 4;
							}
							if (text5 == "0" || text5 == "FALSE")
							{
								options.SetFlag(flag, false);
								goto IL_33E;
							}
							if (text5 == "1" || text5 == "TRUE")
							{
								options.SetFlag(flag, true);
								goto IL_33E;
							}
							if (!throwOnError)
							{
								return null;
							}
							throw new XslTransformException("Collation option '{0}' cannot have the value '{1}'.", new string[]
							{
								array2[0],
								array2[1]
							});
							IL_2AB:
							if (!throwOnError)
							{
								return null;
							}
							throw new XslTransformException("Unsupported option '{0}' in collation.", new string[]
							{
								array2[0]
							});
						}
						IL_33E:
						i++;
					}
				}
			}
			if (options.UpperFirst && options.IgnoreCase)
			{
				options.UpperFirst = false;
			}
			if (options.Ordinal)
			{
				options.CompareOptions = CompareOptions.Ordinal;
				options.UpperFirst = false;
			}
			if (text2 != null && cultureInfo != null)
			{
				int langID = XmlCollation.GetLangID(cultureInfo.LCID);
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1363454193U)
				{
					if (num <= 1283486598U)
					{
						if (num != 1278716217U)
						{
							if (num == 1283486598U)
							{
								if (text2 == "trad")
								{
									goto IL_5DE;
								}
							}
						}
						else if (text2 == "dict")
						{
							goto IL_5DE;
						}
					}
					else if (num != 1339334217U)
					{
						if (num == 1363454193U)
						{
							if (text2 == "phn")
							{
								if (langID == 1031)
								{
									cultureInfo = new CultureInfo(66567);
									goto IL_5DE;
								}
								goto IL_5DE;
							}
						}
					}
					else if (text2 == "uni")
					{
						if (langID == 1041 || langID == 1042)
						{
							cultureInfo = new CultureInfo(XmlCollation.MakeLCID(cultureInfo.LCID, 1));
							goto IL_5DE;
						}
						goto IL_5DE;
					}
				}
				else if (num <= 3314303423U)
				{
					if (num != 2751005041U)
					{
						if (num == 3314303423U)
						{
							if (text2 == "bopo")
							{
								if (langID == 1028)
								{
									cultureInfo = new CultureInfo(197636);
									goto IL_5DE;
								}
								goto IL_5DE;
							}
						}
					}
					else if (text2 == "tech")
					{
						if (langID == 1038)
						{
							cultureInfo = new CultureInfo(66574);
							goto IL_5DE;
						}
						goto IL_5DE;
					}
				}
				else if (num != 3629878817U)
				{
					if (num != 3751703171U)
					{
						if (num == 3879610370U)
						{
							if (text2 == "pron")
							{
								goto IL_5DE;
							}
						}
					}
					else if (text2 == "mod")
					{
						if (langID == 1079)
						{
							cultureInfo = new CultureInfo(66615);
							goto IL_5DE;
						}
						goto IL_5DE;
					}
				}
				else if (text2 == "strk")
				{
					if (langID == 2052 || langID == 3076 || langID == 4100 || langID == 5124)
					{
						cultureInfo = new CultureInfo(XmlCollation.MakeLCID(cultureInfo.LCID, 2));
						goto IL_5DE;
					}
					goto IL_5DE;
				}
				if (!throwOnError)
				{
					return null;
				}
				throw new XslTransformException("Unsupported sort option '{0}' in collation.", new string[]
				{
					text2
				});
			}
			IL_5DE:
			return new XmlCollation(cultureInfo, options);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x001053F0 File Offset: 0x001035F0
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			XmlCollation xmlCollation = obj as XmlCollation;
			return xmlCollation != null && this.options == xmlCollation.options && object.Equals(this.cultInfo, xmlCollation.cultInfo);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x00105438 File Offset: 0x00103638
		public override int GetHashCode()
		{
			int num = this.options;
			if (this.cultInfo != null)
			{
				num ^= this.cultInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x00105468 File Offset: 0x00103668
		internal void GetObjectData(BinaryWriter writer)
		{
			writer.Write((this.cultInfo != null) ? this.cultInfo.LCID : -1);
			writer.Write(this.options);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x00105498 File Offset: 0x00103698
		internal XmlCollation(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			this.cultInfo = ((num != -1) ? new CultureInfo(num) : null);
			this.options = new XmlCollation.Options(reader.ReadInt32());
			this.compops = this.options.CompareOptions;
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x001054E7 File Offset: 0x001036E7
		internal bool UpperFirst
		{
			get
			{
				return this.options.UpperFirst;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x001054F4 File Offset: 0x001036F4
		internal bool EmptyGreatest
		{
			get
			{
				return this.options.EmptyGreatest;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x00105501 File Offset: 0x00103701
		internal bool DescendingOrder
		{
			get
			{
				return this.options.DescendingOrder;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x0010550E File Offset: 0x0010370E
		internal CultureInfo Culture
		{
			get
			{
				if (this.cultInfo == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this.cultInfo;
			}
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x00105524 File Offset: 0x00103724
		internal XmlSortKey CreateSortKey(string s)
		{
			SortKey sortKey = this.Culture.CompareInfo.GetSortKey(s, this.compops);
			if (!this.UpperFirst)
			{
				return new XmlStringSortKey(sortKey, this.DescendingOrder);
			}
			byte[] keyData = sortKey.KeyData;
			if (this.UpperFirst && keyData.Length != 0)
			{
				int num = 0;
				while (keyData[num] != 1)
				{
					num++;
				}
				do
				{
					num++;
				}
				while (keyData[num] != 1);
				do
				{
					num++;
					byte[] array = keyData;
					int num2 = num;
					array[num2] ^= byte.MaxValue;
				}
				while (keyData[num] != 254);
			}
			return new XmlStringSortKey(keyData, this.DescendingOrder);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x001055B4 File Offset: 0x001037B4
		private static int MakeLCID(int langid, int sortid)
		{
			return (langid & 65535) | (sortid & 15) << 16;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x001055C5 File Offset: 0x001037C5
		private static int GetLangID(int lcid)
		{
			return lcid & 65535;
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x001055CE File Offset: 0x001037CE
		// Note: this type is marked as 'beforefieldinit'.
		static XmlCollation()
		{
		}

		// Token: 0x040022A3 RID: 8867
		private const int deDE = 1031;

		// Token: 0x040022A4 RID: 8868
		private const int huHU = 1038;

		// Token: 0x040022A5 RID: 8869
		private const int jaJP = 1041;

		// Token: 0x040022A6 RID: 8870
		private const int kaGE = 1079;

		// Token: 0x040022A7 RID: 8871
		private const int koKR = 1042;

		// Token: 0x040022A8 RID: 8872
		private const int zhTW = 1028;

		// Token: 0x040022A9 RID: 8873
		private const int zhCN = 2052;

		// Token: 0x040022AA RID: 8874
		private const int zhHK = 3076;

		// Token: 0x040022AB RID: 8875
		private const int zhSG = 4100;

		// Token: 0x040022AC RID: 8876
		private const int zhMO = 5124;

		// Token: 0x040022AD RID: 8877
		private const int zhTWbopo = 197636;

		// Token: 0x040022AE RID: 8878
		private const int deDEphon = 66567;

		// Token: 0x040022AF RID: 8879
		private const int huHUtech = 66574;

		// Token: 0x040022B0 RID: 8880
		private const int kaGEmode = 66615;

		// Token: 0x040022B1 RID: 8881
		private CultureInfo cultInfo;

		// Token: 0x040022B2 RID: 8882
		private XmlCollation.Options options;

		// Token: 0x040022B3 RID: 8883
		private CompareOptions compops;

		// Token: 0x040022B4 RID: 8884
		private static XmlCollation cp = new XmlCollation(CultureInfo.InvariantCulture, new XmlCollation.Options(1073741824));

		// Token: 0x040022B5 RID: 8885
		private const int LOCALE_CURRENT = -1;

		// Token: 0x0200046A RID: 1130
		private struct Options
		{
			// Token: 0x06002BCD RID: 11213 RVA: 0x001055E9 File Offset: 0x001037E9
			public Options(int value)
			{
				this.value = value;
			}

			// Token: 0x06002BCE RID: 11214 RVA: 0x001055F2 File Offset: 0x001037F2
			public bool GetFlag(int flag)
			{
				return (this.value & flag) != 0;
			}

			// Token: 0x06002BCF RID: 11215 RVA: 0x001055FF File Offset: 0x001037FF
			public void SetFlag(int flag, bool value)
			{
				if (value)
				{
					this.value |= flag;
					return;
				}
				this.value &= ~flag;
			}

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x00105622 File Offset: 0x00103822
			// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x0010562F File Offset: 0x0010382F
			public bool UpperFirst
			{
				get
				{
					return this.GetFlag(4096);
				}
				set
				{
					this.SetFlag(4096, value);
				}
			}

			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x0010563D File Offset: 0x0010383D
			public bool EmptyGreatest
			{
				get
				{
					return this.GetFlag(8192);
				}
			}

			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x0010564A File Offset: 0x0010384A
			public bool DescendingOrder
			{
				get
				{
					return this.GetFlag(16384);
				}
			}

			// Token: 0x17000850 RID: 2128
			// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x00105657 File Offset: 0x00103857
			public bool IgnoreCase
			{
				get
				{
					return this.GetFlag(1);
				}
			}

			// Token: 0x17000851 RID: 2129
			// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x00105660 File Offset: 0x00103860
			public bool Ordinal
			{
				get
				{
					return this.GetFlag(1073741824);
				}
			}

			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x0010566D File Offset: 0x0010386D
			// (set) Token: 0x06002BD7 RID: 11223 RVA: 0x0010567B File Offset: 0x0010387B
			public CompareOptions CompareOptions
			{
				get
				{
					return (CompareOptions)(this.value & -28673);
				}
				set
				{
					this.value = ((this.value & 28672) | (int)value);
				}
			}

			// Token: 0x06002BD8 RID: 11224 RVA: 0x00105691 File Offset: 0x00103891
			public static implicit operator int(XmlCollation.Options options)
			{
				return options.value;
			}

			// Token: 0x040022B6 RID: 8886
			public const int FlagUpperFirst = 4096;

			// Token: 0x040022B7 RID: 8887
			public const int FlagEmptyGreatest = 8192;

			// Token: 0x040022B8 RID: 8888
			public const int FlagDescendingOrder = 16384;

			// Token: 0x040022B9 RID: 8889
			private const int Mask = 28672;

			// Token: 0x040022BA RID: 8890
			private int value;
		}
	}
}
