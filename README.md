# SSE
Long time ago was created https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichText.html.
Idea created from day to day merged and proggramed into 3AM when next day had school.
- With the help of ChatGPT ♥️.

So what does it stand for? It's in the name of the repository SaskycStylesEasy.
Have you ever coded in .css and though: "hmm I would really want to refractor it into child of .css and .cs (c#) which will use tags from this game that needs another framework to help it.
Yes you will have to install hint service meow. https://github.com/MeowServer/HintServiceMeow.

### Introduction
Plugin creates folder in your plugins folder (where the .dll is located)
- This uses custom extension called .sse
- All files will have to use .sse (no exceptions)
Then you write inside "tags" that may be executed etc. Tags can't have duplicated

So saskyc tell us the structure! Sure.
```
TagName {
  property: thing;
  variable: thing;
}

TagName(hexColor, myText) {
  text: myText;
  color: hexColor;
}
```
### Example:
```
tagName {
  text: hello world;
  bold: true;
  hintId: epicId;
  hintX: 0;
  hintY: 700;
  hintDuration: 8;
  align: left;
  show: true;
}
```
### Explain of structure
* The `text` is used as the base basically meaning the thing mainly going to be used.
* `Bold` makes the text use <b> around it.
* `hintId` adds hint Id to it if 2 of them will be in the same use the before one will be deleted.
* `hintX` is the X coordinate alongside hintY being the Y coordinate.
* `hintDuration` is how long the hint lasts.
* `align` is where it should be located. modes left | center | right.
* `show` means to actually show hint you require to add hintX, hintY, hintId and show.

# The tags

| Tag  | Usage | Required tags/variables | Information |
| ------------- | ------------- | ------------- | ------------- |
| Text | String | None | Set's the text used in the tag |
| [Color](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextColor.html)  | Hex  | None | Changes the color of the text. |
| [Align](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextAlignment.html) | Left/Center/Right | None | Changes the align. |
| [Bold](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextBoldItalic.html) | true/false | None | Makes the text bold. Use false to disable. |
| [Italic](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextBoldItalic.html) | true/false | None | Makes the text italic. Use false to disable. |
| [Underline](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextStrikethroughUnderline.html) | true/false | None | Tag draws the line slightly below the baseline to underline the text. Use false to disable. |
| [Strikethrough](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextStrikethroughUnderline.html) | true/false | None | Tag places the line slightly above the baseline so it crosses out the text. Use false to disable. |
| [Allcaps](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextLetterCase.html) | true/false | None | Makes the text uppercase. Use false to disable. |
| [Uppercase](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextLetterCase.html) | true/false | None | Makes the text uppercase. Use false to disable. |
| [Smallcaps](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextLetterCase.html) | true/false | None | Makes the text smallcase. Use false to disable. |
| [Lowecase](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextLetterCase.html) | true/false | None | Makes the text lowercase. Use false to disable. |
| [Size](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextSize.html) | Integer | None | Adjust the font size of your text. Use numbers |
| [Indent](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextIndentation.html) | Integer | None | Tag controls the horizontal caret position the same way the <pos> tag does |
| [Width](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextWidth.html) | Integer | None | Adjust the horizontal size of text area. |
| [Alpha](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextOpacity.html) | Integer | None | Used to change opacity of text. Will be automatically next to the text. |
| [Cspace](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextRotate.html) | Integer | Rotate | Adjusts character spacing, and can help correct overlap caused by rotation |
| [Rotate](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextRotate.html) | Integer | None (CSPACE will be 0 by default) | Used to the roatation of text. |
| [Margin](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextMargins.html) | Integer | None | Increase the horizontal margins of the text |
| [Mark](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/RichTextMark.html) | Hex | markUp, markDown, markRight, markLeft | Makes mark around your text. |
| Enclose | tag1, tag2... | None | Takes output from tags and puts it around text. |
| Execute | tag1, tag2... | None | Executes tags from this tag (is runned as last property). |
| Addition | tag1, tag2... | None | Executes the tags and adds the output to the end (may be used with Show). |
| Show | true/false | hintId, hintX, hintY, hintDuration | Shows the hint, use false to disable it. |

