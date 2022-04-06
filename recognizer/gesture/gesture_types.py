import pickle as pkl
from typing import List, Tuple

import numpy as np


class Gesture:
    def __init__(self, frames: List[Tuple[int, float, float, float]]):
        """
        A gesture.
        :param frames: list of (t, x, y, z) points
        """
        self._frames = frames

    @property
    def frames(self):
        return self._frames

    @property
    def num_frames(self):
        return len(self._frames)

    def to_numpy(self):
        return np.array(self._frames)


class GestureDatabase:
    def __init__(self, filepath: str = "gesture_data.pkl"):
        self._filepath = filepath
        self._data = {}
        # self.load_data()

    @property
    def num_labels(self):
        return len(self._data)

    @property
    def data(self):
        return self._data

    def add_template(self, label: str, template: Gesture):
        if label not in self._data:
            self._data[label] = []
        self._data[label].append(template)

    def save_data(self):
        with open(self._filepath, "wb") as f:
            pkl.dump(self._data, f)

    def load_data(self):
        with open(self._filepath, "rb") as f:
            self._data = pkl.load(f)

    def clear_data(self):
        self._data = {}
