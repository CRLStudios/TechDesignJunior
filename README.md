# Junior Tech Designer
This is a Technical Design test written for applicants of Pracy Studios.

## Instructions
- Begin by cloning or downloading this repository.
- Install Unity 2022.3.7f1 and open the project.
- Read the Documentation.
- Complete all the Objectives.
- Test to ensure everything is working as intended.
- Track the total time spent on this test.
- Zip up the project when complete and send it to Pracy Studios for review.

Note: One of the known bugs requires a small code change to fix. The rest of the Objectives should require no code changes.

## Objectives
- Fix all bugs in the "Known Bugs" section.
- Implement the following:
  1) Setup the scene colliders. The player should not be able to pass through any walls, trees, lamps, bridge rails, water, nor walk completely off the edge of the screen.
  2) Implement the "Pillbug" cutscene.
  3) Implement the "Mantis" cutscene.
  4) Implement the "End" cutscene.

## In-Game Controls
- Use the Arrow Keys or WSAD to move around.
- Press Enter to Interact with items or cutscene triggers.
- Press Enter to progresss dialoge.
- Hold Shift to run.

## Documentation
#### Inventory
- Player's inventory can only hold one item. If the player picks up an item while holding a different item the currently held item will be returned to its original position.
- Items are identified with an itemId which is just a string.
#### Progress Flags
- Progress flags are Key-Value pairs that are used to record events that have happened or things the player has done.
- The progress flag name is the same as the Key and can be any string. Any progress flag Key can be used in the graph and the value will default to 0 if it has not been set or incremented.
- The default value of a progress flag that has not been set or incremented is 0.
- There is also a ProgressFlagEnabler component that can be used to enable or disable game objects depending on the value of a progress flag.
#### Interactions
- InteractableObjects are used to start cutscenes and set progress flags. They require an InteractableGraph asset to decide if they can run or not.
- You can add an InteractableObject component to a character to create an interactable NPC.
- InteractableGraphs are ScriptableObjects that contain a graph of nodes. These node types control the branching of cutscenes.
- You can create an interactable graph by using the create menu in the project tab.
- Select an interactable graph and click the edit button in the inspector tab to open the graph editor window.
- Right click in the editor graph window to show a menu of nodes that can be added to the graph.
- Start a graph by adding a Interactable Start Node and an Interactable Exit Node. The interaction will flow from the Start node towards the Exit node.
- You can use the Set Progress Flag, Get Progress Flag, and Has Item nodes to do branching logic inside of interactable graphs.
- An interactable object can only be activated by the player if the current branching path in the interactable graph contains a node associated with an action such as running a cutscene or setting a progress flag. If the graph gets to the Exit node without encountering an action node it will not display the interaction alert to the player and will not trigger a cutscene.

## Cutscenes
- You can create a cutscene using the Cutscene component.
- Cutscenes contain a list of actions for dialogue and moving characters.
- Cutscenes have a "Cutscene Id" which is just the name of the GameObject that has a Cutscene component added to it. You can see this Id in the inspector window at the bottom of the Cutscene component.
- Use the cutscene Id and an Interaction Cutscene node to start a cutscene from the interactable node graph.
- Pillbug and Mantis cannot speak. They gesture in italics.
#### Pillbug Cutscene
- The first interaction with the pillbug:
	- Karma: "Excuse me, may I pass?"
	- Narrator: "*The pillbug gestures to it's belly with its tiny arms*"
	- Karma: "What? Are you hungry?"
	- Karma: "If I bring you something to eat will you let me pass?"
	- Narrator: "*The pillbug seems to nod its bulbous head*"
	- Karma: "Alright, I'll see if I can find anything"
- If Karma interacts with the pillbug without any items:
	- Karma: "What does a pillbug eat?"
	- Narrator: "*The pillbug just stares at you hungrily gesturing towards its stomach again.*"
- If Karma interacts with the pillbug while holding a fish:
	- Karma: "How do you like this fish?"
	- Narrator: "*The pillbug makes a face that you think is disapproval*"
- If Karma interacts with the pillbug while holding a ladybug:
	- Karma: "How about this juicy ladybug?"
	- Narrator: "*The pillbug shrieks in horror*"
	- Karma: "Oh, too close to home I guess."
- If Karma interacts with the pillbug while holding the cucumber but before offering either fish or the ladybug:
	- Karma: "How about this cucumber?"
	- Narrator: "*The pillbug squeals in delight*"
	- Karma: "Nailed it, first try!"
	- Pillbug should move to the right hand side of the bridge clearing the way for Karma to cross to the cliffs above.
- If Karma interacts with the pillbug while holding the cucumber after first offering either fish or the ladybug or both:
	- Karma: "Alright, I found this cucumber."
	- Narrator: "*The pillbug squeals in delight*"
	- Karma: "Who knew a Pillbug could be so picky!"
	- Pillbug should move to the right hand side of the bridge clearing the way for Karma to cross to the cliffs above.
- After the pillbug has moved:
	- No interaction should be available.

#### Mantis Cutscene
- If the player interacts with the mantis before interacting with the Pillbug:
	- Narrator: "The Mantis gestures upwards towards the bridge"
- If the player interacts with the Mantis after feeding the pillbug the cucumber:
	- Narrator: "*The Mantis is busy sharpening its claws*"
- If the player interacts with the Mantis after interacting with the Pillbug but is not holding the cucumber:
	- Narrator: "*The Mantis gestures toward the bushes to the west.*"
- If the player interacts with the Mantis after interacting with the Pillbug while holding the cucumber:
	- Narrator: "The Mantis gestures upwards towards the bridge"

#### End Cutscene
- After the player crosses the bridge and reaches the top of the small set of stairs, a cutscene should automatically start without the player pressing a button:
	- Player should move to the center of the grassy patch and face south.
	- Narrator: "The End!"

## Known Bugs
- The shadow of the main character "Karma" appears ontop their feet instead of below.
- The interaction alert sometimes sorts behind objects it should be in front of. It should sort together with the player sprite.
- The interaction alert is visible until the player enters and exits an interactable trigger. The alert should be hidden when the scene starts.
- The pillbug collider does not collide with the player and prevent the player from passing. Player should not be able to walk past the Pillbug until feeding it the cucumber.
- Cucumber cannot be picked up.
