using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the standard paper sizes.</summary>
	// Token: 0x020000BA RID: 186
	public enum PaperKind
	{
		/// <summary>The paper size is defined by the user.</summary>
		// Token: 0x0400066C RID: 1644
		Custom,
		/// <summary>Letter paper (8.5 in. by 11 in.).</summary>
		// Token: 0x0400066D RID: 1645
		Letter,
		/// <summary>Legal paper (8.5 in. by 14 in.).</summary>
		// Token: 0x0400066E RID: 1646
		Legal = 5,
		/// <summary>A4 paper (210 mm by 297 mm).</summary>
		// Token: 0x0400066F RID: 1647
		A4 = 9,
		/// <summary>C paper (17 in. by 22 in.).</summary>
		// Token: 0x04000670 RID: 1648
		CSheet = 24,
		/// <summary>D paper (22 in. by 34 in.).</summary>
		// Token: 0x04000671 RID: 1649
		DSheet,
		/// <summary>E paper (34 in. by 44 in.).</summary>
		// Token: 0x04000672 RID: 1650
		ESheet,
		/// <summary>Letter small paper (8.5 in. by 11 in.).</summary>
		// Token: 0x04000673 RID: 1651
		LetterSmall = 2,
		/// <summary>Tabloid paper (11 in. by 17 in.).</summary>
		// Token: 0x04000674 RID: 1652
		Tabloid,
		/// <summary>Ledger paper (17 in. by 11 in.).</summary>
		// Token: 0x04000675 RID: 1653
		Ledger,
		/// <summary>Statement paper (5.5 in. by 8.5 in.).</summary>
		// Token: 0x04000676 RID: 1654
		Statement = 6,
		/// <summary>Executive paper (7.25 in. by 10.5 in.).</summary>
		// Token: 0x04000677 RID: 1655
		Executive,
		/// <summary>A3 paper (297 mm by 420 mm).</summary>
		// Token: 0x04000678 RID: 1656
		A3,
		/// <summary>A4 small paper (210 mm by 297 mm).</summary>
		// Token: 0x04000679 RID: 1657
		A4Small = 10,
		/// <summary>A5 paper (148 mm by 210 mm).</summary>
		// Token: 0x0400067A RID: 1658
		A5,
		/// <summary>B4 paper (250 mm by 353 mm).</summary>
		// Token: 0x0400067B RID: 1659
		B4,
		/// <summary>B5 paper (176 mm by 250 mm).</summary>
		// Token: 0x0400067C RID: 1660
		B5,
		/// <summary>Folio paper (8.5 in. by 13 in.).</summary>
		// Token: 0x0400067D RID: 1661
		Folio,
		/// <summary>Quarto paper (215 mm by 275 mm).</summary>
		// Token: 0x0400067E RID: 1662
		Quarto,
		/// <summary>Standard paper (10 in. by 14 in.).</summary>
		// Token: 0x0400067F RID: 1663
		Standard10x14,
		/// <summary>Standard paper (11 in. by 17 in.).</summary>
		// Token: 0x04000680 RID: 1664
		Standard11x17,
		/// <summary>Note paper (8.5 in. by 11 in.).</summary>
		// Token: 0x04000681 RID: 1665
		Note,
		/// <summary>#9 envelope (3.875 in. by 8.875 in.).</summary>
		// Token: 0x04000682 RID: 1666
		Number9Envelope,
		/// <summary>#10 envelope (4.125 in. by 9.5 in.).</summary>
		// Token: 0x04000683 RID: 1667
		Number10Envelope,
		/// <summary>#11 envelope (4.5 in. by 10.375 in.).</summary>
		// Token: 0x04000684 RID: 1668
		Number11Envelope,
		/// <summary>#12 envelope (4.75 in. by 11 in.).</summary>
		// Token: 0x04000685 RID: 1669
		Number12Envelope,
		/// <summary>#14 envelope (5 in. by 11.5 in.).</summary>
		// Token: 0x04000686 RID: 1670
		Number14Envelope,
		/// <summary>DL envelope (110 mm by 220 mm).</summary>
		// Token: 0x04000687 RID: 1671
		DLEnvelope = 27,
		/// <summary>C5 envelope (162 mm by 229 mm).</summary>
		// Token: 0x04000688 RID: 1672
		C5Envelope,
		/// <summary>C3 envelope (324 mm by 458 mm).</summary>
		// Token: 0x04000689 RID: 1673
		C3Envelope,
		/// <summary>C4 envelope (229 mm by 324 mm).</summary>
		// Token: 0x0400068A RID: 1674
		C4Envelope,
		/// <summary>C6 envelope (114 mm by 162 mm).</summary>
		// Token: 0x0400068B RID: 1675
		C6Envelope,
		/// <summary>C65 envelope (114 mm by 229 mm).</summary>
		// Token: 0x0400068C RID: 1676
		C65Envelope,
		/// <summary>B4 envelope (250 mm by 353 mm).</summary>
		// Token: 0x0400068D RID: 1677
		B4Envelope,
		/// <summary>B5 envelope (176 mm by 250 mm).</summary>
		// Token: 0x0400068E RID: 1678
		B5Envelope,
		/// <summary>B6 envelope (176 mm by 125 mm).</summary>
		// Token: 0x0400068F RID: 1679
		B6Envelope,
		/// <summary>Italy envelope (110 mm by 230 mm).</summary>
		// Token: 0x04000690 RID: 1680
		ItalyEnvelope,
		/// <summary>Monarch envelope (3.875 in. by 7.5 in.).</summary>
		// Token: 0x04000691 RID: 1681
		MonarchEnvelope,
		/// <summary>6 3/4 envelope (3.625 in. by 6.5 in.).</summary>
		// Token: 0x04000692 RID: 1682
		PersonalEnvelope,
		/// <summary>US standard fanfold (14.875 in. by 11 in.).</summary>
		// Token: 0x04000693 RID: 1683
		USStandardFanfold,
		/// <summary>German standard fanfold (8.5 in. by 12 in.).</summary>
		// Token: 0x04000694 RID: 1684
		GermanStandardFanfold,
		/// <summary>German legal fanfold (8.5 in. by 13 in.).</summary>
		// Token: 0x04000695 RID: 1685
		GermanLegalFanfold,
		/// <summary>ISO B4 (250 mm by 353 mm).</summary>
		// Token: 0x04000696 RID: 1686
		IsoB4,
		/// <summary>Japanese postcard (100 mm by 148 mm).</summary>
		// Token: 0x04000697 RID: 1687
		JapanesePostcard,
		/// <summary>Standard paper (9 in. by 11 in.).</summary>
		// Token: 0x04000698 RID: 1688
		Standard9x11,
		/// <summary>Standard paper (10 in. by 11 in.).</summary>
		// Token: 0x04000699 RID: 1689
		Standard10x11,
		/// <summary>Standard paper (15 in. by 11 in.).</summary>
		// Token: 0x0400069A RID: 1690
		Standard15x11,
		/// <summary>Invitation envelope (220 mm by 220 mm).</summary>
		// Token: 0x0400069B RID: 1691
		InviteEnvelope,
		/// <summary>Letter extra paper (9.275 in. by 12 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.</summary>
		// Token: 0x0400069C RID: 1692
		LetterExtra = 50,
		/// <summary>Legal extra paper (9.275 in. by 15 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.</summary>
		// Token: 0x0400069D RID: 1693
		LegalExtra,
		/// <summary>Tabloid extra paper (11.69 in. by 18 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.</summary>
		// Token: 0x0400069E RID: 1694
		TabloidExtra,
		/// <summary>A4 extra paper (236 mm by 322 mm). This value is specific to the PostScript driver and is used only by Linotronic printers to help save paper.</summary>
		// Token: 0x0400069F RID: 1695
		A4Extra,
		/// <summary>Letter transverse paper (8.275 in. by 11 in.).</summary>
		// Token: 0x040006A0 RID: 1696
		LetterTransverse,
		/// <summary>A4 transverse paper (210 mm by 297 mm).</summary>
		// Token: 0x040006A1 RID: 1697
		A4Transverse,
		/// <summary>Letter extra transverse paper (9.275 in. by 12 in.).</summary>
		// Token: 0x040006A2 RID: 1698
		LetterExtraTransverse,
		/// <summary>SuperA/SuperA/A4 paper (227 mm by 356 mm).</summary>
		// Token: 0x040006A3 RID: 1699
		APlus,
		/// <summary>SuperB/SuperB/A3 paper (305 mm by 487 mm).</summary>
		// Token: 0x040006A4 RID: 1700
		BPlus,
		/// <summary>Letter plus paper (8.5 in. by 12.69 in.).</summary>
		// Token: 0x040006A5 RID: 1701
		LetterPlus,
		/// <summary>A4 plus paper (210 mm by 330 mm).</summary>
		// Token: 0x040006A6 RID: 1702
		A4Plus,
		/// <summary>A5 transverse paper (148 mm by 210 mm).</summary>
		// Token: 0x040006A7 RID: 1703
		A5Transverse,
		/// <summary>JIS B5 transverse paper (182 mm by 257 mm).</summary>
		// Token: 0x040006A8 RID: 1704
		B5Transverse,
		/// <summary>A3 extra paper (322 mm by 445 mm).</summary>
		// Token: 0x040006A9 RID: 1705
		A3Extra,
		/// <summary>A5 extra paper (174 mm by 235 mm).</summary>
		// Token: 0x040006AA RID: 1706
		A5Extra,
		/// <summary>ISO B5 extra paper (201 mm by 276 mm).</summary>
		// Token: 0x040006AB RID: 1707
		B5Extra,
		/// <summary>A2 paper (420 mm by 594 mm).</summary>
		// Token: 0x040006AC RID: 1708
		A2,
		/// <summary>A3 transverse paper (297 mm by 420 mm).</summary>
		// Token: 0x040006AD RID: 1709
		A3Transverse,
		/// <summary>A3 extra transverse paper (322 mm by 445 mm).</summary>
		// Token: 0x040006AE RID: 1710
		A3ExtraTransverse,
		/// <summary>Japanese double postcard (200 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006AF RID: 1711
		JapaneseDoublePostcard,
		/// <summary>A6 paper (105 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B0 RID: 1712
		A6,
		/// <summary>Japanese Kaku #2 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B1 RID: 1713
		JapaneseEnvelopeKakuNumber2,
		/// <summary>Japanese Kaku #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B2 RID: 1714
		JapaneseEnvelopeKakuNumber3,
		/// <summary>Japanese Chou #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B3 RID: 1715
		JapaneseEnvelopeChouNumber3,
		/// <summary>Japanese Chou #4 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B4 RID: 1716
		JapaneseEnvelopeChouNumber4,
		/// <summary>Letter rotated paper (11 in. by 8.5 in.).</summary>
		// Token: 0x040006B5 RID: 1717
		LetterRotated,
		/// <summary>A3 rotated paper (420 mm by 297 mm).</summary>
		// Token: 0x040006B6 RID: 1718
		A3Rotated,
		/// <summary>A4 rotated paper (297 mm by 210 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B7 RID: 1719
		A4Rotated,
		/// <summary>A5 rotated paper (210 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B8 RID: 1720
		A5Rotated,
		/// <summary>JIS B4 rotated paper (364 mm by 257 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006B9 RID: 1721
		B4JisRotated,
		/// <summary>JIS B5 rotated paper (257 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006BA RID: 1722
		B5JisRotated,
		/// <summary>Japanese rotated postcard (148 mm by 100 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006BB RID: 1723
		JapanesePostcardRotated,
		/// <summary>Japanese rotated double postcard (148 mm by 200 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006BC RID: 1724
		JapaneseDoublePostcardRotated,
		/// <summary>A6 rotated paper (148 mm by 105 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006BD RID: 1725
		A6Rotated,
		/// <summary>Japanese rotated Kaku #2 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006BE RID: 1726
		JapaneseEnvelopeKakuNumber2Rotated,
		/// <summary>Japanese rotated Kaku #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006BF RID: 1727
		JapaneseEnvelopeKakuNumber3Rotated,
		/// <summary>Japanese rotated Chou #3 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C0 RID: 1728
		JapaneseEnvelopeChouNumber3Rotated,
		/// <summary>Japanese rotated Chou #4 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C1 RID: 1729
		JapaneseEnvelopeChouNumber4Rotated,
		/// <summary>JIS B6 paper (128 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C2 RID: 1730
		B6Jis,
		/// <summary>JIS B6 rotated paper (182 mm by 128 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C3 RID: 1731
		B6JisRotated,
		/// <summary>Standard paper (12 in. by 11 in.). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C4 RID: 1732
		Standard12x11,
		/// <summary>Japanese You #4 envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C5 RID: 1733
		JapaneseEnvelopeYouNumber4,
		/// <summary>Japanese You #4 rotated envelope. Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C6 RID: 1734
		JapaneseEnvelopeYouNumber4Rotated,
		/// <summary>16K paper (146 mm by 215 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C7 RID: 1735
		Prc16K,
		/// <summary>32K paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C8 RID: 1736
		Prc32K,
		/// <summary>32K big paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006C9 RID: 1737
		Prc32KBig,
		/// <summary>#1 envelope (102 mm by 165 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006CA RID: 1738
		PrcEnvelopeNumber1,
		/// <summary>#2 envelope (102 mm by 176 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006CB RID: 1739
		PrcEnvelopeNumber2,
		/// <summary>#3 envelope (125 mm by 176 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006CC RID: 1740
		PrcEnvelopeNumber3,
		/// <summary>#4 envelope (110 mm by 208 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006CD RID: 1741
		PrcEnvelopeNumber4,
		/// <summary>#5 envelope (110 mm by 220 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006CE RID: 1742
		PrcEnvelopeNumber5,
		/// <summary>#6 envelope (120 mm by 230 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006CF RID: 1743
		PrcEnvelopeNumber6,
		/// <summary>#7 envelope (160 mm by 230 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D0 RID: 1744
		PrcEnvelopeNumber7,
		/// <summary>#8 envelope (120 mm by 309 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D1 RID: 1745
		PrcEnvelopeNumber8,
		/// <summary>#9 envelope (229 mm by 324 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D2 RID: 1746
		PrcEnvelopeNumber9,
		/// <summary>#10 envelope (324 mm by 458 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D3 RID: 1747
		PrcEnvelopeNumber10,
		/// <summary>16K rotated paper (146 mm by 215 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D4 RID: 1748
		Prc16KRotated,
		/// <summary>32K rotated paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D5 RID: 1749
		Prc32KRotated,
		/// <summary>32K big rotated paper (97 mm by 151 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D6 RID: 1750
		Prc32KBigRotated,
		/// <summary>#1 rotated envelope (165 mm by 102 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D7 RID: 1751
		PrcEnvelopeNumber1Rotated,
		/// <summary>#2 rotated envelope (176 mm by 102 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D8 RID: 1752
		PrcEnvelopeNumber2Rotated,
		/// <summary>#3 rotated envelope (176 mm by 125 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006D9 RID: 1753
		PrcEnvelopeNumber3Rotated,
		/// <summary>#4 rotated envelope (208 mm by 110 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006DA RID: 1754
		PrcEnvelopeNumber4Rotated,
		/// <summary>Envelope #5 rotated envelope (220 mm by 110 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006DB RID: 1755
		PrcEnvelopeNumber5Rotated,
		/// <summary>#6 rotated envelope (230 mm by 120 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006DC RID: 1756
		PrcEnvelopeNumber6Rotated,
		/// <summary>#7 rotated envelope (230 mm by 160 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006DD RID: 1757
		PrcEnvelopeNumber7Rotated,
		/// <summary>#8 rotated envelope (309 mm by 120 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006DE RID: 1758
		PrcEnvelopeNumber8Rotated,
		/// <summary>#9 rotated envelope (324 mm by 229 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006DF RID: 1759
		PrcEnvelopeNumber9Rotated,
		/// <summary>#10 rotated envelope (458 mm by 324 mm). Requires Windows 98, Windows NT 4.0, or later.</summary>
		// Token: 0x040006E0 RID: 1760
		PrcEnvelopeNumber10Rotated
	}
}
