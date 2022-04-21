import speech_recognition as sr
import multiprocessing
from multiprocessing import Process


def async_record_process(seconds, queue):
    mic = sr.Microphone()
    recognizer = sr.Recognizer()
    try:
        with mic as source:
            audio = recognizer.listen(source, timeout=seconds + 2, phrase_time_limit=seconds)
        try:
            result = recognizer.recognize_google(audio)
        except sr.UnknownValueError:
            # print("Couldn't recognize audio")
            result = None
    except sr.WaitTimeoutError:
        # print("Recording timed out")
        result = None
    queue.put(result)


def start_speech_process(seconds: float):
    queue = multiprocessing.Queue()
    process = Process(target=async_record_process, args=(seconds, queue))
    process.start()
    return process, queue


def get_speech_result(process, queue):
    if process.is_alive():
        return False
    result = queue.get()
    queue.close()
    return result
