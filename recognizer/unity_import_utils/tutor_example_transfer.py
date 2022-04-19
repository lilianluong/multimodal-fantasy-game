"""
Script that takes lists of gesture frame lists and outputs copy-and-pastable code for C#
To be used to export gesture templates to Unity for the spell tutor
"""
from typing import List, Tuple

from gesture.gesture_types import GestureDatabase


def export_gesture(name: str, gesture_frames: List[Tuple[int, float, float, float]]):
    print('{ "' + name + '", new List<GestureFrame>() {')
    for i, (t, x, y, z) in enumerate(gesture_frames):
        print(f"new GestureFrame({t}, {x}f, {y}f, {z}f)" + ("," if i != len(gesture_frames) - 1 else ""))
    print("}},")


if __name__ == "__main__":
    db = GestureDatabase("../gesture_data.pkl")
    for label in db.labels:
        frames = db.data[label][0].frames
        export_gesture(label, frames)
