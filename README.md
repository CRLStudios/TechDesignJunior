# Junior Tech Designer
This is a Technical Design test written for applicants of Pracy Studios.

## Instructions
- Create a gihub account if you do not have one. <br/>
- Begin by making a fork of this repository on GitHub.<br/>
- Install Unity 2022.3.7f1 <br/>
- Clone your forked repository to your local machine. <br/>
- Read the documentation and update the project to meet the Requirements listed below as well as fix the known bugs.
- One of the known bugs may require a small code change to fix. The rest of the tasks should require no code changes to complete but you may make code changes or add new scripts if you desire. <br/>
- Push your changes to your forked github repository when you are finished so all your changes get uploaded to github. <br/> 
- Finally open a Pull Request on the github website so we know it is ready for us to review. <br/>

## Documentation
 - ProgressFlags are Key-Value pairs that are used to record events that have happened or things the player has done.
 - Player's inventory can only hold one item. If the player picks up an item while holding a different item the currently held item will be returned to its original position.
 - Interactions
 	- InteractableObjects are used to start cutscenes and set progress flags. They require an InteractableGraph asset to decide if they can run or not.
 	- InteractableGraphs are scriptable objects that contain a graph of nodes. These node types control the branching of cutscenes.
 		- You can create interactable graph options by using the create menu in the project tab.
 		- Select an interactable graph and click the edit button in the inspector tab to open the graph editor window.
 		- Right click in the editor graph window to show a menu of nodes that can be added to the graph.
 		- Start a graph by adding a Interactable Start Node and an Interactable Exit Node. The interaction will flow from the Start node towards the Exit node.
 		- You can use the Set Progress Flag and Get Progress Flag nodes to do branching logic inside of interactable graphs.
 		- An interactable object can only be activated by the player if the current branching path in the interactable graph contains a node associated with an action such as running a cutscene or setting a progress flag. If the graph gets to the Exit node without encountering an action node it will not display the interaction alert to the player or activate when the player enters the trigger and presses enter.
 - Cutscenes
 	- You can create a cutscene using the Cutscene component.
 	- Cutscenes have a "Cutscene Id" which is just the name of the GameObject that has a Cutscene component added to it. You can see this id in the inspector
 	- Use the cutscene id to start a cutscene from the interactable node graph.

## Requirements
 - Player must figure out the Pillbug's favorite food to cross the bridge and get to the goal
 	- Pillbug and Mantis cannot speak. They gesture in italics.
 	- The scene colliders are not complete. Setup colliders for the scene. The player should not be able to pass through any walls, trees, lamps, bridge rails, or water nor walk completely off the edge of the screen.
  - Cutscenes and Interactions are as follows
 	  - Pillbug
   		- The first interaction with the pill bug:
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
   			- Karma: "How about this fat juicy ladybug?"
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
   	- Mantis
   		- If the player interacts with the mantis before interacting with the Pillbug:
   		 - Narrator: "The Mantis gestures upwards towards the bridge"
   		- If the player interacts with the Mantis after feeding the pillbug the cucumber:
   		 - Narrator: "*The Mantis is busy sharpening its claws*"
   		- If the player interacts with the Mantis after interacting with the Pillbug but is not holding the cucumber:
   		 - Narrator: "*The Mantis gestures toward the bushes to the west.*"
   		- If the player interacts with the Mantis after interacting with the Pillbug while holding the cucumber:
   		 - Narrator: "The Mantis gestures upwards towards the bridge"
   	- The End
   		- When the player reaches the top of the small set of stairs after crossing the bridge a cutscene should automatically start without the player pressing a button:
   			- Player should move to the center of the grassy patch and face south
   			- Narrator: "The End!"

## Known Bugs
 - Karma's shadow overlays their feet.
 - The interaction alert sometimes sorts behind objects it should be in front of. It should sort together with the player sprite.
 - The interaction alert is visible until the player enters and exits an interactable trigger. The alert should be hidden when the scene starts.
 - The pillbug collider does not collide with the player and prevent the player from moving.
 - Cucumber cannot be picked up.
 - Player should not be able to walk past the Pillbug until feeding it the cucumber.
