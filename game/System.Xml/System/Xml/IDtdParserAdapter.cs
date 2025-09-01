using System;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200003C RID: 60
	internal interface IDtdParserAdapter
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001E3 RID: 483
		XmlNameTable NameTable { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001E4 RID: 484
		IXmlNamespaceResolver NamespaceResolver { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001E5 RID: 485
		Uri BaseUri { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001E6 RID: 486
		char[] ParsingBuffer { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001E7 RID: 487
		int ParsingBufferLength { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001E8 RID: 488
		// (set) Token: 0x060001E9 RID: 489
		int CurrentPosition { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001EA RID: 490
		int LineNo { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001EB RID: 491
		int LineStartPosition { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001EC RID: 492
		bool IsEof { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001ED RID: 493
		int EntityStackLength { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001EE RID: 494
		bool IsEntityEolNormalized { get; }

		// Token: 0x060001EF RID: 495
		int ReadData();

		// Token: 0x060001F0 RID: 496
		void OnNewLine(int pos);

		// Token: 0x060001F1 RID: 497
		int ParseNumericCharRef(StringBuilder internalSubsetBuilder);

		// Token: 0x060001F2 RID: 498
		int ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder);

		// Token: 0x060001F3 RID: 499
		void ParsePI(StringBuilder sb);

		// Token: 0x060001F4 RID: 500
		void ParseComment(StringBuilder sb);

		// Token: 0x060001F5 RID: 501
		bool PushEntity(IDtdEntityInfo entity, out int entityId);

		// Token: 0x060001F6 RID: 502
		bool PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId);

		// Token: 0x060001F7 RID: 503
		bool PushExternalSubset(string systemId, string publicId);

		// Token: 0x060001F8 RID: 504
		void PushInternalDtd(string baseUri, string internalDtd);

		// Token: 0x060001F9 RID: 505
		void OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo);

		// Token: 0x060001FA RID: 506
		void OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo);

		// Token: 0x060001FB RID: 507
		void Throw(Exception e);

		// Token: 0x060001FC RID: 508
		Task<int> ReadDataAsync();

		// Token: 0x060001FD RID: 509
		Task<int> ParseNumericCharRefAsync(StringBuilder internalSubsetBuilder);

		// Token: 0x060001FE RID: 510
		Task<int> ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder);

		// Token: 0x060001FF RID: 511
		Task ParsePIAsync(StringBuilder sb);

		// Token: 0x06000200 RID: 512
		Task ParseCommentAsync(StringBuilder sb);

		// Token: 0x06000201 RID: 513
		Task<Tuple<int, bool>> PushEntityAsync(IDtdEntityInfo entity);

		// Token: 0x06000202 RID: 514
		Task<bool> PushExternalSubsetAsync(string systemId, string publicId);
	}
}
