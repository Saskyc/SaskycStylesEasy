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

TagName {
  property: thing;
  variable: thing;
}

TagName(hexColor, myText) {
  text: myText;
  color: hexColor;
}

### Example:

tagName {
  text: hello world;
  bold: true;
  hintId: epicId;
  hintX: 0;
  hintY: 700;
  hintTime: 8;
  align: left;
  show: true;
}

### Explain of structure
The text is used as the base basically meaning the thing mainly going to be used.
Bold makes the text use <b> around it.
hintId adds hint Id to it if 2 of them will be in the same use the before one will be deleted.
hintX is the X coordinate alongside hintY being the Y coordinate.
hintTime is how long the hint lasts.
align is where it should be located. modes left | center | right.
show means to actually show hint you require to add hintX, hintY, hintId and show.

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
### Before:
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
### After:
