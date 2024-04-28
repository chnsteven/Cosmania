INCLUDE Globals.ink
* {currentState ? beforeGuiding} ->beforeGuidingFn
* {currentState ? guiding} ->duringGuidingFn
* {currentState ? afterGuiding} ->afterGuidingFn
* {currentState ? nextScene} ->repeatDialog

== beforeGuidingFn
Welcome to our mansion. # speaker:servant # portrait:npc
I am the servant here.
I will guide you to the entrance.
Let me know when you are ready.
-> beforeGuidingFn.Choices
= Choices
+ [I am ready.]
    Understood. Follow me please. # speaker:servant # portrait:npc
    ~ currentState++
    ->DONE
+ [Not Yet.]
    Alright. Talk to me when you are ready. # speaker:servant # portrait:npc
    ->DONE
->END


== duringGuidingFn
~ currentState++
->afterGuidingFn
->END

== afterGuidingFn
{repeat == true: ->repeatDialog}
We have arrived our destination. # speaker:servant # portrait:npc
My master is inside waiting for you.
~ currentState = nextScene
-> afterGuidingFn.Choices
= Choices
VAR dots = "..."
+ [Bye.]
    ~ repeat = true
    ->repeatDialog
+ [{dots}]
    {dots}
    ~dots += "."
    {dots == "......": Please get in.|->afterGuidingFn.Choices} # speaker:servant # portrait:npc
    
    ->DONE
-> END

== repeatDialog
Take care. # speaker:servant # portrait:npc
->END

