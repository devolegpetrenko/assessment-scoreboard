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

- I would like to extract `GameScore` as separate entity as it feels like a part of `Game` that can be tracked and changed quite a lot without changing parent entity itself, so lets make a separate data set that requires foreign key to `Game`