<Query Kind="FSharpProgram">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <NuGetReference>FParsec</NuGetReference>
  <Namespace>FParsec</Namespace>
  <Namespace>FParsec.Internal</Namespace>
  <Namespace>Microsoft.FSharp.Collections</Namespace>
  <Namespace>Microsoft.FSharp.Control</Namespace>
  <Namespace>Microsoft.FSharp.Core</Namespace>
  <Namespace>Microsoft.FSharp.Core.CompilerServices</Namespace>
  <Namespace>Microsoft.FSharp.Data.UnitSystems.SI.UnitNames</Namespace>
  <Namespace>Microsoft.FSharp.Linq</Namespace>
  <Namespace>Microsoft.FSharp.Linq.QueryRunExtensions</Namespace>
  <Namespace>Microsoft.FSharp.Linq.RuntimeHelpers</Namespace>
  <Namespace>Microsoft.FSharp.NativeInterop</Namespace>
  <Namespace>Microsoft.FSharp.Quotations</Namespace>
  <Namespace>Microsoft.FSharp.Reflection</Namespace>
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

(*
http://chronos-st.blogspot.com/2007/12/smalltalk-in-one-page.html
http://www.csci.csusb.edu/dick/samples/smalltalk.syntax.html
*)
open FParsec

let test parser text =
    match (run parser text) with
    | Success(result,_,_) -> printfn "Success: %A" result
    | Failure(_,error,_) -> printfn "Error: %A" error


let Character = letter
let WhitespaceCharacter = spaces
let ws = WhitespaceCharacter
let atLeastOne = many1Chars
let zeroOrMore = manyChars

let underscore = pchar '_'

let Letter = asciiLetter // [A-Z][a-z]
let DecimalDigit = digit // [0-9]

//let LetterOrDigit = (DecimalDigit <|> Letter)

let Identifier =  (Letter <|> underscore) >>. zeroOrMore (Letter <|> underscore ) .>> ws

//  Comment = '"', {CommentCharacter}, '"'
let commentDelim = "\""
let notCommentDelim = satisfy(fun c-> c <> ( commentDelim.[0] ) ) (* Any character other than a double quote *)
let CommentStart = pstring commentDelim
let CommentEnd = pstring commentDelim
let Comment = between CommentStart CommentEnd (manyChars notCommentDelim)

//let OptionalWhitespace = WhitespaceCharacter <|> Comment

test Identifier "_qwo8i_djc"
            
test Comment "\" a comment should be contained in quotes\""

//  
//  let Letter = "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K" | "L" | "M"
//                   | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V" | "W" | "X" | "Y" | "Z"
//                   | "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m"
//                   | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z"
//  
//  let CommentCharacter = Character - '"'
//  

