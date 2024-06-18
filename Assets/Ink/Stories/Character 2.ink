VAR choices = 8
VAR choiceAvailable = ""
VAR isGood = true
VAR eventTag = "good"
VAR nextCharacter = "null"
VAR Mats = false
VAR Ceramics = false
VAR Containers = false
VAR Initial = true
VAR Tableware = false
VAR endTextHeaven = "Fair Choice."
VAR endTextHell = "Not what I would have Chosen, but you're the one in charge."
->Olivia
=== Olivia ===
The next client is a woman named Olivia, from what I can gather she's lived a pretty uneventful life so usually we'd be able to send them up without bothering you #name:Receptionist
only thing is a few years before her death she stole something, and doesn't seem willing to confess to what it was, which is where you come in.#name:Receptionist
While this client speaks, you'll have some insight into her thoughts, but only enough to glean useful information from yes or no questions.#name:Receptionist
You're also limited to the amount of times you can use this, choose your questions wisely#name:Receptionist
I'll send her in now. #name:Receptionist
What... where am I now?#name:Olivia
You're here to be questioned by me instead, seeing how cooperative you've been when questioned by others#name:
Let's start.#name:
->Questioning
= Repeat
~choices = choices - 1
{choices <= 0:
    ~choiceAvailable = "Inactive"
}
->Questioning
=Questioning
*[Is it valueable #oneTime]
    The item you stole, is it valuable?#name:
    I'm not willing to answer that. #name:Olivia
    (Yes) #name:Olivia(Thoughts)
    (Expensive. what materials could it be made from?)
    ~Mats = true
*What did you steal#name:
    I'm not answering that.#name:Olivia
    (idde I tfsaekrab eltso edmitil gum) #name:Olivia(Thoughts)
    (Got to avoid questions with complicated answers)#name:
*{Mats}Is it Ceramic?#name:
    Yes#name:Olivia
    (Ceramic and valuable, maybe a Vase? tableware?)#name:
    ~Mats = false
    ~Ceramics = true
*{Mats}[Is it Made from metals?]
    Is it made from metals or precious gems?#name:
    Uh, no#name:Olivia
    (No)#name:Olivia(thoughts)
*{Ceramics} Is it a container for liquids?#name:
    ...#name:Olivia
    (Yes)#name:Olivia(thoughts)
    (must be getting close)#name:
    ~Containers = true
    ~Ceramics = false
*{Ceramics} Is it some form of tableware?#name:
    ...#name:Olivia
    (Yes)#name:Olivia(thoughts)
    (must be getting close)#name:
    ~Tableware = true
    ~Ceramics = false
*{Containers} Is it... a mug?
    ...No#name:Olivia
    (Yes)#name:Olivia(thoughts)
    ->mug
*{Containers} Is it a Vase?
    (No)#name:Olivia(thoughts)
*{Containers} Is it a Bowl?
    (No)#name:Olivia(thoughts)
*{Tableware} Is it a Cup?
    Kinda?#name:Olivia
*{Tableware} Is it... a mug?
    ...No#name:Olivia
    (Yes)#name:Olivia(thoughts)
    ->mug
-
{choices > 1:
    ({choices-1} uses left)#name:
}
{choices <= 1:
    ({choices-1} use left)#name:
}
->Repeat
=mug
*Wait, it's just a mug?#name:
    "just" a mug? I'll have you know that it was the Limited Edition Minecraft Steve mug#name:Olivia
    Wait.. didn't you say that it was valuable? you can get one of those off the internet for like £9#name:
    (oh, valuable wasn't in reference to monetary worth...)#name:
    oh, well. the time's come for me to decide
    **Choose#Inactive
->DONE