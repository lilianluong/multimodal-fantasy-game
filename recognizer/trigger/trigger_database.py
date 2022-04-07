import pickle as pkl
from typing import List


class TriggerDatabase:
    def __init__(self, filepath: str = "trigger_data.pkl"):
        self._filepath = filepath
        self._data = {}
        try:
            self.load_data()
        except FileNotFoundError:
            self.save_data()

    @property
    def num_labels(self):
        return len(self._data)

    @property
    def data(self):
        return self._data

    @property
    def labels(self):
        return set(self._data.keys())

    def add_trigger(self, label: str, phrase: List[str], gesture_name: str):
        self._data[label] = {"phrase": phrase, "gesture": gesture_name}

    def get_trigger(self, label: str):
        return self._data[label]

    def get_phrase(self, label: str):
        return self._data[label]["phrase"]

    def get_gesture_name(self, label: str):
        return self._data[label]["gesture"]

    def save_data(self):
        with open(self._filepath, "wb") as f:
            pkl.dump(self._data, f)

    def load_data(self):
        with open(self._filepath, "rb") as f:
            self._data = pkl.load(f)

    def clear_data(self):
        self._data = {}
