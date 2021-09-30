# File Viewer v2

Simulated file viewer / File hierarchy visualizer in Blazor in WebAssembly.

For exercise details, check EXERCISE.md.

This is a clean-room remake of [File Viewer](https://github.com/robertsundstrom/FileViewer) (v1). 

Approx. 1,5 year later.

Watch the [video](https://www.youtube.com/watch?v=kWz61jGjuKM).

## Scenario

**TL;DR;** Parses file paths and displays them in a tree view.

The program parses hard-coded paths and builds an object hierarchy out of them. This tree consists if ```Nodes``` that can represent either files or directories. A directory may have child nodes.

The nodes then get displayed in a Graphical User Interface (GUI) consisting of a Tree View and an Item View. The Item View displays both files and directories. Directories can be displayed as either a grid or a table of child items. When clicking in either view the state will change in the other.

Items can be created and deleted from the UI.

## Differences from v1

* Built on .NET 6 and the latest version C# (version 10) and Blazor
* Using MudBlazor component library instead of Bootstrap - Material Design and more goodies out of the box
* Simplified data model - from parsing the path to building and manipulating the tree
* Better unit tests - and I'm using Shoudly.

## Issues
* When focusing an item in the Item View, the corresponding item does not get selected in the Tree View.
* The "Go Back" does not get enabled or disabled when navigating using the tree view.

Watch out for comments in code!

## Build instructions

You need to have .NET 6 SDK installed to build this project.

No other dependencies required. Not even Node.