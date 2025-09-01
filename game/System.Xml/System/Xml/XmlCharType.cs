using System;
using System.Threading;

namespace System.Xml
{
	// Token: 0x02000224 RID: 548
	internal struct XmlCharType
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000805CC File Offset: 0x0007E7CC
		private static object StaticLock
		{
			get
			{
				if (XmlCharType.s_Lock == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref XmlCharType.s_Lock, value, null);
				}
				return XmlCharType.s_Lock;
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000805F8 File Offset: 0x0007E7F8
		private static void InitInstance()
		{
			object staticLock = XmlCharType.StaticLock;
			lock (staticLock)
			{
				if (XmlCharType.s_CharProperties == null)
				{
					byte[] chProps = new byte[65536];
					XmlCharType.SetProperties(chProps, "\t\n\r\r  ", 1);
					XmlCharType.SetProperties(chProps, "AZazÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣", 2);
					XmlCharType.SetProperties(chProps, "AZ__azÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣", 4);
					XmlCharType.SetProperties(chProps, "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ̀͠͡ͅΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ҃҆ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆֹֻֽֿֿׁׂ֑֣֡ׄׄאתװײءغـْ٠٩ٰڷںھۀێېۓە۪ۭۨ۰۹ँःअह़्॑॔क़ॣ०९ঁঃঅঌএঐওনপরললশহ়়াৄেৈো্ৗৗড়ঢ়য়ৣ০ৱਂਂਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ਼਼ਾੂੇੈੋ੍ਖ਼ੜਫ਼ਫ਼੦ੴઁઃઅઋઍઍએઑઓનપરલળવહ઼ૅેૉો્ૠૠ૦૯ଁଃଅଌଏଐଓନପରଲଳଶହ଼ୃେୈୋ୍ୖୗଡ଼ଢ଼ୟୡ୦୯ஂஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹாூெைொ்ௗௗ௧௯ఁఃఅఌఎఐఒనపళవహాౄెైొ్ౕౖౠౡ౦౯ಂಃಅಌಎಐಒನಪಳವಹಾೄೆೈೊ್ೕೖೞೞೠೡ೦೯ംഃഅഌഎഐഒനപഹാൃെൈൊ്ൗൗൠൡ൦൯กฮะฺเ๎๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະູົຽເໄໆໆ່ໍ໐໙༘༙༠༩༹༹༵༵༷༷༾ཇཉཀྵ྄ཱ྆ྋྐྕྗྗྙྭྱྷྐྵྐྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ⃐⃜⃡⃡ΩΩKÅ℮℮ↀↂ々々〇〇〡〯〱〵ぁゔ゙゚ゝゞァヺーヾㄅㄬ一龥가힣", 8);
					XmlCharType.SetProperties(chProps, "\t\n\r\r ퟿�", 16);
					XmlCharType.SetProperties(chProps, "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ̀͠͡ͅΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ҃҆ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆֹֻֽֿֿׁׂ֑֣֡ׄׄאתװײءغـْ٠٩ٰڷںھۀێېۓە۪ۭۨ۰۹ँःअह़्॑॔क़ॣ०९ঁঃঅঌএঐওনপরললশহ়়াৄেৈো্ৗৗড়ঢ়য়ৣ০ৱਂਂਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ਼਼ਾੂੇੈੋ੍ਖ਼ੜਫ਼ਫ਼੦ੴઁઃઅઋઍઍએઑઓનપરલળવહ઼ૅેૉો્ૠૠ૦૯ଁଃଅଌଏଐଓନପରଲଳଶହ଼ୃେୈୋ୍ୖୗଡ଼ଢ଼ୟୡ୦୯ஂஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹாூெைொ்ௗௗ௧௯ఁఃఅఌఎఐఒనపళవహాౄెైొ్ౕౖౠౡ౦౯ಂಃಅಌಎಐಒನಪಳವಹಾೄೆೈೊ್ೕೖೞೞೠೡ೦೯ംഃഅഌഎഐഒനപഹാൃെൈൊ്ൗൗൠൡ൦൯กฮะฺเ๎๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະູົຽເໄໆໆ່ໍ໐໙༘༙༠༩༹༹༵༵༷༷༾ཇཉཀྵ྄ཱ྆ྋྐྕྗྗྙྭྱྷྐྵྐྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ⃐⃜⃡⃡ΩΩKÅ℮℮ↀↂ々々〇〇〡〯〱〵ぁゔ゙゚ゝゞァヺーヾㄅㄬ一龥가힣", 32);
					XmlCharType.SetProperties(chProps, " %';=\\^퟿�", 64);
					XmlCharType.SetProperties(chProps, " !#%(;==?퟿�", 128);
					Thread.MemoryBarrier();
					XmlCharType.s_CharProperties = chProps;
				}
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000806BC File Offset: 0x0007E8BC
		private static void SetProperties(byte[] chProps, string ranges, byte value)
		{
			for (int i = 0; i < ranges.Length; i += 2)
			{
				int j = (int)ranges[i];
				int num = (int)ranges[i + 1];
				while (j <= num)
				{
					int num2 = j;
					chProps[num2] |= value;
					j++;
				}
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00080703 File Offset: 0x0007E903
		private XmlCharType(byte[] charProperties)
		{
			this.charProperties = charProperties;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0008070C File Offset: 0x0007E90C
		public static XmlCharType Instance
		{
			get
			{
				if (XmlCharType.s_CharProperties == null)
				{
					XmlCharType.InitInstance();
				}
				return new XmlCharType(XmlCharType.s_CharProperties);
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00080728 File Offset: 0x0007E928
		public bool IsWhiteSpace(char ch)
		{
			return (this.charProperties[(int)ch] & 1) > 0;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00080737 File Offset: 0x0007E937
		public bool IsExtender(char ch)
		{
			return ch == '·';
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00080741 File Offset: 0x0007E941
		public bool IsNCNameSingleChar(char ch)
		{
			return (this.charProperties[(int)ch] & 8) > 0;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00080750 File Offset: 0x0007E950
		public bool IsStartNCNameSingleChar(char ch)
		{
			return (this.charProperties[(int)ch] & 4) > 0;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0008075F File Offset: 0x0007E95F
		public bool IsNameSingleChar(char ch)
		{
			return this.IsNCNameSingleChar(ch) || ch == ':';
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00080771 File Offset: 0x0007E971
		public bool IsStartNameSingleChar(char ch)
		{
			return this.IsStartNCNameSingleChar(ch) || ch == ':';
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00080783 File Offset: 0x0007E983
		public bool IsCharData(char ch)
		{
			return (this.charProperties[(int)ch] & 16) > 0;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00080793 File Offset: 0x0007E993
		public bool IsPubidChar(char ch)
		{
			return ch < '\u0080' && ((int)"␀\0ﾻ꿿￿蟿￾߿"[(int)(ch >> 4)] & 1 << (int)(ch & '\u000f')) != 0;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000807B9 File Offset: 0x0007E9B9
		internal bool IsTextChar(char ch)
		{
			return (this.charProperties[(int)ch] & 64) > 0;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x000807C9 File Offset: 0x0007E9C9
		internal bool IsAttributeValueChar(char ch)
		{
			return (this.charProperties[(int)ch] & 128) > 0;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000807DC File Offset: 0x0007E9DC
		public bool IsLetter(char ch)
		{
			return (this.charProperties[(int)ch] & 2) > 0;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000807EB File Offset: 0x0007E9EB
		public bool IsNCNameCharXml4e(char ch)
		{
			return (this.charProperties[(int)ch] & 32) > 0;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000807FB File Offset: 0x0007E9FB
		public bool IsStartNCNameCharXml4e(char ch)
		{
			return this.IsLetter(ch) || ch == '_';
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0008080D File Offset: 0x0007EA0D
		public bool IsNameCharXml4e(char ch)
		{
			return this.IsNCNameCharXml4e(ch) || ch == ':';
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0008081F File Offset: 0x0007EA1F
		public bool IsStartNameCharXml4e(char ch)
		{
			return this.IsStartNCNameCharXml4e(ch) || ch == ':';
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00080831 File Offset: 0x0007EA31
		public static bool IsDigit(char ch)
		{
			return XmlCharType.InRange((int)ch, 48, 57);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0008083D File Offset: 0x0007EA3D
		public static bool IsHexDigit(char ch)
		{
			return XmlCharType.InRange((int)ch, 48, 57) || XmlCharType.InRange((int)ch, 97, 102) || XmlCharType.InRange((int)ch, 65, 70);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00080863 File Offset: 0x0007EA63
		internal static bool IsHighSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 56319);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00080875 File Offset: 0x0007EA75
		internal static bool IsLowSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 56320, 57343);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00080887 File Offset: 0x0007EA87
		internal static bool IsSurrogate(int ch)
		{
			return XmlCharType.InRange(ch, 55296, 57343);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00080899 File Offset: 0x0007EA99
		internal static int CombineSurrogateChar(int lowChar, int highChar)
		{
			return lowChar - 56320 | (highChar - 55296 << 10) + 65536;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x000808B4 File Offset: 0x0007EAB4
		internal static void SplitSurrogateChar(int combinedChar, out char lowChar, out char highChar)
		{
			int num = combinedChar - 65536;
			lowChar = (char)(56320 + num % 1024);
			highChar = (char)(55296 + num / 1024);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000808E9 File Offset: 0x0007EAE9
		internal bool IsOnlyWhitespace(string str)
		{
			return this.IsOnlyWhitespaceWithPos(str) == -1;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000808F8 File Offset: 0x0007EAF8
		internal int IsOnlyWhitespaceWithPos(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[(int)str[i]] & 1) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00080930 File Offset: 0x0007EB30
		internal int IsOnlyCharData(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if ((this.charProperties[(int)str[i]] & 16) == 0)
					{
						if (i + 1 >= str.Length || !XmlCharType.IsHighSurrogate((int)str[i]) || !XmlCharType.IsLowSurrogate((int)str[i + 1]))
						{
							return i;
						}
						i++;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00080994 File Offset: 0x0007EB94
		internal static bool IsOnlyDigits(string str, int startPos, int len)
		{
			for (int i = startPos; i < startPos + len; i++)
			{
				if (!XmlCharType.IsDigit(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000809C0 File Offset: 0x0007EBC0
		internal static bool IsOnlyDigits(char[] chars, int startPos, int len)
		{
			for (int i = startPos; i < startPos + len; i++)
			{
				if (!XmlCharType.IsDigit(chars[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x000809E8 File Offset: 0x0007EBE8
		internal int IsPublicId(string str)
		{
			if (str != null)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (!this.IsPubidChar(str[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00080A1B File Offset: 0x0007EC1B
		private static bool InRange(int value, int start, int end)
		{
			return value - start <= end - start;
		}

		// Token: 0x04001294 RID: 4756
		internal const int SurHighStart = 55296;

		// Token: 0x04001295 RID: 4757
		internal const int SurHighEnd = 56319;

		// Token: 0x04001296 RID: 4758
		internal const int SurLowStart = 56320;

		// Token: 0x04001297 RID: 4759
		internal const int SurLowEnd = 57343;

		// Token: 0x04001298 RID: 4760
		internal const int SurMask = 64512;

		// Token: 0x04001299 RID: 4761
		internal const int fWhitespace = 1;

		// Token: 0x0400129A RID: 4762
		internal const int fLetter = 2;

		// Token: 0x0400129B RID: 4763
		internal const int fNCStartNameSC = 4;

		// Token: 0x0400129C RID: 4764
		internal const int fNCNameSC = 8;

		// Token: 0x0400129D RID: 4765
		internal const int fCharData = 16;

		// Token: 0x0400129E RID: 4766
		internal const int fNCNameXml4e = 32;

		// Token: 0x0400129F RID: 4767
		internal const int fText = 64;

		// Token: 0x040012A0 RID: 4768
		internal const int fAttrValue = 128;

		// Token: 0x040012A1 RID: 4769
		private const string s_PublicIdBitmap = "␀\0ﾻ꿿￿蟿￾߿";

		// Token: 0x040012A2 RID: 4770
		private const uint CharPropertiesSize = 65536U;

		// Token: 0x040012A3 RID: 4771
		internal const string s_Whitespace = "\t\n\r\r  ";

		// Token: 0x040012A4 RID: 4772
		private const string s_NCStartName = "AZ__azÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣";

		// Token: 0x040012A5 RID: 4773
		private const string s_NCName = "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ̀͠͡ͅΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ҃҆ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆֹֻֽֿֿׁׂ֑֣֡ׄׄאתװײءغـْ٠٩ٰڷںھۀێېۓە۪ۭۨ۰۹ँःअह़्॑॔क़ॣ०९ঁঃঅঌএঐওনপরললশহ়়াৄেৈো্ৗৗড়ঢ়য়ৣ০ৱਂਂਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ਼਼ਾੂੇੈੋ੍ਖ਼ੜਫ਼ਫ਼੦ੴઁઃઅઋઍઍએઑઓનપરલળવહ઼ૅેૉો્ૠૠ૦૯ଁଃଅଌଏଐଓନପରଲଳଶହ଼ୃେୈୋ୍ୖୗଡ଼ଢ଼ୟୡ୦୯ஂஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹாூெைொ்ௗௗ௧௯ఁఃఅఌఎఐఒనపళవహాౄెైొ్ౕౖౠౡ౦౯ಂಃಅಌಎಐಒನಪಳವಹಾೄೆೈೊ್ೕೖೞೞೠೡ೦೯ംഃഅഌഎഐഒനപഹാൃെൈൊ്ൗൗൠൡ൦൯กฮะฺเ๎๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະູົຽເໄໆໆ່ໍ໐໙༘༙༠༩༹༹༵༵༷༷༾ཇཉཀྵ྄ཱ྆ྋྐྕྗྗྙྭྱྷྐྵྐྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ⃐⃜⃡⃡ΩΩKÅ℮℮ↀↂ々々〇〇〡〯〱〵ぁゔ゙゚ゝゞァヺーヾㄅㄬ一龥가힣";

		// Token: 0x040012A6 RID: 4774
		private const string s_CharData = "\t\n\r\r ퟿�";

		// Token: 0x040012A7 RID: 4775
		private const string s_PublicID = "\n\n\r\r !#%';==?Z__az";

		// Token: 0x040012A8 RID: 4776
		private const string s_Text = " %';=\\^퟿�";

		// Token: 0x040012A9 RID: 4777
		private const string s_AttrValue = " !#%(;==?퟿�";

		// Token: 0x040012AA RID: 4778
		private const string s_LetterXml4e = "AZazÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂ〇〇〡〩ぁゔァヺㄅㄬ一龥가힣";

		// Token: 0x040012AB RID: 4779
		private const string s_NCNameXml4e = "-.09AZ__az··ÀÖØöøıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁːˑ̀͠͡ͅΆΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁ҃҆ҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆֹֻֽֿֿׁׂ֑֣֡ׄׄאתװײءغـْ٠٩ٰڷںھۀێېۓە۪ۭۨ۰۹ँःअह़्॑॔क़ॣ०९ঁঃঅঌএঐওনপরললশহ়়াৄেৈো্ৗৗড়ঢ়য়ৣ০ৱਂਂਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹ਼਼ਾੂੇੈੋ੍ਖ਼ੜਫ਼ਫ਼੦ੴઁઃઅઋઍઍએઑઓનપરલળવહ઼ૅેૉો્ૠૠ૦૯ଁଃଅଌଏଐଓନପରଲଳଶହ଼ୃେୈୋ୍ୖୗଡ଼ଢ଼ୟୡ୦୯ஂஃஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹாூெைொ்ௗௗ௧௯ఁఃఅఌఎఐఒనపళవహాౄెైొ్ౕౖౠౡ౦౯ಂಃಅಌಎಐಒನಪಳವಹಾೄೆೈೊ್ೕೖೞೞೠೡ೦೯ംഃഅഌഎഐഒനപഹാൃെൈൊ്ൗൗൠൡ൦൯กฮะฺเ๎๐๙ກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະູົຽເໄໆໆ່ໍ໐໙༘༙༠༩༹༹༵༵༷༷༾ཇཉཀྵ྄ཱ྆ྋྐྕྗྗྙྭྱྷྐྵྐྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼ⃐⃜⃡⃡ΩΩKÅ℮℮ↀↂ々々〇〇〡〯〱〵ぁゔ゙゚ゝゞァヺーヾㄅㄬ一龥가힣";

		// Token: 0x040012AC RID: 4780
		private static object s_Lock;

		// Token: 0x040012AD RID: 4781
		private static volatile byte[] s_CharProperties;

		// Token: 0x040012AE RID: 4782
		internal byte[] charProperties;
	}
}
