### guidelines

- use in-memory store
- use TDD
- UI components to serve the functionality

### features

- start a new game
- update game score
- finish game
- get a summary of games in progress

### notes on solution

- I will extract `GameScore` as a separate entity as it feels like a part of `Game` that can be tracked and changed quite a lot without changing a parent entity itself, so let's make a separate data set that requires a foreign key to `Game`
- Not sure about how to separate filtering and ordering functionality between repository and service, the options are:
  1. I am used to reading data from external sources and usually, it is possible to request filtering and ordering as an input
  2. In the case of in-memory storage - it makes more sense to put filtering and ordering pieces into a service responsibility so that storage does not complete any business logic, just returns data as is
  
  will proceed with option 2 considering the sandbox implementation
