# https://realpython.com/python-speech-recognition/
# pip3 install SpeechRecognition
# brew install portaudio
# pip3 install pyaudio
# python3 -m speech_recognition

import speech_recognition as sr
import multiprocessing
from multiprocessing import Process
import time

def speech_recog_test():
    r = sr.Recognizer()
    mic = sr.Microphone()

    print("Audio Files:")

    harvard = sr.AudioFile('speech/audio_files_harvard.wav')

    with harvard as source:
        audio0 = r.record(source)
        
    transcript_harvard = r.recognize_google(audio0)
    print(transcript_harvard)


    with harvard as source:
        audio1 = r.record(source, duration=4)
        audio2 = r.record(source, offset=4, duration=3)
        
    print(r.recognize_google(audio1))
    print(r.recognize_google(audio2))


    jackhammer = sr.AudioFile('speech/audio_files_jackhammer.wav')
    with jackhammer as source:
        r.adjust_for_ambient_noise(source)
        audio3 = r.record(source)

    transcript_jackhammer = r.recognize_google(audio3)
    print(transcript_jackhammer)
    print(r.recognize_google(audio3, show_all=True))


    print("Microphone:")

    with mic as source:
        audio = r.listen(source)

    transcript = r.recognize_google(audio)
    print(transcript)
    
 
def loop():
    counter = 0
    print("Start loop")
    while counter < 15:
        counter += 1
        print("Delay", counter)
        time.sleep(1)
    print("End loop")
    
if __name__=='__main__':
    p1 = Process(target = speech_recog_test)
    p1.start()
    p2 = Process(target = loop)
    p2.start()
