from collections import deque
from typing import Union

from recorders.leap import Leap


class GestureRecorder:
    def __init__(self, buffer_length: Union[int, None] = None):
        self._last_id = -1

        self._controller = Leap.Controller()
        while not self._controller.is_connected:
            pass
        self._controller.set_policy(Leap.Controller.POLICY_BACKGROUND_FRAMES)
        print("Gesture recorder started!")

        self._buffer_length = buffer_length
        self._frames = deque()

    @property
    def is_connected(self):
        return self._controller.is_connected

    def update(self):
        frame = self._controller.frame()
        if frame.id != self._last_id:
            if not frame.hands.is_empty:
                hand_position = frame.hands.rightmost.palm_position
                self._frames.append((frame.timestamp,) + hand_position.to_tuple())
                if self._buffer_length is not None and len(self._frames) > self._buffer_length:
                    self._frames.popleft()
            self._last_id = frame.id

    def reset(self):
        self._frames = deque()

    def get_frames(self, num_frames: Union[int, None] = None):
        if num_frames is None or num_frames > self._buffer_length:
            return list(self._frames)
        return list(self._frames)[-num_frames:]
