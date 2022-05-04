"""
Script that takes lists of gesture frame lists and outputs copy-and-pastable code for C#
To be used to export gesture templates to Unity for the spell tutor
"""
from typing import List, Tuple

from gesture.gesture_types import GestureDatabase
from trigger.trigger_database import TriggerDatabase


def export_gesture(name: str, gesture_frames: List[Tuple[int, float, float, float]]):
    xs = [x for t, x, y, z in gesture_frames]
    avg_x = sum(xs) / len(xs)
    ys = [y for t, x, y, z in gesture_frames]
    avg_y = sum(ys) / len(ys)
    print('\t\t{ "' + name + '", new List<GestureFrame>() {')
    for i, (t, x, y, z) in enumerate(gesture_frames):
        print(f"\t\t\tnew GestureFrame({t}, {x - avg_x}f, {y - avg_y}f, {z}f)" + ("," if i != len(gesture_frames) - 1 else ""))
    print("\t\t}},")


if __name__ == "__main__":
    db = GestureDatabase("../gesture_data.pkl")
    trigger_db = TriggerDatabase("../trigger_data.pkl")
    for label in trigger_db.labels:
        frames = db.data[trigger_db.get_gesture_name(label)][0].frames
        export_gesture(label, frames)
