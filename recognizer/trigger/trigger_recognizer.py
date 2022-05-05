import time
from typing import Tuple, Union

import config
from gesture.gesture_recognizer import recognize_gesture
from gesture.hardcoded_gestures import hardcoded_gestures
from gesture.gesture_types import GestureDatabase
from speech.speech_recorder import start_speech_process, start_speech_process_loop, get_speech_result
from trigger.trigger_database import TriggerDatabase
from multiprocessing import Value


class TriggerRecognizer:
    def __init__(self, use_gesture = True, use_speech = True):
        self._gesture_db = GestureDatabase()
        self._trigger_db = TriggerDatabase()
        if use_gesture:
            from recorders.gesture_recorder import GestureRecorder
            self._gesture_recorder = GestureRecorder()
        if use_speech:
            self._speech_process_args = (
                Value('d', time.time()),  # timeStartedAt
                Value('d', 2.5),  # turnLength
                Value('i', -1),  # turnFinished
            )
            self._speech_process, self._speech_queue = start_speech_process_loop(self._speech_process_args)

    def take_turn(self, num_seconds=config.TURN_SECONDS, use_gesture = True, use_speech = True,
                  timeStartedAt = None, turnFinished = None,
                  verbose = 1) -> Tuple[Union[str, float, None], ...]:
        # Check if using speech, record
        if use_speech:
            # Record speech and gesture
            # speech_process = start_speech_process(num_seconds,
            #                                       global_args=global_args if global_args is not None else (None, None))
            # speech_result = False
            # while speech_result is False:
            #     for _ in range(config.LEAP_AUDIO_RECORD_RATIO):
            #         if use_gesture: self._gesture_recorder.update()
            #         else: continue
            #     speech_result = get_speech_result(*speech_process)
            speechTimeStartedAt, turnLength, speechTurnFinished = self._speech_process_args
            turnLength.value = num_seconds
            speechTurnFinished.value = 0
            while speechTurnFinished.value < 1:
                pass
            print("Recording started for", num_seconds, "seconds")
            timeStartedAt.value = speechTimeStartedAt.value
            turnFinished.value = 1
            while speechTurnFinished.value < 2:
                if use_gesture:
                    self._gesture_recorder.update()
            speech_result = self._speech_queue.get()
            turnFinished.value = 2
        else:
            # Hardcoded speech
            speech_result = "flame"
            t0 = time.time()
            # if global_args is not None:
            #     global_args[0].value = t0
            #     global_args[1].value = 1
            while time.time() - t0 < num_seconds:
                self._gesture_recorder.update()

        if verbose > 0: print("Speech result:", None if speech_result is None else str(speech_result).lower())

        # Check if using gesture, record
        if use_gesture:
            gesture_frames = self._gesture_recorder.get_frames()
            self._gesture_recorder.reset()
        else:
            # Replace with hardcoded gesture frames
            gesture_frames = hardcoded_gestures["horizontal_line"][0]
        
        # Check if data recorded
        if not len(gesture_frames):
            if verbose > 0: print("No gesture detected")
            return None, None, str(speech_result).lower()
        if speech_result is None:
            if verbose > 0: print("No audio detected")
            return None, None, ""

        # Process speech
        if use_speech:
            speech_result = str(speech_result).lower()
            valid_speech_labels = set()
            for label in self._trigger_db.labels:
                for incantation in self._trigger_db.get_phrase(label):
                    if incantation in speech_result:
                        valid_speech_labels.add(label)
        else:
            # Replace with hardcoded speech labels
            valid_speech_labels = {"flame"}

        # Process gesture
        gesture_result = recognize_gesture(gesture_frames, self._gesture_db)
        if verbose > 0: print("Gesture result:", gesture_result)

        # Combine results
        best_label = None
        best_score = float('inf')
        for label in valid_speech_labels:
            gesture_label = self._trigger_db.get_gesture_name(label)
            if gesture_label in gesture_result and gesture_result[gesture_label] < best_score:
                best_score = gesture_result[gesture_label]
                best_label = label

        return best_label, best_score, speech_result
