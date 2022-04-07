import speech_recognition as sr
import multiprocessing
from multiprocessing import Process


def async_record_process(seconds, queue):
    mic = sr.Microphone()
    recognizer = sr.Recognizer()
    with mic as source:
        audio = recognizer.listen(source, phrase_time_limit=seconds)
    result = recognizer.recognize_google(audio)
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
