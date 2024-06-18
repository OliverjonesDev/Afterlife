VAR choices = 3
VAR choiceAvailable = ""
VAR isGood = false
VAR eventTag = "bad"
VAR nextCharacter = "null"
VAR BonusQ1A = false
VAR BonusQ1B = false
VAR endTextHeaven = "Great idea, this man will be a great asset to have in heaven. Maybe things will finally get done around here with more people like him."
VAR endTextHell = "It's a shame you didn't let him in, I know he did a lot of bad things in life but he seemed genuine about it and we could use more guys like him in here."
->Moore
=== Moore ===
So the first person you are going to see is a man by the name of Mr Moore. He is... or shall I say was a criminal defense lawyer well known for getting the worst murderers off the hook. #name:Receptionist
His death was rather ironic, he shot himself in the court room showing how his clients murder victim shot himself. Shall I bring him in then? #name:Receptionist
And that you honor, is why the defendant is innocent.... Hang on a minute, where am I? #name:Moore
*[You've died]
    Unfortunately Mr Moore, you have died. #name:
    What do you mean? #name:Moore
    **You've kicked the bucket
        ...#name:Moore
        ***Bit the dust#name:
            ....#name:Moore
            You are dead.#name:
            ....#name:Moore
        ***You are dead.#name:
            ....#name:Moore
            Bit the dust#name:
            ....#name:Moore
    **You've Bit the dust#name:
        ...#name:Moore
        ***Kicked the bucket#name:
            ....#name:Moore
            You are dead.#name:
            ....#name:Moore
        ***You are dead.#name:
            ....#name:Moore
            Kicked the bucket#name:
            ....#name:Moore
    **I'm afraid you are dead#name:
        ...#name:Moore
        ***Kicked the bucket#name:
            ....#name:Moore
            Bit the dust.#name:
            ....#name:Moore
        ***Bit the dust.#name:
            ....#name:Moore
            Kicked the bucket#name:
            ....#name:Moore
*[You're in a dream #oneTime #{choiceAvailable}]
    This is all a dream, you are about to wake up in.  #name:
    3....    #name:
    2.....   #name:
    1....    #name:
    Pinches his arm*#name:Moore
	Well? I'm still here.#name:Moore

    **[Keep Pinching]
        Keep pinching, I'm sure you'll wake up soon.
        Pinches his arm again 
		Still nothing#name:Moore
		***Try slapping youself
		    This is getting stupid now! I demand to know where I am.#name:Moore
		    You've died.#name:
    **[You've died]
        You are dead Mr. Moore.  #name:
        What do you mean? #name:Moore
        ***You've kicked the bucket
            ...#name:Moore
             ****Bit the dust#name:
                ....#name:Moore
                You are dead.#name:
                ....#name:Moore
            ****You are dead.#name:
                ....#name:Moore
                Bit the dust#name:
                ....#name:Moore
        ***You've Bit the dust#name:
            ...#name:Moore
            ****Kicked the bucket#name:
                ....#name:Moore
                You are dead.#name:
                ....#name:Moore
            ****You are dead.#name:
                ....#name:Moore
                Kicked the bucket#name:
                ....#name:Moore
        ***I'm afraid you are dead#name:
            ...#name:Moore
        
            ****Kicked the bucket#name:
                ....#name:Moore
                Bit the dust.#name:
                ....#name:Moore
            ****Bit the dust.#name:
                ....#name:Moore
                Kicked the bucket#name:
                ....#name:Moore
-.......#name:Moore
...if I'm dead, then this must be heaven? It doesn't look much like heaven to me.#name:Moore 
*[We're underfunded.]
    Well we would have more decorations but currently we are heavily underfunded. #name:
*[What did you Expect?]
    So you expected more golden braziers, angels blowing trumpets? I'm sure you expected me to have long hair a beard and wearing a toga too. #name:
-I can't be dead, just a minute ago I was in the courtroom!#name:Moore
 I demand to know how I am dead!#name:Moore
->Questioning
= Repeat
~choices = choices - 1
{choices <= 0:
    ~choiceAvailable = "Inactive"
}
{choices} 
->Questioning
=Questioning
   +You finally realised how awful you are for defending murderers and decided to off yourself.#name
    Funny. I'm not weak willed enough to take my own life, how did I actually die?#name:Moore
     ** Seriously, it made the news.
        Have I done anything to receive this sort of reception?
        ***[You Shot Yourself]
            The truth is that you died in a stupid accident.#name:
            You were showing the jury that the person your client murdered actually died in a suicide. #name:
		    The gun you used to demonstrate was loaded.#name:
		    That would explain headache.#name:Moore
	    ***You know what you did.#name:
	        I don't know what you are talking about!#name:Moore
	**[You Shot Yourself]
	        The truth is that you died in a stupid accident.#name:
            You were showing the jury that the person your client murdered actually died in a suicide. #name:
		    The gun you used to demonstrate was loaded.#name:
		    That would explain headache.#name:Moore
	+[You died in an accident]
	    I'm sorry to tell you this, but you died in an accident in court.#name:
	    Was I poisoned?#name:Moore
	    No.
	    My client went on a rampage and took my life?#name:Moore
	    Is that the sort of thing you expect from your clients?
	    Try to remember what happened before you got here.
	    Well, I was showing the jury the "murder weapon". I was demonstrating how the gun was used by the victim. #name:Moore
	    Go on.
	    I put the gun to my head and then.... oh.#name:Moore
	    Probably should've checked if it was loaded
	    I can't believe I would do such a thing! Someone must have set me up and loaded that gun!#name:Moore
	    
	    
-Anyway, Who even are you and why am I here? #name:Moore
+[Here to decide if you're worthy]
    I am here to decide if you are worthy of going to heaven.
    I am, I dedicated my entire life to the persuit of justice. All of my clients deserved to have their case heard.#name:Moore
+[I am your judge]
    If this is a court room then I am your judge, its time to plead your case.
    I haven't done anything wrong. It's up to the court to decide if my clients guilty, It's just part of the job.#name:Moore
+[Deeds Done]
    I have here a list of deeds from your life on earth. Some of them are quite impressive but there are also a lot which are quite concerning.
    Please hear me out, as a defense lawyer I was just doing my job, everyone deserves a fair trial and I was just ensuring justice was served.#name:Moore
-That is an admirable attitude, but some of the cases you defended were clearly guilty and you knew it. You got them off on technicalities and justice was not served.#name:
+And doing so you let guilty people escape punishment.
    And plenty of innocent people too!#name:Moore
+Thus you are guilty of all their future crimes.
    If I died the way you say I did, I would say that I even sacrificed my life for my client. A very honourable death.#name:Moore
-o
**Choose#Inactive
->DONE




