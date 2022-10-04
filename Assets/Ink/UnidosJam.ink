VAR stramerChoice = false
VAR baloonChoice = false
VAR signChoice = false

Gracias mamá for helping me with dinner. #speaker: Abuelita
First, we need to cook the rice. #speaker: Abuelita 
Can you grab the <color=red>olive oil</color> and pour some into the <color=blue>pot</color> for the frijoles? #speaker: Abuelita #WaitUntil #Recipe: 1
//– do the action –
Next, we need the <color=red>vegetable oil</color> for the rice in the <color=green>frying pan</color>. #speaker: Abuelita #WaitUntil #Recipe: 1

Oye! We need some help over here! #speaker: Cousin1
Go ahead. I’ll be here. #speaker: Abuelita #prompt: left

//– exclamation point prompt –

Where do you think we should put the streamers? #speaker: Cousin2
Aquí? Near the T.V.? #speaker: Cousin1
O aquí en la mesa? #speaker: Cousin2

* [near the T.V.]
    ~ stramerChoice = true
    HA! Knew it. #speaker: Cousin1
    Cállate. #speaker: Cousin2

* [on the table]
    ~ stramerChoice =  false
    I knew you had good taste, prima. #speaker: Cousin2

- ¡Mijita! #speaker: Abuelita #prompt: right

//– exclamation point prompt –

Let’s keep cooking so we finish on time. #speaker: Abuelita
Pues, now we add the <color=red>rice</color> to the <color=green>pan</color>. #speaker: Abuelita #WaitUntil #Recipe: 1

//– do the action –

¡Guau, que cocinera! #speaker: Abuelita
Ok, I’ll stir the rice. Can you chop the <color=red>garlic</color> and add it to the <color=blue>pot</color>? #speaker: Abuelita #WaitUntil #Recipe: 3
// -do the action -

Now, chop the <color=red>onion</color> to add to the <color=blue>pot</color>, por favor. #speaker: Abuelita #WaitUntil #Recipe: 3

//– do the action –

¡Necesito ayuda, por favor! #speaker: aunt

Go help them, mijita. #speaker: Abuelita #prompt: right

//– exclamation point prompt –

¡Gracias a dios! I can’t get this zipper up. #speaker: aunt
I need your tiny fingers. #speaker: aunt
Alberto, don’t move. #speaker: aunt #WaitUntil 

//During the minigame:
¡Ay, cuidado! You’re going to get me. #speaker: uncle
¡Shh! #speaker: aunt

//When you zip it up all the way:
Ah! Finally. Gracias, mija. #speaker: aunt
You can go back to helping grandma now. #speaker: aunt #prompt: left

//exclamation prompt –

You’re back, finally! Pues… #speaker: Abuelita
Add the <color=red>beans</color>, <color=red>cilantro</color>, <color=red>cumin</color> and <color=red>salt</color> to the <color=blue>pot</color>. #speaker: Abuelita #WaitUntil #Recipe: 4

//– do the action –

¡Agh! I need help again! #speaker: aunt #ButtonTime
Ay ay ay… #speaker: Abuelita
¡Go! ¡Rápidamente! #speaker: Abuelita #prompt: right

//– exclamation prompt –

Lo siento mija, I’m missing a button. I think I left it… somewhere. #speaker: aunt
Can you find it? #speaker: aunt #WaitUntil 

//– do the action – find the button –

You’re a lifesaver! #speaker: aunt

¡El arroz! #speaker: Abuelita #prompt: left 

//– exclamation prompt –

It almost burned, but good thing you have grandma to watch out. #speaker: Abuelita
Add <color=red>chicken seasoning</color> to <color=red>water</color> to make a broth and add it to the <color=green>rice</color>. #speaker: Abuelita #WaitUntil #Recipe: 2
 
//– do the action –

Great job! Now– #speaker: Abuelita

Priiimmmaaa! #speaker: Cousin2

Oy. #speaker: Abuelita #prompt: left 

//– exclamation prompt –

Ok, this time around… Los globos. #speaker: Cousin2
What do you think? Tied to the chairs? #speaker: Cousin2
Or taped to the wall? #speaker: Cousin1

* [tied to the chairs]
    ~ baloonChoice = false
    Finally!! Someone with taste. #speaker: Cousin2

* [taped to the wall]
   ~ baloonChoice = false
    Clásica! Perfecto. #speaker: Cousin1
    Thank you, cousin. #speaker: Cousin1

- Now, what about… #speaker: Cousin1

Mijita, ¿un momento? #speaker: Abuelita

Hurry back! #speaker: Cousin2 #prompt: right 

//– exclamation point –

Stay here with me! #speaker: Abuelita
Chop more <color=red>garlic</color> for me, please. #speaker: Abuelita #WaitUntil #Recipe: 2

//– do the action – 

Now,add <color=red>garlic</color>, <color=red>tomato sauce</color>, <color=red>water</color>, <color=red>cumin</color>, and <color=red>salt</color>. All in the <color=green>rice pan</color>. #speaker: Abuelita #WaitUntil #Recipe: 5

//– do the action – 

We can cook ground beef for the beans if you want? #speaker: Abuelita
Noooooo, abuelita! ¡No como carne! #speaker: Cousin1
Ay, sí sí lo siento. Ok, no meat. #speaker: Abuelita

Now, I think we’re done. All that’s left to do is wait. #speaker: Abuelita
Go ahead and check on your cousins. #speaker: Abuelita #prompt: left 

//– exclamation point –

Hey! We were just about to decide where to put the last decorations. #speaker: Cousin1
Where should we put the happy birthday sign? #speaker: Cousin2
Across the wall? #speaker: Cousin2
Or hanging in the entrance? #speaker: Cousin1

* [Across the wall]
    ~signChoice  = false
    Good choice. #speaker: Cousin2

* [In the entrance]
    ~signChoice =  true
    Told ya so! #speaker: Cousin1

- Food is ready! #speaker: Abuelita

Sweet! Let's Party! #speaker: Cousin1  #PictureTime

//— now is the part where you put everyone together for a party! —

    -> END