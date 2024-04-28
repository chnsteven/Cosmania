INCLUDE Globals.ink
# speaker:player # portrait:player
* {currentState ? enterLivingRoom} ->EnterLivingRoom
* {currentState ? beforeInvestigation} ->BeforeInvestigation
* {currentState ? afterInvestigation} ->AfterInvestigation


== EnterLivingRoom
There's nothing else to do at the moment.
Let's sit on the couch and wait.
~currentState = beforeInvestigation
->END


== BeforeInvestigation
Let's look around some.
See if there's anything worth noting.
~currentState = afterInvestigation
->END

== AfterInvestigation
The servant is quite slow. I should go and check on her.
She should be in the kitchen.
~currentState = nextScene
->END

