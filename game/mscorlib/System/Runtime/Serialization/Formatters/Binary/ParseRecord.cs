using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006B8 RID: 1720
	internal sealed class ParseRecord
	{
		// Token: 0x06003FA6 RID: 16294 RVA: 0x0000259F File Offset: 0x0000079F
		internal ParseRecord()
		{
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x000DF264 File Offset: 0x000DD464
		internal void Init()
		{
			this.PRparseTypeEnum = InternalParseTypeE.Empty;
			this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
			this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
			this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
			this.PRmemberValueEnum = InternalMemberValueE.Empty;
			this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
			this.PRname = null;
			this.PRvalue = null;
			this.PRkeyDt = null;
			this.PRdtType = null;
			this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRisEnum = false;
			this.PRobjectId = 0L;
			this.PRidRef = 0L;
			this.PRarrayElementTypeString = null;
			this.PRarrayElementType = null;
			this.PRisArrayVariant = false;
			this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRrank = 0;
			this.PRlengthA = null;
			this.PRpositionA = null;
			this.PRlowerBoundA = null;
			this.PRupperBoundA = null;
			this.PRindexMap = null;
			this.PRmemberIndex = 0;
			this.PRlinearlength = 0;
			this.PRrectangularMap = null;
			this.PRisLowerBound = false;
			this.PRtopId = 0L;
			this.PRheaderId = 0L;
			this.PRisValueTypeFixup = false;
			this.PRnewObj = null;
			this.PRobjectA = null;
			this.PRprimitiveArray = null;
			this.PRobjectInfo = null;
			this.PRisRegistered = false;
			this.PRmemberData = null;
			this.PRsi = null;
			this.PRnullCount = 0;
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000DF386 File Offset: 0x000DD586
		// Note: this type is marked as 'beforefieldinit'.
		static ParseRecord()
		{
		}

		// Token: 0x0400297E RID: 10622
		internal static int parseRecordIdCount = 1;

		// Token: 0x0400297F RID: 10623
		internal int PRparseRecordId;

		// Token: 0x04002980 RID: 10624
		internal InternalParseTypeE PRparseTypeEnum;

		// Token: 0x04002981 RID: 10625
		internal InternalObjectTypeE PRobjectTypeEnum;

		// Token: 0x04002982 RID: 10626
		internal InternalArrayTypeE PRarrayTypeEnum;

		// Token: 0x04002983 RID: 10627
		internal InternalMemberTypeE PRmemberTypeEnum;

		// Token: 0x04002984 RID: 10628
		internal InternalMemberValueE PRmemberValueEnum;

		// Token: 0x04002985 RID: 10629
		internal InternalObjectPositionE PRobjectPositionEnum;

		// Token: 0x04002986 RID: 10630
		internal string PRname;

		// Token: 0x04002987 RID: 10631
		internal string PRvalue;

		// Token: 0x04002988 RID: 10632
		internal object PRvarValue;

		// Token: 0x04002989 RID: 10633
		internal string PRkeyDt;

		// Token: 0x0400298A RID: 10634
		internal Type PRdtType;

		// Token: 0x0400298B RID: 10635
		internal InternalPrimitiveTypeE PRdtTypeCode;

		// Token: 0x0400298C RID: 10636
		internal bool PRisVariant;

		// Token: 0x0400298D RID: 10637
		internal bool PRisEnum;

		// Token: 0x0400298E RID: 10638
		internal long PRobjectId;

		// Token: 0x0400298F RID: 10639
		internal long PRidRef;

		// Token: 0x04002990 RID: 10640
		internal string PRarrayElementTypeString;

		// Token: 0x04002991 RID: 10641
		internal Type PRarrayElementType;

		// Token: 0x04002992 RID: 10642
		internal bool PRisArrayVariant;

		// Token: 0x04002993 RID: 10643
		internal InternalPrimitiveTypeE PRarrayElementTypeCode;

		// Token: 0x04002994 RID: 10644
		internal int PRrank;

		// Token: 0x04002995 RID: 10645
		internal int[] PRlengthA;

		// Token: 0x04002996 RID: 10646
		internal int[] PRpositionA;

		// Token: 0x04002997 RID: 10647
		internal int[] PRlowerBoundA;

		// Token: 0x04002998 RID: 10648
		internal int[] PRupperBoundA;

		// Token: 0x04002999 RID: 10649
		internal int[] PRindexMap;

		// Token: 0x0400299A RID: 10650
		internal int PRmemberIndex;

		// Token: 0x0400299B RID: 10651
		internal int PRlinearlength;

		// Token: 0x0400299C RID: 10652
		internal int[] PRrectangularMap;

		// Token: 0x0400299D RID: 10653
		internal bool PRisLowerBound;

		// Token: 0x0400299E RID: 10654
		internal long PRtopId;

		// Token: 0x0400299F RID: 10655
		internal long PRheaderId;

		// Token: 0x040029A0 RID: 10656
		internal ReadObjectInfo PRobjectInfo;

		// Token: 0x040029A1 RID: 10657
		internal bool PRisValueTypeFixup;

		// Token: 0x040029A2 RID: 10658
		internal object PRnewObj;

		// Token: 0x040029A3 RID: 10659
		internal object[] PRobjectA;

		// Token: 0x040029A4 RID: 10660
		internal PrimitiveArray PRprimitiveArray;

		// Token: 0x040029A5 RID: 10661
		internal bool PRisRegistered;

		// Token: 0x040029A6 RID: 10662
		internal object[] PRmemberData;

		// Token: 0x040029A7 RID: 10663
		internal SerializationInfo PRsi;

		// Token: 0x040029A8 RID: 10664
		internal int PRnullCount;
	}
}
