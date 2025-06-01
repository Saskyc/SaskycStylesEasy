# SSE
So what does it stand for? It's in the name of the repository SaskycStylesEasy.
Have you ever coded in .css and though: "hmm I would really want to refractor it into child of .css and .cs (c#) which will use tags from this game that needs another framework to help it.
Yes you will have to install hint service meow. https://github.com/MeowServer/HintServiceMeow.

So saskyc tell us the structure! Sure.

TagName {
  property: thing;
  variable: thing;
}

Example:

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

| Tag  | Usage | Required tags | Information |
| ------------- | ------------- | ------------- | ------------- |
| Color  | Hex  | None | Changes the color of the text |
| Align | Left/Center/Right | None | Changes the align |
