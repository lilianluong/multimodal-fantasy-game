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