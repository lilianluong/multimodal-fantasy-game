import speech_recognition as sr
import multiprocessing
import time
from multiprocessing import Process
from shell.shell import PrintColors


def async_record_process(seconds, queue, timeStartedAt, turnFinished):
    mic = sr.Microphone()
    recognizer = sr.Recognizer()
    # PrintColors.print_paragraph_text("Your turn!")
    print("Recording now")
    timeStartedAt.value = time.time()
    turnFinished.value = 1
    try:
        with mic as source:
            audio = recognizer.listen(source, timeout=seconds + 1, phrase_time_limit=seconds)
        try:
            result = recognizer.recognize_google(audio)
        except sr.UnknownValueError:
            # print("Couldn't recognize audio")
            result = None
        except Exception as e:
            print(e)
            result = None
    except sr.WaitTimeoutError:
        # print("Recording timed out")
        result = None
    queue.put(result)


def start_speech_process(seconds: float, global_args):
    queue = multiprocessing.Queue()
    process = Process(target=async_record_process, args=(seconds, queue) + global_args)
    process.start()
    return process, queue


def get_speech_result(process, queue):
    if process.is_alive():
        return False
    result = queue.get()
    queue.close()
    return result
