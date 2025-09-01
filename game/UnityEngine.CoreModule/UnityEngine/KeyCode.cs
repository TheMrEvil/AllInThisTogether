using System;

namespace UnityEngine
{
	// Token: 0x020001B8 RID: 440
	public enum KeyCode
	{
		// Token: 0x040005EE RID: 1518
		None,
		// Token: 0x040005EF RID: 1519
		Backspace = 8,
		// Token: 0x040005F0 RID: 1520
		Delete = 127,
		// Token: 0x040005F1 RID: 1521
		Tab = 9,
		// Token: 0x040005F2 RID: 1522
		Clear = 12,
		// Token: 0x040005F3 RID: 1523
		Return,
		// Token: 0x040005F4 RID: 1524
		Pause = 19,
		// Token: 0x040005F5 RID: 1525
		Escape = 27,
		// Token: 0x040005F6 RID: 1526
		Space = 32,
		// Token: 0x040005F7 RID: 1527
		Keypad0 = 256,
		// Token: 0x040005F8 RID: 1528
		Keypad1,
		// Token: 0x040005F9 RID: 1529
		Keypad2,
		// Token: 0x040005FA RID: 1530
		Keypad3,
		// Token: 0x040005FB RID: 1531
		Keypad4,
		// Token: 0x040005FC RID: 1532
		Keypad5,
		// Token: 0x040005FD RID: 1533
		Keypad6,
		// Token: 0x040005FE RID: 1534
		Keypad7,
		// Token: 0x040005FF RID: 1535
		Keypad8,
		// Token: 0x04000600 RID: 1536
		Keypad9,
		// Token: 0x04000601 RID: 1537
		KeypadPeriod,
		// Token: 0x04000602 RID: 1538
		KeypadDivide,
		// Token: 0x04000603 RID: 1539
		KeypadMultiply,
		// Token: 0x04000604 RID: 1540
		KeypadMinus,
		// Token: 0x04000605 RID: 1541
		KeypadPlus,
		// Token: 0x04000606 RID: 1542
		KeypadEnter,
		// Token: 0x04000607 RID: 1543
		KeypadEquals,
		// Token: 0x04000608 RID: 1544
		UpArrow,
		// Token: 0x04000609 RID: 1545
		DownArrow,
		// Token: 0x0400060A RID: 1546
		RightArrow,
		// Token: 0x0400060B RID: 1547
		LeftArrow,
		// Token: 0x0400060C RID: 1548
		Insert,
		// Token: 0x0400060D RID: 1549
		Home,
		// Token: 0x0400060E RID: 1550
		End,
		// Token: 0x0400060F RID: 1551
		PageUp,
		// Token: 0x04000610 RID: 1552
		PageDown,
		// Token: 0x04000611 RID: 1553
		F1,
		// Token: 0x04000612 RID: 1554
		F2,
		// Token: 0x04000613 RID: 1555
		F3,
		// Token: 0x04000614 RID: 1556
		F4,
		// Token: 0x04000615 RID: 1557
		F5,
		// Token: 0x04000616 RID: 1558
		F6,
		// Token: 0x04000617 RID: 1559
		F7,
		// Token: 0x04000618 RID: 1560
		F8,
		// Token: 0x04000619 RID: 1561
		F9,
		// Token: 0x0400061A RID: 1562
		F10,
		// Token: 0x0400061B RID: 1563
		F11,
		// Token: 0x0400061C RID: 1564
		F12,
		// Token: 0x0400061D RID: 1565
		F13,
		// Token: 0x0400061E RID: 1566
		F14,
		// Token: 0x0400061F RID: 1567
		F15,
		// Token: 0x04000620 RID: 1568
		Alpha0 = 48,
		// Token: 0x04000621 RID: 1569
		Alpha1,
		// Token: 0x04000622 RID: 1570
		Alpha2,
		// Token: 0x04000623 RID: 1571
		Alpha3,
		// Token: 0x04000624 RID: 1572
		Alpha4,
		// Token: 0x04000625 RID: 1573
		Alpha5,
		// Token: 0x04000626 RID: 1574
		Alpha6,
		// Token: 0x04000627 RID: 1575
		Alpha7,
		// Token: 0x04000628 RID: 1576
		Alpha8,
		// Token: 0x04000629 RID: 1577
		Alpha9,
		// Token: 0x0400062A RID: 1578
		Exclaim = 33,
		// Token: 0x0400062B RID: 1579
		DoubleQuote,
		// Token: 0x0400062C RID: 1580
		Hash,
		// Token: 0x0400062D RID: 1581
		Dollar,
		// Token: 0x0400062E RID: 1582
		Percent,
		// Token: 0x0400062F RID: 1583
		Ampersand,
		// Token: 0x04000630 RID: 1584
		Quote,
		// Token: 0x04000631 RID: 1585
		LeftParen,
		// Token: 0x04000632 RID: 1586
		RightParen,
		// Token: 0x04000633 RID: 1587
		Asterisk,
		// Token: 0x04000634 RID: 1588
		Plus,
		// Token: 0x04000635 RID: 1589
		Comma,
		// Token: 0x04000636 RID: 1590
		Minus,
		// Token: 0x04000637 RID: 1591
		Period,
		// Token: 0x04000638 RID: 1592
		Slash,
		// Token: 0x04000639 RID: 1593
		Colon = 58,
		// Token: 0x0400063A RID: 1594
		Semicolon,
		// Token: 0x0400063B RID: 1595
		Less,
		// Token: 0x0400063C RID: 1596
		Equals,
		// Token: 0x0400063D RID: 1597
		Greater,
		// Token: 0x0400063E RID: 1598
		Question,
		// Token: 0x0400063F RID: 1599
		At,
		// Token: 0x04000640 RID: 1600
		LeftBracket = 91,
		// Token: 0x04000641 RID: 1601
		Backslash,
		// Token: 0x04000642 RID: 1602
		RightBracket,
		// Token: 0x04000643 RID: 1603
		Caret,
		// Token: 0x04000644 RID: 1604
		Underscore,
		// Token: 0x04000645 RID: 1605
		BackQuote,
		// Token: 0x04000646 RID: 1606
		A,
		// Token: 0x04000647 RID: 1607
		B,
		// Token: 0x04000648 RID: 1608
		C,
		// Token: 0x04000649 RID: 1609
		D,
		// Token: 0x0400064A RID: 1610
		E,
		// Token: 0x0400064B RID: 1611
		F,
		// Token: 0x0400064C RID: 1612
		G,
		// Token: 0x0400064D RID: 1613
		H,
		// Token: 0x0400064E RID: 1614
		I,
		// Token: 0x0400064F RID: 1615
		J,
		// Token: 0x04000650 RID: 1616
		K,
		// Token: 0x04000651 RID: 1617
		L,
		// Token: 0x04000652 RID: 1618
		M,
		// Token: 0x04000653 RID: 1619
		N,
		// Token: 0x04000654 RID: 1620
		O,
		// Token: 0x04000655 RID: 1621
		P,
		// Token: 0x04000656 RID: 1622
		Q,
		// Token: 0x04000657 RID: 1623
		R,
		// Token: 0x04000658 RID: 1624
		S,
		// Token: 0x04000659 RID: 1625
		T,
		// Token: 0x0400065A RID: 1626
		U,
		// Token: 0x0400065B RID: 1627
		V,
		// Token: 0x0400065C RID: 1628
		W,
		// Token: 0x0400065D RID: 1629
		X,
		// Token: 0x0400065E RID: 1630
		Y,
		// Token: 0x0400065F RID: 1631
		Z,
		// Token: 0x04000660 RID: 1632
		LeftCurlyBracket,
		// Token: 0x04000661 RID: 1633
		Pipe,
		// Token: 0x04000662 RID: 1634
		RightCurlyBracket,
		// Token: 0x04000663 RID: 1635
		Tilde,
		// Token: 0x04000664 RID: 1636
		Numlock = 300,
		// Token: 0x04000665 RID: 1637
		CapsLock,
		// Token: 0x04000666 RID: 1638
		ScrollLock,
		// Token: 0x04000667 RID: 1639
		RightShift,
		// Token: 0x04000668 RID: 1640
		LeftShift,
		// Token: 0x04000669 RID: 1641
		RightControl,
		// Token: 0x0400066A RID: 1642
		LeftControl,
		// Token: 0x0400066B RID: 1643
		RightAlt,
		// Token: 0x0400066C RID: 1644
		LeftAlt,
		// Token: 0x0400066D RID: 1645
		LeftMeta = 310,
		// Token: 0x0400066E RID: 1646
		LeftCommand = 310,
		// Token: 0x0400066F RID: 1647
		LeftApple = 310,
		// Token: 0x04000670 RID: 1648
		LeftWindows,
		// Token: 0x04000671 RID: 1649
		RightMeta = 309,
		// Token: 0x04000672 RID: 1650
		RightCommand = 309,
		// Token: 0x04000673 RID: 1651
		RightApple = 309,
		// Token: 0x04000674 RID: 1652
		RightWindows = 312,
		// Token: 0x04000675 RID: 1653
		AltGr,
		// Token: 0x04000676 RID: 1654
		Help = 315,
		// Token: 0x04000677 RID: 1655
		Print,
		// Token: 0x04000678 RID: 1656
		SysReq,
		// Token: 0x04000679 RID: 1657
		Break,
		// Token: 0x0400067A RID: 1658
		Menu,
		// Token: 0x0400067B RID: 1659
		Mouse0 = 323,
		// Token: 0x0400067C RID: 1660
		Mouse1,
		// Token: 0x0400067D RID: 1661
		Mouse2,
		// Token: 0x0400067E RID: 1662
		Mouse3,
		// Token: 0x0400067F RID: 1663
		Mouse4,
		// Token: 0x04000680 RID: 1664
		Mouse5,
		// Token: 0x04000681 RID: 1665
		Mouse6,
		// Token: 0x04000682 RID: 1666
		JoystickButton0,
		// Token: 0x04000683 RID: 1667
		JoystickButton1,
		// Token: 0x04000684 RID: 1668
		JoystickButton2,
		// Token: 0x04000685 RID: 1669
		JoystickButton3,
		// Token: 0x04000686 RID: 1670
		JoystickButton4,
		// Token: 0x04000687 RID: 1671
		JoystickButton5,
		// Token: 0x04000688 RID: 1672
		JoystickButton6,
		// Token: 0x04000689 RID: 1673
		JoystickButton7,
		// Token: 0x0400068A RID: 1674
		JoystickButton8,
		// Token: 0x0400068B RID: 1675
		JoystickButton9,
		// Token: 0x0400068C RID: 1676
		JoystickButton10,
		// Token: 0x0400068D RID: 1677
		JoystickButton11,
		// Token: 0x0400068E RID: 1678
		JoystickButton12,
		// Token: 0x0400068F RID: 1679
		JoystickButton13,
		// Token: 0x04000690 RID: 1680
		JoystickButton14,
		// Token: 0x04000691 RID: 1681
		JoystickButton15,
		// Token: 0x04000692 RID: 1682
		JoystickButton16,
		// Token: 0x04000693 RID: 1683
		JoystickButton17,
		// Token: 0x04000694 RID: 1684
		JoystickButton18,
		// Token: 0x04000695 RID: 1685
		JoystickButton19,
		// Token: 0x04000696 RID: 1686
		Joystick1Button0,
		// Token: 0x04000697 RID: 1687
		Joystick1Button1,
		// Token: 0x04000698 RID: 1688
		Joystick1Button2,
		// Token: 0x04000699 RID: 1689
		Joystick1Button3,
		// Token: 0x0400069A RID: 1690
		Joystick1Button4,
		// Token: 0x0400069B RID: 1691
		Joystick1Button5,
		// Token: 0x0400069C RID: 1692
		Joystick1Button6,
		// Token: 0x0400069D RID: 1693
		Joystick1Button7,
		// Token: 0x0400069E RID: 1694
		Joystick1Button8,
		// Token: 0x0400069F RID: 1695
		Joystick1Button9,
		// Token: 0x040006A0 RID: 1696
		Joystick1Button10,
		// Token: 0x040006A1 RID: 1697
		Joystick1Button11,
		// Token: 0x040006A2 RID: 1698
		Joystick1Button12,
		// Token: 0x040006A3 RID: 1699
		Joystick1Button13,
		// Token: 0x040006A4 RID: 1700
		Joystick1Button14,
		// Token: 0x040006A5 RID: 1701
		Joystick1Button15,
		// Token: 0x040006A6 RID: 1702
		Joystick1Button16,
		// Token: 0x040006A7 RID: 1703
		Joystick1Button17,
		// Token: 0x040006A8 RID: 1704
		Joystick1Button18,
		// Token: 0x040006A9 RID: 1705
		Joystick1Button19,
		// Token: 0x040006AA RID: 1706
		Joystick2Button0,
		// Token: 0x040006AB RID: 1707
		Joystick2Button1,
		// Token: 0x040006AC RID: 1708
		Joystick2Button2,
		// Token: 0x040006AD RID: 1709
		Joystick2Button3,
		// Token: 0x040006AE RID: 1710
		Joystick2Button4,
		// Token: 0x040006AF RID: 1711
		Joystick2Button5,
		// Token: 0x040006B0 RID: 1712
		Joystick2Button6,
		// Token: 0x040006B1 RID: 1713
		Joystick2Button7,
		// Token: 0x040006B2 RID: 1714
		Joystick2Button8,
		// Token: 0x040006B3 RID: 1715
		Joystick2Button9,
		// Token: 0x040006B4 RID: 1716
		Joystick2Button10,
		// Token: 0x040006B5 RID: 1717
		Joystick2Button11,
		// Token: 0x040006B6 RID: 1718
		Joystick2Button12,
		// Token: 0x040006B7 RID: 1719
		Joystick2Button13,
		// Token: 0x040006B8 RID: 1720
		Joystick2Button14,
		// Token: 0x040006B9 RID: 1721
		Joystick2Button15,
		// Token: 0x040006BA RID: 1722
		Joystick2Button16,
		// Token: 0x040006BB RID: 1723
		Joystick2Button17,
		// Token: 0x040006BC RID: 1724
		Joystick2Button18,
		// Token: 0x040006BD RID: 1725
		Joystick2Button19,
		// Token: 0x040006BE RID: 1726
		Joystick3Button0,
		// Token: 0x040006BF RID: 1727
		Joystick3Button1,
		// Token: 0x040006C0 RID: 1728
		Joystick3Button2,
		// Token: 0x040006C1 RID: 1729
		Joystick3Button3,
		// Token: 0x040006C2 RID: 1730
		Joystick3Button4,
		// Token: 0x040006C3 RID: 1731
		Joystick3Button5,
		// Token: 0x040006C4 RID: 1732
		Joystick3Button6,
		// Token: 0x040006C5 RID: 1733
		Joystick3Button7,
		// Token: 0x040006C6 RID: 1734
		Joystick3Button8,
		// Token: 0x040006C7 RID: 1735
		Joystick3Button9,
		// Token: 0x040006C8 RID: 1736
		Joystick3Button10,
		// Token: 0x040006C9 RID: 1737
		Joystick3Button11,
		// Token: 0x040006CA RID: 1738
		Joystick3Button12,
		// Token: 0x040006CB RID: 1739
		Joystick3Button13,
		// Token: 0x040006CC RID: 1740
		Joystick3Button14,
		// Token: 0x040006CD RID: 1741
		Joystick3Button15,
		// Token: 0x040006CE RID: 1742
		Joystick3Button16,
		// Token: 0x040006CF RID: 1743
		Joystick3Button17,
		// Token: 0x040006D0 RID: 1744
		Joystick3Button18,
		// Token: 0x040006D1 RID: 1745
		Joystick3Button19,
		// Token: 0x040006D2 RID: 1746
		Joystick4Button0,
		// Token: 0x040006D3 RID: 1747
		Joystick4Button1,
		// Token: 0x040006D4 RID: 1748
		Joystick4Button2,
		// Token: 0x040006D5 RID: 1749
		Joystick4Button3,
		// Token: 0x040006D6 RID: 1750
		Joystick4Button4,
		// Token: 0x040006D7 RID: 1751
		Joystick4Button5,
		// Token: 0x040006D8 RID: 1752
		Joystick4Button6,
		// Token: 0x040006D9 RID: 1753
		Joystick4Button7,
		// Token: 0x040006DA RID: 1754
		Joystick4Button8,
		// Token: 0x040006DB RID: 1755
		Joystick4Button9,
		// Token: 0x040006DC RID: 1756
		Joystick4Button10,
		// Token: 0x040006DD RID: 1757
		Joystick4Button11,
		// Token: 0x040006DE RID: 1758
		Joystick4Button12,
		// Token: 0x040006DF RID: 1759
		Joystick4Button13,
		// Token: 0x040006E0 RID: 1760
		Joystick4Button14,
		// Token: 0x040006E1 RID: 1761
		Joystick4Button15,
		// Token: 0x040006E2 RID: 1762
		Joystick4Button16,
		// Token: 0x040006E3 RID: 1763
		Joystick4Button17,
		// Token: 0x040006E4 RID: 1764
		Joystick4Button18,
		// Token: 0x040006E5 RID: 1765
		Joystick4Button19,
		// Token: 0x040006E6 RID: 1766
		Joystick5Button0,
		// Token: 0x040006E7 RID: 1767
		Joystick5Button1,
		// Token: 0x040006E8 RID: 1768
		Joystick5Button2,
		// Token: 0x040006E9 RID: 1769
		Joystick5Button3,
		// Token: 0x040006EA RID: 1770
		Joystick5Button4,
		// Token: 0x040006EB RID: 1771
		Joystick5Button5,
		// Token: 0x040006EC RID: 1772
		Joystick5Button6,
		// Token: 0x040006ED RID: 1773
		Joystick5Button7,
		// Token: 0x040006EE RID: 1774
		Joystick5Button8,
		// Token: 0x040006EF RID: 1775
		Joystick5Button9,
		// Token: 0x040006F0 RID: 1776
		Joystick5Button10,
		// Token: 0x040006F1 RID: 1777
		Joystick5Button11,
		// Token: 0x040006F2 RID: 1778
		Joystick5Button12,
		// Token: 0x040006F3 RID: 1779
		Joystick5Button13,
		// Token: 0x040006F4 RID: 1780
		Joystick5Button14,
		// Token: 0x040006F5 RID: 1781
		Joystick5Button15,
		// Token: 0x040006F6 RID: 1782
		Joystick5Button16,
		// Token: 0x040006F7 RID: 1783
		Joystick5Button17,
		// Token: 0x040006F8 RID: 1784
		Joystick5Button18,
		// Token: 0x040006F9 RID: 1785
		Joystick5Button19,
		// Token: 0x040006FA RID: 1786
		Joystick6Button0,
		// Token: 0x040006FB RID: 1787
		Joystick6Button1,
		// Token: 0x040006FC RID: 1788
		Joystick6Button2,
		// Token: 0x040006FD RID: 1789
		Joystick6Button3,
		// Token: 0x040006FE RID: 1790
		Joystick6Button4,
		// Token: 0x040006FF RID: 1791
		Joystick6Button5,
		// Token: 0x04000700 RID: 1792
		Joystick6Button6,
		// Token: 0x04000701 RID: 1793
		Joystick6Button7,
		// Token: 0x04000702 RID: 1794
		Joystick6Button8,
		// Token: 0x04000703 RID: 1795
		Joystick6Button9,
		// Token: 0x04000704 RID: 1796
		Joystick6Button10,
		// Token: 0x04000705 RID: 1797
		Joystick6Button11,
		// Token: 0x04000706 RID: 1798
		Joystick6Button12,
		// Token: 0x04000707 RID: 1799
		Joystick6Button13,
		// Token: 0x04000708 RID: 1800
		Joystick6Button14,
		// Token: 0x04000709 RID: 1801
		Joystick6Button15,
		// Token: 0x0400070A RID: 1802
		Joystick6Button16,
		// Token: 0x0400070B RID: 1803
		Joystick6Button17,
		// Token: 0x0400070C RID: 1804
		Joystick6Button18,
		// Token: 0x0400070D RID: 1805
		Joystick6Button19,
		// Token: 0x0400070E RID: 1806
		Joystick7Button0,
		// Token: 0x0400070F RID: 1807
		Joystick7Button1,
		// Token: 0x04000710 RID: 1808
		Joystick7Button2,
		// Token: 0x04000711 RID: 1809
		Joystick7Button3,
		// Token: 0x04000712 RID: 1810
		Joystick7Button4,
		// Token: 0x04000713 RID: 1811
		Joystick7Button5,
		// Token: 0x04000714 RID: 1812
		Joystick7Button6,
		// Token: 0x04000715 RID: 1813
		Joystick7Button7,
		// Token: 0x04000716 RID: 1814
		Joystick7Button8,
		// Token: 0x04000717 RID: 1815
		Joystick7Button9,
		// Token: 0x04000718 RID: 1816
		Joystick7Button10,
		// Token: 0x04000719 RID: 1817
		Joystick7Button11,
		// Token: 0x0400071A RID: 1818
		Joystick7Button12,
		// Token: 0x0400071B RID: 1819
		Joystick7Button13,
		// Token: 0x0400071C RID: 1820
		Joystick7Button14,
		// Token: 0x0400071D RID: 1821
		Joystick7Button15,
		// Token: 0x0400071E RID: 1822
		Joystick7Button16,
		// Token: 0x0400071F RID: 1823
		Joystick7Button17,
		// Token: 0x04000720 RID: 1824
		Joystick7Button18,
		// Token: 0x04000721 RID: 1825
		Joystick7Button19,
		// Token: 0x04000722 RID: 1826
		Joystick8Button0,
		// Token: 0x04000723 RID: 1827
		Joystick8Button1,
		// Token: 0x04000724 RID: 1828
		Joystick8Button2,
		// Token: 0x04000725 RID: 1829
		Joystick8Button3,
		// Token: 0x04000726 RID: 1830
		Joystick8Button4,
		// Token: 0x04000727 RID: 1831
		Joystick8Button5,
		// Token: 0x04000728 RID: 1832
		Joystick8Button6,
		// Token: 0x04000729 RID: 1833
		Joystick8Button7,
		// Token: 0x0400072A RID: 1834
		Joystick8Button8,
		// Token: 0x0400072B RID: 1835
		Joystick8Button9,
		// Token: 0x0400072C RID: 1836
		Joystick8Button10,
		// Token: 0x0400072D RID: 1837
		Joystick8Button11,
		// Token: 0x0400072E RID: 1838
		Joystick8Button12,
		// Token: 0x0400072F RID: 1839
		Joystick8Button13,
		// Token: 0x04000730 RID: 1840
		Joystick8Button14,
		// Token: 0x04000731 RID: 1841
		Joystick8Button15,
		// Token: 0x04000732 RID: 1842
		Joystick8Button16,
		// Token: 0x04000733 RID: 1843
		Joystick8Button17,
		// Token: 0x04000734 RID: 1844
		Joystick8Button18,
		// Token: 0x04000735 RID: 1845
		Joystick8Button19
	}
}
