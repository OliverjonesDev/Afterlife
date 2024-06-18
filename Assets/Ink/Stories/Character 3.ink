VAR choices = 3
VAR choiceAvailable = ""
VAR isGood = false
VAR eventTag = "bad"
VAR nextCharacter = "null"
VAR BonusQ1A = false
VAR BonusQ1B = false
VAR endTextHeaven = "Well done, now he won't be able to turn earth into a farm and hopefully if he gets hungry in heaven he can sort out our overpopulation issue."
VAR endTextHell = "Good Job, you have doomed the entire human race, I hope you are ready for the constant stream of people coming in, all of which have you to blame."
This is odd, I have never seen someones record like this, it says that they are not from this planet.. #name:Receptionist
Have a talk with them and see what this is all about. #name:Receptionist
->Alien
===Alien===
+Are you an Alien?
    What would you consider an "Alien"?#name:Alien?
    **Someone who doesn't come from Earth.#name:
        Then yes, to you I am an alien.#name:Alien
    **The little green man from E.T.
        uh huh...#name:Alien?
    **[I don't really know.]#name:
        Now that I think of it, I don't really know.#name:
-Alien is fine. #name:Alien
So...#name:
->first
=first
*How did you end up here?#name:
    Well, first you will need to tell me where here is.#name:Alien
    **You are in our afterlife.
    **You are in heaven.
        What is heaven? Is this an estate agents or something?#name:Alien
        No, It's the place you go to when you die.#name:
        I died? I thought that when I died I would go to "forever-rest" and welcomed by my ancestors, why am I speaking to a human?#name:Alien
    **[You're in a Simulation]
        We have captured you and you are in a simulation.#name:
        You are lying to me Human, my body readings tell me that both my hearts had a critical failure which means I reached the end of my lifespan.#name:Alien
		I am supposed to be welcomed by my ancestors when I die, why am I talking to a Human?#name:Alien
-uh
*To be honest we are just as confused as you are...#name:
    I think I know why...#name:Alien
    I had a heart attack just as I was flying by your planet, since I was within your planets sphere of influence, I think I was sent here instead of seeing my ancestors back home.#name:Alien
*I think I know why... #name:
    you call us humans so you must know of us, atleast.#name:
    Yes, I was flying by earth when I had the heart attack.#name:Alien
    Then this must mean that because you were closer to my planet than yours, you must have been sent here by accident.#name:
-Is there any way for you to bring me back as this is obviously a mistake?#name:Alien
->second
=second
*[Don't think I can do anything]
    I don't think there is anything I can do for you.#name:
    So I'm just stuck here then? What am I supposed to do now?#name:Alien
	The next step is for me to decide if you are allowed into heaven or not.#name:
*[This is the first time this has happened]
    This is the first time this has happened to us, there is no protocol.#name:
    Well there aren't many life forms that visit this planet and when they do the stay is short as the food here is just terrible.#name:Alien
    Like the pizza?#name:
    Something more of the carnivorous variety.#name:Alien
    Ah, so you don't like the steak...#name:
	Well I am going to need to decide if you are allowed into heaven or not.#name:
	
-And if not?#name:Alien
Eternal Damnation.#name:
Well since this is obviously a mistake then you can just let me in right?#name:Alien
First off I'm going to need to understand you a bit better.#name:
->third
=third
Actually I'm sorry for interrupting, but in these circumstances I am able to give you permission to bring him back to life.#name:Receptionist
When were you going to tell me about this?#name:
It never came up... anyway I took the liberty of changing your red button so instead of sending him to hell it will bring him back to life. Do not take this power lightly!#name:Receptionist
    You don't even want to know what happened last time we brought someone back to life, the humanities department spent an entire month cleaning up that mess.#name:Receptionist
Okay thank you, I will be very carful about my decision.#name:
Please bring me back to life, this is a mistake and I need to get back home.#name:Alien
Just hold on a minute, as she said... this is a serious decision and I think I need to know a little more about you first.#name:
->fourth
=fourth
So why did you come to earth?#name:
    I own a fast food franchise, we serve fast food on planets all over the galaxy.#name:Alien
    Currently I am looking to branch out and open a new restaurant in this quadrant of the galaxy and your planet seems to be a great candidate. #name:Alien
    We will be able to serve you humans at such a low cost that we could turn a profit in under a year.#name:Alien
*[Do you even have permission?]
You can't just turn up to a planet and open a restaurant if you feel like it! You need to contact the correct authorities and request planning permission and all sorts.#name:
	Actually I have done, the Inter Galactic Federation of Fast Food Services have granted me full permission to use your planet.#name:Alien
	Oh well I guess I can't argue with that.#name:
*[I doubt you'd turn a profit]
Why open a restaurant here? If anything we have too many fast food companies I doubt you would turn a profit with the competition.#name:
	Actually I won't be competing with any of your companies as we won't be serving to humans.#name:Alien
	So you will be bringing more aliens with you? It's too crowded here already, we are running out of space.#name:
	With each alien that comes to earth there will be a human going with them, I think it's favourable for both of us.#name:Alien
*[What would you serve?]
    So what food do aliens eat? What would you be serving?#name:
	My restaurant serve economical proteins, at a cheap price to low income families across the galaxy.#name:Alien
	So like burgers?#name:
	No, Humans...#name:Alien
	Human meat tends to be an affordable meat that is high in unsaturated fat and a good source of vitamin B12.#name:Alien
	You are planning on serving humans? All this time I thought you were opening a restaurant to serve food to humans, not eat us!!#name:
	it's not like I'm trying to hide this from you.#name:Alien
-
->fifth 
=fifth
*[I'll bring you back if you leave Earth]
    If I bring you back to life you need to promise me that you will take this development elsewhere.#name:
	Why should I? I will be doing both of us a favour by opening this restaurant.#name:Alien
	How on earth would this be doing me a favour?#name:
	Because I will deal with your population problem.#name:Alien
	You can't eat innocent people, and if you did there would be even more people flooding into my office and we don't have the room.#name:
*[Can I just send you to hell?]
	Can I just send you to hell? It seems like the easier option.#name:
	Actually I already changed your button once and I'm not doing it again.#name:Receptionist
	Well thanks a lot! You are a load of help.#name:
	Please bring me back to life.#name:Alien
*[If I didn't bring you back]
    So if I didn't bring you back to life, is there any chance that someone else would come along and open another similar restaurant?#name:
	Most likely one of my competitors will come along, the human meat is so affordable and low quality it's hard to pass on the money making opportunity.#name:Alien
-
->last
=last
**Choose#Inactive
->DONE