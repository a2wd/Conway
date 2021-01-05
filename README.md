# Conway
An implementation of Conway's game-of-life, in Unity

## Instructions

Assuming a binary has been downloaded from the [releases](https://github.com/a2wd/Conway/releases) page:

1. Click cells to turn them from **DEAD** (white) to **ALIVE** (red).
2. Space bar starts/stops the simulation.

## General rules for Conway's Game Of Life

Per [the wiki](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life), for each iteration of the world state, cells change state depending on the following rules:

1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
2. Any live cell with two or three live neighbours lives on to the next generation.
3. Any live cell with more than three live neighbours dies, as if by overpopulation.
4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

This can be used to create open ended or closed simulations.