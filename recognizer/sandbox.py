import asyncio


async def start_turn():
    print("do start turn thigns")
    await asyncio.sleep(1)
    print("do start turn thigns done")


def post_post_start_turn():

    print('Do some actions 1')
    print('Do some actions 2')
    print('Do some actions 3')


async def post_start_turn():
    asyncio.create_task(start_turn())  # fire and forget async_foo()
    
    loop = asyncio.get_event_loop()
    loop.run_until_complete(post_post_start_turn())
    
def main():
    loop = asyncio.get_event_loop()
    loop.run_until_complete(post_start_turn())
    
    
main()