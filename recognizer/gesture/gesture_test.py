import time

from gesture.gesture_recognizer import recognize_gesture
from gesture.gesture_types import GestureDatabase
from recorders.gesture_recorder import GestureRecorder


def test_loop(recorder: GestureRecorder, db: GestureDatabase, time_limit: float = 3):
    print("Starting gesture tracking...")
    t0 = time.time()
    while time.time() - t0 < time_limit:
        recorder.update()
    frames = recorder.get_frames()
    if len(frames):
        result = recognize_gesture(recorder.get_frames(), db)
        print(result)
    else:
        print("No gesture detected")
        result = None
    recorder.reset()
    return result


if __name__ == "__main__":
    gesture_recorder = GestureRecorder()
    gesture_database = GestureDatabase()
    gesture_database.load_data()
    while True:
        test_loop(gesture_recorder, gesture_database)
