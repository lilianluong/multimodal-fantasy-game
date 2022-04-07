import time

from speech.speech_recorder import start_speech_process, get_speech_result


def test_loop(time_limit: float = 3):
    print("Starting recording...")
    process, queue = start_speech_process(time_limit)
    result = False
    while result is False:
        time.sleep(0.05)
        result = get_speech_result(process, queue)
    print(result)
    return result


if __name__ == "__main__":
    while True:
        test_loop()
