using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Platform.Installations;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Data;

namespace Parse.Platform.Installations
{
	// Token: 0x02000033 RID: 51
	public class ParseInstallationCoder : IParseInstallationCoder
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000A51D File Offset: 0x0000871D
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000A525 File Offset: 0x00008725
		private IParseObjectClassController ClassController
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassController>k__BackingField;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A530 File Offset: 0x00008730
		public ParseInstallationCoder(IParseDataDecoder decoder, IParseObjectClassController classController)
		{
			this.Decoder = decoder;
			this.ClassController = classController;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A558 File Offset: 0x00008758
		public IDictionary<string, object> Encode(ParseInstallation installation)
		{
			IObjectState state = installation.State;
			IDictionary<string, object> dictionary = PointerOrLocalIdEncoder.Instance.Encode(state.ToDictionary((KeyValuePair<string, object> pair) => pair.Key, (KeyValuePair<string, object> pair) => pair.Value), installation.Services) as IDictionary<string, object>;
			dictionary["objectId"] = state.ObjectId;
			if (state.CreatedAt != null)
			{
				dictionary["createdAt"] = state.CreatedAt.Value.ToString(ParseClient.DateFormatStrings[0]);
			}
			if (state.UpdatedAt != null)
			{
				dictionary["updatedAt"] = state.UpdatedAt.Value.ToString(ParseClient.DateFormatStrings[0]);
			}
			return dictionary;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A648 File Offset: 0x00008848
		public ParseInstallation Decode(IDictionary<string, object> data, IServiceHub serviceHub)
		{
			return this.ClassController.GenerateObjectFromState(ParseObjectCoder.Instance.Decode(data, this.Decoder, serviceHub), "_Installation", serviceHub);
		}

		// Token: 0x04000071 RID: 113
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		private readonly IParseObjectClassController <ClassController>k__BackingField;

		// Token: 0x02000108 RID: 264
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006F3 RID: 1779 RVA: 0x000156E1 File Offset: 0x000138E1
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x000156ED File Offset: 0x000138ED
			public <>c()
			{
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x000156F5 File Offset: 0x000138F5
			internal string <Encode>b__7_0(KeyValuePair<string, object> pair)
			{
				return pair.Key;
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x000156FE File Offset: 0x000138FE
			internal object <Encode>b__7_1(KeyValuePair<string, object> pair)
			{
				return pair.Value;
			}

			// Token: 0x04000226 RID: 550
			public static readonly ParseInstallationCoder.<>c <>9 = new ParseInstallationCoder.<>c();

			// Token: 0x04000227 RID: 551
			public static Func<KeyValuePair<string, object>, string> <>9__7_0;

			// Token: 0x04000228 RID: 552
			public static Func<KeyValuePair<string, object>, object> <>9__7_1;
		}
	}
}
