import speech_recognition as sr
import multiprocessing
import time
from multiprocessing import Process
from shell.shell import PrintColors


def async_record_process(seconds, queue, timeStartedAt, turnFinished):
    # PrintColors.print_paragraph_text("Your turn!")

    mic = sr.Microphone()
    recognizer = sr.Recognizer()
    print("hi")
    if timeStartedAt is not None:
        timeStartedAt.value = time.time()
        turnFinished.value = 1
    print("Recording now")
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


def async_record_process_loop(queue, timeStartedAt, turnLength, turnFinished):
    mic = sr.Microphone()
    recognizer = sr.Recognizer()
    print("Recorder ready")
    while True:
        if turnFinished.value == 0:
            timeStartedAt.value = time.time()
            turnFinished.value = 1
            seconds = turnLength.value
            try:
                with mic as source:
                    audio = recognizer.listen(source, timeout=seconds + 1, phrase_time_limit=seconds)
                try:
                    result = recognizer.recognize_google(audio)
                except sr.UnknownValueError:
                    print("Couldn't recognize audio")
                    result = None
                except Exception as e:
                    print(e)
                    result = None
            except sr.WaitTimeoutError:
                print("Recording timed out")
                result = None
            queue.put(result)
            turnFinished.value = 2


def start_speech_process(seconds: float, global_args):
    queue = multiprocessing.Queue()
    process = Process(target=async_record_process, args=(seconds, queue) + global_args)
    process.start()
    return process, queue


def start_speech_process_loop(speechProcessArgs):
    queue = multiprocessing.Queue()
    process = Process(target=async_record_process_loop, args=(queue,) + speechProcessArgs)
    process.start()
    return process, queue


def get_speech_result(process, queue):
    if process.is_alive():
        return False
    result = queue.get()
    queue.close()
    return result
