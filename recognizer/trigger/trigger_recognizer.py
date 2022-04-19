import time
from typing import Tuple, Union

import config
from gesture.gesture_recognizer import recognize_gesture
from gesture.gesture_types import GestureDatabase
from gesture.hardcoded_gestures import hardcoded_gestures
from recorders.gesture_recorder import GestureRecorder
from speech.speech_recorder import start_speech_process, get_speech_result
from trigger.trigger_database import TriggerDatabase


class TriggerRecognizer:
    def __init__(self, use_gesture: bool = True, use_speech: bool = True):
        self._use_gesture = use_gesture
        self._use_speech = use_speech

        self._gesture_db = GestureDatabase()
        self._trigger_db = TriggerDatabase()
        self._gesture_recorder = GestureRecorder() if use_gesture else None

    def take_turn(self) -> Tuple[Union[str, None], Union[float, None]]:
        # Record speech and gesture
        speech_process = start_speech_process(config.TURN_SECONDS) if self._use_speech else time.time()
        # print("Recording...")
        speech_result = False
        while speech_result is False:
            if self._use_gesture:
                for _ in range(config.LEAP_AUDIO_RECORD_RATIO):
                    self._gesture_recorder.update()
            if self._use_speech:
                speech_result = get_speech_result(*speech_process)
            else:
                speech_result = time.time() - speech_process >= config.TURN_SECONDS

        print("Speech result:", speech_result if self._use_speech else "<skipped>")

        if self._use_gesture:
            gesture_frames = self._gesture_recorder.get_frames()
        else:
            # replace with hardcoded gesture labels
            gesture_frames = hardcoded_gestures["horizontal_line"][0]

        if self._use_gesture:
            self._gesture_recorder.reset()

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
