<Query Kind="Program">
  <NuGetReference>State.OR.Oya.Core.Utility</NuGetReference>
  <Namespace>State.OR.Oya.Core.Utility.Caching</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Diagnostics</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.IO</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Logging</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Reflection</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.ServiceClient</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.String</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Threading</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Xml</Namespace>
</Query>

void Main()
{
	var regx =
			@"\[Datalink:(?<dataLinkId>\d+)\s*\|?\s*
                (?<length>\d\.?\d*)\s*\|?\s*
                (?<type>.)\s*\|\s*
                (?<fontInfo>[^\d\s][^\d]+)?\s*
                (?<fontSize>\d{1,2})?\s*
                (?<formatting>([^\]]*))\s*
                \]";
	var regex = new Regex(regx, RegexOptions.IgnorePatternWhitespace);

	//var regex = new Regex(@"\[Datalink:(?<dataLinkId>\d+)\|(?<length>(\d\.?)+)\|?(?<type>.)?\|(?<formatting>((bold|italic|underline)?\s?)*)\]");


	var s = @"{\rtf1\ansi{\fonttbl{\f0\fnil times new roman;}}
{\stylesheet{\s900 \f0\fs22 Normal;}}
\paperw12240\paperh15840\margl1080\margr1080\margt720\margb720\gutter0
{\info{\creatim\yr2002\mo11\dy25\hr11\min41}{\revtim\yr2010\mo7\dy26\hr16\min50}}\deftab720\pard\plain \s900\qr \f0\fs22 {\f0\fs14 State of Oregon}
\par \pard\plain \s900\qr \f0\fs22 {\f0\fs14 OREGON YOUTH AUTHORITY}
\par \trowd \trgaph108\trleft0\clbrdrr\brdrs\brdrw40\brsp0\cellx1440\pard\plain \intbl \s900 \f0\fs22 \cell \clbrdrt\brdrs\brdrw40\brsp0\clbrdrb\brdrs\brdrw40\brsp0\clbrdrl\brdrs\brdrw40\brsp0\clbrdrr\brdrs\brdrw40\brsp0\cellx8640\pard\plain \intbl \s900\qc \f0\fs22 {\f0\fs36\b NOTICE OF PRELIMINARY HEARING}\cell \clbrdrl\brdrs\brdrw40\brsp0\cellx10080\pard\plain \intbl \s900 \f0\fs22 \cell 
\row \pard\plain \s900 \f0\fs22 
\par \pard\plain \s900 \tx5328\f0\fs22 In the matter of   {\f0\fs26\b\ul [Datalink:850018059005|2.5|F|Times New Roman 14 bold underline]}  \tab JJIS #: {\ul [Datalink:603528889242|1.0|F| underline]}    DOB:  {\ul [Datalink:603529749248|1.0|F| underline]}
\par \pard\plain \s900 \f0\fs22 
\par \pard\plain \s900 \tx5328\f0\fs22 County of Commitment:  {\f0\fs22\ul [Datalink:700005139017|1.5|F|Times New Roman 12 underline]}  \tab Date Commitment Expires:  {\f0\fs22\ul [Datalink:603543442319|1.5|F|Times New Roman 12 underline]}
\par \pard\plain \s900 \f0\fs22 
\par \pard\plain \s900 \tx5328\f0\fs22 Pursuant to ORS Chapters 420 - 420A and Oregon Youth Authority Administrative Rules 416-300-0000 thru 0120, you are hereby notified that a Preliminary Hearing will be held at:
\par \pard\plain \s900\sb172 \tx5328\f0\fs22 {\f0\fs22\ul [Datalink:0|0|L|Times New Roman 12 underline]}
\par \pard\plain \s900 \tx3312\f0\fs22 \tab {\f0\fs20 (Place/Address)}
\par \pard\plain \s900\sb172 \tx4320\f0\fs22 at    {\f0\fs22\ul [Datalink:0|1.5|F|Times New Roman 12 underline]}    on    {\f0\fs22\ul [Datalink:0|0|V|Times New Roman 12 underline]}.
\par \pard\plain \s900 \tx720\tx3312\f0\fs22 \tab {\f0\fs20 (Time)\tab (Date)}
\par \pard\plain \s900 \f0\fs22 
\par \pard\plain \s900 \tx720\tx4320\f0\fs22 The purpose of the hearing is to determine: 
\par \pard\plain \s900\sb86 \tx720\tx4320\f0\fs22 1) If there is probable cause to believe that you have violated the conditions of your parole in the following particulars:
\par \trowd \trgaph108\trleft0\cellx288\pard\plain \intbl \s900 \f0\fs22 \cell \cellx10080\pard\plain \intbl \s900\sb86 \f0\fs22 {\f0\fs22\ul [Datalink:0|0|L|Times New Roman 12 underline]}\cell 
\row \pard\plain \s900 \f0\fs22 
\par \pard\plain \s900 \tx720\tx4320\f0\fs22 or 2) If revocation of your parole would be in your best interest and/or the interest of the community.
\par \pard\plain \s900 \f0\fs22 
\par \pard\plain \s900 \tx720\tx4320\f0\fs22 The {\ul Name(s) and Title(s)} of the person(s) giving information against you are as follows:
\par \trowd \trgaph108\trleft0\cellx288\pard\plain \intbl \s900 \f0\fs22 \cell \cellx10080\pard\plain \intbl \s900\sb86 \f0\fs22 {\f0\fs22\ul [Datalink:0|0|L|Times New Roman 12 underline]}\cell 
\row \pard\plain \s900 \tx720\tx4320\f0\fs22 
\par \pard\plain \s900 \tx720\tx4320\f0\fs22 At the hearing:
\par \trowd \trgaph108\trleft0\cellx576\pard\plain \intbl \s900\qr\sb43 \f0\fs22 a)\par \pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900\qr \f0\fs22 b)\par \pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900\qr \f0\fs22 c)\cell \cellx10080\pard\plain \intbl \s900\sb43 \f0\fs22 You have the right to be present and to have the above person(s) present and to question them and to have\par \pard\plain \intbl \s900 \f0\fs22 help from the hearing officer to make sure that they are present (if confrontation is allowed).\par \pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 You may admit or deny the charge(s) and support your decision with letters, documents, and statement from other persons or your testimony.\par \pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 If you choose, you have the right to have an attorney at your own expense, or at the State's expense under certain limited circumstances which may be explained to you by the Preliminary Hearing Officer prior to the hearing.\cell 
\row \pard\plain \s900 \f0\fs22 
\par \pard\plain \s900 \tx720\tx4320\f0\fs22 {\f0\fs26\b I have read and understand (or have had read and explained to me) the above charges and rights and agree to appear at the above stated address on the above stated date and time.}
\par \trowd \trgaph108\trleft0\cellx3168\pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 \cell \cellx3312\pard\plain \intbl \s900 \f0\fs22 \cell \cellx4752\pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 \cell \cellx5328\pard\plain \intbl \s900 \f0\fs22 \cell \cellx8496\pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 \cell \cellx8640\pard\plain \intbl \s900 \f0\fs22 \cell \cellx10080\pard\plain \intbl \s900 \f0\fs22 \par \pard\plain \intbl \s900 \f0\fs22 \cell 
\row \trowd \trgaph108\trleft0\clbrdrt\brdrs\brdrw20\brsp0\cellx3168\pard\plain \intbl \s900\qc \f0\fs22 {\f0\fs20 (Youth Signature)}\cell \cellx3312\pard\plain \intbl \s900\qc \f0\fs22 \cell \clbrdrt\brdrs\brdrw20\brsp0\cellx4752\pard\plain \intbl \s900\qc \f0\fs22 {\f0\fs20 (Date)}\cell \cellx5328\pard\plain \intbl \s900\qc \f0\fs22 \cell \clbrdrt\brdrs\brdrw20\brsp0\cellx8496\pard\plain \intbl \s900\qc \f0\fs22 {\f0\fs20 (Witness Signature)}\cell \cellx8640\pard\plain \intbl \s900\qc \f0\fs22 \cell \clbrdrt\brdrs\brdrw20\brsp0\cellx10080\pard\plain \intbl \s900\qc \f0\fs22 {\f0\fs20 (Date)}\cell 
\row \pard\plain \s900 \f0\fs22 
\par }[Datalink:603543442319|1.5|F|Times New Roman 12 underline bold italic strike]";

	var rtfStringBuilder = new StringBuilder(s);
	var matches = regex.Matches(s);
	matches.Count.Dump();
	foreach (Match datalinkMatch in matches)
	{
		datalinkMatch.Value.Dump();
		var formattingString = datalinkMatch.Groups["formatting"].Value;
		if (formattingString.Contains("bold"))
		{
			rtfStringBuilder.Replace(datalinkMatch.Value, $@"\b {datalinkMatch.Value}\b0");
		}
		if (formattingString.Contains("underline"))
		{
			rtfStringBuilder.Replace(datalinkMatch.Value, $@"\ul {datalinkMatch.Value}\ulnone");
		}
		if (formattingString.Contains("italic"))
		{
			rtfStringBuilder.Replace(datalinkMatch.Value, $@"\i {datalinkMatch.Value}\i0");
		}
	}
	
}

// Define other methods and classes here
