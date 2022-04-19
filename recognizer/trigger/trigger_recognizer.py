import time
from typing import Tuple, Union

import config
from gesture.gesture_recognizer import recognize_gesture
from gesture.hardcoded_gestures import hardcoded_gestures
from gesture.gesture_types import GestureDatabase
from speech.speech_recorder import start_speech_process, get_speech_result
from trigger.trigger_database import TriggerDatabase


class TriggerRecognizer:
    def __init__(self, use_gesture = True, use_speech = True):
        self._gesture_db = GestureDatabase()
        self._trigger_db = TriggerDatabase()
        if use_gesture:
            from recorders.gesture_recorder import GestureRecorder
            self._gesture_recorder = GestureRecorder()

    def take_turn(self, use_gesture = True, use_speech = True) -> Union[str, None]:
        # Check if using speech, record
        if use_speech:
            # Record speech and gesture
            speech_process = start_speech_process(config.TURN_SECONDS)
            # print("Recording...")
            speech_result = False
            while speech_result is False:
                for _ in range(config.LEAP_AUDIO_RECORD_RATIO):
                    if use_gesture: self._gesture_recorder.update()
                    else: continue
                speech_result = get_speech_result(*speech_process)
        else:
            # Hardcoded speech
            speech_result = "attack"
            
        print("Speech result:", speech_result)

        # Check if using gesture, record
        if use_gesture:
            from recorders.gesture_recorder import GestureRecorder
            gesture_frames = self._gesture_recorder.get_frames()
            self._gesture_recorder.reset()
        else:
            # Hardcoded gesture
            gesture_frames = hardcoded_gestures["horizontal_line"][0]
        
        # Check if data recorded
        if not len(gesture_frames):
            print("No gesture detected")
            return None, None
        if speech_result is None:
            print("No audio detected")
            return None, None

        # Process speech
        if self._use_speech:
            speech_result = str(speech_result).lower()
            valid_speech_labels = set()
            for label in self._trigger_db.labels:
                for incantation in self._trigger_db.get_phrase(label):
                    if incantation in speech_result:
                        valid_speech_labels.add(label)
        else:
            # replace with hardcoded speech labels
            valid_speech_labels = {"attack"}

        # Process gesture
        gesture_result = recognize_gesture(gesture_frames, self._gesture_db)
        print("Gesture result:", gesture_result)

        # Combine results
        best_label = None
        best_score = float('inf')
        for label in valid_speech_labels:
            gesture_label = self._trigger_db.get_gesture_name(label)
            if gesture_label in gesture_result and gesture_result[gesture_label] < best_score:
                best_score = gesture_result[gesture_label]
                best_label = label

        return best_label, best_score