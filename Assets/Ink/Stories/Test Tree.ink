VAR choices = 2
VAR choiceAvailable = ""
VAR isGood = true
VAR eventTag = "good"
VAR nextCharacter = "Person2"
VAR BonusQ1A = false
VAR BonusQ1B = false
->Dewuyi
=== Dewuyi ===
all who have played League of Legends may not enter the Kingdom of Heaven #name: Rules today
Test: good
Is a Gamer(derog.)

->Questioning
= Repeat
~choices = choices - 1
{choices <= 0:
    ~choiceAvailable = "Inactive"
}
{choices} choices left
->Questioning
=Questioning
Ask About: #ignore
    +  [Games played recently #oneTime #{choiceAvailable}] What games have you been playing recently, or rather, before you died? #name:You
        ~BonusQ1B = true
        I was playing the new season of Destiny 2, they've really fallen off with that in terms of quality.  #name:Dewuyi

    +[Games by Riot #oneTime #{choiceAvailable}] You played any games by Riot?#name:You
    ~BonusQ1A = true
        Yeah, a while ago#name:Dewuyi
    +[How his day is going #onetime #{choiceAvailable}] How's your day been#name:You
        Aside from the fact that i'm dead, it's been going pretty good#name:Dewuyi
    + {BonusQ1A}{BonusQ1B}[If he plays League of Legends #oneTime]OK, that's cool and all, but do you play League of Legends#name:You
        No #name:Dewuyi
        I see#name:You
        ->Questioning
    
    -I see #name:You
    ->Repeat
===Person2===
~choices = 1
~isGood = false
~eventTag = ""
~nextCharacter = "null"
~choiceAvailable = ""
Test: bad
Is a Gamer
->Questioning
=Repeat

~choices = choices - 1
{choices <= 0:
    ~choiceAvailable = "Inactive"
}
{choices} choices left
->Questioning
=Questioning
Ask About: #ignore
    + [If they play League #oneTime #{choiceAvailable}] do you play League of Legends? #name:You
        Whenever I get the chance #name:Person2
    + [If they're a Gamer #oneTime #{choiceAvailable}] are you a Gamer?#name:You
        Yep, play most every day#name:Person2
    -I see #name:You
    ->Repeat
   
   






