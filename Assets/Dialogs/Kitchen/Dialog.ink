INCLUDE Globals.ink
* {currentState ? enterKitchen} 
    ->EnterKitchen
* {currentState ? playerDialog} ->PlayerDialog
* {currentState ? playerDialog2} ->PlayerDialog2
* {currentState ? servantDialog} ->ServantDialog
* {currentState ? investigate} ->Investigate
* {currentState ? noInvestigate} ->NoInvestigate


== EnterKitchen
# speaker:player # portrait:player
Strange. The servant should be preping tea. 
(What is she doing over here?)
Let's get closer to see how she is doing.
~currentState++
->END


== PlayerDialog
# speaker:player # portrait:player
Hey, I've been waiting for quite a while.
You seem finished preparing for tea. 
~currentState++
->END

== ServantDialog
# speaker:servant # portrait:npc
Yes. Sorry for waiting so long. 
I still have some unfinished business here. 
Wait me outside in the dinning room. I will be there in just a moment. 
~currentState++
->END

== PlayerDialog2
# speaker:player # portrait:player
I notice there is an odd piece of blanket on the ground.
(Should I lift up the blanket?)
->PlayerDialog2.Choices
->END
= Choices
* [Investigate.]
    You lifted up the blanket.
    ~ currentState = investigate
    ->END
* [Do not investigate.]
    You left the blanket untouched.
    ~ currentState = noInvestigate
    ->END


== Investigate
# speaker:player # portrait:player
Hmm. A corpse. 
Could it be the corpse that the master requested me to investigate?
I don't have time to think. The servant must be the murderer.
I need to run away from her right now.
->END

== NoInvestigate
# speaker:servant # portrait:npc
Even though you didn't see the corpse under the blanket, it's still too dangerous to leave you alive.
Now, sadly, I should bury you with the corpse together. 
->END