# Example of changing normal tags to .sse
<details>
<summary>Before:</summary>
```
<line-height=-200>\n</line-height>
<size=60><i><color=#ffe6a6>📝</color> <u>You are</u><color=#e85c39></i>❓</color></size>
<size=50><b>Researcher</b></size>







<align=right><color=#ffb600>⚠️</color> Area-46 <color=#ffb600>⚠️</color></align>
<align=right><size=20><u>We develop in</u> <b><color=#7e7f80>dark</color></b> <u>so 
you may play in the</u> <color=#62b8ff><b>light</b></color></size>
<size=20><b><color=#7a0fa5><alpha=#99><pos=170>💠<alpha=#99><pos=370>💠</color> <pos=200>Site Inspection</b></size> <size=50><color=#131313><pos=150><alpha=#50><rotate=50>🌍</rotate></color><color=#131313><pos=200><alpha=#50><rotate=100>🌍</rotate></color><color=#131313><pos=250><alpha=#50><rotate=150>🌍</rotate></color><color=#131313><pos=300><alpha=#50><rotate=200>🌍</rotate></color><color=#131313><pos=350><alpha=#50><rotate=250>🌍</rotate></color></size></align>





<mark=#000000 padding="2800, 2800, -130, 60">‎</mark>
<mark=#000000aa padding="2800, 2800, -60, 290">‎</mark>
<color=#4bbbdc><size=25>⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬⏬</size></color>
<size=20>Your job is to help others</size><size=20>
<size=20>Do their job</size>
```
</details>
<details>
<summary>After:</summary>
```
roleShowManager{
    execute: header, subHeader, area46InfoHeader, lastText;
}

header {
    size: 60;
    text: You are;
    execute: headerFirstEmoji, headerSecondEmoji;

    hintId: header id;
    hintX: 0;
    hintY: 200;
    hintDuration: 8;

    show: true;
}

subHeader {
    text: Researcher;
    size: 50;
    align: center;
    bold: true;
    hintid: subHeader;
    hintX: 0;
    hintY: 260;
    hintDuration: 8;
    show: true;
}

area46InfoHeaderEnclosing {
    align: right;
}

area46HeaderWarn{
    size: 30;
    text: ⚠️;
    align: right;
    color: #ffb600;
}

area46InfoHeader{
    text: ;
    addition: 
        area46HeaderWarn, 
        area64HeaderTextPart, 
        area46HeaderWarn;
    enclose: area46InfoHeaderEnclosing;
    execute: area46FirstSmallText, area46SecondSmallText, area46ThirdSmallText;
    hintid: area64InfoHeader;
    hintX: 0;
    hintY: 530;
    hintDuration: 8;
    show: true;
}

area64HeaderTextPart{
    text: Area-46;
    size: 30;
}

DynamicText(inputText, inputBold, inputColor, inputUnderline){
    size: 20;
    align: right;
    bold: inputBold;
    underline: inputUnderline;
    text: inputText;
    color: inputColor;
}

area46FirstSmallText {
    text: ;

    hintid: area46FirstSmallText;
    hintX: 0;
    hintY: 560;
    hintDuration: 8;

    addition: 
        DynamicText(We develop in, false, white, true), 
        DynamicText(dark, true, #7e7f80, false), 
        DynamicText(so, false, white, true);
    
    show: true;
}

area46SecondSmallText {
    text: ;

    hintid: area46SecondSmallText;
    hintX: 0;
    hintY: 580;
    hintDuration: 8;

    addition: 
        DynamicText(you may play in the, false, white, true),
        DynamicText(light, true, #62b8ff, false);
    
    show: true;
}

enclosingTest {
    text: s;
    italic: true;
    align: left;
}

area46ThirdSmallText {
    text: ;

    hintid: area46ThirdSmallText;
    hintX: 0;
    hintY: 600;
    hintDuration: 8;
    enclose: enclosingTest;

    addition: 
        DynamicText(Site Inspection, true ,white, false);
    
    show: true;
}

headerFirstEmoji {
    hintId: header first emoji id;
    hintX: -430;
    hintY: 200;
    hintDuration: 8;
    text: 📝;
    size: 60;
    italic: true;
    color: #ffe6a6;
    show: true;
}

headerTextPart {
    text: You are;
    size: 60;
    underline: true;
    italic: true;
}

headerSecondEmoji {
    hintId: header emoji id;
    hintX: 270;
    hintY: 200;
    hintDuration: 8;
    text: ❓;
    size: 60;
    color: #e85c39;
    show: true;
}

lastText {
    text: ;
    execute: FirstDifference, SecondDifference;
}

FirstDifference {
    color: #000000aa;
    size: 500;
    text: █;
    width: 25%;
    hintid: firstDifferencee;
    hintX: 0;
    hintY: 900;
    hintDuration: 8;
    show: true;
}

SecondDifference {
    color: #000000aa;
    size: 500;
    text: █;
    width: 25%;
    hintid: secondDifferencee;
    hintX: 50;
    hintY: 900;
    hintDuration: 8;
    show: true;
}
```
</details>
