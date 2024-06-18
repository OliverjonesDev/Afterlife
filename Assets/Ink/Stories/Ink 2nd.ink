VAR choices = 3
VAR choiceAvailable = ""
VAR isGood = true
VAR eventTag = "good"
VAR nextCharacter = "Olly"
VAR BonusQ1A = false
VAR BonusQ1B = false
->Dewuyi
=== Dewuyi ===
This is text, this will be the dialogue for now#name:Dewuyi
have you heard of the hit game Monster Hunter World, truly one of the games of our time #name:Dewuyi

->Questioning
= Repeat
~choices = choices - 1
{choices <= 0:
    ~choiceAvailable = "Inactive"
}
{choices} choices left
->Questioning
=Questioning
    + Why do you have s many hours in Monster Hunter? #name:Me [#oneTime #{choiceAvailable}] 
        ~BonusQ1B = true
        The real question is why don't you, truly a skill issue #name:Dewuyi
        **Thinking of questions to ask is kinda hard #name:Me
        A1A
        **Q1B
        A1B
        **Q1C
        A1C

    + something else #name:Me[#oneTime #{choiceAvailable}]
    ~BonusQ1A = true
        A2
        **Q2A
        A2A
        **Q2B
        A2B
        **Q2C
        A2C
            ***Q2CA
            A2CA
            ***Q2CB
            A2CB
    + {BonusQ1A == true}{BonusQ1B == true}Q3[#oneTime]
        A2
        **Q2A
        A2A
        **Q2B
        A2B
        **Q2C
        A2C
            ***Q2CA
            A2CA
            ***Q2CB
            A2CB
    -I see
    ->Repeat
===Olly===
~choices = 3
~isGood = false
~eventTag = ""
~nextCharacter = "null"
~choiceAvailable = ""
First Dialogue of 2nd Character
Some info
->Repeat
=Repeat

~choices = choices - 1
{choices <= 0:
    ~choiceAvailable = "Inactive"
}
{choices} choices left
->Questioning
=Questioning
    + Q1[#oneTime #{choiceAvailable}]
        A1 #name:Dewuyi
        **Q1A
        A1A#name:Dewuyi
        **Q1B
        A1B
        **Q1C
        A1C
    + Q2[#oneTime #{choiceAvailable}]
        A2
        **Q2A
        A2A
        **Q2B
        A2B
        **Q2C
        A2C
    -I see 
    ->Repeat
   
   






