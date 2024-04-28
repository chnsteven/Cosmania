INCLUDE Globals.ink
* {currentState ? beforeDialog} -> Dialog

= Dialog
I will prepare some drinks for you. # speaker:servant # portrait:npc
In the mean time, you can wait for me inside the living room.
It's located near the end of the hallway.
~ currentState = nextScene
->END


