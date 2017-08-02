# Lifelike

Lifelike is a (C# WinForms) application in which given some settings a user can artificially select and breed cellular automata; i.e., it performs a genetic algorithm in which the user manually provides the fitness criteria interactively.

I decided I wanted to only allow a reproduction step in which I scramble together state tables in various ways, guessing that using “DNA” more complex than commensurate 2D tables of numbers wouldn’t work well for a genetic process in this case. I characterize CA rules as applying only relative to a given number of states and a given, what I call, “cell structure” and “neighborhood function”. Cell structure just means a lattice type and neighborhood –e.g. square, with four neighbors; square, with eight neighbors; hex, with six, etc. “Neighborhood function” is an arbitrary function that given the  states of the n cells in some cell’s neighborhood returns an integer from 0 to r where r is dependent on the neighborhood function and possibly number of states. For example, Conway Life uses the neighborhood function I refer to as “alive cell count”, and for an n-cell neighborhood, r equals n because the greatest number of alive cells that can surround a cell is just the size of the neighborhood. If the user has selected s total states, the state tables will be s by r.

Lifelike works as follows

* The user selects a number of states, cellular structure, and neighborhood function and kicks off the genetic process.
* Lifelike sets the current generation to nil, where by “generation” we just mean a set of cellular automata that have been tagged with fitness values.
* While the user has not clicked the “go to the next generation” button,
  * If the current generation is nil, Lifelike randomly generates a cellular automata, CA, from scratch by making an s by r state table filled with random numbers from 0 to s. (The random states are generated via a discrete distribution controllable by the user). If the generation is not nil, Lifelike selects a reproduction method requiring k parents, selects k parents from the current generation such that this selection is weighted on the fitness of the automata, generates CA using the reproduction method and parents, and then possibly selects a random mutation function and mutates CA, selecting the mutation function via a discrete distribution controllable by the user and applying it with a “temperature” controlled by the user.
  * Lifelike presents CA in a window.
* The user either skips CA in which case it no longer plays a role in the algorithm or applies a fitness value to it and adds it to the next generation.
* When the user decides to go to next generation, the selections the user just made become the new parent generation and processing continues.
