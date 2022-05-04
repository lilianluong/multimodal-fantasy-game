import asyncio


async def start_turn():
    print("do start turn thigns")
    await asyncio.sleep(1)
    print("do start turn thigns done")


async def post_post_start_turn():
    asyncio.create_task(start_turn())

    await asyncio.sleep(1)
    print('Do some actions 1')
    await asyncio.sleep(1)
    print('Do some actions 2')
    await asyncio.sleep(1)
    print('Do some actions 3')


def post_start_turn():
    
    loop = asyncio.get_event_loop()
    loop.run_until_complete(post_post_start_turn())
    
    
post_start_turn()



# In this game, you will battle enemies using your arsenal of spells.
# To use a spell, you must speak the spell incantation and perform the associated gesture with
# an open palm facing downwards over the spell sensor.

# Spells may either heal and shield you, damage your enemy, or enact some combination of these effects.
# Your goal is to bring your enemy to 0HP - choose your spells wisely!
# The quality of your spellcasting will impact the effectiveness of your spell.


# Your player details are below, followed by your attributes. These attributes have
# an impact on the effectiveness of your spells as well as your enemy's spells on you.
# Begin by practicing some of your available spells.

# At any point, say "game tutorial" to view these instructions, "begin game" to fight an enemy,
# "move tutor" to practice your moves, or "end game" to finish playing."