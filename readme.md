# Paper3D
An experimental project design to emulate the graphical style of the classic _Paper Mario_ games.

## Goals
The goals of this project are as follows:
 - Have a re-usable codebase that can be dropped into any MonoGame project for rendering
 - Understand the 3D rendering process in MonoGame
 - Enable Spritesheet animations with the target plane
 - Have this all ready before August 9, 2025 (the start date of Magical Girl Game Jam 12)

While it'll be nice to be a drag-and-drop solution, some setup will be required by the project. Furthermore, understanding the content [here](https://docs.monogame.net/articles/getting_to_know/whatis/graphics/WhatIs_3DRendering.html) will be crucial.

And while I'm at it, I might learn Gum, too. That's a UI library for MonoGame.

## Requirements
 - Good performance is a must. Many of the games I'll release using this rendering engine will be published on web, which is slower than native apps.
 - MUST Render to a render target. This will make resizing the viewport easier in the long run.
 - MAY support custom 3D models.
 - MAY support 3D animation, but that's left as an exercise to the reader. I'm not doing that.
 - MUST use an index buffer for its built-in objects/rendering. This is for performance's sake.

## Supporting Libaries
Okay, it won't be completely drag-and-drop. We're going to rely on MonoGame.Extended's ECS for rendering 3D objects.

## Resources
https://gamefromscratch.com/monogame-tutorial-beginning-3d-programming/