//  
//  let OptionalWhitespace = {WhitespaceCharacter | Comment}
//  
//  let Whitespace = (WhitespaceCharacter | Comment), OptionalWhitespace
//  
//  let LetterOrDigit = DecimalDigit | Letter
//
//  let Identifier = (Letter | "_"), {(LetterOrDigit | "_")}
//  
//  let Reference = Identifier
//  
//  let ConstantReference =
//                         "nil"
//                         | "false"
//                         | "true"
//  
//  let PseudoVariableReference =
//                       "self"
//                       | "super"
//                       | "thisContext"
//  
//  /* "thisContext" is not defined by the ANSI Standard, but is widely used anyway */
//  let ReservedIdentifier = PseudoVariableReference | ConstantReference
//
//  let BindableIdentifier = Identifier - ReservedIdentifier
//
//  let UnaryMessageSelector = Identifier
//
//  let Keyword = Identifier, ":"
//
//  let KeywordMessageSelector = Keyword, {Keyword}
//
//  let BinarySelectorChar = "~" | "!" | "@" | "%" | "&" | "*" | "-" | "+" | "=" | "|" | "\" | "<" | ">" | "," | "?" | "/"
//
//  let BinaryMessageSelector = BinarySelectorChar, [BinarySelectorChar]
//
//  let IntegerLiteral = ["-"], UnsignedIntegerLiteral
//
//  let UnsignedIntegerLiteral =   DecimalIntegerLiteral | Radix, "r", BaseNIntegerLiteral
//
//  let DecimalIntegerLiteral = DecimalDigit, {DecimalDigit}
//  
//  let Radix = DecimalIntegerLiteral
//  
//  let BaseNIntegerLiteral = LetterOrDigit, {LetterOrDigit}
//  
//  let ScaledDecimalLiteral = ["-"], DecimalIntegerLiteral, [".", DecimalIntegerLiteral], "s", [DecimalIntegerLiteral]
//  
//  let FloatingPointLiteral = ["-"], DecimalIntegerLiteral, (".", DecimalIntegerLiteral, [Exponent] | Exponent)
//  
//  let Exponent = ("e" | "d" | "q"), [["-"], DecimalIntegerLiteral]
//  
//  let CharacterLiteral = "$", Character
//  
//  let StringLiteral = "'", {StringLiteralCharacter | "''"}, "'"
//  
//  /* To embed a "'" character in a String literal, use two consecutive single quotes */
//  let StringLiteralCharacter = Character - "'"
//  
//  /* Any character other than a single quote */
//  let SymbolInArrayLiteral =
//                            UnaryMessageSelector - ConstantReference
//                            | KeywordMessageSelector
//                            | BinaryMessageSelector
//  
//  let SymbolLiteral = "#", (SymbolInArrayLiteral | ConstantReference | StringLiteral)
//  
//  let ArrayLiteral =  ObjectArrayLiteral | ByteArrayLiteral
//
//  let ObjectArrayLiteral = "#", NestedObjectArrayLiteral
//  
//  let NestedObjectArrayLiteral = "(", OptionalWhitespace, [LiteralArrayElement, {Whitespace, LiteralArrayElement}], OptionalWhitespace, ")"
//  
//  let LiteralArrayElement =
//                            Literal - BlockLiteral
//                            | NestedObjectArrayLiteral
//                            | SymbolInArrayLiteral
//                            | ConstantReference
//  
//  let ByteArrayLiteral = "#[", OptionalWhitespace, [UnsignedIntegerLiteral, {Whitespace, UnsignedIntegerLiteral}], OptionalWhitespace,"]"
//
//  /* The preceding production rules would usually be handled by the lexical analyzer
//     the following production rules would usually be handled by the parser */
//
//  let FormalBlockArgumentDeclaration = ":", BindableIdentifier
//  
//  let FormalBlockArgumentDeclarationList = FormalBlockArgumentDeclaration, {Whitespace, FormalBlockArgumentDeclaration}
//  
//  let BlockLiteral = "[", [OptionalWhitespace, FormalBlockArgumentDeclarationList, OptionalWhitespace, "|"], ExecutableCode, OptionalWhitespace, "]"
//
//  let Literal = ConstantReference
//                   | IntegerLiteral
//                   | ScaledDecimalLiteral
//                   | FloatingPointLiteral
//                   | CharacterLiteral
//                   | StringLiteral
//                   | SymbolLiteral
//                   | ArrayLiteral
//                   | BlockLiteral
//
//  let NestedExpression = "(", Statement, OptionalWhitespace, ")"
//  
//  let Operand =
//                   Literal
//                   | Reference
//                   | NestedExpression
//
//  let UnaryMessage = UnaryMessageSelector
//  
//  let UnaryMessageChain = {OptionalWhitespace, UnaryMessage}
//  
//  let BinaryMessageOperand = Operand, UnaryMessageChain
//  
//  let BinaryMessage = BinaryMessageSelector, OptionalWhitespace, BinaryMessageOperand
//  
//  let BinaryMessageChain = {OptionalWhitespace, BinaryMessage}
//  
//  let KeywordMessageArgument = BinaryMessageOperand, BinaryMessageChain
//  
//  let KeywordMessageSegment = Keyword, OptionalWhitespace, KeywordMessageArgument
//  
//  let KeywordMessage = KeywordMessageSegment, {OptionalWhitespace, KeywordMessageSegment}
//  
//  let MessageChain =
//                   UnaryMessage, UnaryMessageChain, BinaryMessageChain, [KeywordMessage]
//                   | BinaryMessage, BinaryMessageChain, [KeywordMessage]
//                   | KeywordMessage
//  
//  let CascadedMessage = "", OptionalWhitespace, MessageChain
//  
//  let Expression = Operand, [OptionalWhitespace, MessageChain, {OptionalWhitespace, CascadedMessage}]
//  
//  let AssignmentOperation = OptionalWhitespace, BindableIdentifier, OptionalWhitespace, ":="
//
//  let Statement = {AssignmentOperation}, OptionalWhitespace, Expression
//
//  let MethodReturnOperator = OptionalWhitespace, "^"
//
//  let FinalStatement = [MethodReturnOperator], Statement
//
//  let LocalVariableDeclarationList = OptionalWhitespace, "|", OptionalWhitespace, [BindableIdentifier, {Whitespace, BindableIdentifier}], OptionalWhitespace, "|"
//
//  let ExecutableCode = [LocalVariableDeclarationList], [{Statement, OptionalWhitespace, "."}, FinalStatement, ["."]]
//
//  let UnaryMethodHeader = UnaryMessageSelector
//
//  let BinaryMethodHeader = BinaryMessageSelector, OptionalWhitespace, BindableIdentifier
//
//  let KeywordMethodHeaderSegment = Keyword, OptionalWhitespace, BindableIdentifier
//
//  let KeywordMethodHeader = KeywordMethodHeaderSegment, {Whitespace, KeywordMethodHeaderSegment}
//
//  let MethodHeader =
//         UnaryMethodHeader
//         | BinaryMethodHeader
//         | KeywordMethodHeader
//
//  let MethodDeclaration = OptionalWhiteSpace, MethodHeader, ExecutableCode